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
        public void AddTransaction(Transaction dbTransaction);
        public void Save();
    }

    class TransactionsRepository:ITransactionRepository
    {
        protected readonly BankAppDataContext _dbContext;
        public TransactionsRepository(BankAppDataContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public void AddTransaction(Transaction dbTransaction)
        {
            _dbContext.Transactions.Add(dbTransaction);
        }

        public IQueryable<Transaction> GetAllTransactions()
        {
            return _dbContext.Transactions;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
