using System;
using System.Collections.Generic;

namespace DataProviders.Models
{
    public partial class Place
    {
        public int PlaceId { get; set; }
        public Guid TransactionId { get; set; }
    }
}
