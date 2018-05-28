using System;
using System.Collections.Generic;
using DataProviders.Models;

namespace WEA_AspOneFilipJakab.Models
{
	public class TransactionModel
	{
		public int UserId { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public DateTime Date { get; set; }

		public List<int> TagIds { get; set; }

		public long? Longitude { get; set; }

		public long? Latitude { get; set; }

		public int? CategoryId { get; set; }
	}
}