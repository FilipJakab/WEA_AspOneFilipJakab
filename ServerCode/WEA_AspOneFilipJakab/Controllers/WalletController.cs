using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using DataProviders;
using DataProviders.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WEA_AspOneFilipJakab.Handlers;
using WEA_AspOneFilipJakab.Helpers;
using WEA_AspOneFilipJakab.Models;
using WEA_AspOneFilipJakab.Providers;

namespace WEA_AspOneFilipJakab.Controllers
{
	[Authorize]
	[Route("wallet")]
	[EnableCors("AllowAnyImigrant")]
	public class WalletController : Controller
	{
		//private readonly Filip_WEAAspOneFilipJakabContext ctx;
		private readonly DbProvider dbProvider;

		public WalletController(Filip_WEAAspOneFilipJakabContext ctx)
		{
			//this.ctx = ctx;
			dbProvider = new DbProvider(ctx);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public JsonResult Get()
		{
			return RequestHandlers.Handle(() =>
			{
				int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");

				if (userId == 0)
					throw new ArgumentOutOfRangeException(nameof(userId), "Could not retrieve UserId from Claims");

				Mapper mapper = new Mapper();

				UserModel mappedUser = mapper.MapUser(dbProvider.GetUser(userId));

				List<TagModel> tags = dbProvider
					.GetTags(userId)
					.Select(x => mapper.MapTag(x))
					.ToList();

				List<TransactionModel> transactions = dbProvider
					.GetTransactions(userId)
					.Select(x => mapper.MapTransaction(x))
					.ToList();

				List<TransactionCategoryModel> categories = dbProvider
					.GetCategories()
					.Select(mapper.MapTransactionCategory)
					.ToList();

				return new UserInfoPayload
				{
					Tags = tags,
					Transactions = transactions,
					User = mappedUser,
					Categories = categories 
				};
			});
		}

		[HttpPost]
		public JsonResult Post([FromBody] TransactionModel model)
		{
			return RequestHandlers.Handle(() => processOneTransaction(model));
		}

		[HttpPost("transactions")]
		public JsonResult Transactions([FromBody] List<TransactionModel> transactions)
		{
			return RequestHandlers.Handle(() => transactions.ForEach(processOneTransaction));
		}

		// Update
		[HttpPut]
		public JsonResult Put([FromBody] TransactionModel model)
		{
			throw new NotImplementedException();
		}

		[HttpDelete]
		public JsonResult Delete(int transactionId)
		{
			throw new NotImplementedException();
		}

		private void processOneTransaction(TransactionModel transaction)
		{
			Mapper mapper = new Mapper();

			Transaction parsedTransaction = mapper.ParseTransaction(transaction);

			parsedTransaction.TransactionTags = transaction.TagIds.Select(tagId => new TransactionTags
			{
				TagId = tagId,
				TransactionCode = parsedTransaction.TransactionCode
			}).ToList();

			dbProvider.AddTransaction(parsedTransaction);
		}
	}
}