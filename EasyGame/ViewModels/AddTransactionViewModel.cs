using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyGame.ViewModels
{
    public class AddTransactionViewModel
    {
        public int ClientId { get; set; }
        public IEnumerable<SelectListItem> TransactionTypes { get; set; }
        public int SelectedTransactionTypeId { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
    }

}