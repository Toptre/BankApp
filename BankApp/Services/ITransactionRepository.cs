using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Data;

namespace BankApp.Services
{
    public interface ITransactionRepository
    {
        IQueryable<Transaction> GetAllTransactions();
    }

    class TransactionsRepository:ITransactionRepository
    {
        protected readonly BankAppDataContext _dbcontext;
        public TransactionsRepository(BankAppDataContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IQueryable<Transaction> GetAllTransactions()
        {
            return _dbcontext.Transactions;
        }
    }
}
