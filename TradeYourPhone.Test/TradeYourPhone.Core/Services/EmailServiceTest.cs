using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradeYourPhone.Core.Services.Interface;
using TradeYourPhone.Test.SetupData;
using TradeYourPhone.Core.Services.Implementation;
using TradeYourPhone.Core.Models;

namespace TradeYourPhone.Test
{
    [TestClass]
    public class EmailServiceTest
    {
        IEmailService emailService;

        public EmailServiceTest()
        {
            emailService = new EmailService();
        }

        [TestMethod]
        public void SendQuoteSubmittedEmailTest()
        {
            Quote quote = new Quote {
                QuoteReferenceId = "BFF8MGD3",
                Customer = new Customer
                {
                    FullName = "NavTest",
                    Email = "ntrous@gmail.com"
                }
            };
            //emailService.SendQuoteSubmittedEmail(quote);
        }
    }
}
