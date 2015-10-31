using TradeYourPhone.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TradeYourPhone.Core.ViewModels
{
    public class PhoneModelViewModel
    {
        public PhoneModel Model { get; set; }

        /// <summary>
        /// This property is used instead of Model.PhoneConditionPrices as we cant bind the model to an ICollection
        /// Instead we need the ConditionPrices in a List. Thus this property.
        /// </summary>
        public IList<PhoneConditionPrice> ConditionPrices { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase PrimaryImage { get; set; }

        public SelectList PhoneMakes { get; set; }
    }
}