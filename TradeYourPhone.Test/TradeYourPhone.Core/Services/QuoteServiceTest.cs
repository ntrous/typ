using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradeYourPhone.Core.Models;
using System.Collections.Generic;
using TradeYourPhone.Core.Services.Interface;
using System.Linq;
using TradeYourPhone.Web.ViewModels;
using TradeYourPhone.Test.SetupData;
using TradeYourPhone.Core.Enums;
using System.Transactions;
using System.Globalization;
using TradeYourPhone.Core.Services.Implementation;

namespace TradeYourPhone.Test
{
    [TestClass]
    public class QuoteServiceTest
    {
        IQuoteService quoteService;
        IPhoneService phoneService;
        CreateMockData cmd;
        TransactionScope _trans;

        public QuoteServiceTest()
        {
            cmd = new CreateMockData();
        }

        [TestInitialize()]
        public void Init()
        {
            phoneService = cmd.GetPhoneService();
            quoteService = cmd.GetQuoteService();
            _trans = new TransactionScope();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            _trans.Dispose();
        }

        #region PaymentTypeTests

        [TestMethod]
        public void GetAllPaymentTypesTest()
        {
            IEnumerable<PaymentType> paymentTypes = quoteService.GetAllPaymentTypes();

            var paymentType = paymentTypes.Select(x => x.PaymentTypeName).ElementAt(0);
            Assert.IsTrue(paymentTypes.Count() == 2);
            Assert.AreEqual("Bank Transfer", paymentType);
        }

        [TestMethod]
        public void GetAllPaymentTypeNamesTest()
        {
            List<string> paymentTypes = quoteService.GetAllPaymentTypeNames();

            Assert.IsTrue(paymentTypes.Count() == 2);
            Assert.AreEqual("Bank Transfer", paymentTypes.Select(x => x).ElementAt(0));
            Assert.AreEqual("Paypal", paymentTypes.Select(x => x).ElementAt(1));
        }

        [TestMethod]
        public void GetPaymentTypeByIdTest()
        {
            PaymentType paymentType = quoteService.GetPaymentTypeById(2);
            Assert.AreEqual("Paypal", paymentType.PaymentTypeName);
        }

        [TestMethod]
        public void CreatePaymentTypeTest()
        {
            PaymentType paymentType = new PaymentType
            {
                PaymentTypeName = "Direct Debit"
            };

            quoteService.CreatePaymentType(paymentType);
            PaymentType newPaymentType = quoteService.GetAllPaymentTypes().OrderByDescending(x => x.ID).FirstOrDefault();
            Assert.AreEqual("Direct Debit", newPaymentType.PaymentTypeName);
        }

        [TestMethod]
        public void ModifyPaymentTypeTest()
        {
            PaymentType paymentType = quoteService.GetPaymentTypeById(2);
            Assert.AreEqual("Paypal", paymentType.PaymentTypeName);

            PaymentType modifiedPaymentType = new PaymentType
            {
                ID = 2,
                PaymentTypeName = "Bongo"
            };

            Assert.AreEqual(true, quoteService.ModifyPaymentType(modifiedPaymentType));
            PaymentType PaymentTypeAfterMod = quoteService.GetPaymentTypeById(2);
            Assert.AreEqual("Bongo", PaymentTypeAfterMod.PaymentTypeName);
        }

        //[TestMethod]
        //public void DeletePaymentTypeByIdTest()
        //{
        //    Assert.AreEqual(2, quoteService.GetPaymentTypeById(2).ID);
        //    quoteService.DeletePaymentTypeById(2);
        //    PaymentType paymentType = quoteService.GetPaymentTypeById(2);
        //    Assert.AreEqual(null, paymentType);
        //}

        #endregion

        #region QuoteStatusTests

        [TestMethod]
        public void GetAllQuoteStatusesTest()
        {
            IEnumerable<QuoteStatus> quoteStatuses = quoteService.GetAllQuoteStatuses();

            var quoteStatus = quoteStatuses.Select(x => x.QuoteStatusName).ElementAt(0);
            Assert.IsTrue(quoteStatuses.Count() == 9);
            Assert.AreEqual("New", quoteStatus);
        }

