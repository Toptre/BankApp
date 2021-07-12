using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Services;
using BankApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Controllers
{
    public class AccountController : BaseController
    {

        protected readonly ITransactionRepository _trans; 

        public AccountController(ITransactionRepository trans, IAccountRepository account):base(account)
        {
            _trans = trans;
        }
        public IActionResult Index(int id)
        {
            var viewModel = new AccountViewModel();

            var repoAccount = _accounts.GetAllAccounts()
                .First(s => s.AccountId.Equals(id));

            viewModel.AccountId = repoAccount.AccountId;
            viewModel.Balance = repoAccount.Balance;

            viewModel.Transactions = _trans.GetAllTransactions().Where(x=> x.AccountId.Equals(id)).OrderByDescending(y => y.TransactionId).Skip(0).Take(20).Select(p => new TransactionRowViewModel
            {
                AccountId = p.AccountId,
                TransactionId = p.TransactionId,
                Amount = p.Amount,
                Balance = p.Balance,
                Bank = p.Bank,
                Date = p.Date,
                 Account = p.Account,
                Operation = p.Operation,
                Symbol = p.Symbol,
                 Type = p.Type

            }).ToList();



            return View(viewModel);
        }
        public IActionResult LoadMoreTransactions(int id, int skip)
        {
            var viewModel = new AccountLoadedTransactionsViewModel();

            var repoAccount = _accounts.GetAllAccounts().Include(r => r.Transactions)
                .First(s => s.AccountId.Equals(id));

            viewModel.Transactions = _trans.GetAllTransactions().Where(x => x.AccountId.Equals(id)).OrderByDescending(y => y.TransactionId).Skip(skip).Take(20).Select(p => new TransactionRowViewModel
            {
                AccountId = p.AccountId,
                TransactionId = p.TransactionId,
                Amount = p.Amount,
                Balance = p.Balance,
                Bank = p.Bank,
                Date = p.Date,
                Account = p.Account,
                Operation = p.Operation,
                Symbol = p.Symbol,
                Type = p.Type

            }).ToList();



            return View(viewModel);
        }

        
    }
}
