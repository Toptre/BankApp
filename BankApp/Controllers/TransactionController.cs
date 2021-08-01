using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Data;
using BankApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers
{
    public class TransactionController : BaseController
    {
        public TransactionController ()
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NewDeposit()
        {
            var viewModel = new NewDepositViewModel();
            viewModel.Type = "Credit";
            viewModel.Operation = "Credit in Cash";

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult NewDeposit(NewDepositViewModel model)
        {

            if (ModelState.IsValid)
            {
                var dbTrans = new Transaction();
                _transaction.AddTrans(dbTrans);
                dbTrans.AccountId = viewModel.AccountId;
                dbTrans.Date = DateTime.Now;
                dbTrans.Type = "Credit";
                dbTrans.Operation = "Credit in Cash";
                dbTrans.Amount = viewModel.Amount;


                var dbAcc = _account.GetAllAccount().First(r => r.AccountId == viewModel.AccountId);
                var balance = dbAcc.Balance + viewModel.Amount;

                dbTrans.Balance = balance;
                dbAcc.Balance = balance;

                _transaction.Save();
                return RedirectToAction("New");


            }

            return View();
        }


        
    }
}
