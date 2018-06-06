using System;
using System.Linq;
using System.Security.Claims;
using DataProviders;
using DataProviders.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WEA_AspOneFilipJakab.Helpers;
using WEA_AspOneFilipJakab.Models;
using WEA_AspOneFilipJakab.Providers;
using static WEA_AspOneFilipJakab.Handlers.RequestHandlers;

namespace WEA_AspOneFilipJakab.Controllers
{
	[Authorize]
	[EnableCors("AllowAnyImigrant")]
	[Route("user")]
	public class UserController : Controller
	{
		private readonly Mapper mapper;
		private readonly DbProvider provider;

		public UserController(Filip_WEAAspOneFilipJakabContext ctx)
		{
			provider = new DbProvider(ctx);
			mapper = new Mapper();
		}

		[HttpGet]
		public JsonResult Get()
		{
			return Handle(() =>
			{
				if (!int.TryParse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value, out int userId))
					throw new ArgumentException("Error when parsing user Id");

				return mapper.MapUser(provider.GetUser(userId));
			});
		}

		[HttpPut]
		public JsonResult Put([FromBody] UserModel model) =>
			Handle(() => mapper.MapUser(provider.UpdateUser(mapper.ParseUser(model))));
	}
}