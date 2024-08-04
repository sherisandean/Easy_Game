using EasyGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGame.ViewModels
{
    public class DatabaseViewModel
    {


        public IEnumerable<Client> Clients { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
        public IEnumerable<TransactionType> TransactionTypes { get; set; }
    }
}