using System;
using System.Collections.Generic;

namespace DataProviders.Models
{
    public partial class TransactionCategory
    {
        public TransactionCategory()
        {
            Transaction = new HashSet<Transaction>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }

        public ICollection<Transaction> Transaction { get; set; }
    }
}
