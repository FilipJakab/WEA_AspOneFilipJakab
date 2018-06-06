using System;

namespace WEA_AspOneFilipJakab.Models
{
	public class UserModel
	{
		public int UserId { get; set; }

		public string Email { get; set; }

		public decimal Balance { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Birthdate { get; set; }
	}
}