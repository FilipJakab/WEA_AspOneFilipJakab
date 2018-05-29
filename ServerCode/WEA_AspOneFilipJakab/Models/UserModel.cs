using System;

namespace WEA_AspOneFilipJakab.Models
{
	public class UserModel
	{
		public int UserId { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public DateTime Birthdate { get; set; }
	}
}