﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.ViewModels
{
    public class NewDepositViewModel
    {
        public int TransactionId { get; set; }
        
        public int AccountId { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; } = "Credit";
        public string Operation { get; } = "Credit in Cash";


        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string Symbol { get; set; }
        public string Bank { get; set; }
        public string Account { get; set; }
    }
}
