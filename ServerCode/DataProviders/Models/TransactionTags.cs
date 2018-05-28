using System;
using System.Collections.Generic;

namespace DataProviders.Models
{
    public partial class TransactionTags
    {
        public int TransactionTagsId { get; set; }
        public int TagId { get; set; }
        public Guid TransactionCode { get; set; }

        public Tag Tag { get; set; }
        public Transaction TransactionCodeNavigation { get; set; }
    }
}
