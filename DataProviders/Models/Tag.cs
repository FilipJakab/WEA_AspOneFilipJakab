using System;
using System.Collections.Generic;

namespace DataProviders.Models
{
    public partial class Tag
    {
        public Tag()
        {
            TransactionTags = new HashSet<TransactionTags>();
        }

        public int TagId { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public ICollection<TransactionTags> TransactionTags { get; set; }
    }
}
