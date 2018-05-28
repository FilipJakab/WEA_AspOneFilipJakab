using System;
using System.Text;
using DataProviders;
using DataProviders.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WEA_AspOneFilipJakab.Models;
using WEA_AspOneFilipJakab.Handlers;
using WEA_AspOneFilipJakab.Helpers;
using WEA_AspOneFilipJakab.Providers;

namespace WEA_AspOneFilipJakab.Controllers
{
	[Route("auth")]
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

				return authProvider.Authenticate(model.Email, model.Password);
			});
		}

		//[HttpPost("validatetoken")]
		//public JsonResult ValidateToken([FromBody] string token)
		//{

		//}
	}
}