using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradeYourPhone.Core.Models;
using System.Collections.Generic;
using TradeYourPhone.Core.Services.Interface;
using System.Linq;
using TradeYourPhone.Core.ViewModels;
using TradeYourPhone.Test.SetupData;
using TradeYourPhone.Core.Enums;
using TradeYourPhone.Core.Models.DomainModels;
using System.Transactions;

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
            phoneService = cmd.GetPhoneService();
            quoteService = cmd.GetQuoteService();
        }

        [TestInitialize()]
        public void Init()
        {
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

            QuoteDetailsResult addPhoneResult = quoteService.AddPhoneToQuote(key, "5", "1");
            Quote quote = quoteService.GetQuoteByReferenceId(key);
            Phone newPhone = quote.Phones.OrderByDescending(x => x.Id).FirstOrDefault();

            Assert.AreEqual("OK", addPhoneResult.Status);
            Assert.AreEqual(4, addPhoneResult.QuoteDetails.Phones.Count);
            Assert.AreEqual(610, addPhoneResult.QuoteDetails.TotalAmount);
            Assert.AreEqual("New", addPhoneResult.QuoteDetails.QuoteStatus);
            Assert.AreEqual(3, newPhone.PhoneMakeId);
            Assert.AreEqual(5, newPhone.PhoneModelId);
            Assert.AreEqual(1, newPhone.PhoneConditionId);
        }

        [TestMethod]
        public void AddPhoneToQuoteNullParametersTest()
        {
            QuoteDetailsResult addPhoneResult = quoteService.AddPhoneToQuote(null, null, null);
            Assert.AreEqual("Error", addPhoneResult.Status);
            Assert.AreEqual("One or more Parameters are null", addPhoneResult.Exception.Message);
            Assert.AreEqual(null, addPhoneResult.QuoteDetails);
        }

        [TestMethod]
        public void AddPhoneToQuoteEmptyParametersTest()
        {
            QuoteDetailsResult addPhoneResult = quoteService.AddPhoneToQuote("", "", "");
            Assert.AreEqual("Error", addPhoneResult.Status);
            Assert.AreEqual("One or more Parameters are null", addPhoneResult.Exception.Message);
            Assert.AreEqual(null, addPhoneResult.QuoteDetails);
        }

        [TestMethod]
        public void AddPhoneToQuoteNonExistentKeyTest()
        {
            QuoteDetailsResult addPhoneResult = quoteService.AddPhoneToQuote("thiskeydoesntexist", "1", "3");
            Assert.AreEqual("Error", addPhoneResult.Status);
            Assert.AreEqual("500", addPhoneResult.Exception.Message);
            Assert.AreEqual("Quote does not exist", addPhoneResult.Exception.InnerMessage);
            Assert.AreEqual(null, addPhoneResult.QuoteDetails);
        }

        [TestMethod]
        public void AddPhoneToQuoteNonExistentModelTest()
        {
            QuoteDetailsResult addPhoneResult = quoteService.AddPhoneToQuote("asd", "100000", "3");
            Assert.AreEqual("Error", addPhoneResult.Status);
            Assert.AreEqual("Model and/or Condition do not exist", addPhoneResult.Exception.Message);
            Assert.AreEqual(null, addPhoneResult.QuoteDetails);
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
        public void GetQuoteDetailsByQuoteReferenceIdTest()
        {
            QuoteDetailsResult getDetailsResult = quoteService.GetQuoteDetailsByQuoteReferenceId("asd");
            Assert.AreEqual("OK", getDetailsResult.Status);
            Assert.AreEqual(null, getDetailsResult.Exception);
            Assert.AreEqual(3, getDetailsResult.QuoteDetails.Phones.Count);
            Assert.AreEqual("Apple", getDetailsResult.QuoteDetails.Phones[0].PhoneMakeName);
            Assert.AreEqual("Galaxy S4", getDetailsResult.QuoteDetails.Phones[0].PhoneModelName);
            Assert.AreEqual("Good", getDetailsResult.QuoteDetails.Phones[0].PhoneCondition);
            Assert.AreEqual("Jack Daniels", getDetailsResult.QuoteDetails.Customer.fullname);
            Assert.AreEqual("3000", getDetailsResult.QuoteDetails.Customer.postagePostcode);
        }

        [TestMethod]
        public void GetQuoteDetailsByQuoteReferenceIdNoKeyTest()
        {
            QuoteDetailsResult getDetailsResult = quoteService.GetQuoteDetailsByQuoteReferenceId("");
            Assert.AreEqual("Error", getDetailsResult.Status);
            Assert.AreEqual("key parameter is empty", getDetailsResult.Exception.Message);
            Assert.AreEqual(null, getDetailsResult.QuoteDetails);
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
                        State = new State() { StateNameShort = "VIC" }
                    },
                    PaymentDetail = new PaymentDetail()
                    {
                        PaymentType = new PaymentType() { PaymentTypeName = "Bank Transfer" },
                        BSB = "013365",
                        AccountNumber = "123456789"
                    }
                }
            };

            QuoteDetailsResult result = quoteService.SaveQuote("asd", viewModel);
            Assert.AreEqual("OK", result.Status);
            Assert.AreEqual(null, result.Exception);
            Assert.AreEqual("asd", result.QuoteDetails.QuoteReferenceId);
            Assert.AreEqual("New", result.QuoteDetails.QuoteStatus);
            Assert.AreEqual(false, result.QuoteDetails.AgreedToTerms);
            Assert.AreEqual("Free Satchel", result.QuoteDetails.PostageMethod.PostageMethodName);
            Assert.AreEqual(510, result.QuoteDetails.TotalAmount);
            Assert.AreEqual(3, result.QuoteDetails.Phones.Count());

            Quote quote = quoteService.GetQuoteByReferenceId("asd");
            Assert.IsNull(quote.QuoteFinalisedDate);
            Assert.AreEqual((int)QuoteStatusEnum.New, quote.QuoteStatusId);
            Assert.AreEqual("Free Satchel", quote.PostageMethod.PostageMethodName);
            Assert.AreEqual(false, quote.AgreedToTerms);
        }

        [TestMethod]
        public void SaveQuoteWithNullKeyTest()
        {
            QuoteDetailsResult result = quoteService.SaveQuote(null, null);

            Assert.AreEqual("Error", result.Status);
            Assert.AreEqual("One or more Parameters are null", result.Exception.Message);
        }

        [TestMethod]
        public void SaveQuoteWithEmptyKeyTest()
        {
            QuoteDetailsResult result = quoteService.SaveQuote("", null);

            Assert.AreEqual("Error", result.Status);
            Assert.AreEqual("One or more Parameters are null", result.Exception.Message);
        }

        [TestMethod]
        public void SaveQuoteWithNonExistingQuoteTest()
        {
            QuoteDetailsResult result = quoteService.SaveQuote("doesntexist", new SaveQuoteViewModel());

            Assert.AreEqual("Error", result.Status);
            Assert.AreEqual("500", result.Exception.Message);
            Assert.AreEqual("Quote does not exist", result.Exception.InnerMessage);
        }

        [TestMethod]
        public void FinaliseQuoteTest()
        {
            SaveQuoteViewModel viewModel = new SaveQuoteViewModel()
            {
                PostageMethodId = 2,
                AgreedToTerms = true,
                Customer = new Customer()
                {
                    FirstName = "TestFirstName",
                    LastName = "TestLastName",
                    Email = "test@test.com",
                    PhoneNumber = "0449651096",
                    Address = new Address()
                    {
                        AddressLine1 = "TestAddressLine1",
                        AddressLine2 = "TestAddressLine2",
                        CountryId = 1,
                        PostCode = "2000",
                        State = new State() { StateNameShort = "NSW" }
                    },
                    PaymentDetail = new PaymentDetail()
                    {
                        PaymentType = new PaymentType() { PaymentTypeName = "Bank Transfer" },
                        BSB = "013365",
                        AccountNumber = "123456789"
                    }
                }
            };

            QuoteDetailsResult result = quoteService.FinaliseQuote("asd", viewModel);
            Assert.AreEqual("OK", result.Status);
            Assert.AreEqual(null, result.Exception);
            Assert.AreEqual("asd", result.QuoteDetails.QuoteReferenceId);
            Assert.AreEqual("Waiting For Delivery", result.QuoteDetails.QuoteStatus);
            Assert.AreEqual(true, result.QuoteDetails.AgreedToTerms);
            Assert.AreEqual("Post Yourself", result.QuoteDetails.PostageMethod.PostageMethodName);
            Assert.AreEqual(510, result.QuoteDetails.TotalAmount);
            Assert.AreEqual(3, result.QuoteDetails.Phones.Count());

            Quote quote = quoteService.GetQuoteByReferenceId("asd");
            Assert.IsNotNull(quote.QuoteFinalisedDate);
            Assert.AreEqual((int)QuoteStatusEnum.WaitingForDelivery, quote.QuoteStatusId);
            Assert.AreEqual("Post Yourself", quote.PostageMethod.PostageMethodName);
            Assert.AreEqual(true, quote.AgreedToTerms);
        }

        [TestMethod]
        public void FinaliseQuoteWithNoKeyTest()
        {
            QuoteDetailsResult result = quoteService.FinaliseQuote(null, null);

            Assert.AreEqual("Error", result.Status);
            Assert.AreEqual("One or more Parameters are null", result.Exception.Message);
        }

        [TestMethod]
        public void FinaliseQuoteWithNonExistingQuoteTest()
        {
            QuoteDetailsResult result = quoteService.FinaliseQuote("doesntexist", new SaveQuoteViewModel());

            Assert.AreEqual("Error", result.Status);
            Assert.AreEqual("500", result.Exception.Message);
            Assert.AreEqual("Quote does not exist", result.Exception.InnerMessage);
        }

        [TestMethod]
        public void ModifyQuoteTest()
        {
            QuoteDetailsViewModel vm = new QuoteDetailsViewModel();
            Quote quote = quoteService.GetQuoteById(2);
            
            quote.Customer.Address.AddressLine2 = "Brunswick";
            quote.Customer.Email = "ntrous@gmail.com";
            quote.Customer.PaymentDetail.BSB = "444444";
            quote.QuoteExpiryDate = DateTime.Parse("02/07/2015");
            quote.Notes = "This is a test note!";

            Phone phone = quote.Phones.First();
            phone.PurchaseAmount = 10000;
            phone.PhoneConditionId = 2;

            vm.quote = quote;
            vm.UserId = User.SystemUser.Value;
            Assert.AreEqual(true, quoteService.ModifyQuote(vm));
            Quote modifiedQuote = quoteService.GetQuoteById(2);

            Assert.AreEqual("Brunswick", modifiedQuote.Customer.Address.AddressLine2);
            Assert.AreEqual("ntrous@gmail.com", modifiedQuote.Customer.Email);
            Assert.AreEqual("444444", modifiedQuote.Customer.PaymentDetail.BSB);
            Assert.AreEqual(DateTime.Parse("02/07/2015"), modifiedQuote.QuoteExpiryDate);
            Assert.AreEqual("This is a test note!", modifiedQuote.Notes);
            Assert.AreEqual(10000, modifiedQuote.Phones.First().PurchaseAmount);
            Assert.AreEqual(2, modifiedQuote.Phones.First().PhoneConditionId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Parameter cannot be null")]
        public void ModifyQuoteWithNoParamTest()
        {
            quoteService.ModifyQuote(null);
        }

        //[TestMethod]
        //public void DeleteQuoteByIdTest()
        //{
        //    Assert.AreEqual(2, quoteService.GetQuoteById(2).ID);
        //    quoteService.DeleteQuoteById(2);
        //    Quote quote = quoteService.GetQuoteById(2);
        //    Assert.AreEqual(null, quote);
        //}

        //[TestMethod]
        //public void DeleteQuoteByIdWithZeroParamTest()
        //{
        //    bool result = quoteService.DeleteQuoteById(0);
        //    Assert.AreEqual(false, result);
        //}

        [TestMethod]
        public void SearchQuotesTest()
        {
            List<Quote> quotes = quoteService.SearchQuotes(null, null, null, null, 0);
            Assert.AreEqual(5, quotes.Count);
        }

        [TestMethod]
        public void SearchQuotesEmptyParamTest()
        {
            List<Quote> quotes = quoteService.SearchQuotes("", "", "", "", 0);
            Assert.AreEqual(5, quotes.Count);
        }

        [TestMethod]
        public void SearchQuotesTestByReferenceId()
        {
            List<Quote> quotes = quoteService.SearchQuotes("asd", null, null, null, 0);
            Assert.AreEqual(1, quotes.Count);
            Assert.AreEqual("Jack", quotes[0].Customer.FirstName);
        }

        [TestMethod]
        public void SearchQuotesTestByEmail()
        {
            List<Quote> quotes = quoteService.SearchQuotes(null, "Jim@johnson.com", null, null, 0);
            Assert.AreEqual(1, quotes.Count);
            Assert.AreEqual("Jim", quotes[0].Customer.FirstName);
        }

        [TestMethod]
        public void SearchQuotesTestByLastname()
        {
            List<Quote> quotes = quoteService.SearchQuotes(null, null, "Beam", null, 0);
            Assert.AreEqual(1, quotes.Count);
            Assert.AreEqual("Jim", quotes[0].Customer.FirstName);
        }

        [TestMethod]
        public void SearchQuotesTestByFirstname()
        {
            List<Quote> quotes = quoteService.SearchQuotes(null, null, null, "Jim", 0);
            Assert.AreEqual(1, quotes.Count);
            Assert.AreEqual("Beam", quotes[0].Customer.LastName);
        }

        [TestMethod]
        public void GetSortedQuotesTest()
        {
            QuoteIndexViewModel viewModel = new QuoteIndexViewModel
            {
                sortOrder = null
            };
            var quotes = quoteService.GetAllQuotes().ToList();
            quotes = quoteService.GetSortedQuotes(quotes, viewModel);

            Assert.AreEqual("Waiting For Delivery", quotes.ElementAt(2).QuoteStatus.QuoteStatusName);
            Assert.AreEqual("Waiting For Delivery", quotes.ElementAt(1).QuoteStatus.QuoteStatusName);
            Assert.AreEqual("Waiting For Delivery", quotes.ElementAt(0).QuoteStatus.QuoteStatusName);
            Assert.AreEqual("status_asc", viewModel.StatusSortParm);
            Assert.AreEqual("name_asc", viewModel.NameSortParm);
            Assert.AreEqual("email_asc", viewModel.EmailSortParm);
            Assert.AreEqual("created_date_asc", viewModel.CreatedDateSortParm);
            Assert.AreEqual("quote_finalised_date_asc", viewModel.QuoteFinalisedDateSortParm);
        }

        [TestMethod]
        public void GetSortedQuotesByNameTest()
        {
            QuoteIndexViewModel viewModel = new QuoteIndexViewModel
            {
                sortOrder = "name_asc"
            };
            var quotes = quoteService.GetAllQuotes().ToList();
            quotes = quoteService.GetSortedQuotes(quotes, viewModel);

            Assert.AreEqual("Beam", quotes.ElementAt(0).Customer.LastName);
            Assert.AreEqual("Buyer", quotes.ElementAt(1).Customer.LastName);
            Assert.AreEqual("Daniels", quotes.ElementAt(2).Customer.LastName);
            Assert.AreEqual("status_asc", viewModel.StatusSortParm);
            Assert.AreEqual("name_desc", viewModel.NameSortParm);
            Assert.AreEqual("email_asc", viewModel.EmailSortParm);
            Assert.AreEqual("created_date_asc", viewModel.CreatedDateSortParm);
            Assert.AreEqual("", viewModel.QuoteFinalisedDateSortParm);
        }

        [TestMethod]
        public void GetSortedQuotesByNameDescTest()
        {
            QuoteIndexViewModel viewModel = new QuoteIndexViewModel
            {
                sortOrder = "name_desc"
            };
            var quotes = quoteService.GetAllQuotes().ToList();
            quotes = quoteService.GetSortedQuotes(quotes, viewModel);

            Assert.AreEqual("Daniels", quotes.ElementAt(2).Customer.LastName);
            Assert.AreEqual("Johnson", quotes.ElementAt(1).Customer.LastName);
            Assert.AreEqual("Seller", quotes.ElementAt(0).Customer.LastName);
            Assert.AreEqual("status_asc", viewModel.StatusSortParm);
            Assert.AreEqual("name_asc", viewModel.NameSortParm);
            Assert.AreEqual("email_asc", viewModel.EmailSortParm);
            Assert.AreEqual("created_date_asc", viewModel.CreatedDateSortParm);
            Assert.AreEqual("", viewModel.QuoteFinalisedDateSortParm);
        }

        [TestMethod]
        public void GetSortedQuotesByStatusDescTest()
        {
            QuoteIndexViewModel viewModel = new QuoteIndexViewModel
            {
                sortOrder = "status_desc"
            };
            var quotes = quoteService.GetAllQuotes().ToList();
            quotes = quoteService.GetSortedQuotes(quotes, viewModel);

            Assert.AreEqual("Waiting For Delivery", quotes.ElementAt(2).QuoteStatus.QuoteStatusName);
            Assert.AreEqual("Waiting For Delivery", quotes.ElementAt(1).QuoteStatus.QuoteStatusName);
            Assert.AreEqual("Waiting For Delivery", quotes.ElementAt(0).QuoteStatus.QuoteStatusName);
            Assert.AreEqual("status_asc", viewModel.StatusSortParm);
            Assert.AreEqual("name_asc", viewModel.NameSortParm);
            Assert.AreEqual("email_asc", viewModel.EmailSortParm);
            Assert.AreEqual("created_date_asc", viewModel.CreatedDateSortParm);
            Assert.AreEqual("", viewModel.QuoteFinalisedDateSortParm);
        }

        [TestMethod]
        public void GetSortedQuotesByStatusTest()
        {
            QuoteIndexViewModel viewModel = new QuoteIndexViewModel
            {
                sortOrder = "status_asc"
            };
            var quotes = quoteService.GetAllQuotes().ToList();
            quotes = quoteService.GetSortedQuotes(quotes, viewModel);

            Assert.AreEqual("New", quotes.ElementAt(0).QuoteStatus.QuoteStatusName);
            Assert.AreEqual("New", quotes.ElementAt(1).QuoteStatus.QuoteStatusName);
            Assert.AreEqual("Waiting For Delivery", quotes.ElementAt(2).QuoteStatus.QuoteStatusName);
            Assert.AreEqual("status_desc", viewModel.StatusSortParm);
            Assert.AreEqual("name_asc", viewModel.NameSortParm);
            Assert.AreEqual("email_asc", viewModel.EmailSortParm);
            Assert.AreEqual("created_date_asc", viewModel.CreatedDateSortParm);
            Assert.AreEqual("", viewModel.QuoteFinalisedDateSortParm);
        }

        [TestMethod]
        public void GetSortedQuotesByEmailTest()
        {
            QuoteIndexViewModel viewModel = new QuoteIndexViewModel
            {
                sortOrder = "email_asc"
            };
            var quotes = quoteService.GetAllQuotes().ToList();
            quotes = quoteService.GetSortedQuotes(quotes, viewModel);

            Assert.AreEqual("bob@johnson.com", quotes.ElementAt(0).Customer.Email);
            Assert.AreEqual("i@b.com", quotes.ElementAt(1).Customer.Email);
            Assert.AreEqual("Jack@bourbon.com", quotes.ElementAt(2).Customer.Email);
            Assert.AreEqual("status_asc", viewModel.StatusSortParm);
            Assert.AreEqual("name_asc", viewModel.NameSortParm);
            Assert.AreEqual("email_desc", viewModel.EmailSortParm);
            Assert.AreEqual("created_date_asc", viewModel.CreatedDateSortParm);
            Assert.AreEqual("", viewModel.QuoteFinalisedDateSortParm);
        }

        [TestMethod]
        public void GetSortedQuotesByEmailDescTest()
        {
            QuoteIndexViewModel viewModel = new QuoteIndexViewModel
            {
                sortOrder = "email_desc"
            };
            var quotes = quoteService.GetAllQuotes().ToList();
            quotes = quoteService.GetSortedQuotes(quotes, viewModel);

            Assert.AreEqual("Jack@bourbon.com", quotes.ElementAt(2).Customer.Email);
            Assert.AreEqual("Jim@johnson.com", quotes.ElementAt(1).Customer.Email);
            Assert.AreEqual("sam@sung.com", quotes.ElementAt(0).Customer.Email);
            Assert.AreEqual("status_asc", viewModel.StatusSortParm);
            Assert.AreEqual("name_asc", viewModel.NameSortParm);
            Assert.AreEqual("email_asc", viewModel.EmailSortParm);
            Assert.AreEqual("created_date_asc", viewModel.CreatedDateSortParm);
            Assert.AreEqual("", viewModel.QuoteFinalisedDateSortParm);
        }

        [TestMethod]
        public void GetSortedQuotesByCreatedDateTest()
        {
            QuoteIndexViewModel viewModel = new QuoteIndexViewModel
            {
                sortOrder = "created_date_asc"
            };
            var quotes = quoteService.GetAllQuotes().ToList();
            quotes = quoteService.GetSortedQuotes(quotes, viewModel);

            Assert.AreEqual(Convert.ToDateTime("13/10/2015"), quotes.ElementAt(0).CreatedDate);
            Assert.AreEqual(Convert.ToDateTime("14/10/2015"), quotes.ElementAt(1).CreatedDate);
            Assert.AreEqual(Convert.ToDateTime("17/10/2015"), quotes.ElementAt(2).CreatedDate);
            Assert.AreEqual("status_asc", viewModel.StatusSortParm);
            Assert.AreEqual("name_asc", viewModel.NameSortParm);
            Assert.AreEqual("email_asc", viewModel.EmailSortParm);
            Assert.AreEqual("created_date_desc", viewModel.CreatedDateSortParm);
            Assert.AreEqual("", viewModel.QuoteFinalisedDateSortParm);
        }

        [TestMethod]
        public void GetSortedQuotesByCreatedDateDescTest()
        {
            QuoteIndexViewModel viewModel = new QuoteIndexViewModel
            {
                sortOrder = "created_date_desc"
            };
            var quotes = quoteService.GetAllQuotes().ToList();
            quotes = quoteService.GetSortedQuotes(quotes, viewModel);

            Assert.AreEqual(Convert.ToDateTime("17/10/2015"), quotes.ElementAt(2).CreatedDate);
            Assert.AreEqual(Convert.ToDateTime("01/11/2015"), quotes.ElementAt(1).CreatedDate);
            Assert.AreEqual(Convert.ToDateTime("01/11/2015"), quotes.ElementAt(0).CreatedDate);
            Assert.AreEqual("status_asc", viewModel.StatusSortParm);
            Assert.AreEqual("name_asc", viewModel.NameSortParm);
            Assert.AreEqual("email_asc", viewModel.EmailSortParm);
            Assert.AreEqual("created_date_asc", viewModel.CreatedDateSortParm);
            Assert.AreEqual("", viewModel.QuoteFinalisedDateSortParm);
        }

        [TestMethod]
        public void GetSortedQuotesByQuoteFinalisedDateTest()
        {
            QuoteIndexViewModel viewModel = new QuoteIndexViewModel
            {
                sortOrder = "quote_finalised_date_asc"
            };
            var quotes = quoteService.GetAllQuotes().ToList();
            quotes = quoteService.GetSortedQuotes(quotes, viewModel);

            Assert.AreEqual(null, quotes.ElementAt(0).QuoteFinalisedDate);
            Assert.AreEqual(null, quotes.ElementAt(1).QuoteFinalisedDate);
            Assert.AreEqual(Convert.ToDateTime("17/10/2015"), quotes.ElementAt(2).QuoteFinalisedDate);
            Assert.AreEqual("status_asc", viewModel.StatusSortParm);
            Assert.AreEqual("name_asc", viewModel.NameSortParm);
            Assert.AreEqual("email_asc", viewModel.EmailSortParm);
            Assert.AreEqual("created_date_asc", viewModel.CreatedDateSortParm);
            Assert.AreEqual("", viewModel.QuoteFinalisedDateSortParm);
        }

        [TestMethod]
        public void GetSortedQuotesByQuoteFinalisedDateDescTest()
        {
            QuoteIndexViewModel viewModel = new QuoteIndexViewModel
            {
                sortOrder = ""
            };
            var quotes = quoteService.GetAllQuotes().ToList();
            quotes = quoteService.GetSortedQuotes(quotes, viewModel);

            Assert.AreEqual(Convert.ToDateTime("01/11/2015"), quotes.ElementAt(0).QuoteFinalisedDate);
            Assert.AreEqual(Convert.ToDateTime("01/11/2015"), quotes.ElementAt(1).QuoteFinalisedDate);
            Assert.AreEqual(Convert.ToDateTime("17/10/2015"), quotes.ElementAt(2).QuoteFinalisedDate);
            Assert.AreEqual("status_asc", viewModel.StatusSortParm);
            Assert.AreEqual("name_asc", viewModel.NameSortParm);
            Assert.AreEqual("email_asc", viewModel.EmailSortParm);
            Assert.AreEqual("created_date_asc", viewModel.CreatedDateSortParm);
            Assert.AreEqual("quote_finalised_date_asc", viewModel.QuoteFinalisedDateSortParm);
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
    }
}

