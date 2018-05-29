using System.Collections.Generic;

namespace WEA_AspOneFilipJakab.Models
{
	public class UserInfoPayload
	{
		public UserModel User { get; set; }

		public List<TagModel> Tags { get; set; }

		public List<TransactionModel> Transactions { get; set; }

		public List<TransactionCategoryModel> Categories { get; set; }
	}
}