        [TestMethod]
        public void GetQuoteStatusByIdTest()
        {
            QuoteStatus quoteStatus = quoteService.GetQuoteStatusById(2);
            Assert.AreEqual("Waiting For Delivery", quoteStatus.QuoteStatusName);
        }

        [TestMethod]
        public void CreateQuoteStatusTest()
        {
            QuoteStatus quoteStatus = new QuoteStatus
            {
                QuoteStatusName = "In Progress"
            };

            quoteService.CreateQuoteStatus(quoteStatus);
            QuoteStatus newQuoteStatus = quoteService.GetAllQuoteStatuses().OrderByDescending(x => x.ID).FirstOrDefault();
            Assert.AreEqual("In Progress", newQuoteStatus.QuoteStatusName);
        }

        [TestMethod]
        public void ModifyQuoteStatusTest()
        {
            QuoteStatus quoteStatus = quoteService.GetQuoteStatusById(2);
            Assert.AreEqual("Waiting For Delivery", quoteStatus.QuoteStatusName);

            QuoteStatus newQuoteStatus = new QuoteStatus
            {
                ID = 2,
                QuoteStatusName = "Changed"
            };

            Assert.AreEqual(true, quoteService.ModifyQuoteStatus(newQuoteStatus));
            QuoteStatus modifiedQuoteService = quoteService.GetQuoteStatusById(2);
            Assert.AreEqual("Changed", modifiedQuoteService.QuoteStatusName);
        }

        //[TestMethod]
        //public void DeleteQuoteStatusByIdTest()
        //{
        //    Assert.AreEqual(2, quoteService.GetQuoteStatusById(2).ID);
        //    quoteService.DeleteQuoteStatusById(2);
        //    QuoteStatus quoteStatus = quoteService.GetQuoteStatusById(2);
        //    Assert.AreEqual(null, quoteStatus);
        //}

        #endregion

        #region QuoteTests

        [TestMethod]
        public void GetAllQuotesTest()
        {
            IEnumerable<Quote> quotes = quoteService.GetAllQuotes();

            var quote = quotes.Select(x => x.ID).ElementAt(1);
            Assert.IsTrue(quotes.Count() == 5);
            Assert.AreEqual(3, quote);
        }

        [TestMethod]
        public void GetQuoteByIdTest()
        {
            Quote quote = quoteService.GetQuoteById(2);
            Assert.AreEqual("Bob", quote.Customer.FirstName);
            Assert.AreEqual("Johnson", quote.Customer.LastName);
        }

        [TestMethod]
        public void GetQuoteByIdZeroTest()
        {
            Quote quote = quoteService.GetQuoteById(0);
            Assert.AreEqual(null, quote);
        }

