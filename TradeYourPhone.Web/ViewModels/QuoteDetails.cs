using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Core.Models;

namespace TradeYourPhone.Web.ViewModels
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
        public decimal TotalAmount
        {
            get
            {
                return Phones.Where(x => !string.IsNullOrEmpty(x.OfferPrice)).Select(x => Convert.ToDecimal(x.OfferPrice)).Sum();
            }
        }

        /// <summary>
        /// Constructs a QuoteDetails object from a Quote object
        /// </summary>
        /// <param name="quote"></param>
        /// <returns></returns>
        public void MapQuote(Quote quote)
        {
            QuoteReferenceId = quote.QuoteReferenceId;
            QuoteStatus = quote.QuoteStatus.QuoteStatusName;
            Phones = new List<PhoneDetail>();
            PostageMethod = quote.PostageMethod;
            AgreedToTerms = quote.AgreedToTerms;

            if (quote.Customer != null)
            {
                quote.Customer.Quotes = null;
                Customer = new CustomerDetail
                {
                    fullname = quote.Customer.FullName,
                    email = quote.Customer.Email,
                    emailConfirm = quote.Customer.Email,
                    mobile = quote.Customer.PhoneNumber,
                    postageStreet = quote.Customer.Address.AddressLine1,
                    postageSuburb = quote.Customer.Address.AddressLine2,
                    postageState = quote.Customer.Address.State,
                    postagePostcode = quote.Customer.Address.PostCode,
                    paymentType = quote.Customer.PaymentDetail.PaymentType,
                    bsb = quote.Customer.PaymentDetail.BSB,
                    accountNum = quote.Customer.PaymentDetail.AccountNumber,
                    paypalEmail = quote.Customer.PaymentDetail.PaypalEmail,
                    paypalSameAsPersonal = quote.Customer.PaymentDetail.PaypalEmail == quote.Customer.Email
                };
            }

            foreach (var details in quote.Phones.OrderBy(x => x.Id).Select(phone => new PhoneDetail()
            {
                Id = phone.Id,
                PhoneMakeName = phone.PhoneMake.MakeName,
                PhoneModelName = phone.PhoneModel.ModelName,
                PhoneCondition = phone.PhoneCondition.Condition,
                OfferPrice = phone.PurchaseAmount.ToString(),
                PrimaryImageString = phone.PhoneModel.PrimaryImageString
            }))
            {
                Phones.Add(details);
            }
        }
    }
}
