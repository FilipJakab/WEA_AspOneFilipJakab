using System.Collections.Generic;
using DataProviders.Models;
using WEA_AspOneFilipJakab.Models;

namespace WEA_AspOneFilipJakab.Helpers
{
	public class Mapper
	{
		public User ParseUser(UserRegistrationModel model)
		{
			return new User
			{
				Birthdate = model.Birthdate,
				Email = model.Email,
				FirstName = model.FirstName,
				LastName = model.LastName,
				Password = model.Password
			};
		}

		public Transaction ParseTransaction(TransactionModel model)
		{
			return new Transaction
			{
				UserId = model.UserId,
				CategoryId = model.CategoryId,
				Date = model.Date,
				Descrpition = model.Description,
				Name = model.Name,
				Latitude = model.Latitude,
				Longitude = model.Longitude
			};
		}
	}
}