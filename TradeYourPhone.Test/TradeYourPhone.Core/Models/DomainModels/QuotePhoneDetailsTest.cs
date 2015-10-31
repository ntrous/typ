using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradeYourPhone.Core.Models.DomainModels;

namespace TradeYourPhone.Test.TradeYourPhone.Core.Models.DomainModels
{
    [TestClass]
    public class QuotePhoneDetailsTest
    {
        [TestMethod]
        public void TotalAmountTest()
        {
            QuoteDetails details = new QuoteDetails() { Phones = new System.Collections.Generic.List<PhoneDetail>() };
            PhoneDetail phone1 = new PhoneDetail()
            {
                OfferPrice = "110"
            };
            PhoneDetail phone2 = new PhoneDetail()
            {
                OfferPrice = "125.34"
            };

            details.Phones.Add(phone1);
            details.Phones.Add(phone2);

            Assert.AreEqual((decimal)235.34, details.TotalAmount);
        }
    }
}
