using TradeYourPhone.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TradeYourPhone.Web.ViewModels
{
    public class GetQuoteViewModel
    {
        //Phone
        public int PhoneMakeId { get; set; }
        public int PhoneModelId { get; set; }
        public int PhoneConditionId { get; set; }
        public decimal PurchaseAmount { get; set; }

        // Initial data
        public SelectList AllPhoneMakes { get; set; }
        public SelectList AllPhoneModels { get; set; }
        public SelectList AllPhoneConditions { get; set; }
        
    }
}