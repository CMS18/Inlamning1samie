using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using samiesbank.Models;
using samiesbank.Models.ViewModels;

namespace samiesbank.Controllers
{
    public class TransferController : Controller
    {
            public IActionResult Index(TransferViewModel model = null)
            {
                if (model == null)
                {
                    model = new TransferViewModel();
                }
                return View(model);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Transfer(TransferViewModel model)
            {
                if (ModelState.IsValid)
                {
                    var accounts = BankRepository.GetAccounts();
                    var from = accounts.Find(x => x.AccountID == model.TransferFromId);
                    var to = accounts.Find(x => x.AccountID == model.TransferToId);
                    Account result = new Account();
                    result.Transfer(model.Amount, from, to);
                    model.ErrorMessage = result.ErrorMessage;
                    model.SuccessMessage = result.SuccessMessage;
                }
                return View("Index", model);
            }     
    }
}

