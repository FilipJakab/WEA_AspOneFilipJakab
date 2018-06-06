using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
				Balance = model.Balance,
				FirstName = model.FirstName,
				LastName = model.LastName,
				Password = model.Password
			};
		}

		public User ParseUser(UserModel model)
		{
			DateTime birthdate = DateTime.ParseExact(model.Birthdate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

			return new User
			{
				UserId = model.UserId,
				Email = model.Email,
				Balance = model.Balance,
				Birthdate = birthdate,
				FirstName = model.FirstName,
				LastName = model.LastName
			};
		}

		public Transaction ParseTransaction(TransactionModel model)
		{
			DateTime date = DateTime.ParseExact(model.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture);

			return new Transaction
			{
				TransactionCode = model.TransactionCode,
				UserId = model.UserId,
				Date = date,
				Amount = model.Amount,
				Description = model.Description,
				CategoryId = model.CategoryId,
				Name = model.Name,
				Latitude = model.Latitude,
				Longitude = model.Longitude
			};
		}

		public UserModel MapUser(User user)
		{
			return new UserModel
			{
				Birthdate = user.Birthdate.ToString("yyyy MMMM dd"),
				Email = user.Email,
				Balance = user.Balance,
				FirstName = user.FirstName,
				LastName = user.LastName,
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

		public Tag ParseTag(TagModel model)
		{
			return new Tag
			{
				Name = model.Name,
				TagId = model.TagId,
				UserId = model.UserId
			};
		}

		public TransactionModel MapTransaction(Transaction transaction, IEnumerable<Tag> tags)
		{
			return new TransactionModel
			{
				TransactionCode = transaction.TransactionCode,
				UserId = transaction.UserId,
				Date = transaction.Date.ToString("yyyy MMMM dd"),
				Description = transaction.Description,
				Amount = transaction.Amount,
				Name = transaction.Name,
				CategoryId = transaction.CategoryId,
				TagModels = transaction.TransactionTags.Select(x => MapTag(tags.First(y => y.TagId == x.TagId))),
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