using System.Collections.Generic;
using System.Linq;
using DataProviders;
using DataProviders.Models;

namespace WEA_AspOneFilipJakab.Providers
{
	public class DbProvider
	{
		private readonly Filip_WEAAspOneFilipJakabContext ctx;

		public DbProvider(Filip_WEAAspOneFilipJakabContext ctx)
		{
			this.ctx = ctx;
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

		//public Transaction GetTarnsaction(Guid transactionId)
		//{
		//	return ctx.Transaction.FirstOrDefault(x => x.TransactionId == transactionId);
		//}

		public List<Transaction> GetTransactions(int userId)
		{
			return ctx.Transaction.Where(x => x.UserId == userId).ToList();
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