using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.ViewModels
{
    public class AccountViewModel
    {
        public int AccountId { get; set; }
        public decimal Balance { get; set; }

        public List<TransactionRowViewModel> Transactions { get; set; } = new List<TransactionRowViewModel>();

    }
}
