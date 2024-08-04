using EasyGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGame.ViewModels
{
    public class ClientDetailsViewModel
    {
        public Client Client { get; set; }
        public List<TransactionViewModel> Transactions { get; set; }
    }
}