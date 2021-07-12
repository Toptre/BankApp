using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Data;
using BankApp.Models;
using BankApp.Services;
using BankApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BankApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        protected readonly ICustomerRepository _customers;

        public HomeController(ILogger<HomeController> logger, ICustomerRepository customers, IAccountRepository accounts) : base(accounts)
        {
            _logger = logger;
            _customers = customers;
        }

        [ResponseCache(Duration = 30)]
        public IActionResult Index()
        {
            var viewModel = new HomeStatViewModel();

            viewModel.NoOfAccounts = _accounts.GetAllAccounts().Count();
            viewModel.NoOfCustomers = _customers.GetAllCustomers().Count();
            viewModel.GlobalTotalBalance = _accounts.GetAllAccounts().Sum(r => r.Balance);
            viewModel.LastUpdate = DateTime.Now;
                 

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