        [TestMethod]
        public void AddPhoneToQuoteTest()
        {
            string key = "asd";

            Quote quote = quoteService.AddPhoneToQuote(key, "5", "1");

            Assert.AreEqual(4, quote.Phones.Count);
            Assert.AreEqual("New", quote.QuoteStatus.QuoteStatusName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "One or more Parameters are null")]
        public void AddPhoneToQuoteNullParametersTest()
        {
            quoteService.AddPhoneToQuote(null, null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "One or more Parameters are null")]
        public void AddPhoneToQuoteEmptyParametersTest()
        {
            quoteService.AddPhoneToQuote("", "", "");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
            "Quote does not exist")]
        public void AddPhoneToQuoteNonExistentKeyTest()
        {
            Quote addPhoneResult = quoteService.AddPhoneToQuote("thiskeydoesntexist", "1", "3");
            Assert.AreEqual(null, addPhoneResult);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
            "PhoneConditionPrice does not exist")]
        public void AddPhoneToQuoteNonExistentModelTest()
        {
            quoteService.AddPhoneToQuote("asd", "100000", "3");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
            "Quote is not of status 'New'")]
        public void AddPhoneToQuoteNonNewQuoteTest()
        {
            quoteService.AddPhoneToQuote("zxc", "100000", "3");
        }

        [TestMethod]
        public void GetQuoteByReferenceIdTest()
        {
            Quote quote = quoteService.GetQuoteByReferenceId("asd");

            Assert.AreEqual(4, quote.CustomerId);
            Assert.AreEqual(1, quote.QuoteStatusId);
            Assert.AreEqual("Jack", quote.Customer.FirstName);
            Assert.AreEqual(3, quote.Phones.First().PhoneMakeId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Parameter cannot be null")]
        public void GetQuoteByReferenceIdWithNoParamTest()
        {
            Quote quote = quoteService.GetQuoteByReferenceId(null);
        }

        [TestMethod]
        public void DeletePhoneFromQuoteTest()
        {
            string key = "asd";
            Quote quote = quoteService.GetQuoteByReferenceId(key);

            Phone beforeDelete = phoneService.GetPhoneById(16);
            Assert.IsNotNull(beforeDelete);

            quoteService.DeletePhoneFromQuote(key, "16");

            Phone phone = phoneService.GetPhoneById(16);
            Assert.AreEqual(null, phone);

            Phone otherPhone = phoneService.GetPhoneById(17);
            Assert.IsNotNull(otherPhone);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Parameter(s) cannot be null")]
        public void DeletePhoneFromQuoteWithNoParamTest()
        {
            quoteService.DeletePhoneFromQuote(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Parameter(s) cannot be null")]
        public void DeletePhoneFromQuoteWithNullParamTest()
        {
            quoteService.DeletePhoneFromQuote("asd", null);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
            "500")]
        public void DeletePhoneFromQuoteWithNonNewQuoteTest()
        {
            quoteService.DeletePhoneFromQuote("zxc", "16");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
            "500")]
        public void DeletePhoneFromQuoteWithNonExistentPhoneTest()
        {
            quoteService.DeletePhoneFromQuote("asd", "16666");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Parameter(s) cannot be null")]
        public void DeletePhoneFromQuoteWithEmptyParamTest()
        {
            quoteService.DeletePhoneFromQuote("", "");
        }

        [TestMethod]
        public void CreateQuoteTest()
        {
            string quoteId = quoteService.CreateQuote();
            Quote newQuote = quoteService.GetAllQuotes().OrderByDescending(x => x.ID).FirstOrDefault();
            Assert.AreEqual(quoteId, newQuote.QuoteReferenceId);
            Assert.AreEqual(1, newQuote.QuoteStatusId);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void CreateQuoteWithNullUnitOfWorkTest()
        {
            quoteService = new QuoteService(null, null, null);
            quoteService.CreateQuote();
        }

        [TestMethod]
        public void SaveQuoteTest()
        {
            SaveQuoteViewModel viewModel = new SaveQuoteViewModel()
            {
                PostageMethodId = 1,
                AgreedToTerms = false,
                Customer = new Customer()
                {
                    FullName = "TestFirstName TestLastName",
                    Email = "test@test.com",
                    PhoneNumber = "0449651096",
                    Address = new Address()
                    {
                        AddressLine1 = "TestAddressLine1",
                        AddressLine2 = "TestAddressLine2",
                        CountryId = 1,
                        PostCode = "3000",
                        StateId = 1
                    },
                    PaymentDetail = new PaymentDetail()
                    {
                        PaymentTypeId = 1,
                        BSB = "013365",
                        AccountNumber = "123456789"
                    }
                }
            };

            Quote quote = quoteService.GetQuoteByReferenceId("asd");
            quote.PostageMethodId = viewModel.PostageMethodId;
            quote.AgreedToTerms = viewModel.AgreedToTerms;
            quote.Customer = viewModel.Customer;

            Quote result = quoteService.SaveQuote(quote);
            Assert.AreEqual("asd", result.QuoteReferenceId);
            Assert.AreEqual("New", result.QuoteStatus.QuoteStatusName);
            Assert.AreEqual(false, result.AgreedToTerms);
            Assert.AreEqual("Free Satchel", result.PostageMethod.PostageMethodName);
            Assert.AreEqual(3, result.Phones.Count());
            Assert.IsNull(quote.QuoteFinalisedDate);
            Assert.AreEqual((int)QuoteStatusEnum.New, quote.QuoteStatusId);
            Assert.AreEqual("Free Satchel", quote.PostageMethod.PostageMethodName);
            Assert.AreEqual(false, quote.AgreedToTerms);
        }

        [TestMethod]
        public void SaveQuoteWithOnlyFirstNameTest()
        {
            SaveQuoteViewModel viewModel = new SaveQuoteViewModel()
            {
                PostageMethodId = 1,
                AgreedToTerms = false,
                Customer = new Customer()
                {
                    FullName = "TestFirstName",
                    Email = "test@test.com",
                    PhoneNumber = "0449651096",
                    Address = new Address()
                    {
                        AddressLine1 = "TestAddressLine1",
                        AddressLine2 = "TestAddressLine2",
                        CountryId = 1,
                        PostCode = "3000",
                        StateId = 1
                    },
                    PaymentDetail = new PaymentDetail()
                    {
                        PaymentTypeId = 1,
                        BSB = "013365",
                        AccountNumber = "123456789"
                    }
                }
            };

            Quote quote = quoteService.GetQuoteByReferenceId("asd");
            quote.PostageMethodId = viewModel.PostageMethodId;
            quote.AgreedToTerms = viewModel.AgreedToTerms;
            quote.Customer = viewModel.Customer;

            Quote result = quoteService.SaveQuote(quote);
            Assert.AreEqual("asd", result.QuoteReferenceId);
            Assert.AreEqual("New", result.QuoteStatus.QuoteStatusName);
            Assert.AreEqual(false, result.AgreedToTerms);
            Assert.AreEqual("Free Satchel", result.PostageMethod.PostageMethodName);
            Assert.AreEqual(3, result.Phones.Count());
            Assert.IsNull(quote.QuoteFinalisedDate);
            Assert.AreEqual((int)QuoteStatusEnum.New, quote.QuoteStatusId);
            Assert.AreEqual("Free Satchel", quote.PostageMethod.PostageMethodName);
            Assert.AreEqual(false, quote.AgreedToTerms);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "quoteToSave parameter is null/empty")]
        public void SaveQuoteWithNullKeyTest()
        {
            quoteService.SaveQuote(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "quoteToSave parameter is null/empty")]
        public void SaveQuoteWithNonExistingQuoteTest()
        {
            quoteService.SaveQuote(new Quote());
        }

        [TestMethod]
        public void FinaliseQuoteWithSelfPostTest()
        {
            SaveQuoteViewModel viewModel = new SaveQuoteViewModel()
            {
                PostageMethodId = 2,
                AgreedToTerms = true,
                Customer = new Customer()
                {
                    FullName = "TestFirstName TestLastName",
                    Email = "test@test.com",
                    PhoneNumber = "0449651096",
                    Address = new Address()
                    {
                        AddressLine1 = "TestAddressLine1",
                        AddressLine2 = "TestAddressLine2",
                        CountryId = 1,
                        PostCode = "2000",
                        StateId = 2
                    },
                    PaymentDetail = new PaymentDetail()
                    {
                        PaymentTypeId = 1,
                        BSB = "013365",
                        AccountNumber = "123456789"
                    }
                }
            };

            Quote quote = quoteService.GetQuoteByReferenceId("asd");
            quote.PostageMethodId = viewModel.PostageMethodId;
            quote.AgreedToTerms = viewModel.AgreedToTerms;
            quote.Customer = viewModel.Customer;

            Quote result = quoteService.FinaliseQuote(quote);
            Assert.AreEqual("asd", result.QuoteReferenceId);
            Assert.AreEqual("Waiting For Delivery", result.QuoteStatus.QuoteStatusName);
            Assert.AreEqual(true, result.AgreedToTerms);
            Assert.AreEqual("Post Yourself", result.PostageMethod.PostageMethodName);
            Assert.AreEqual(3, result.Phones.Count());
            Assert.IsNotNull(quote.QuoteFinalisedDate);
            Assert.AreEqual((int)QuoteStatusEnum.WaitingForDelivery, quote.QuoteStatusId);
            Assert.AreEqual("Post Yourself", quote.PostageMethod.PostageMethodName);
            Assert.AreEqual(true, quote.AgreedToTerms);
        }

        [TestMethod]
        public void FinaliseQuoteWithSatchelTest()
        {
            SaveQuoteViewModel viewModel = new SaveQuoteViewModel()
            {
                PostageMethodId = 1,
                AgreedToTerms = true,
                Customer = new Customer()
                {
                    FullName = "TestFirstName TestLastName",
                    Email = "test@test.com",
                    PhoneNumber = "0449651096",
                    Address = new Address()
                    {
                        AddressLine1 = "TestAddressLine1",
                        AddressLine2 = "TestAddressLine2",
                        CountryId = 1,
                        PostCode = "2000",
                        StateId = 2
                    },
                    PaymentDetail = new PaymentDetail()
                    {
                        PaymentTypeId = 1,
                        BSB = "013365",
                        AccountNumber = "123456789"
                    }
                }
            };

            Quote quote = quoteService.GetQuoteByReferenceId("asd");
            quote.PostageMethodId = viewModel.PostageMethodId;
            quote.AgreedToTerms = viewModel.AgreedToTerms;
            quote.Customer = viewModel.Customer;

            Quote result = quoteService.FinaliseQuote(quote);
            Assert.AreEqual("asd", result.QuoteReferenceId);
            Assert.AreEqual("Requires Satchel", result.QuoteStatus.QuoteStatusName);
            Assert.AreEqual(true, result.AgreedToTerms);
            Assert.AreEqual("Free Satchel", result.PostageMethod.PostageMethodName);
            Assert.AreEqual(3, result.Phones.Count());
            Assert.IsNotNull(quote.QuoteFinalisedDate);
            Assert.AreEqual((int)QuoteStatusEnum.RequiresSatchel, quote.QuoteStatusId);
            Assert.AreEqual(true, quote.AgreedToTerms);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "quoteToSave parameter is null/empty")]
        public void FinaliseQuoteWithNoKeyTest()
        {
            quoteService.FinaliseQuote(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "quoteToSave parameter is null/empty")]
        public void FinaliseQuoteWithNonExistingQuoteTest()
        {
             quoteService.FinaliseQuote(new Quote());
        }

        [TestMethod]
        public void ModifyQuoteTest()
        {
            Quote quote = quoteService.GetQuoteById(2);
            
            quote.Customer.Address.AddressLine2 = "Brunswick";
            quote.Customer.Email = "ntrous@gmail.com";
            quote.Customer.PaymentDetail.BSB = "444444";
            quote.QuoteExpiryDate = DateTime.Parse("02/07/2015", new CultureInfo("en-AU"));
            quote.Notes = "This is a test note!";

            Phone phone = quote.Phones.First();
            phone.PurchaseAmount = 10000;
            phone.PhoneConditionId = 2;

            Assert.IsNotNull(quoteService.ModifyQuote(quote, User.SystemUser.Value));
            Quote modifiedQuote = quoteService.GetQuoteById(2);

            Assert.AreEqual("Brunswick", modifiedQuote.Customer.Address.AddressLine2);
            Assert.AreEqual("ntrous@gmail.com", modifiedQuote.Customer.Email);
            Assert.AreEqual("444444", modifiedQuote.Customer.PaymentDetail.BSB);
            Assert.AreEqual(DateTime.Parse("02/07/2015", new CultureInfo("en-AU")), modifiedQuote.QuoteExpiryDate);
            Assert.AreEqual("This is a test note!", modifiedQuote.Notes);
            Assert.AreEqual(10000, modifiedQuote.Phones.First().PurchaseAmount);
            Assert.AreEqual(2, modifiedQuote.Phones.First().PhoneConditionId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Parameter cannot be null")]
        public void ModifyQuoteWithNoParamsTest()
        {
            quoteService.ModifyQuote(null, null);
        }

        [TestMethod]
        public void SearchQuotesTest()
        {
            List<Quote> quotes = quoteService.SearchQuotes(null, null, null, 0);
            Assert.AreEqual(5, quotes.Count);
        }

        [TestMethod]
        public void SearchQuotesEmptyParamTest()
        {
            List<Quote> quotes = quoteService.SearchQuotes("", "", "", 0);
            Assert.AreEqual(5, quotes.Count);
        }

        [TestMethod]
        public void SearchQuotesTestByReferenceId()
        {
            List<Quote> quotes = quoteService.SearchQuotes("asd", null, null, 0);
            Assert.AreEqual(1, quotes.Count);
            Assert.AreEqual("Jack", quotes[0].Customer.FirstName);
        }

        [TestMethod]
        public void SearchQuotesTestByEmail()
        {
            List<Quote> quotes = quoteService.SearchQuotes(null, "Jim@johnson.com", null, 0);
            Assert.AreEqual(1, quotes.Count);
            Assert.AreEqual("Jim", quotes[0].Customer.FirstName);
        }

        [TestMethod]
        public void SearchQuotesTestByLastname()
        {
            List<Quote> quotes = quoteService.SearchQuotes(null, null, "Beam", 0);
            Assert.AreEqual(1, quotes.Count);
            Assert.AreEqual("Jim", quotes[0].Customer.FirstName);
        }

        [TestMethod]
        public void SearchQuotesTestByFirstname()
        {
            List<Quote> quotes = quoteService.SearchQuotes(null, null, "Jim", 0);
            Assert.AreEqual(1, quotes.Count);
            Assert.AreEqual("Beam", quotes[0].Customer.LastName);
        }

        [TestMethod]
        public void GetSortedQuotesTest()
        {
            var quotes = quoteService.GetAllQuotes().ToList();
            quotes = quoteService.GetSortedQuotes(quotes, null);

            Assert.AreEqual("Waiting For Delivery", quotes.ElementAt(2).QuoteStatus.QuoteStatusName);
            Assert.AreEqual("Waiting For Delivery", quotes.ElementAt(1).QuoteStatus.QuoteStatusName);
            Assert.AreEqual("Waiting For Delivery", quotes.ElementAt(0).QuoteStatus.QuoteStatusName);
        }

        [TestMethod]
        public void GetSortedQuotesByNameTest()
        {
            var quotes = quoteService.GetAllQuotes().ToList();
            quotes = quoteService.GetSortedQuotes(quotes, "name_asc");

            Assert.AreEqual("Beam", quotes.ElementAt(0).Customer.LastName);
            Assert.AreEqual("Buyer", quotes.ElementAt(1).Customer.LastName);
            Assert.AreEqual("Daniels", quotes.ElementAt(2).Customer.LastName);
        }

        [TestMethod]
        public void GetSortedQuotesByNameDescTest()
        {
            var quotes = quoteService.GetAllQuotes().ToList();
            quotes = quoteService.GetSortedQuotes(quotes, "name_desc");

            Assert.AreEqual("Daniels", quotes.ElementAt(2).Customer.LastName);
            Assert.AreEqual("Johnson", quotes.ElementAt(1).Customer.LastName);
            Assert.AreEqual("Seller", quotes.ElementAt(0).Customer.LastName);
        }

        [TestMethod]
        public void GetSortedQuotesByStatusDescTest()
        { 
            var quotes = quoteService.GetAllQuotes().ToList();
            quotes = quoteService.GetSortedQuotes(quotes, "status_desc");

            Assert.AreEqual("Waiting For Delivery", quotes.ElementAt(2).QuoteStatus.QuoteStatusName);
            Assert.AreEqual("Waiting For Delivery", quotes.ElementAt(1).QuoteStatus.QuoteStatusName);
            Assert.AreEqual("Waiting For Delivery", quotes.ElementAt(0).QuoteStatus.QuoteStatusName);
        }

        [TestMethod]
        public void GetSortedQuotesByStatusTest()
        {
            var quotes = quoteService.GetAllQuotes().ToList();
            quotes = quoteService.GetSortedQuotes(quotes, "status_asc");

            Assert.AreEqual("New", quotes.ElementAt(0).QuoteStatus.QuoteStatusName);
            Assert.AreEqual("New", quotes.ElementAt(1).QuoteStatus.QuoteStatusName);
            Assert.AreEqual("Waiting For Delivery", quotes.ElementAt(2).QuoteStatus.QuoteStatusName);
        }

        [TestMethod]
        public void GetSortedQuotesByEmailTest()
        {
            var quotes = quoteService.GetAllQuotes().ToList();
            quotes = quoteService.GetSortedQuotes(quotes, "email_asc");

            Assert.AreEqual("bob@johnson.com", quotes.ElementAt(0).Customer.Email);
            Assert.AreEqual("i@b.com", quotes.ElementAt(1).Customer.Email);
            Assert.AreEqual("Jack@bourbon.com", quotes.ElementAt(2).Customer.Email);
        }

        [TestMethod]
        public void GetSortedQuotesByEmailDescTest()
        {
            var quotes = quoteService.GetAllQuotes().ToList();
            quotes = quoteService.GetSortedQuotes(quotes, "email_desc");

            Assert.AreEqual("Jack@bourbon.com", quotes.ElementAt(2).Customer.Email);
            Assert.AreEqual("Jim@johnson.com", quotes.ElementAt(1).Customer.Email);
            Assert.AreEqual("sam@sung.com", quotes.ElementAt(0).Customer.Email);
        }

        [TestMethod]
        public void GetSortedQuotesByCreatedDateTest()
        {
            var quotes = quoteService.GetAllQuotes().ToList();
            quotes = quoteService.GetSortedQuotes(quotes, "created_date_asc");

            Assert.AreEqual(Convert.ToDateTime("13/10/2015", new CultureInfo("en-AU")), quotes.ElementAt(0).CreatedDate);
            Assert.AreEqual(Convert.ToDateTime("14/10/2015", new CultureInfo("en-AU")), quotes.ElementAt(1).CreatedDate);
            Assert.AreEqual(Convert.ToDateTime("17/10/2015", new CultureInfo("en-AU")), quotes.ElementAt(2).CreatedDate);
        }

        [TestMethod]
        public void GetSortedQuotesByCreatedDateDescTest()
        {
            var quotes = quoteService.GetAllQuotes().ToList();
            quotes = quoteService.GetSortedQuotes(quotes, "created_date_desc");

            Assert.AreEqual(Convert.ToDateTime("17/10/2015", new CultureInfo("en-AU")), quotes.ElementAt(2).CreatedDate);
            Assert.AreEqual(Convert.ToDateTime("01/11/2015", new CultureInfo("en-AU")), quotes.ElementAt(1).CreatedDate);
            Assert.AreEqual(Convert.ToDateTime("01/11/2015", new CultureInfo("en-AU")), quotes.ElementAt(0).CreatedDate);
        }

        [TestMethod]
        public void GetSortedQuotesByQuoteFinalisedDateTest()
        {
            var quotes = quoteService.GetAllQuotes().ToList();
            quotes = quoteService.GetSortedQuotes(quotes, "quote_finalised_date_asc");

            Assert.AreEqual(null, quotes.ElementAt(0).QuoteFinalisedDate);
            Assert.AreEqual(null, quotes.ElementAt(1).QuoteFinalisedDate);
            Assert.AreEqual(Convert.ToDateTime("17/10/2015", new CultureInfo("en-AU")), quotes.ElementAt(2).QuoteFinalisedDate);
        }

        [TestMethod]
        public void GetSortedQuotesByQuoteFinalisedDateDescTest()
        {
            var quotes = quoteService.GetAllQuotes().ToList();
            quotes = quoteService.GetSortedQuotes(quotes, "");

            Assert.AreEqual(Convert.ToDateTime("01/11/2015", new CultureInfo("en-AU")), quotes.ElementAt(0).QuoteFinalisedDate);
            Assert.AreEqual(Convert.ToDateTime("01/11/2015", new CultureInfo("en-AU")), quotes.ElementAt(1).QuoteFinalisedDate);
            Assert.AreEqual(Convert.ToDateTime("17/10/2015", new CultureInfo("en-AU")), quotes.ElementAt(2).QuoteFinalisedDate);
        }

        [TestMethod]
        public void RevalidatePricesWithHigherAmountTest()
        {
            var quote = quoteService.GetQuoteByReferenceId("asd");
            quote.Phones.ElementAt(0).PurchaseAmount = 1000;
            quote = quoteService.ReValidatePhonePrices(quote);

            Assert.AreEqual(120, quote.Phones.ElementAt(0).PurchaseAmount);
            Assert.AreEqual(100, quote.Phones.ElementAt(1).PurchaseAmount);
            Assert.AreEqual(100, quote.Phones.ElementAt(2).PurchaseAmount);
        }

        [TestMethod]
        public void RevalidatePricesWithLowerAmountTest()
        {
            var quote = quoteService.GetQuoteByReferenceId("asd");
            quote.Phones.ElementAt(0).PurchaseAmount = 1;
            quote = quoteService.ReValidatePhonePrices(quote);

            Assert.AreEqual(1, quote.Phones.ElementAt(0).PurchaseAmount);
            Assert.AreEqual(100, quote.Phones.ElementAt(1).PurchaseAmount);
            Assert.AreEqual(100, quote.Phones.ElementAt(2).PurchaseAmount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Quote cannot be null")]
        public void RevalidatePricesWithnullParamTest()
        {
            quoteService.ReValidatePhonePrices(null);
        }

        [TestMethod]
        public void RevalidatePricesWithNonNewQuoteTest()
        {
            var quote = quoteService.GetQuoteByReferenceId("zxc");
            quote.Phones.ElementAt(0).PurchaseAmount = 1;
            quote = quoteService.ReValidatePhonePrices(quote);

            Assert.AreEqual(1, quote.Phones.ElementAt(0).PurchaseAmount);
            Assert.AreEqual(120, quote.Phones.ElementAt(1).PurchaseAmount);
            Assert.AreEqual(180, quote.Phones.ElementAt(2).PurchaseAmount);
        }

        #endregion

        #region Address

        [TestMethod]
        public void GetAllStatesTest()
        {
            IEnumerable<State> states = quoteService.GetAllStates();

            var state = states.ElementAt(0);
            Assert.IsTrue(states.Count() == 6);
            Assert.AreEqual("Victoria", state.StateNameLong);
            Assert.AreEqual("VIC", state.StateNameShort);
        }

        [TestMethod]
        public void GetAllStateNamesTest()
        {
            List<string> states = quoteService.GetAllStateNames();

            Assert.IsTrue(states.Count() == 6);
            Assert.AreEqual("VIC", states[0]);
            Assert.AreEqual("NSW", states[1]);
            Assert.AreEqual("WA", states[2]);
        }

        [TestMethod]
        public void GetAllCountriesTest()
        {
            IEnumerable<Country> countries = quoteService.GetAllCountries();

            var country = countries.ElementAt(0);
            Assert.IsTrue(countries.Count() == 1);
            Assert.AreEqual("Australia", country.CountryName);
        }

        #endregion

        #region PostageMethodTests

        [TestMethod]
        public void GetAllPostageMethodsTest()
        {
            var postageMethods = quoteService.GetAllPostageMethods();
            Assert.AreEqual(2, postageMethods.Count());
        }

        #endregion

        #region State

        [TestMethod]
        public void GetStateByIdTest()
        {
            var state = quoteService.GetStateById(1);
            Assert.AreEqual("VIC", state.StateNameShort);
        }

        #endregion
    }
}

