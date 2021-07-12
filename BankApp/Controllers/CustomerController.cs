using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Data;
using BankApp.Services;
using BankApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Controllers
{
    public class CustomerController : BaseController
    {
        protected readonly ICustomerRepository _customers;
        protected readonly IDispositionRepository _dispositions;
        public CustomerController(ICustomerRepository customers, IDispositionRepository dispositions, IAccountRepository accounts) : base(accounts)
        {
            _customers = customers;
            _dispositions = dispositions;

        }

        public IActionResult SearchResult(string q, int page = 1)
        {
            int pageSize = 50;

            var viewModel = new CustomerSearchResultViewModel();

            var query = _customers.GetAllCustomers()
                .Where(r => q == null ||  r.Givenname.Contains(q) || r.City.Contains(q) || r.Surname.Contains(q));
            
            //int totalRowCount = query.Count();

            var pageCount = (double)(query.Count()) / pageSize;
            viewModel.TotalPages = (int)Math.Ceiling(pageCount);

            int howManyRecordsToSkip = (page - 1) * pageSize;
            query = query.Skip(howManyRecordsToSkip).Take(pageSize);



            viewModel.Customers = query
                .Select(dbCust => new CustomerViewModel
                {
                    CustomerId = dbCust.CustomerId,
                    NationalId = dbCust.NationalId,
                    Givenname = dbCust.Givenname,
                    Surname = dbCust.Surname,
                    Streetaddress = dbCust.Streetaddress,
                    City = dbCust.City

                }).ToList();
            
            viewModel.q = q;
            viewModel.Page = page;
            

            return View(viewModel);

        }

        public IActionResult SelectedCustomer(int CustomerId)
        {
            var viewModel = new CustomerViewModel();

            var dbCustomer = _customers.GetAllCustomers().Include(r => r.Dispositions)
                                  .FirstOrDefault(x => x.CustomerId == CustomerId); 
                
                /*_dbContext.Customers.Include(r => r.Dispositions)
                                  .First(x => x.CustomerId == CustomerId);*/
            
            if (dbCustomer == null)
            {
                viewModel.NotFound = true;
                return View(viewModel);

            }
                



             
             viewModel.CustomerId = dbCustomer.CustomerId;
             viewModel.Gender = dbCustomer.Gender;
             viewModel.Givenname = dbCustomer.Givenname;
             viewModel.Surname = dbCustomer.Surname;
             viewModel.Streetaddress = dbCustomer.Streetaddress;
             viewModel.City = dbCustomer.City;
             viewModel.Zipcode = dbCustomer.Zipcode;
             viewModel.Country = dbCustomer.Country;
             viewModel.CountryCode = dbCustomer.CountryCode;
             viewModel.Birthday = dbCustomer.Birthday;
             viewModel.NationalId = dbCustomer.NationalId;
             viewModel.Telephonecountrycode = dbCustomer.Telephonecountrycode;

             var dispAcc = dbCustomer.Dispositions.ToList();

            foreach (var d in dispAcc)
            {
                var accout = new CustomerAccountsViewModel();
                var dbacc = _accounts.GetAccountById(d.AccountId);
                    /*.Accounts.First(n => n.AccountId.Equals(d.AccountId));*/
                accout.AccountId = dbacc.AccountId;
                accout.Balance = dbacc.Balance;
                accout.Created = dbacc.Created;
                accout.Frequency = dbacc.Frequency;

                viewModel.Accounts.Add(accout);

            }

            viewModel.SumOfCustomerAccounts = viewModel.Accounts.Sum(x => x.Balance);


                
            return View(viewModel);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult New()
        {
            var viewModel = new NewCustomerViewModel();

            return View(viewModel);
        }

        public IActionResult New(NewCustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var dbCustomer = new Customer();
                var dbAccount = new Account();
                var dbDisposition = new Disposition();

                _customers.AddCustomer(dbCustomer);
                _accounts.AddAccount(dbAccount);
                _dispositions.AddDisposition(dbDisposition);

                dbCustomer.Gender = viewModel.Gender;
                dbCustomer.Givenname = viewModel.Givenname;
                dbCustomer.Surname = viewModel.Surname;
                dbCustomer.Streetaddress = viewModel.Streetaddress;
                dbCustomer.City = viewModel.City;
                dbCustomer.Zipcode = viewModel.Zipcode;
                dbCustomer.Country = viewModel.Country;
                dbCustomer.CountryCode = viewModel.CountryCode;
                dbCustomer.Birthday = viewModel.Birthday;
                dbCustomer.NationalId = viewModel.NationalId;
                dbCustomer.Telephonecountrycode = viewModel.Telephonecountrycode;
                dbCustomer.Telephonenumber = viewModel.Telephonenumber;
                dbCustomer.Emailaddress = viewModel.Emailaddress;

                dbAccount.Created = DateTime.Now;
                dbAccount.Frequency = "Monthly";
                dbAccount.Balance = 0;




                _customers.Save();

                return RedirectToAction("Search");
            }

            return View(viewModel);
        }
      

    }
}
