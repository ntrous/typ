using TradeYourPhone.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TradeYourPhone.Core.ViewModels
{
    public class QuoteDetailsViewModel
    {
        public Quote quote { get; set; }

        // This is for maintaining the quote status so after postback we can see if the status has changed
        public int CurrentQuoteStatus { get; set; }

        // This list is so we can iterate over the list and bind to the view
        public IList<Phone> phones { get; set; }

        // UserId of the person updating the quote details
        public string UserId { get; set; }

        public SelectList QuoteStatuses { get; set; }
        public SelectList PostageMethods { get; set; }
        public SelectList PaymentTypes { get; set; }
        public SelectList States { get; set; }
        public SelectList Countries { get; set; }
        public SelectList PhoneStatuses { get; set; }
        public SelectList PhoneMakes { get; set; }
        public SelectList PhoneModels { get; set; }
        public SelectList Conditions { get; set; }
    }
}