using EasyGame.Models;
using EasyGame.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyGame.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var model = new ClientTransactionsViewModel
            {
                Clients = db.Clients.Select(c => new SelectListItem
                {
                    Value = c.ClientId.ToString(),
                    Text = c.Name
                }).ToList(),
                Transactions = Enumerable.Empty<Transaction>()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(int selectedClientId)
        {
            var selectedClient = db.Clients.Find(selectedClientId);
            var model = new ClientTransactionsViewModel
            {
                Clients = db.Clients.Select(c => new SelectListItem
                {
                    Value = c.ClientId.ToString(),
                    Text = c.Name
                }).ToList(),
                Transactions = db.Transactions.Where(t => t.ClientId == selectedClientId).ToList(),
                SelectedClientId = selectedClientId,
                SelectedClientName = selectedClient?.Name,
                SelectedClientBalance = selectedClient?.Balance ?? 0
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult EditComment(int transactionId, string comment)
        {
            var transaction = db.Transactions.Find(transactionId);
            if (transaction != null)
            {
                transaction.Comment = comment;
                db.SaveChanges();
            }

            // Redirect back to Index with the selected client to show the updated list of transactions
            return RedirectToAction("Index", new { selectedClientId = transaction.ClientId });
        }

        public ActionResult AddTransaction(int clientId)
        {
            var model = new AddTransactionViewModel
            {
                ClientId = clientId,
                TransactionTypes = db.TransactionTypes.Select(tt => new SelectListItem
                {
                    Value = tt.TransactionTypeID.ToString(),
                    Text = tt.Name
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult AddTransaction(AddTransactionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var transaction = new Transaction
                {
                    ClientId = model.ClientId,
                    TransactionType = model.SelectedTransactionTypeId,
                    Amount = model.Amount,
                    Comment = model.Comment
                };

                db.Transactions.Add(transaction);
                db.SaveChanges();

                var client = db.Clients.Find(model.ClientId);
                if (client != null)
                {
                    if (model.SelectedTransactionTypeId == 1) // Assuming 1 is Debit
                    {
                        client.Balance -= model.Amount;
                    }
                    else if (model.SelectedTransactionTypeId == 2) // Assuming 2 is Credit
                    {
                        client.Balance += model.Amount;
                    }
                    db.SaveChanges();
                }

                TempData["Message"] = "Transaction added successfully.";
                TempData["NewBalance"] = client?.Balance ?? 0;

                return RedirectToAction("Index", new { selectedClientId = model.ClientId });
            }

            model.TransactionTypes = db.TransactionTypes.Select(tt => new SelectListItem
            {
                Value = tt.TransactionTypeID.ToString(),
                Text = tt.Name
            }).ToList();

            return View(model);
        }

        public ActionResult AddClient()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddClient(Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                TempData["ClientMessage"] = "Client added successfully.";
                return RedirectToAction("Index");
            }
            return View(client);
        }

        public ActionResult ViewAllClients(string sortOrder, string searchString)
        {
            var clients = db.Clients.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                clients = clients.Where(c => c.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    clients = clients.OrderByDescending(c => c.Name);
                    break;
                case "balance":
                    clients = clients.OrderByDescending(c => c.Balance);
                    break;
                default:
                    clients = clients.OrderBy(c => c.Name);
                    break;
            }

            var model = new ClientTransactionsViewModel
            {
                ClientsList = clients.ToList()
            };

            return View(model);
        }

        public ActionResult ClientDetails(int clientId)
        {
            var client = db.Clients.Find(clientId);
            if (client == null)
            {
                return HttpNotFound();
            }

            var transactions = db.Transactions
                .Where(t => t.ClientId == clientId)
                .Select(t => new TransactionViewModel
                {
                    Amount = t.Amount,
                    Comment = t.Comment
                }).ToList();

            var model = new ClientDetailsViewModel
            {
                Client = client,
                Transactions = transactions
            };

            return View(model);
        }
    }
}
