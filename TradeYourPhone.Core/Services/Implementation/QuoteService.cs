using TradeYourPhone.Core.Enums;
using TradeYourPhone.Core.Models;
using TradeYourPhone.Core.Utilities;
using TradeYourPhone.Core.Repositories.Interface;
using TradeYourPhone.Core.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Reflection;

namespace TradeYourPhone.Core.Services.Implementation
{
    public class QuoteService : IQuoteService
    {
        private IUnitOfWork unitOfWork;
        private IPhoneService phoneService;
        private IEmailService emailService;

        public QuoteService(IUnitOfWork unitOfWork, IPhoneService phoneService, IEmailService emailService)
        {
            this.unitOfWork = unitOfWork;
            this.phoneService = phoneService;
            this.emailService = emailService;
        }

        #region PaymentTypes

        /// <summary>
        /// Returns all Payment Types
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PaymentType> GetAllPaymentTypes()
        {
            return unitOfWork.PaymentTypeRepository.Get();
        }

        /// <summary>
        /// Gets all Payment Type Names
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllPaymentTypeNames()
        {
            return unitOfWork.PaymentTypeRepository.Get().Select(x => x.PaymentTypeName).ToList();
        }

        /// <summary>
        /// Returns Payment Type by ID
        /// </summary>
        /// <param name="paymentTypeId"></param>
        /// <returns></returns>
        public PaymentType GetPaymentTypeById(int paymentTypeId)
        {
            PaymentType paymentType = unitOfWork.PaymentTypeRepository.GetByID(paymentTypeId);
            return paymentType;
        }

