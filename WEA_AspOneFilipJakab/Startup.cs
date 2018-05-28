using System;
using System.Text;
using DataProviders;
using DataProviders.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace WEA_AspOneFilipJakab
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<Filip_WEAAspOneFilipJakabContext>(options =>
			{
				options.UseSqlServer(Configuration.GetConnectionString("MainDB"));
			});

			services.AddCors(options =>
			{
				options.AddPolicy("AllowAnyImigrant",
					builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
			});

			// Add Jwt Token Authentication
			IConfigurationSection authConfig = Configuration.GetSection("AuthOptions");

			if (!int.TryParse(authConfig["TokenLifeTimeInHours"], out int tokenLifeTimeInHours))
				throw new ArgumentOutOfRangeException(nameof(tokenLifeTimeInHours), "Could not parse value from appsettings");

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = options.DefaultChallengeScheme = "JwtBearer";
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(
						Encoding.UTF8.GetBytes(Configuration.GetSection("SecretKeys")["IssuerSignInKey"])),

					ValidateAudience = true,
					ValidAudience = authConfig["Audience"] /*"Prestizni Studenti"*/,

					ValidateIssuer = true,
					ValidIssuer = authConfig["Issuer"]/*"Programator"*/,

					ValidateLifetime = true,
					ClockSkew = new TimeSpan(0, tokenLifeTimeInHours, 0, 0)
				};
			});

			services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseMvc();
		}
	}
}
