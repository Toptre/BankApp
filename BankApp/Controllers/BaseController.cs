using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Data;
using BankApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers
{
    public class BaseController : Controller
    {
        
        protected readonly IAccountRepository _accounts;
        

        public BaseController(IAccountRepository accounts)
        {
            _accounts = accounts;
           
        }

        protected BaseController()
        {
        }
    }
}
