using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.ViewModels
{
    public class HomeStatViewModel
    {
        public int NoOfCustomers { get; set; }
        public int NoOfAccounts { get; set; }

        public decimal GlobalTotalBalance { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}
