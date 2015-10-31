using TradeYourPhone.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TradeYourPhone.Core.ViewModels
{
    public class FinaliseQuoteViewModel
    {
        public Customer customer { get; set; }

        public SelectList AllStates { get; set; }
        public SelectList AllCountries { get; set; }
        public SelectList AllPaymentTypes { get; set; }
    }
}