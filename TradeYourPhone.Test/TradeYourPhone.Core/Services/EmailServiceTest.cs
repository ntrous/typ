using System;
using System.Collections.Generic;
using System.Web.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SendGrid;
using TradeYourPhone.Core.Enums;
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
        public void BuildQuoteConfirmationSatchelTest()
        {
            Quote quote = new Quote {
                QuoteReferenceId = "BFF8MGD3",
                Customer = new Customer
                {
                    FullName = "NavTest Magoo",
                    Email = "ntrous@gmail.com"
                }
            };

            var msg = emailService.BuildTemplateMessage(new SendGridMessage(), EmailTemplate.QuoteConfirmationSatchel, quote);
            JObject json = JObject.Parse(msg.Header.JsonString());
            var firstName = json["sub"][":FirstName"][0].Value<string>();
            var quoteRef = json["sub"][":QuoteRef"][0].Value<string>();

            Assert.AreEqual("NavTest", firstName);
            Assert.AreEqual("BFF8MGD3", quoteRef);
        }

        [TestMethod]
        public void BuildQuoteConfirmationSelfPostTest()
        {
            Quote quote = new Quote
            {
                QuoteReferenceId = "BFF8MGD3",
                Customer = new Customer
                {
                    FullName = "NavTest",
                    Email = "ntrous@gmail.com"
                }
            };

            var msg = emailService.BuildTemplateMessage(new SendGridMessage(), EmailTemplate.QuoteConfirmationSelfPost, quote);
            JObject json = JObject.Parse(msg.Header.JsonString());
            var firstName = json["sub"][":FirstName"][0].Value<string>();
            var quoteRef = json["sub"][":QuoteRef"][0].Value<string>();

            Assert.AreEqual("NavTest", firstName);
            Assert.AreEqual("BFF8MGD3", quoteRef);
        }

        [TestMethod]
        public void BuildSatchelSentTest()
        {
            Quote quote = new Quote
            {
                QuoteReferenceId = "AA88MGD3",
                Customer = new Customer
                {
                    FullName = "NavTest Magoo",
                    Email = "ntrous@gmail.com"
                }
            };

            var msg = emailService.BuildTemplateMessage(new SendGridMessage(), EmailTemplate.SatchelSent, quote);
            JObject json = JObject.Parse(msg.Header.JsonString());
            var firstName = json["sub"][":FirstName"][0].Value<string>();
            var quoteRef = json["sub"][":QuoteRef"][0].Value<string>();

            Assert.AreEqual("NavTest", firstName);
            Assert.AreEqual("AA88MGD3", quoteRef);
        }

        [TestMethod]
        public void BuildPaidEmailTest()
        {
            Quote quote = new Quote
            {
                QuoteReferenceId = "AA88MGD3",
                Customer = new Customer
                {
                    FullName = "NavTest Magoo",
                    Email = "ntrous@gmail.com"
                },
                Phones = new List<Phone>
                {
                    new Phone
                    {
                        PurchaseAmount = (decimal)45.0000,
                        PhoneStatusId = (int)PhoneStatusEnum.Paid
                    },
                    new Phone
                    {
                        PurchaseAmount = (decimal)100,
                        PhoneStatusId = (int)PhoneStatusEnum.Paid
                    }
                }
            };

            var msg = emailService.BuildTemplateMessage(new SendGridMessage(), EmailTemplate.Paid, quote);
            JObject json = JObject.Parse(msg.Header.JsonString());
            var firstName = json["sub"][":FirstName"][0].Value<string>();
            var totalAmount = json["sub"][":TotalAmount"][0].Value<decimal>();
            var quoteRef = json["sub"][":QuoteRef"][0].Value<string>();

            Assert.AreEqual("NavTest", firstName);
            Assert.AreEqual("AA88MGD3", quoteRef);
            Assert.AreEqual((decimal)145.00, totalAmount);
            Assert.AreEqual("145.00", totalAmount.ToString());
        }

        [TestMethod]
        public void BuildPaidEmailWithNonPaidPhonesTest()
        {
            Quote quote = new Quote
            {
                QuoteReferenceId = "AA88MGD3",
                Customer = new Customer
                {
                    FullName = "NavTest Magoo",
                    Email = "ntrous@gmail.com"
                },
                Phones = new List<Phone>
                {
                    new Phone
                    {
                        PurchaseAmount = (decimal)45.0000,
                        PhoneStatusId = (int)PhoneStatusEnum.Assessing
                    },
                    new Phone
                    {
                        PurchaseAmount = (decimal)100,
                        PhoneStatusId = (int)PhoneStatusEnum.Assessing
                    }
                }
            };

            var msg = emailService.BuildTemplateMessage(new SendGridMessage(), EmailTemplate.Paid, quote);
            JObject json = JObject.Parse(msg.Header.JsonString());
            var firstName = json["sub"][":FirstName"][0].Value<string>();
            var totalAmount = json["sub"][":TotalAmount"][0].Value<decimal>();
            var quoteRef = json["sub"][":QuoteRef"][0].Value<string>();

            Assert.AreEqual("NavTest", firstName);
            Assert.AreEqual("AA88MGD3", quoteRef);
            Assert.AreEqual((decimal)0.00, totalAmount);
            Assert.AreEqual("0.00", totalAmount.ToString());
        }

        [TestMethod]
        public void BuildPaidEmailWithMixOfPaidPhonesTest()
        {
            Quote quote = new Quote
            {
                QuoteReferenceId = "AA88MGD3",
                Customer = new Customer
                {
                    FullName = "NavTest Magoo",
                    Email = "ntrous@gmail.com"
                },
                Phones = new List<Phone>
                {
                    new Phone
                    {
                        PurchaseAmount = (decimal)45.0000,
                        PhoneStatusId = (int)PhoneStatusEnum.Assessing
                    },
                    new Phone
                    {
                        PurchaseAmount = (decimal)100,
                        PhoneStatusId = (int)PhoneStatusEnum.Paid
                    }
                }
            };

            var msg = emailService.BuildTemplateMessage(new SendGridMessage(), EmailTemplate.Paid, quote);
            JObject json = JObject.Parse(msg.Header.JsonString());
            var firstName = json["sub"][":FirstName"][0].Value<string>();
            var totalAmount = json["sub"][":TotalAmount"][0].Value<decimal>();
            var quoteRef = json["sub"][":QuoteRef"][0].Value<string>();

            Assert.AreEqual("NavTest", firstName);
            Assert.AreEqual("AA88MGD3", quoteRef);
            Assert.AreEqual((decimal)100.00, totalAmount);
            Assert.AreEqual("100.00", totalAmount.ToString());
        }

        [TestMethod]
        public void BuildAssessingEmailTest()
        {
            Quote quote = new Quote
            {
                QuoteReferenceId = "AA88MGD3",
                Customer = new Customer
                {
                    FullName = "NavTest Magoo",
                    Email = "ntrous@gmail.com"
                }
            };

            var msg = emailService.BuildTemplateMessage(new SendGridMessage(), EmailTemplate.Assessing, quote);
            JObject json = JObject.Parse(msg.Header.JsonString());
            var firstName = json["sub"][":FirstName"][0].Value<string>();
            var quoteRef = json["sub"][":QuoteRef"][0].Value<string>();

            Assert.AreEqual("NavTest", firstName);
            Assert.AreEqual("AA88MGD3", quoteRef);
        }
    }
}
