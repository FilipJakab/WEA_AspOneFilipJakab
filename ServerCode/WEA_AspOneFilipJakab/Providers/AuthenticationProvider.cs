using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using DataProviders;
using DataProviders.Models;
using Microsoft.IdentityModel.Tokens;
using WEA_AspOneFilipJakab.Helpers;
using WEA_AspOneFilipJakab.Models;

namespace WEA_AspOneFilipJakab.Providers
{
	public class AuthenticationProvider
	{
		private readonly Filip_WEAAspOneFilipJakabContext ctx;
		private readonly byte[] issuerSignInKey;
		private readonly string issuer;
		private readonly string audience;
		private readonly TimeSpan tokenLifeTime;

		public AuthenticationProvider(Filip_WEAAspOneFilipJakabContext ctx,
			byte[] issuerSignInKey, string issuer, string audience, TimeSpan tokenLifeTime)
		{
			this.ctx = ctx;
			this.issuerSignInKey = issuerSignInKey;
			this.issuer = issuer;
			this.audience = audience;
			this.tokenLifeTime = tokenLifeTime;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="email"></param>
		/// <param name="password"></param>
		/// <param name="user"></param>
		/// <returns>JWT token</returns>
		public string Authenticate(string email, string password, out UserModel user)
		{
			user = (new Mapper()).MapUser(ctx
				.User
				.FirstOrDefault(x => string.Equals(x.Email, email, StringComparison.InvariantCultureIgnoreCase)
				                     && x.Password == password));

			if (user == null)
				throw new UnauthorizedAccessException("Email or Password doesnt match");

			SymmetricSecurityKey issuerKey = new SymmetricSecurityKey(issuerSignInKey);

			List<Claim> claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.DateOfBirth, DateTime.ParseExact(user.Birthdate, "yyyy MMMM dd", CultureInfo.InvariantCulture).ToString(CultureInfo.CurrentCulture)),
				new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
			};

			JwtSecurityToken token = new JwtSecurityToken(
				claims: claims,
				notBefore: DateTime.Now,
				expires: DateTime.Now.Add(tokenLifeTime),
				issuer: issuer,
				audience: audience,
				signingCredentials: new SigningCredentials(issuerKey, SecurityAlgorithms.HmacSha256));

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}