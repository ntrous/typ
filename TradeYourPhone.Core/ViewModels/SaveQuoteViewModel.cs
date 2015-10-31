using TradeYourPhone.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeYourPhone.Core.ViewModels
{
    public class SaveQuoteViewModel
    {
        public Customer Customer { get; set; }
        public int PostageMethodId { get; set; }
        public bool AgreedToTerms { get; set; }

        public override string ToString()
        {
            return String.Format("PostageMethodId: {0}, AgreedToTerms: {1}", PostageMethodId.ToString(), AgreedToTerms);
        }
    }
}
