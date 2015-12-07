using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TradeYourPhone.Core.Models;
using TradeYourPhone.Core.Services.Interface;
using TradeYourPhone.Web.ViewModels;
using TradeYourPhone.Core.Enums;
using PagedList;
using Microsoft.AspNet.Identity;
using TradeYourPhone.Core.DTO;

namespace TradeYourPhone.Web.Controllers
{
    public class QuotesController : Controller
    {
        private IQuoteService quoteService;
        private IPhoneService phoneService;

        public QuotesController(IQuoteService quoteService, IPhoneService phoneService)
        {
            this.quoteService = quoteService;
            this.phoneService = phoneService;
        }

        // GET: Quotes
        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(QuoteIndexViewModel viewModel)
        {
            QuoteIndexViewModel quoteIndexViewModel = viewModel ?? new QuoteIndexViewModel();
            viewModel.PageSize = 20;
            var quotes = quoteService.SearchQuotes(quoteIndexViewModel.referenceId, quoteIndexViewModel.email, quoteIndexViewModel.fullName, quoteIndexViewModel.statusId);

            viewModel.QuoteFinalisedDateSortParm = String.IsNullOrEmpty(viewModel.sortOrder) ? "quote_finalised_date_asc" : "";
            viewModel.NameSortParm = viewModel.sortOrder == "name_asc" ? "name_desc" : "name_asc";
            viewModel.EmailSortParm = viewModel.sortOrder == "email_asc" ? "email_desc" : "email_asc";
            viewModel.CreatedDateSortParm = viewModel.sortOrder == "created_date_asc" ? "created_date_desc" : "created_date_asc";
            viewModel.StatusSortParm = viewModel.sortOrder == "status_asc" ? "status_desc" : "status_asc";
            quotes = quoteService.GetSortedQuotes(quotes, viewModel.sortOrder);

            quoteIndexViewModel.TotalQuotes = quotes.Count;
            if (quoteIndexViewModel.PageNumber == 0 || (quoteIndexViewModel.PageNumber > (int)System.Math.Ceiling(((double)quoteIndexViewModel.TotalQuotes / (double)viewModel.PageSize))))
            { quoteIndexViewModel.PageNumber = 1; }

            var pagedQuotes = quotes.ToPagedList(quoteIndexViewModel.PageNumber, viewModel.PageSize);

            quoteIndexViewModel.MapQuotes(pagedQuotes);
            quoteIndexViewModel.MapStatuses(quoteService.GetAllQuoteStatuses().ToList());

            return Json(quoteIndexViewModel, JsonRequestBehavior.AllowGet);
        }

