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
				Description = model.Description,
				Name = model.Name,
				Latitude = model.Latitude,
				Longitude = model.Longitude
			};
		}

		public UserModel MapUser(User user)
		{
			return new UserModel
			{
				Birthdate = user.Birthdate,
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Password = user.Password,
				UserId = user.UserId
			};
		}

		public TagModel MapTag(Tag tag)
		{
			return new TagModel
			{
				Name = tag.Name,
				TagId = tag.TagId,
				UserId = tag.UserId
			};
		}

		public TransactionModel MapTransaction(Transaction transaction)
		{
			return new TransactionModel
			{
				UserId = transaction.UserId,
				CategoryId = transaction.CategoryId,
				Date = transaction.Date,
				Description = transaction.Description,
				Name = transaction.Name,
				Latitude = transaction.Latitude,
				Longitude = transaction.Longitude
			};
		}

		public TransactionCategoryModel MapTransactionCategory(TransactionCategory category)
		{
			return new TransactionCategoryModel
			{
				CategoryId = category.CategoryId,
				Name = category.Name
			};
		}
	}
}