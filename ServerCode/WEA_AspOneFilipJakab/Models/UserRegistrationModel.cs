using System;

namespace WEA_AspOneFilipJakab.Models
{
	public class UserRegistrationModel
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

		public decimal Balance { get; set; }

		public DateTime Birthdate { get; set; }

		public string Password { get; set; }
	}
}