        /// <summary>
        /// Creates a new Payment Type
        /// </summary>
        /// <param name="paymentType"></param>
        /// <returns></returns>
        public bool CreatePaymentType(PaymentType paymentType)
        {
            if (!DoesPaymentTypeExist(paymentType.PaymentTypeName))
            {
                paymentType.PaymentTypeName = Util.UppercaseFirst(paymentType.PaymentTypeName);
                unitOfWork.PaymentTypeRepository.Insert(paymentType);
                unitOfWork.Save();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Modifies Payment Type
        /// </summary>
        /// <param name="paymentType"></param>
        /// <returns></returns>
        public bool ModifyPaymentType(PaymentType paymentType)
        {
            if (!DoesPaymentTypeExist(paymentType.PaymentTypeName))
            {
                PaymentType originalPaymentType = GetPaymentTypeById(paymentType.ID);
                originalPaymentType.PaymentTypeName = Util.UppercaseFirst(paymentType.PaymentTypeName);
                unitOfWork.PaymentTypeRepository.Update(originalPaymentType);
                unitOfWork.Save();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if PaymentType already exists
        /// </summary>
        /// <param name="paymentType"></param>
        /// <returns></returns>
        private bool DoesPaymentTypeExist(string paymentType)
        {
            IEnumerable<PaymentType> paymentTypes = GetAllPaymentTypes().Where(x => x.PaymentTypeName.ToLower() == paymentType.ToLower());
            if (paymentTypes.Any()) { return true; }
            return false;
        }

        #endregion

        #region QuoteStatus

        /// <summary>
        /// Retuns all Quote Statuses
        /// </summary>
        /// <returns></returns>
        public IEnumerable<QuoteStatus> GetAllQuoteStatuses()
        {
            return unitOfWork.QuoteStatusRepository.Get(orderBy: x => x.OrderBy(s => s.SortOrder));
        }

        /// <summary>
        /// Get Quote Status by ID
        /// </summary>
        /// <param name="quoteStatusId"></param>
        /// <returns></returns>
        public QuoteStatus GetQuoteStatusById(int quoteStatusId)
        {
            QuoteStatus quoteStatus = unitOfWork.QuoteStatusRepository.GetByID(quoteStatusId);
            return quoteStatus;
        }

        /// <summary>
        /// Creates a new Quote Status
        /// </summary>
        /// <param name="quoteStatus"></param>
        /// <returns></returns>
        public bool CreateQuoteStatus(QuoteStatus quoteStatus)
        {
            if (!DoesQuoteStatusExist(quoteStatus.QuoteStatusName))
            {
                quoteStatus.QuoteStatusName = Util.UppercaseFirst(quoteStatus.QuoteStatusName);
                unitOfWork.QuoteStatusRepository.Insert(quoteStatus);
                unitOfWork.Save();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Modify the QuoteStatus
        /// </summary>
        /// <param name="quoteStatus"></param>
        /// <returns></returns>
        public bool ModifyQuoteStatus(QuoteStatus quoteStatus)
        {
            if (!DoesQuoteStatusExist(quoteStatus.QuoteStatusName))
            {
                QuoteStatus originalQuoteStatus = GetQuoteStatusById(quoteStatus.ID);
                originalQuoteStatus.QuoteStatusName = Util.UppercaseFirst(quoteStatus.QuoteStatusName);
                unitOfWork.QuoteStatusRepository.Update(originalQuoteStatus);
                unitOfWork.Save();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Retuns true if Quote Status already exists
        /// </summary>
        /// <param name="quoteStatus"></param>
        /// <returns></returns>
        private bool DoesQuoteStatusExist(string quoteStatus)
        {
            IEnumerable<QuoteStatus> quoteStatuses = GetAllQuoteStatuses().Where(x => x.QuoteStatusName.ToLower() == quoteStatus.ToLower());
            if (quoteStatuses.Any()) { return true; }
            return false;
        }

        #endregion

        #region Quote

        /// <summary>
        /// Returns all Quotes
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Quote> GetAllQuotes()
        {
            return unitOfWork.QuoteRepository.Get();
        }

        /// <summary>
        /// Returns the Quote based on the ID provided
        /// </summary>
        /// <param name="quoteId"></param>
        /// <returns></returns>
        public Quote GetQuoteById(int quoteId)
        {
            Quote quote = unitOfWork.QuoteRepository.GetByID(quoteId);
            return quote;
        }

        /// <summary>
        /// Returns Quote based on the referenceId provided
        /// </summary>
        /// <param name="refId"></param>
        /// <returns></returns>
        public Quote GetQuoteByReferenceId(string refId)
        {
            if (refId == null)
            {
                throw new System.ArgumentException("Parameter cannot be null");
            }
            Quote quote = unitOfWork.QuoteRepository.Get(x => x.QuoteReferenceId == refId, null, "Phones").FirstOrDefault();
            if (quote == null)
            {
                throw new Exception("Quote does not exist");
            }
            return quote;
        }

        /// <summary>
        /// Checks all phones on the given quote and ensures the latest rate is applied and saved.
        /// If the latest rate is higher than the original offer, no change is made.
        /// If the quote is not of status 'New' no change is made.
        /// </summary>
        /// <param name="quote"></param>
        public Quote ReValidatePhonePrices(Quote quote)
        {
            try
            {
                if (quote == null)
                {
                    throw new ArgumentException("Quote cannot be null");
                }
                if (quote.QuoteStatusId != (int)QuoteStatusEnum.New) return quote;
                foreach (var phone in quote.Phones)
                {
                    decimal offer =
                        phoneService.GetPhoneConditionPrice(phone.PhoneModelId, phone.PhoneConditionId).OfferAmount;
                    if (phone.PurchaseAmount > offer)
                    {
                        phone.PurchaseAmount = offer;
                        unitOfWork.QuoteRepository.Update(quote);
                    }
                    unitOfWork.Save();
                }
                return quote;
            }
            catch (Exception ex)
            {
                emailService.SendAlertEmailAndLogException("ReValidatePhonePrices Failed!", MethodBase.GetCurrentMethod(), ex, quote);
                throw ex;
            }
        }

        /// <summary>
        /// Adds a phone to the Quote. Returns a PhoneModelDetailsViewModel object with ALL phones associated with the quote.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public Quote AddPhoneToQuote(string key, string phoneModelId, string phoneConditionId)
        {
            try
            {
                if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(phoneModelId) ||
                    string.IsNullOrEmpty(phoneConditionId))
                {
                    throw new ArgumentException("One or more Parameters are null");
                }

                Quote quote = GetQuoteByReferenceId(key);
                if (!IsQuoteNew(key))
                {
                    throw new Exception("Quote is not of status 'New'");

                }

                var modelId = Convert.ToInt32(phoneModelId);
                var conditionId = Convert.ToInt32(phoneConditionId);
                PhoneConditionPrice conditionPrice = phoneService.GetPhoneConditionPrice(modelId, conditionId);

                Phone phone = unitOfWork.PhoneRepository.GetNewPhoneObject();
                phone.PhoneModelId = modelId;
                phone.PhoneConditionId = conditionId;
                phone.PhoneStatusId = (int)PhoneStatusEnum.New;
                phone.PurchaseAmount = conditionPrice.OfferAmount;
                phone.PhoneMakeId = phoneService.GetPhoneMakeIdByModelId(modelId);

                quote.Phones.Add(phone);
                var updatedQuote = ModifyQuote(quote, User.SystemUser.Value);

                return updatedQuote;
            }
            catch (Exception ex)
            {
                emailService.SendAlertEmailAndLogException("AddPhoneToQuote Failed!", MethodBase.GetCurrentMethod(), ex, key, phoneModelId, phoneConditionId);
                throw ex;
            }
        }

        /// <summary>
        /// Deletes a phone from a New Quote
        /// </summary>
        /// <param name="key"></param>
        /// <param name="phoneId"></param>
        /// <returns></returns>
        public Quote DeletePhoneFromQuote(string key, string phoneId)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(phoneId))
            {
                throw new System.ArgumentException("Parameter(s) cannot be null");
            }

            try
            {
                bool quoteNew = IsQuoteNew(key);
                bool phoneInQuote = IsPhoneInQuote(key, Convert.ToInt32(phoneId));
                if (!quoteNew || !phoneInQuote)
                {
                    Exception inner = new Exception
                    (
                        quoteNew == false ? "Quote is not of status 'New'" : "Phone does not exist in quote"
                    );
                    throw new Exception("500", inner);
                }
                phoneService.DeletePhoneById(Convert.ToInt32(phoneId));
                Quote quote = GetQuoteByReferenceId(key);

                return quote;
            }
            catch (Exception ex)
            {
                emailService.SendAlertEmailAndLogException("DeletePhoneFromQuote Failed!", MethodBase.GetCurrentMethod(), ex, key, phoneId);
                throw ex;
            }
        }

        /// <summary>
        /// Returns true if the phone exists within the quote
        /// </summary>
        /// <param name="key">ReferenceId for the quote to search</param>
        /// <param name="phoneId">PhoneID of the phone to check for</param>
        /// <returns></returns>
        private bool IsPhoneInQuote(string key, int phoneId)
        {
            Quote quote = GetQuoteByReferenceId(key);
            int exists = quote.Phones.Where(x => x.Id == phoneId).Count();
            return exists > 0;
        }

        /// <summary>
        /// Returns true if the quote with the provided quote key is New
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private bool IsQuoteNew(string key)
        {
            return GetQuoteByReferenceId(key).QuoteStatusId == (int)QuoteStatusEnum.New;
        }

        /// <summary>
        /// Creates a new quote with a status of New
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string CreateQuote()
        {
            try
            {
                string key = GetUniqueQuoteKey();
                Quote quote = new Quote()
                {
                    QuoteStatusId = (int)QuoteStatusEnum.New,
                    QuoteReferenceId = key,
                    CreatedDate = Util.GetAEST(DateTime.Now)
                };

                unitOfWork.QuoteRepository.Insert(quote, null);
                unitOfWork.Save();

                return key;
            }
            catch (Exception ex)
            {
                emailService.SendAlertEmailAndLogException("CreateQuote Failed!", MethodBase.GetCurrentMethod(), ex);
                throw ex;
            }
        }

        /// <summary>
        /// Saves a Quote with the status of New
        /// </summary>
        /// <param name="key"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public Quote SaveQuote(Quote quote)
        {
            if (quote == null || quote.ID == 0)
            {
                throw new ArgumentException("quoteToSave parameter is null/empty");
            }

            quote.QuoteStatusId = (int)QuoteStatusEnum.New;
            unitOfWork.QuoteRepository.Update(quote, null);
            unitOfWork.Save();

            return quote;
        }

        /// <summary>
        /// Finalises the Quote by creating the customer and setting the quote status
        /// to Submitted.
        /// </summary>
        /// <param name="key">Unique Quote Key</param>
        /// <param name="customer">Customer object</param>
        /// <returns>QuoteDetailResult</returns>
        public Quote FinaliseQuote(Quote quote)
        {
            try
            {
                if (quote == null || quote.ID == 0)
                {
                    throw new ArgumentException("quoteToSave parameter is null/empty");
                }

                // Save all quote related properties
                quote.QuoteFinalisedDate = Util.GetAEST(DateTime.Now);
                quote.QuoteExpiryDate = Util.GetAEST(DateTime.Now.AddDays(14));
                quote.QuoteStatusId = (int)GetQuoteStatusByPostageMethod((int)quote.PostageMethodId);

                // Update every phone to status Waiting on Delivery
                foreach (var phone in quote.Phones)
                {
                    phone.PhoneStatusId = (int)PhoneStatusEnum.WaitingForDelivery;
                }

                // Commit all changes for Quote and Child entities
                unitOfWork.QuoteRepository.Update(quote, null);
                unitOfWork.Save();

                // Send customer a confirmation email
                var template = EmailTemplate.QuoteConfirmationSatchel;
                switch (quote.PostageMethodId)
                {
                    case (int)PostageMethodEnum.Satchel:
                        template = EmailTemplate.QuoteConfirmationSatchel;
                        break;
                    case (int)PostageMethodEnum.SelfPost:
                        template = EmailTemplate.QuoteConfirmationSelfPost;
                        break;
                }

                emailService.SendEmailTemplate(template, quote);

                return quote;
            }
            catch (Exception ex)
            {
                emailService.SendAlertEmailAndLogException("FinaliseQuote Failed!", MethodBase.GetCurrentMethod(), ex, quote);
                throw ex;
            }
        }

        /// <summary>
        /// Get the Quote status based on which postage method was selected
        /// </summary>
        /// <param name="postageMethodId"></param>
        /// <returns></returns>
        private QuoteStatusEnum GetQuoteStatusByPostageMethod(int postageMethodId)
        {
            QuoteStatusEnum status = QuoteStatusEnum.WaitingForDelivery;

            if (postageMethodId == (int)PostageMethodEnum.Satchel)
            {
                status = QuoteStatusEnum.RequiresSatchel;
            }
            else if (postageMethodId == (int)PostageMethodEnum.SelfPost)
            {
                status = QuoteStatusEnum.WaitingForDelivery;
            }

            return status;
        }

        /// <summary>
        /// Modifies the quote object
        /// </summary>
        /// <param name="quote"></param>
        /// <returns></returns>
        public Quote ModifyQuote(Quote quote, string userId)
        {
            if (quote == null || userId.Length == 0)
            {
                throw new System.ArgumentException("Parameter cannot be null", "quote");
            }

            int currentQuoteStatusId = unitOfWork.QuoteRepository.GetQuoteStatusId(quote.ID);
            SendEmailBasedOnStatus(currentQuoteStatusId, quote);

            unitOfWork.QuoteRepository.Update(quote, userId);
            unitOfWork.Save();
            return quote;
        }

        /// <summary>
        /// Depending on the quote status an email may be triggered
        /// </summary>
        /// <param name="quote"></param>
        private void SendEmailBasedOnStatus(int currentQuoteStatusId, Quote quote)
        {
            // Check if we need to send email
            if (quote.QuoteStatusId == (int)QuoteStatusEnum.WaitingForDelivery)
            {
                if (currentQuoteStatusId == (int)QuoteStatusEnum.RequiresSatchel)
                {
                    emailService.SendEmailTemplate(EmailTemplate.SatchelSent, quote);
                }
            }
            else if (quote.QuoteStatusId == (int)QuoteStatusEnum.Paid)
            {
                if (currentQuoteStatusId == (int)QuoteStatusEnum.Assessing
                    || currentQuoteStatusId == (int)QuoteStatusEnum.WaitingForDelivery
                    || currentQuoteStatusId == (int)QuoteStatusEnum.ReadyForPayment)
                {
                    emailService.SendEmailTemplate(EmailTemplate.Paid, quote);
                }
            }
            else if (quote.QuoteStatusId == (int)QuoteStatusEnum.Assessing)
            {
                if (currentQuoteStatusId == (int)QuoteStatusEnum.WaitingForDelivery)
                {
                    emailService.SendEmailTemplate(EmailTemplate.Assessing, quote);
                }
            }
            else if (quote.QuoteStatusId == (int)QuoteStatusEnum.Assessing)
            {
                if (currentQuoteStatusId == (int)QuoteStatusEnum.WaitingForDelivery)
                {
                    emailService.SendEmailTemplate(EmailTemplate.Assessing, quote);
                }
            }
        }

        /// <summary>
        /// SearchQuotes - Returns a filtered list of Quotes based on parameters provided. If none are provided
        /// all quotes will be returned.
        /// </summary>
        /// <param name="referenceId"></param>
        /// <param name="email"></param>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="quoteStatusId"></param>
        /// <returns></returns>
        public List<Quote> SearchQuotes(string referenceId, string email, string fullName, int quoteStatusId)
        {
            Expression<Func<Quote, bool>> predicate = c => true;

            if (!string.IsNullOrEmpty(referenceId) && referenceId.Trim().Length > 0)
            {
                predicate = predicate.And(c => c.QuoteReferenceId.ToLower() == referenceId.ToLower());
            }
            if (!string.IsNullOrEmpty(email) && email.Trim().Length > 0)
            {
                predicate = predicate.And(c => c.Customer.Email.ToLower() == email.ToLower());
            }
            if (!string.IsNullOrEmpty(fullName) && fullName.Trim().Length > 0)
            {
                predicate = predicate.And(c => c.Customer.FullName.ToLower().Contains(fullName.ToLower()));
            }
            if (quoteStatusId != 0)
            {
                predicate = predicate.And(c => c.QuoteStatusId == quoteStatusId);
            }

            var content = unitOfWork.QuoteRepository.Get(filter: predicate).ToList();

            return content;
        }

        /// <summary>
        /// Based on sortOrder paramater the quotes will be sorted into specific order
        /// </summary>
        /// <param name="quotesToSort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<Quote> GetSortedQuotes(List<Quote> quotesToSort, string sortOrder)
        {
            IEnumerable<Quote> quotes = quotesToSort;

            switch (sortOrder)
            {
                case "name_asc":
                    quotes = quotes.OrderBy(s => s.Customer.LastName).ThenBy(s => s.Customer.FirstName);
                    break;
                case "name_desc":
                    quotes = quotes.OrderByDescending(s => s.Customer.LastName).ThenBy(s => s.Customer.FirstName);
                    break;
                case "status_asc":
                    quotes = quotes.OrderBy(s => s.QuoteStatusId);
                    break;
                case "status_desc":
                    quotes = quotes.OrderByDescending(s => s.QuoteStatusId);
                    break;
                case "email_asc":
                    quotes = quotes.OrderBy(s => s.Customer.Email);
                    break;
                case "email_desc":
                    quotes = quotes.OrderByDescending(s => s.Customer.Email);
                    break;
                case "created_date_asc":
                    quotes = quotes.OrderBy(s => s.CreatedDate);
                    break;
                case "created_date_desc":
                    quotes = quotes.OrderByDescending(s => s.CreatedDate);
                    break;
                case "quote_finalised_date_asc":
                    quotes = quotes.OrderBy(s => s.QuoteFinalisedDate);
                    break;
                default:
                    quotes = quotes.OrderByDescending(s => s.QuoteFinalisedDate);
                    break;
            }

            return quotes.ToList();
        }

        /// <summary>
        /// Returns an unused Unique key for the Quote ReferenceId
        /// </summary>
        /// <returns></returns>
        private string GetUniqueQuoteKey()
        {
            string key = GetUniqueKey();
            bool alreadyExists = unitOfWork.QuoteRepository.DoesQuoteExist(key);

            while (alreadyExists)
            {
                key = GetUniqueKey();
                alreadyExists = unitOfWork.QuoteRepository.DoesQuoteExist(key);
            }

            return key;
        }

        /// <summary>
        /// Generates a Unique 8 character key
        /// </summary>
        /// <returns></returns>
        private string GetUniqueKey()
        {
            int maxSize = 8;
            char[] chars = new char[62];
            string a;
            a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }
            return result.ToString().ToUpper();
        }

        #endregion

        #region Address

        /// <summary>
        /// Returns all States
        /// </summary>
        /// <returns></returns>
        public IEnumerable<State> GetAllStates()
        {
            return unitOfWork.StateRepository.Get();
        }

        /// <summary>
        /// Get all state names
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllStateNames()
        {
            return unitOfWork.StateRepository.Get().Select(x => x.StateNameShort).ToList();
        }

        public State GetStateById(int id)
        {
            return unitOfWork.StateRepository.GetByID(id);
        }

        /// <summary>
        /// Returns all Countries
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Country> GetAllCountries()
        {
            return unitOfWork.CountryRepository.Get();
        }

        #endregion

        #region Postage

        /// <summary>
        /// Returns all Postage Methods
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PostageMethod> GetAllPostageMethods()
        {
            return unitOfWork.PostageMethodRepository.Get();
        }

        #endregion
    }
}
