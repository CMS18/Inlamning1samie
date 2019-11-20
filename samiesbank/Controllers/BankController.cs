using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using samiesbank.Models;
using samiesbank.Models.ViewModels;

namespace samiesbank.Controllers
{
    public class BankController : Controller
    {
        public IActionResult Index(TransactionViewModel model = null)
        {
            if (model == null)
            {
                model = new TransactionViewModel();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Deposit(TransactionViewModel model)
        {
            if (ModelState.IsValid)
            {
                BankRepository.Deposit(model.DepositAccountId, model.DepositAmount);
            }
            model.DepositErrorMessage = BankRepository.Errormessage;
            model.DepositSuccessMessage = BankRepository.SuccessMessage;
            BankRepository.SuccessMessage = "";
            BankRepository.Errormessage = "";
            return View("index", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Withdraw(TransactionViewModel model)
        {
            if (ModelState.IsValid)
            {
                BankRepository.Withdrawal(model.WithdrawalAccountId, model.WithdrawalAmount);

            }
            model.WithdrawalErrorMessage = BankRepository.Errormessage;
            model.WithdrawalSuccessMessage = BankRepository.SuccessMessage;
            BankRepository.SuccessMessage = "";
            BankRepository.Errormessage = "";
            return View("index", model);
        }
    }
}