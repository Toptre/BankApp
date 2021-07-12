using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers
{
    public class TransactionController : Controller
    {
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



            return View();
        }
        
    }
}
