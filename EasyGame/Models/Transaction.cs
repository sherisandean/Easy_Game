using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGame.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int TransactionType { get; set; }
        public decimal Amount { get; set; }
        public int ClientId { get; set; }
        public string Comment { get; set; }
        public virtual Client Client { get; set; }
    }
}
