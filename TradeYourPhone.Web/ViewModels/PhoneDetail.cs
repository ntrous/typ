using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeYourPhone.Web.ViewModels
{
    public class PhoneDetail
    {
        public int Id { get; set; }
        public string PhoneMakeName { get; set; }
        public string PhoneModelName { get; set; }
        public string PhoneCondition { get; set; }
        public string OfferPrice { get; set; }
        public string PrimaryImageString { get; set; }
    }
}
