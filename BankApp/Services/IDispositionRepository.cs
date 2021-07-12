using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Data;

namespace BankApp.Services
{
    public interface IDispositionRepository
    {
        public void AddDisposition(Disposition disposition);
    }

    class DispositionRepository : IDispositionRepository
    {
        protected readonly BankAppDataContext _dbContext;
        public DispositionRepository(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddDisposition(Disposition disposition)
        {
            _dbContext.Dispositions.Add(disposition);
        }
    }
}
