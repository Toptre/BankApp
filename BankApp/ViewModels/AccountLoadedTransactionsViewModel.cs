using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.ViewModels
{
    public class AccountLoadedTransactionsViewModel
    {
        public List<TransactionRowViewModel> Transactions { get; set; } = new List<TransactionRowViewModel>();
    }
}
