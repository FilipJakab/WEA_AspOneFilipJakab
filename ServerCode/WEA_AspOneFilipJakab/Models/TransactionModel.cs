using System;
using System.Collections.Generic;
using DataProviders.Models;

namespace WEA_AspOneFilipJakab.Models
{
	public class TransactionModel
	{
		public Guid TransactionCode { get; set; }

		public int UserId { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public string Date { get; set; }

		public List<TransactionCategoryModel> Categories { get; set; }

		public IEnumerable<TagModel> TagModels { get; set; }

		public decimal? Longitude { get; set; }

		public decimal? Latitude { get; set; }

		public int? CategoryId { get; set; }
	}
}