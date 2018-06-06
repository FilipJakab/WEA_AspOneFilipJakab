using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DataProviders;
using DataProviders.Models;
using Microsoft.EntityFrameworkCore;
using WEA_AspOneFilipJakab.Models;

namespace WEA_AspOneFilipJakab.Providers
{
	public class DbProvider
	{
		private readonly Filip_WEAAspOneFilipJakabContext ctx;

		public DbProvider(Filip_WEAAspOneFilipJakabContext ctx)
		{
			this.ctx = ctx;
		}

		public void DeleteTransaction(Guid transactionCode)
		{
			ctx.Transaction.Remove(GetTransaction(transactionCode));

			ctx.SaveChanges();
		}

		public void DeleteTag(Tag tag)
		{
			ctx.Database.ExecuteSqlCommand("exec dbo.DeleteTag @tagId", new SqlParameter("@tagId", SqlDbType.Int)
			{
				Value = tag.TagId
			});
		}

		public void AddTags(IEnumerable<Tag> tags)
		{
			ctx.Tag.AddRange(tags);

			ctx.SaveChanges();
		}

		public void AddTag(Tag tag)
		{
			ctx.Tag.Add(tag);

			ctx.SaveChanges();
		}

		public Tag GetTag(int tagId)
		{
			return ctx.Tag.FirstOrDefault(x => x.TagId == tagId);
		}

		public List<Tag> GetTags(int userId)
		{
			return ctx.Tag.Where(x => x.UserId == userId).ToList();
		}

		public void AddTransaction(Transaction transaction)
		{
			ctx.Transaction.Add(transaction);

			ctx.SaveChanges();
		}

		public Transaction GetTransaction(Guid transactionCode)
		{
			return ctx.Transaction.FirstOrDefault(x => x.TransactionCode == transactionCode);
		}

		public Transaction UpdateTransaction(Transaction transaction)
		{
			Transaction found = ctx.Transaction.FirstOrDefault(x => x.TransactionCode == transaction.TransactionCode);

			if (found == null)
				throw new ArgumentOutOfRangeException(nameof(transaction.TransactionCode), "not found");

			found.Name = transaction.Name;
			found.Amount = transaction.Amount;
			found.CategoryId = transaction.CategoryId;
			found.Date = transaction.Date;
			found.Description = transaction.Description;

			ctx.SaveChanges();

			return found;
		}

		public List<Transaction> GetTransactions(int userId)
		{
			return ctx.Transaction.Include(x => x.TransactionTags).Where(x => x.UserId == userId).ToList();
		}

		public TransactionCategory GetCategory(int id)
		{
			return ctx.TransactionCategory.First(x => x.CategoryId == id);
		}

		public List<TransactionCategory> GetCategories()
		{
			return ctx.TransactionCategory.ToList();
		}

		public void AddUser(User user)
		{
			ctx.User.Add(user);

			ctx.SaveChanges();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="balanceDiff">add to/remove from balance negative or positive <paramref name="balanceDiff"/></param>
		public void ModifyBalance(int userId, decimal balanceDiff)
		{
			User found = ctx.User.First(x => x.UserId == userId);

			found.Balance += balanceDiff;

			ctx.SaveChanges();
		}

		public User UpdateUser(User user)
		{
			User found = ctx.User.FirstOrDefault(x => x.UserId == user.UserId);

			if (found == null)
				throw new ArgumentOutOfRangeException(nameof(user.UserId), "User for specified Id was not found");

			found.Birthdate = user.Birthdate;
			found.Email = user.Email;
			found.FirstName = user.FirstName;
			found.LastName = user.LastName;

			ctx.SaveChanges();

			return found;
		}

		public User GetUser(int userId)
		{
			return ctx.User.FirstOrDefault(x => x.UserId == userId);
		}

		public User GetUserByEmail(string email)
		{
			return ctx.User.FirstOrDefault(x => x.Email == email);
		}
	}
}