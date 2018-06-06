using System;
using System.Collections;
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
using static WEA_AspOneFilipJakab.Handlers.RequestHandlers;

namespace WEA_AspOneFilipJakab.Controllers
{
	[Authorize]
	[Route("wallet")]
	[EnableCors("AllowAnyImigrant")]
	public class WalletController : Controller
	{
		private readonly DbProvider dbProvider;
		private readonly Mapper mapper;

		private int UserId => int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);

		public WalletController(Filip_WEAAspOneFilipJakabContext ctx)
		{
			mapper = new Mapper();
			dbProvider = new DbProvider(ctx);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public JsonResult Get()
		{
			return Handle(() =>
			{
				UserModel user = mapper.MapUser(dbProvider.GetUser(UserId));

				List<TagModel> tags = dbProvider
					.GetTags(UserId)
					.Select(mapper.MapTag)
					.ToList();

				List<TransactionModel> transactions = dbProvider
					.GetTransactions(UserId)
					.Select(x => mapper.MapTransaction(x, dbProvider.GetTags(UserId)))
					.ToList();

				List<TransactionCategoryModel> categories = dbProvider
					.GetCategories()
					.Select(mapper.MapTransactionCategory)
					.ToList();

				return new UserInfoPayload
				{
					Tags = tags,
					Transactions = transactions,
					User = user,
					Categories = categories
				};
			});
		}

		[HttpGet("transactions")]
		public JsonResult Transactions() =>
			Handle(() => dbProvider.GetTransactions(UserId).Select(x => mapper.MapTransaction(x, dbProvider.GetTags(UserId))));

		[HttpGet("groupedtransactions")]
		public JsonResult GroupedTransactions()
		{
			return Handle(() => dbProvider
				.GetTransactions(UserId)
				.GroupBy(x => x.Date.Month)
				.Select(x => new
				{
					month = x.Key,
					Total = x.ToList().Sum(y => y.Amount)
				}));
		}

		[HttpGet("groupedcategories")]
		public JsonResult GroupedCategories()
		{
			return Handle(() =>
			{
				List<Transaction> transactions = dbProvider.GetTransactions(UserId);

				List<TransactionCategory> categories = dbProvider.GetCategories();

				return transactions
					.Where(x => x.CategoryId != null)
					.GroupBy(x => categories.First(y => y.CategoryId == (x.CategoryId ?? 0)).Name)
					.Select(x => new
					{
						category = x.Key,
						total = x.ToList().Sum(y => y.Amount)
					}).ToList();
			});
		}

		[HttpGet("categories")]
		public JsonResult Categories() => Handle(() => dbProvider.GetCategories());

		[HttpPost]
		public JsonResult Post([FromBody] TransactionModel model) => Handle(() => processOneTransaction(model, mapper));

		[HttpPost("transactions")]
		public JsonResult Transactions([FromBody] List<TransactionModel> transactions)
		{
			return Handle(() => transactions.ForEach(x => processOneTransaction(x, mapper)));
		}

		// Update
		[HttpPut]
		public JsonResult Put([FromBody] TransactionModel model)
		{
			return Handle(() =>
			{
				Transaction parsedTransaction = mapper.ParseTransaction(model);
				Transaction fromDb = dbProvider.GetTransaction(parsedTransaction.TransactionCode);

				decimal amountDiff = fromDb.Amount - parsedTransaction.Amount;

				// create tags
				List<Tag> newTags = model.TagModels.Where(x => x.TagId == 0).Select(mapper.ParseTag).ToList();
				dbProvider.AddTags(newTags);

				// add created tags
				newTags.AddRange(model.TagModels.Where(x => x.TagId != 0).Select(mapper.ParseTag));

				// create middle tables
				parsedTransaction.TransactionTags = newTags.Select(tag => new TransactionTags
				{
					TransactionCode = parsedTransaction.TransactionCode,
					TagId = tag.TagId
				}).ToList();

				dbProvider.ModifyBalance(UserId, amountDiff);
				dbProvider.UpdateTransaction(parsedTransaction);
			});
		}

		[HttpDelete]
		public JsonResult Delete([FromBody] string transactionCode)
		{
			return Handle(() =>
			{
				dbProvider.DeleteTransaction(Guid.Parse(transactionCode));
			});
		}

		[HttpDelete("tag")]
		public JsonResult DeleteTag(int id) => Handle(() => dbProvider.DeleteTag(dbProvider.GetTag(id)));

		[HttpPost("tag")]
		public JsonResult Tag([FromBody] TagModel model) => Handle(() => dbProvider.AddTag(mapper.ParseTag(model)));

		private void processOneTransaction(TransactionModel transaction, Mapper mapper)
		{
			Transaction parsedTransaction = mapper.ParseTransaction(transaction);

			#region Tags

			// create tags
			List<Tag> newTags = transaction.TagModels.Where(x => x.TagId == 0).Select(mapper.ParseTag).ToList();
			dbProvider.AddTags(newTags);

			// add created tags
			newTags.AddRange(transaction.TagModels.Where(x => x.TagId != 0).Select(mapper.ParseTag));

			// create middle tables
			parsedTransaction.TransactionTags = newTags.Select(tag => new TransactionTags
			{
				TransactionCode = parsedTransaction.TransactionCode,
				TagId = tag.TagId
			}).ToList();

			#endregion

			dbProvider.ModifyBalance(transaction.UserId, transaction.Amount * -1);

			dbProvider.AddTransaction(parsedTransaction);
		}
	}
}