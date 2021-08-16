using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Data;
using BankApp.Services;
using BankApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers
{
    public class TransactionController : BaseController
    {
        protected readonly ITransactionRepository _transactions;
        public TransactionController(ITransactionRepository transactions, IAccountRepository accounts):base(accounts)
        {
           _transactions=transactions;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NewDeposit()
        {
            var viewModel = new NewDepositViewModel();
            
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult NewDeposit(NewDepositViewModel viewModel)
        {
            var dbAcc = _accounts.GetAllAccounts().FirstOrDefault(r => r.AccountId == viewModel.AccountId);

            if (dbAcc == null)
            {
                ModelState.AddModelError("AccountId", "Unknown Account number");
            }

            if (viewModel.Amount < 0)
            {
                ModelState.AddModelError("Amount", "Not possible to deposit a negative amount");
            }



            if (ModelState.IsValid)
            {
                var dbTrans = new Transaction();
                _transactions.AddTransaction(dbTrans);
                dbTrans.AccountId = viewModel.AccountId;
                dbTrans.Date = DateTime.Now;
                dbTrans.Type = viewModel.Type;
                dbTrans.Operation = viewModel.Operation;
                dbTrans.Amount = viewModel.Amount;


                
                var balance = dbAcc.Balance + viewModel.Amount;

                dbTrans.Balance = balance;
                dbAcc.Balance = balance;

                _transactions.Save();

                return RedirectToAction("NewDeposit");


            }

            return View();
        }

        public IActionResult NewWithdrawal()
        {
            var viewModel = new NewWithdrawalViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult NewWithdrawal(NewWithdrawalViewModel viewModel)
        {
            var dbAcc = _accounts.GetAllAccounts().FirstOrDefault(r => r.AccountId == viewModel.AccountId);

            if (dbAcc == null)
            {
                ModelState.AddModelError("AccountId", "Unkown Account number");
            }

            if (viewModel.Amount < 0)
            {
                ModelState.AddModelError("Amount", "Not possible to deposit a negative amount");
            }

            if (true)
            {

            }


            if (ModelState.IsValid)
            {
                var dbTrans = new Transaction();
                _transactions.AddTransaction(dbTrans);
                dbTrans.AccountId = viewModel.AccountId;
                dbTrans.Date = DateTime.Now;
                dbTrans.Type = viewModel.Type;
                dbTrans.Operation = viewModel.Operation;
                dbTrans.Amount = decimal.Negate(viewModel.Amount);


                var balance = dbAcc.Balance - viewModel.Amount;

                dbTrans.Balance = balance;
                dbAcc.Balance = balance;

                _transactions.Save();

                return RedirectToAction("NewWithdrawal");
            }

            return View();

        }
    }
}
