using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeYourPhone.Core.Models.DomainModels
{
    public class QuoteDetails
    {
        /// <summary>
        /// The Quote Reference ID
        /// </summary>
        public string QuoteReferenceId { get; set; }

        /// <summary>
        /// The Status of the Quote
        /// </summary>
        public string QuoteStatus { get; set; }

        /// <summary>
        /// Customer details for the Quote
        /// </summary>
        public CustomerDetail Customer { get; set; }

        /// <summary>
        /// List of Phones for Quote
        /// </summary>
        public List<PhoneDetail> Phones { get; set; }

        /// <summary>
        /// Postage Method Id
        /// </summary>
        public PostageMethod PostageMethod { get; set; }

        /// <summary>
        /// Has customer agreed to the terms and conditions
        /// </summary>
        public bool AgreedToTerms { get; set; }

        /// <summary>
        /// Total Amount of all Phones in Phones List where OfferPrice is not null
        /// </summary>
        public decimal TotalAmount { 
            get
            {
                return Phones.Where(x => x.OfferPrice != null && x.OfferPrice.Length > 0).Select(x => Convert.ToDecimal(x.OfferPrice)).Sum();   
            } 
        }
    }
}
