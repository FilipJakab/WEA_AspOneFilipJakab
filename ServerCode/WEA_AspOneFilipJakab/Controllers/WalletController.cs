using System;
using System.Collections.Generic;
using System.Linq;
using DataProviders;
using DataProviders.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEA_AspOneFilipJakab.Helpers;
using WEA_AspOneFilipJakab.Models;
using WEA_AspOneFilipJakab.Providers;

namespace WEA_AspOneFilipJakab.Controllers
{
	[Authorize]
	public class WalletController : Controller
	{
		//private readonly Filip_WEAAspOneFilipJakabContext ctx;
		private readonly DbProvider dbProvider;

		public WalletController(Filip_WEAAspOneFilipJakabContext ctx)
		{
			this.ctx = ctx;
			dbProvider = new DbProvider(ctx);
		}

		///// <summary>
		///// Get UserInfo with his Labels
		///// </summary>
		///// <param name="userId"></param>
		///// <returns></returns>
		//[HttpGet]
		//public JsonResult Get(int userId)
		//{
		//	throw new NotImplementedException();
		//}

		[HttpPost]
		public JsonResult Post([FromBody] TransactionModel model)
		{
			Mapper mapper = new Mapper();

			Transaction transaction = mapper.ParseTransaction(model);

			transaction.TransactionTags = model.TagIds.Select(tagId => new TransactionTags
			{
				TagId = tagId,
				TransactionCode = transaction.TransactionCode
			}).ToList();

			dbProvider.AddTransaction(transaction);

			throw new NotImplementedException();
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
	}
}