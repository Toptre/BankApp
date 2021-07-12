using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Data;

namespace BankApp.Services
{
    public interface IAccountRepository
    {
        IQueryable<Account> GetAllAccounts();
        Account GetAccountById(int id);
        public void AddAccount(Account account);
    }

    class AccountRepository : IAccountRepository
    {
        protected readonly BankAppDataContext _dbContext;
        public AccountRepository(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Account> GetAllAccounts()
        {
            return _dbContext.Accounts;
        }

        public Account GetAccountById(int id)
        {
            return _dbContext.Accounts.Find(id);
        }
        public void AddAccount(Account account)
        {
            _dbContext.Accounts.Add(account);
        }


    }

}
