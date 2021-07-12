using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.ViewModels
{
    public class CustomerSearchResultViewModel
    {
        public List<CustomerViewModel> Customers { get; set; } = new List<CustomerViewModel>();
        public int Page { get; set; }
        public int TotalPages { get; set; }

        public string q{ get; set; }
    }
}
