using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Data;

namespace BankApp.Services
{
    public interface ICustomerRepository
    {
        IQueryable<Customer> GetAllCustomers();
        Customer GetCustomerById(int id);
        public void AddCustomer(Customer customer);
        public void Save();
    }

    class CostumerRepository : ICustomerRepository
    {
        protected readonly BankAppDataContext _dbContext;
        public CostumerRepository(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Customer> GetAllCustomers()
        {
            return _dbContext.Customers;
        }

        public Customer GetCustomerById(int id)
        {
            return _dbContext.Customers.Find(id);
        }

        public void AddCustomer(Customer customer)
        {
            _dbContext.Customers.Add(customer);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

    }
}
