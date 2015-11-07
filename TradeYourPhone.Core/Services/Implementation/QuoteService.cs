using TradeYourPhone.Core;
using TradeYourPhone.Core.Enums;
using TradeYourPhone.Core.Models;
using TradeYourPhone.Core.Utilities;
using TradeYourPhone.Core.Repositories.Interface;
using TradeYourPhone.Core.Services.Interface;
using TradeYourPhone.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using TradeYourPhone.Core.Models.DomainModels;
using System.Reflection;
using System.Web.Security;

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
        /// Get Payment Type ID by providing the Payment Type Name
        /// </summary>
        /// <param name="paymentTypeName"></param>
        /// <returns></returns>
        private int GetPaymentTypeIdByPaymentTypeName(string paymentTypeName)
        {
            return unitOfWork.PaymentTypeRepository.Get().Where(x => x.PaymentTypeName == paymentTypeName).First().ID;
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
        /// Delete Payment Type by ID
        /// </summary>
        /// <param name="paymentTypeId"></param>
        /// <returns></returns>
        //public bool DeletePaymentTypeById(int paymentTypeId)
        //{
        //    unitOfWork.PaymentTypeRepository.Delete(paymentTypeId);
        //    unitOfWork.Save();
        //    return true;
        //}

        /// <summary>
        /// Returns true if PaymentType already exists
        /// </summary>
        /// <param name="paymentType"></param>
        /// <returns></returns>
        private bool DoesPaymentTypeExist(string paymentType)
        {
            IEnumerable<PaymentType> paymentTypes = GetAllPaymentTypes().Where(x => x.PaymentTypeName.ToLower() == paymentType.ToLower());
            if (paymentTypes.Count() > 0) { return true; }
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
        /// Deletes Quote Status by ID
        /// </summary>
        /// <param name="quoteStatusId"></param>
        /// <returns></returns>
        //public bool DeleteQuoteStatusById(int quoteStatusId)
        //{
        //    unitOfWork.QuoteStatusRepository.Delete(quoteStatusId);
        //    unitOfWork.Save();
        //    return true;
        //}

        /// <summary>
        /// Retuns true if Quote Status already exists
        /// </summary>
        /// <param name="quoteStatus"></param>
        /// <returns></returns>
        private bool DoesQuoteStatusExist(string quoteStatus)
        {
            IEnumerable<QuoteStatus> quoteStatuses = GetAllQuoteStatuses().Where(x => x.QuoteStatusName.ToLower() == quoteStatus.ToLower());
            if (quoteStatuses.Count() > 0) { return true; }
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
                throw new System.ArgumentException("Parameter cannot be null", "refId");
            }
            Quote quote = unitOfWork.QuoteRepository.Get(x => x.QuoteReferenceId == refId, null, "Phones").FirstOrDefault();
            return quote;
        }

        /// <summary>
        /// Returns a QuoteDetailsResult object with the customer and all the phones associated with the key provided.
        /// If the quote is in the New status, prices will also be revalidated.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public QuoteDetailsResult GetQuoteDetailsByQuoteReferenceId(string key)
        {
            QuoteDetailsResult getDetailsResult = new QuoteDetailsResult() { Status = "OK" };
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    Quote quote = GetQuoteByReferenceId(key);
                    if (quote != null)
                    {
                        if (quote.QuoteStatusId == (int)QuoteStatusEnum.New)
                        {
                            ReValidatePhonePrices(quote.Phones.ToList());
                        }

                        getDetailsResult.QuoteDetails = BuildQuoteDetails(key, quote);
                    }
                    else
                    {
                        getDetailsResult.Status = "Error";
                        Exception inner = new Exception("Quote does not exist");
                        throw new Exception("500", inner);
                    }
                }
                else { throw new Exception("key parameter is empty"); }
            }
            catch(Exception ex)
            {
                emailService.SendAlertEmailAndLogException("GetQuoteDetailsByQuoteReferenceId Failed!", MethodBase.GetCurrentMethod(), ex, key);
                getDetailsResult.Status = "Error";
                getDetailsResult.Exception = new QuoteDetailsException(ex);
                getDetailsResult.QuoteDetails = null;
            }

            return getDetailsResult;
        }

        /// <summary>
        /// Checks the PurchaseAmount on the Phone record and ensures the latest rate is applied and saved.
        /// If the latest rate is higher than the original offer, no change is made.
        /// </summary>
        /// <param name="phones"></param>
        private void ReValidatePhonePrices(List<Phone> phones)
        {
            foreach(var phone in phones)
            {
                decimal offer = phoneService.GetPhoneConditionPrice(phone.PhoneModelId, phone.PhoneConditionId).OfferAmount;
                if(phone.PurchaseAmount > offer)
                {
                    phone.PurchaseAmount = offer;
                    unitOfWork.PhoneRepository.Update(phone);
                }
                unitOfWork.Save();
            }
        }

        /// <summary>
        /// Adds a phone to the Quote. Returns a PhoneModelDetailsViewModel object with ALL phones associated with the quote.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public QuoteDetailsResult AddPhoneToQuote(string key, string phoneModelId, string phoneConditionId)
        {
            QuoteDetailsResult addPhoneResult = new QuoteDetailsResult() { Status = "OK" };
            try
            {
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(phoneModelId) && !string.IsNullOrEmpty(phoneConditionId))
                {
                    Quote quote = GetQuoteByReferenceId(key);
                    if (quote != null && quote.QuoteStatusId == (int)QuoteStatusEnum.New)
                    {
                        Phone phone = CreatePhoneObject(quote.ID, phoneModelId, phoneConditionId);
                        phoneService.CreatePhone(phone);
                    }
                    else
                    {
                        Exception inner = new Exception(quote == null ? "Quote does not exist" : "Quote is not of status 'New'");
                        throw new Exception("500", inner);
                    }

                    addPhoneResult.QuoteDetails = BuildQuoteDetails(key, quote);
                }
                else
                {
                    throw new Exception("One or more Parameters are null");
                }

                return addPhoneResult;
            }
            catch (Exception ex)
            {
                emailService.SendAlertEmailAndLogException("AddPhoneToQuote Failed!", MethodBase.GetCurrentMethod(), ex, key, phoneModelId, phoneConditionId);
                addPhoneResult.Status = "Error";
                addPhoneResult.Exception = new QuoteDetailsException(ex);
                addPhoneResult.QuoteDetails = null;
                return addPhoneResult;
            }
        }

        /// <summary>
        /// Constructs a Phone object using QuoteId, ModelId and ConditionId. Sets Phone Status to New.
        /// Ensures PurchaseAmount is the latest.
        /// </summary>
        /// <param name="quoteId"></param>
        /// <param name="phoneModelId"></param>
        /// <param name="phoneConditionId"></param>
        /// <returns></returns>
        private Phone CreatePhoneObject(int quoteId, string phoneModelId, string phoneConditionId)
        {
            int modelId = Convert.ToInt32(phoneModelId);
            int conditionId = Convert.ToInt32(phoneConditionId);
            PhoneConditionPrice conditionPrice = phoneService.GetPhoneConditionPrice(modelId, conditionId);
            if(conditionPrice == null)
            {
                throw new Exception("Model and/or Condition do not exist");
            }

            Phone phone = new Phone()
            {
                PhoneModelId = modelId,
                PhoneConditionId = conditionId,
                QuoteId = quoteId,
                PhoneStatusId = (int)PhoneStatusEnum.New,
                PurchaseAmount = conditionPrice.OfferAmount,
                PhoneMakeId = phoneService.GetPhoneMakeIdByModelId(modelId)
            };

            return phone;
        }

        /// <summary>
        /// Constructs a QuoteDetails object from a ICollection of Phones and a nullable customer object
        /// </summary>
        /// <param name="phones"></param>
        /// <returns></returns>
        private QuoteDetails BuildQuoteDetails(string key, Quote quote)
        {
            QuoteDetails quoteDetails = new QuoteDetails()
            {
                QuoteReferenceId = key,
                QuoteStatus = quote.QuoteStatus.QuoteStatusName,
                Phones = new List<PhoneDetail>(),
                PostageMethod = quote.PostageMethod,
                AgreedToTerms = quote.AgreedToTerms
            };

            if(quote.Customer != null)
            {
                quote.Customer.Quotes = null;
                quoteDetails.Customer = new CustomerDetail
                {
                    fullname = quote.Customer.FullName,
                    email = quote.Customer.Email,
                    emailConfirm = quote.Customer.Email,
                    mobile = quote.Customer.PhoneNumber,
                    postageStreet = quote.Customer.Address.AddressLine1,
                    postageSuburb = quote.Customer.Address.AddressLine2,
                    postageState = quote.Customer.Address.State.StateNameShort,
                    postagePostcode = quote.Customer.Address.PostCode,
                    paymentType = quote.Customer.PaymentDetail.PaymentType.PaymentTypeName,
                    bsb = quote.Customer.PaymentDetail.BSB,
                    accountNum = quote.Customer.PaymentDetail.AccountNumber,
                    paypalEmail = quote.Customer.PaymentDetail.PaypalEmail,
                    paypalSameAsPersonal = quote.Customer.PaymentDetail.PaypalEmail == quote.Customer.Email
                };
            }

            foreach (var item in quote.Phones.OrderBy(x => x.Id))
            {
                Phone phoneObj = phoneService.GetPhoneById(item.Id);
                if (phoneObj != null)
                {
                    PhoneDetail details = new PhoneDetail()
                    {
                        Id = phoneObj.Id,
                        PhoneMakeName = phoneObj.PhoneMake.MakeName,
                        PhoneModelName = phoneObj.PhoneModel.ModelName,
                        PhoneCondition = phoneObj.PhoneCondition.Condition,
                        OfferPrice = phoneObj.PurchaseAmount.ToString(),
                        PrimaryImageString = phoneObj.PhoneModel.PrimaryImageString
                    };
                    quoteDetails.Phones.Add(details);
                }
            }

            return quoteDetails;
        }

        /// <summary>
        /// Deletes a phone from a New Quote
        /// </summary>
        /// <param name="key"></param>
        /// <param name="phoneId"></param>
        /// <returns></returns>
        public QuoteDetailsResult DeletePhoneFromQuote(string key, string phoneId)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(phoneId))
            {
                throw new System.ArgumentException("Parameter(s) cannot be null");
            }

            QuoteDetailsResult deletePhoneResult = new QuoteDetailsResult() { Status = "OK" };
            try
            {
                bool quoteNew = IsQuoteNew(key);
                bool phoneInQuote = IsPhoneInQuote(key, Convert.ToInt32(phoneId));
                if (quoteNew && phoneInQuote)
                {
                    phoneService.DeletePhoneById(Convert.ToInt32(phoneId));
                    Quote quote = GetQuoteByReferenceId(key);
                    deletePhoneResult.QuoteDetails = BuildQuoteDetails(key, quote);
                }
                else
                {
                    Exception inner = new Exception
                    (
                        quoteNew == false ? "Quote is not of status 'New'" : "Phone does not exist in quote"
                    );
                    throw new Exception("500", inner);
                }

                return deletePhoneResult;
            }
            catch (Exception ex)
            {
                emailService.SendAlertEmailAndLogException("DeletePhoneFromQuote Failed!", MethodBase.GetCurrentMethod(), ex, key, phoneId);
                deletePhoneResult.Status = "Error";
                deletePhoneResult.Exception = new QuoteDetailsException(ex);
                deletePhoneResult.QuoteDetails = null;
                return deletePhoneResult;
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

                unitOfWork.QuoteRepository.Insert(quote);
                unitOfWork.Save();
                UpdateQuoteStatusHistory(quote.ID, 0, quote.QuoteStatusId, null);
                unitOfWork.Save();

                return key;
            }
            catch(Exception ex)
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
        public QuoteDetailsResult SaveQuote(string key, SaveQuoteViewModel viewModel)
        {
            return SaveQuote(key, viewModel, false);
        } 

        /// <summary>
        /// Finalises the Quote by creating the customer and setting the quote status
        /// to Submitted.
        /// </summary>
        /// <param name="key">Unique Quote Key</param>
        /// <param name="customer">Customer object</param>
        /// <returns>QuoteDetailResult</returns>
        public QuoteDetailsResult FinaliseQuote(string key, SaveQuoteViewModel viewModel)
        {
            return SaveQuote(key, viewModel, true);
        }

        /// <summary>
        /// Saves the Quote by creating/updating the customer and updating Quote Status
        /// </summary>
        /// <param name="key">Unique Quote Key</param>
        /// <param name="customer">Customer object</param>
        /// <returns>QuoteDetailResult</returns>
        private QuoteDetailsResult SaveQuote(string key, SaveQuoteViewModel viewModel, bool finalised)
        {
            QuoteDetailsResult saveQuoteResult = new QuoteDetailsResult() { Status = "OK" };
            try
            {
                if (!string.IsNullOrEmpty(key) && viewModel != null)
                {
                    // Save all quote related properties
                    Quote quote = GetQuoteByReferenceId(key);
                    if (quote != null)
                    {
                        QuoteStatusEnum status = QuoteStatusEnum.New;
                        if(finalised)
                        {
                            status = GetQuoteStatusByPostageMethod(viewModel.PostageMethodId);
                            quote.QuoteFinalisedDate = Util.GetAEST(DateTime.Now);
                            quote.QuoteExpiryDate = Util.GetAEST(DateTime.Now.AddDays(14));

                            // Update every phone to status Waiting on Delivery and then add a status history record
                            foreach(var phone in quote.Phones)
                            {
                                int existingStatusId = phone.PhoneStatusId;
                                phone.PhoneStatusId = (int)PhoneStatusEnum.WaitingForDelivery;
                                phoneService.UpdatePhoneStatusHistory(phone.Id, existingStatusId, phone.PhoneStatusId, User.SystemUser.Value);
                            }
                        }

                        UpdateQuoteStatusHistory(quote.ID, quote.QuoteStatusId, (int)status, null);
                        quote.QuoteStatusId = (int)status;
                        quote.PostageMethodId = viewModel.PostageMethodId;
                        quote.AgreedToTerms = viewModel.AgreedToTerms;
                        
                        unitOfWork.QuoteRepository.Update(quote);

                        // Save the customer associated with this quote
                        SaveCustomer(quote, viewModel.Customer);

                        // Commit all changes for Quote and Child entities
                        unitOfWork.Save();

                        saveQuoteResult.QuoteDetails = BuildQuoteDetails(key, quote);

                        if (finalised)
                        {
                            // Send customer a confirmation email
                            EmailTemplate template = EmailTemplate.QuoteConfirmationSatchel;
                            if(quote.PostageMethodId == (int)PostageMethodEnum.Satchel)
                            {
                                template = EmailTemplate.QuoteConfirmationSatchel;
                            }
                            else if (quote.PostageMethodId == (int)PostageMethodEnum.SelfPost)
                            {
                                template = EmailTemplate.QuoteConfirmationSelfPost;
                            }

                            emailService.SendEmailTemplate(template, quote);
                        }
                    }
                    else
                    {
                        Exception inner = new Exception("Quote does not exist");
                        throw new Exception("500", inner);
                    }
                }
                else
                {
                    throw new Exception("One or more Parameters are null");
                }
            }
            catch (Exception ex)
            {
                emailService.SendAlertEmailAndLogException("SaveQuote Failed!", MethodBase.GetCurrentMethod(), ex, key, viewModel, finalised);
                saveQuoteResult.Status = "Error";
                saveQuoteResult.Exception = new QuoteDetailsException(ex);
                return saveQuoteResult;
            }

            return saveQuoteResult;
        }

        /// <summary>
        /// Save Customer entity and all child entities
        /// </summary>
        /// <param name="quote">Quote that the customer is a child of</param>
        /// <param name="newCustomer">The customer entity to save</param>
        /// <returns></returns>
        private bool SaveCustomer(Quote quote, Customer newCustomer)
        {
            bool success = true;

            if (quote.Customer == null)
            {
                quote.Customer = MergeCustomer(quote.Customer, newCustomer);
                quote.Customer.Quotes.Add(quote);

                unitOfWork.CustomerRepository.Insert(quote.Customer);
                unitOfWork.AddressRepository.Insert(quote.Customer.Address);
                unitOfWork.PaymentDetailRepository.Insert(quote.Customer.PaymentDetail);
            }
            else
            {
                quote.Customer = MergeCustomer(quote.Customer, newCustomer);

                unitOfWork.CustomerRepository.Update(quote.Customer);
                unitOfWork.AddressRepository.Update(quote.Customer.Address);
                unitOfWork.PaymentDetailRepository.Update(quote.Customer.PaymentDetail);
            }

            return success;
        }

        /// <summary>
        /// Merge a new Customer object with an Existing Customer object
        /// </summary>
        /// <param name="customerToMergeTo"></param>
        /// <param name="newCustomer"></param>
        /// <returns></returns>
        private Customer MergeCustomer(Customer customerToMergeTo, Customer newCustomer)
        {
            customerToMergeTo = customerToMergeTo ?? new Customer();
            customerToMergeTo.FirstName = newCustomer.FirstName;
            customerToMergeTo.LastName = newCustomer.LastName;
            customerToMergeTo.Email = newCustomer.Email;
            customerToMergeTo.PhoneNumber = newCustomer.PhoneNumber;
            customerToMergeTo.Address = MergeAddress(customerToMergeTo.Address, newCustomer.Address);
            customerToMergeTo.PaymentDetail = MergePaymentDetail(customerToMergeTo.PaymentDetail, newCustomer.PaymentDetail);

            return customerToMergeTo;
        }

        /// <summary>
        /// Merge Address object with existing Address object
        /// </summary>
        /// <param name="addressToMergeTo"></param>
        /// <param name="newAddress"></param>
        /// <returns></returns>
        private Address MergeAddress(Address addressToMergeTo, Address newAddress)
        {
            addressToMergeTo = addressToMergeTo ?? new Address();
            addressToMergeTo.AddressLine1 = newAddress.AddressLine1;
            addressToMergeTo.AddressLine2 = newAddress.AddressLine2;
            addressToMergeTo.PostCode = newAddress.PostCode;
            addressToMergeTo.CountryId = newAddress.CountryId;
            addressToMergeTo.State = GetStateById(GetStateIdByStateShortName(newAddress.State.StateNameShort));

            return addressToMergeTo;
        }

        /// <summary>
        /// Merge PaymentDetail object with Existing PaymentDetail object
        /// </summary>
        /// <param name="paymentDetailToMergeTo"></param>
        /// <param name="newPaymentDetail"></param>
        /// <returns></returns>
        private PaymentDetail MergePaymentDetail(PaymentDetail paymentDetailToMergeTo, PaymentDetail newPaymentDetail)
        {
            paymentDetailToMergeTo = paymentDetailToMergeTo ?? new PaymentDetail();
            paymentDetailToMergeTo.PaymentTypeId = GetPaymentTypeIdByPaymentTypeName(newPaymentDetail.PaymentType.PaymentTypeName);
            paymentDetailToMergeTo.BSB = newPaymentDetail.BSB;
            paymentDetailToMergeTo.AccountNumber = newPaymentDetail.AccountNumber;
            paymentDetailToMergeTo.PaypalEmail = newPaymentDetail.PaypalEmail;

            return paymentDetailToMergeTo;
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
        public bool ModifyQuote(QuoteDetailsViewModel quoteVM)
        {
            if (quoteVM == null || quoteVM.quote == null)
            {
                throw new System.ArgumentException("Parameter cannot be null", "quote");
            }

            SendEmailBasedOnStatus(quoteVM);
            UpdateQuoteStatusHistory(quoteVM.quote.ID, quoteVM.CurrentQuoteStatus, quoteVM.quote.QuoteStatusId, quoteVM.UserId);

            unitOfWork.QuoteRepository.Update(quoteVM.quote);
            unitOfWork.CustomerRepository.Update(quoteVM.quote.Customer);
            unitOfWork.AddressRepository.Update(quoteVM.quote.Customer.Address);
            unitOfWork.PaymentDetailRepository.Update(quoteVM.quote.Customer.PaymentDetail);
            phoneService.ModifyPhones(quoteVM.quote.Phones.ToList(), quoteVM.UserId);
            unitOfWork.Save();
            return true;
        }

        /// <summary>
        /// If the status has changed a new Status history record will be added
        /// </summary>
        /// <param name="quoteId"></param>
        /// <param name="oldQuoteStatusId"></param>
        /// <param name="newQuoteStatusId"></param>
        /// <param name="UserId">UserId of the user updating the Quote record</param>
        private void UpdateQuoteStatusHistory(int quoteId, int oldQuoteStatusId, int newQuoteStatusId, string UserId)
        {
            if(oldQuoteStatusId != newQuoteStatusId)
            {
                QuoteStatusHistory record = new QuoteStatusHistory
                {
                    QuoteId = quoteId,
                    QuoteStatusId = newQuoteStatusId,
                    StatusDate = Util.GetAEST(DateTime.Now),
                    CreatedBy = UserId ?? User.SystemUser.Value
                };

                unitOfWork.QuoteStatusHistoryRepository.Insert(record);
            }
        }

        /// <summary>
        /// Depending on the quote status an email may be triggered
        /// </summary>
        /// <param name="quote"></param>
        private void SendEmailBasedOnStatus(QuoteDetailsViewModel quoteVM)
        {
            // Check if we need to send email
            if (quoteVM.quote.QuoteStatusId == (int)QuoteStatusEnum.WaitingForDelivery)
            {
                if (quoteVM.CurrentQuoteStatus == (int)QuoteStatusEnum.RequiresSatchel)
                {
                    emailService.SendEmailTemplate(EmailTemplate.SatchelSent, quoteVM.quote);
                }
            }
            else if(quoteVM.quote.QuoteStatusId == (int)QuoteStatusEnum.Paid)
            {
                if (quoteVM.CurrentQuoteStatus == (int)QuoteStatusEnum.Assessing || quoteVM.CurrentQuoteStatus == (int)QuoteStatusEnum.WaitingForDelivery)
                {
                    emailService.SendEmailTemplate(EmailTemplate.Paid, quoteVM.quote);
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
        public List<Quote> SearchQuotes(string referenceId, string email, string lastName, string firstName, int quoteStatusId)
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
            if (!string.IsNullOrEmpty(lastName) && lastName.Trim().Length > 0)
            {
                predicate = predicate.And(c => c.Customer.LastName.ToLower() == lastName.ToLower());
            }
            if (!string.IsNullOrEmpty(firstName) && firstName.Trim().Length > 0)
            {
                predicate = predicate.And(c => c.Customer.FirstName.ToLower() == firstName.ToLower());
            }
            if(quoteStatusId != 0)
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
        public List<Quote> GetSortedQuotes(List<Quote> quotesToSort, QuoteIndexViewModel viewModel)
        {
            viewModel.QuoteFinalisedDateSortParm = String.IsNullOrEmpty(viewModel.sortOrder) ? "quote_finalised_date_asc" : "";
            viewModel.NameSortParm = viewModel.sortOrder == "name_asc" ? "name_desc" : "name_asc";
            viewModel.EmailSortParm = viewModel.sortOrder == "email_asc" ? "email_desc" : "email_asc";
            viewModel.CreatedDateSortParm = viewModel.sortOrder == "created_date_asc" ? "created_date_desc" : "created_date_asc";
            viewModel.StatusSortParm = viewModel.sortOrder == "status_asc" ? "status_desc" : "status_asc";

            IEnumerable<Quote> quotes = quotesToSort;

            switch (viewModel.sortOrder)
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
            bool alreadyExists = GetQuoteByReferenceId(key) != null ? true : false;

            while (alreadyExists)
            {
                key = GetUniqueKey();
                alreadyExists = GetQuoteByReferenceId(key) != null ? true : false;
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

        /// <summary>
        /// Get the State ID by providing the State Short Name
        /// </summary>
        /// <param name="stateShortName"></param>
        /// <returns></returns>
        private int GetStateIdByStateShortName(string stateShortName)
        {
            return unitOfWork.StateRepository.Get().Where(x => x.StateNameShort == stateShortName).First().ID;
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