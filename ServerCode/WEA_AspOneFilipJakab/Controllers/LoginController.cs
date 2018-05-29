using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using DataProviders;
using DataProviders.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WEA_AspOneFilipJakab.Models;
using WEA_AspOneFilipJakab.Handlers;
using WEA_AspOneFilipJakab.Helpers;
using WEA_AspOneFilipJakab.Providers;

namespace WEA_AspOneFilipJakab.Controllers
{
	[Route("auth")]
	[EnableCors("AllowAnyImigrant")]
	[AllowAnonymous]
	public class LoginController : Controller
	{
		private readonly IConfiguration config;
		private readonly Filip_WEAAspOneFilipJakabContext ctx;

		public LoginController(IConfiguration config, Filip_WEAAspOneFilipJakabContext ctx)
		{
			this.config = config;
			this.ctx = ctx;
		}

		[HttpPost("register")]
		public JsonResult Register([FromBody] UserRegistrationModel model)
		{
			return RequestHandlers.Handle(() =>
			{
				Mapper mapper = new Mapper();

				User mappedUser = mapper.ParseUser(model);

				DbProvider provider = new DbProvider(ctx);

				if (provider.GetUserByEmail(mappedUser.Email) != null)
					throw new ArgumentOutOfRangeException(nameof(model.Email), "Email already exists");

				provider.AddUser(mappedUser);
			});
		}

		[HttpPost("login")]
		public JsonResult Login([FromBody] UserLoginModel model)
		{
			return RequestHandlers.Handle(() =>
			{
				IConfigurationSection options = config.GetSection("AuthOptions");

				int.TryParse(options["TokenLifeTimeInHours"], out int tokenLifeTime);

				AuthenticationProvider authProvider = new AuthenticationProvider(
					ctx,
					Encoding.UTF8.GetBytes(config.GetSection("SecretKeys")["IssuerSignInKey"]),
					options["Issuer"],
					options["audience"],
					new TimeSpan(tokenLifeTime, 0, 0));

				Tuple<User, string> userAndToken = authProvider.Authenticate(model.Email, model.Password);

				Mapper mapper = new Mapper();

				return new AuthenticatedUserModel
				{
					Token = userAndToken.Item2,
					User = mapper.MapUser(userAndToken.Item1)
				};
			});
		}

		//[HttpPost("validatetoken")]
		//public JsonResult ValidateToken([FromBody] string token)
		//{

		//}
	}
}