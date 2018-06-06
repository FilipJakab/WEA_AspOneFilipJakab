using System;
using System.Collections.Generic;

namespace DataProviders.Models
{
    public partial class User
    {
        public User()
        {
            Tag = new HashSet<Tag>();
            Transaction = new HashSet<Transaction>();
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public decimal Balance { get; set; }

        public ICollection<Tag> Tag { get; set; }
        public ICollection<Transaction> Transaction { get; set; }
    }
}
