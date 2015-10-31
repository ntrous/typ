using TradeYourPhone.Core.Models;
using TradeYourPhone.Core.Models.DomainModels;
using TradeYourPhone.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradeYourPhone.Core.Services.Interface
{
    public interface IQuoteService
    {
        IEnumerable<PaymentType> GetAllPaymentTypes();
        List<string> GetAllPaymentTypeNames();
        PaymentType GetPaymentTypeById(int paymentTypeId);
        bool CreatePaymentType(PaymentType paymentType);
        bool ModifyPaymentType(PaymentType paymentType);
        //bool DeletePaymentTypeById(int paymentTypeId);

        IEnumerable<QuoteStatus> GetAllQuoteStatuses();
        QuoteStatus GetQuoteStatusById(int quoteStatusId);
        bool CreateQuoteStatus(QuoteStatus quoteStatus);
        bool ModifyQuoteStatus(QuoteStatus quoteStatus);
        //bool DeleteQuoteStatusById(int quoteStatusId);

        /// <summary>
        /// Returns all Quotes
        /// </summary>
        /// <returns></returns>
        IEnumerable<Quote> GetAllQuotes();

        /// <summary>
        /// Returns the Quote based on the ID provided
        /// </summary>
        /// <param name="quoteId"></param>
        /// <returns></returns>
        Quote GetQuoteById(int quoteId);

        /// <summary>
        /// Returns Quote based on the referenceId provided
        /// </summary>
        /// <param name="refId"></param>
        /// <returns></returns>
        Quote GetQuoteByReferenceId(string refId);

        /// <summary>
        /// Returns a QuoteDetailsResult object with the customer and all the phones associated with the key provided.
        /// If the quote is in the New status, prices will also be revalidated.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        QuoteDetailsResult GetQuoteDetailsByQuoteReferenceId(string key);

        /// <summary>
        /// Adds a phone to the Quote. Returns a PhoneModelDetailsViewModel object with ALL phones associated with the quote.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        QuoteDetailsResult AddPhoneToQuote(string key, string phoneModelId, string phoneConditionId);

        /// <summary>
        /// Deletes a phone from a New Quote
        /// </summary>
        /// <param name="key"></param>
        /// <param name="phoneId"></param>
        /// <returns></returns>
        QuoteDetailsResult DeletePhoneFromQuote(string key, string phoneId);

        /// <summary>
        /// Creates a new quote with a status of New and returns Unique reference ID
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Unique Quote Reference ID</returns>
        string CreateQuote();

        /// <summary>
        /// Saves a Quote with the status of New
        /// </summary>
        /// <param name="key"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        QuoteDetailsResult SaveQuote(string key, SaveQuoteViewModel viewModel);

        /// <summary>
        /// Finalises the Quote by creating the customer and setting the quote status
        /// to Submitted.
        /// </summary>
        /// <param name="key">Unique Quote Key</param>
        /// <param name="customer">Customer object</param>
        /// <returns>QuoteDetailResult</returns>
        QuoteDetailsResult FinaliseQuote(string key, SaveQuoteViewModel viewModel);

        /// <summary>
        /// Modifies the quote object
        /// </summary>
        /// <param name="quote"></param>
        /// <returns></returns>
        bool ModifyQuote(QuoteDetailsViewModel quoteVM);

        /// <summary>
        /// Delet the quote by ID provided
        /// </summary>
        /// <param name="quoteId"></param>
        /// <returns></returns>
        //bool DeleteQuoteById(int quoteId);

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
        List<Quote> SearchQuotes(string referenceId, string email, string lastName, string firstName, int quoteStatusId);

        /// <summary>
        /// Based on sortOrder paramater the quotes will be sorted into specific order
        /// </summary>
        /// <param name="quotesToSort"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        List<Quote> GetSortedQuotes(List<Quote> quotesToSort, QuoteIndexViewModel viewModel);

        /// <summary>
        /// Gets all the data for the Dashboard view
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        DashboardViewModel GetDashboardData(DateTime? from, DateTime? to);

        /// <summary>
        /// Get all states
        /// </summary>
        /// <returns></returns>
        IEnumerable<State> GetAllStates();

        /// <summary>
        /// Get state by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        State GetStateById(int id);

        /// <summary>
        /// Get all state names
        /// </summary>
        /// <returns></returns>
        List<string> GetAllStateNames();

        /// <summary>
        /// Get all countries
        /// </summary>
        /// <returns></returns>
        IEnumerable<Country> GetAllCountries();

        /// <summary>
        /// Get all postage methods
        /// </summary>
        /// <returns></returns>
        IEnumerable<PostageMethod> GetAllPostageMethods();
    }
}