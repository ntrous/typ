using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Core.Models;

namespace TradeYourPhone.Core.ViewModels
{
    /// <summary>
    /// This view model is used for loading phone model data into the typeahead on the front page
    /// </summary>
    public class PhoneViewModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public List<Condition> conditionPrices;
    }

    public class Condition
    {
        public int PhoneConditionId { get; set; }
        public decimal OfferAmount { get; set; }
    }
}
