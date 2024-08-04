using EasyGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using EasyGame.Models;
using EasyGame.ViewModels;

namespace EasyGame.ViewModels
{
    public class ClientTransactionsViewModel
    {
        public IEnumerable<SelectListItem> Clients { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
        public int SelectedClientId { get; set; }
        public string SelectedClientName { get; set; }
        public decimal SelectedClientBalance { get; set; }
        public Transaction EditedComment { get; set; }
        public int NewTransactionTypeId { get; set; }
        public decimal NewAmount { get; set; }
        public string NewComment { get; set; }
        public List<Client> ClientsList { get; set; }
    }
}