        // GET: Quotes/Details/5
        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetQuote(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = quoteService.GetQuoteById((int)id);
            if (quote == null)
            {
                return HttpNotFound();
            }

            QuoteDetailsViewModel vm = SetupQuoteDetailsViewModel();
            vm.MapQuote(quote);

            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        // POST: Quotes/Details/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SaveQuoteDetails(QuoteDTO quote)
        {
            Quote quoteToUpdate = quoteService.GetQuoteById(quote.ID);
            quoteToUpdate.UpdateFromDTO(quote);
            quoteToUpdate = quoteService.ModifyQuote(quoteToUpdate, User.Identity.GetUserId());
            QuoteDetailsViewModel vm = SetupQuoteDetailsViewModel();
            vm.MapQuote(quoteToUpdate);

            return Json(vm, JsonRequestBehavior.AllowGet);  
        }

        /// <summary>
        /// Creates a new Quote and returns the Quote Reference ID
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult CreateQuote()
        {
            string key = quoteService.CreateQuote();
            return Json(key, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Add a Phone to a Quote
        /// </summary>
        /// <param name="key">Unique Quote Key</param>
        /// <param name="modelId">PhoneModelId of the Phone to add</param>
        /// <param name="conditionId">ConditionId of the condition the Phone is in</param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddPhoneToQuoteInSession(string key, string modelId, string conditionId)
        {
            QuoteDetailsResult addPhoneResult = new QuoteDetailsResult() { Status = "OK" };

            try
            {
                Quote quote = quoteService.AddPhoneToQuote(key, modelId, conditionId);
                
                addPhoneResult.QuoteDetails = new QuoteDetails();
                addPhoneResult.QuoteDetails.MapQuote(quote);
            }
            catch (Exception ex)
            {
                addPhoneResult.Status = "Error";
                addPhoneResult.Exception = new QuoteDetailsException(ex);
                addPhoneResult.QuoteDetails = null;
            }
            return Json(addPhoneResult, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Saves the Quote
        /// </summary>
        /// <param name="key"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SaveQuote(string key, SaveQuoteViewModel viewModel)
        {
            QuoteDetailsResult result = new QuoteDetailsResult() { Status = "OK" };
            try
            {
                if (ModelState.IsValid)
                {
                    Quote quote = quoteService.GetQuoteByReferenceId(key);
                    quote.AgreedToTerms = viewModel.AgreedToTerms;
                    quote.PostageMethodId = viewModel.PostageMethodId;
                    quote.Customer = quote.Customer ?? viewModel.Customer;
                    quote.Customer.UpdateFromCustomerObj(viewModel.Customer);
                    quote = quoteService.SaveQuote(quote);

                    result.QuoteDetails = new QuoteDetails();
                    result.QuoteDetails.MapQuote(quote);
                }
                else
                {
                    result.Status = "Error";
                    result.Exception = new QuoteDetailsException(new Exception("Customer object is not valid"));
                }
            }
            catch (Exception ex)
            {
                result.Status = "Error";
                result.Exception = new QuoteDetailsException(ex);
                result.QuoteDetails = null;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Finalises the Quote by creating the customer object and setting the quote status to Submitted
        /// </summary>
        /// <param name="key"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FinaliseQuote(string key, SaveQuoteViewModel viewModel)
        {
            QuoteDetailsResult result = new QuoteDetailsResult() { Status = "OK" };
            try
            {
                if (ModelState.IsValid)
                {
                    Quote quote = quoteService.GetQuoteByReferenceId(key);
                    quote.AgreedToTerms = viewModel.AgreedToTerms;
                    quote.PostageMethodId = viewModel.PostageMethodId;
                    quote.Customer = quote.Customer ?? viewModel.Customer;
                    quote = quoteService.FinaliseQuote(quote);

                    result.QuoteDetails = new QuoteDetails();
                    result.QuoteDetails.MapQuote(quote);
                }
                else
                {
                    result.Status = "Error";
                    result.Exception = new QuoteDetailsException(new Exception("Customer object is not valid"));
                }
            }
            catch (Exception ex)
            {
                result.Status = "Error";
                result.Exception = new QuoteDetailsException(ex);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Deletes the Phone record from the Quote
        /// </summary>
        /// <param name="key"></param>
        /// <param name="phoneId"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult DeleteQuotePhone(string key, string phoneId)
        {
            QuoteDetailsResult result = new QuoteDetailsResult
            {
                Status = "OK",
                QuoteDetails = new QuoteDetails()
            };
            try
            {
                Quote quote = quoteService.DeletePhoneFromQuote(key, phoneId);
                result.QuoteDetails.MapQuote(quote);
            }
            catch (Exception ex)
            {
                result.Status = "Error";
                result.Exception = new QuoteDetailsException(ex);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetAllQuotes()
        {
            var quoteDetailsResult = quoteService.GetAllQuotes();

            return Json(quoteDetailsResult, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get all Phones associated with the provided Quote Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetQuoteDetails(string key)
        {
            QuoteDetailsResult result = new QuoteDetailsResult
            {
                Status = "OK",
                QuoteDetails = new QuoteDetails()
            };
            try
            {
                Quote quote = quoteService.GetQuoteByReferenceId(key);
                quote = quoteService.ReValidatePhonePrices(quote);
                result.QuoteDetails.MapQuote(quote);
            }
            catch (Exception ex)
            {
                result.Status = "Error";
                result.Exception = new QuoteDetailsException(ex);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [OutputCache(Duration = (int)TimeEnum.oneweek, VaryByParam = "none", Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult GetStates()
        {
            var states = quoteService.GetAllStates();
            return Json(states, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [OutputCache(Duration = (int)TimeEnum.oneweek, VaryByParam = "none", Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult GetStateNames()
        {
            var states = quoteService.GetAllStateNames();
            var result = (from state in states
                          select new
                          {
                              name = state
                          }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [OutputCache(Duration = (int)TimeEnum.oneweek, VaryByParam = "none", Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult GetPaymentTypes()
        {
            var paymentTypes = quoteService.GetAllPaymentTypes();
            return Json(paymentTypes, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [OutputCache(Duration = (int)TimeEnum.oneweek, VaryByParam = "none", Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult GetPaymentTypeNames()
        {
            var paymentTypeNames = quoteService.GetAllPaymentTypeNames();
            var result = (from name in paymentTypeNames
                          select new
                          {
                              name = name
                          }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [OutputCache(Duration = (int)TimeEnum.oneweek, VaryByParam = "none", Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult GetPostageMethods()
        {
            var postageMethods = quoteService.GetAllPostageMethods();
            return Json(postageMethods, JsonRequestBehavior.AllowGet);
        }

        private QuoteDetailsViewModel SetupQuoteDetailsViewModel(QuoteDetailsViewModel viewModel = null)
        {
            QuoteDetailsViewModel newViewModel = new QuoteDetailsViewModel();
            if (viewModel == null)
            {
                newViewModel = new QuoteDetailsViewModel();
            }
            else
            {
                newViewModel = viewModel;
            }

            newViewModel.MapPhoneStatuses(phoneService.GetAllPhoneStatuses().ToList());
            newViewModel.MapPhoneMakes(phoneService.GetAllPhoneMakes().ToList());
            newViewModel.MapPhoneModels(phoneService.GetAllPhoneModels().ToList());
            newViewModel.MapPhoneConditions(phoneService.GetAllPhoneConditions().ToList());
            newViewModel.MapQuoteStatuses(quoteService.GetAllQuoteStatuses().ToList());
            newViewModel.PostageMethods = quoteService.GetAllPostageMethods().ToList();
            newViewModel.PaymentTypes = quoteService.GetAllPaymentTypes().ToList();
            newViewModel.States = quoteService.GetAllStates().ToList();
            newViewModel.Countries = quoteService.GetAllCountries().ToList();
            return newViewModel;
        }
    }
}
