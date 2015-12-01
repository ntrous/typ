using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TradeYourPhone.Core.Models;
using TradeYourPhone.Core.Services.Interface;
using TradeYourPhone.Core.ViewModels;
using TradeYourPhone.Core.Models.DomainModels;
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
            QuoteIndexViewModel quoteIndexViewModel = quoteService.GetQuotes(viewModel);
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
            QuoteDetailsResult addPhoneResult = quoteService.AddPhoneToQuote(key, modelId, conditionId);
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
            QuoteDetailsResult result = new QuoteDetailsResult();
            if (ModelState.IsValid)
            {
                result = quoteService.SaveQuote(key, viewModel);
            }
            else
            {
                result.Status = "Error";
                result.Exception = new QuoteDetailsException(new Exception("Customer object is not valid"));
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
            QuoteDetailsResult result = new QuoteDetailsResult();
            if (ModelState.IsValid)
            {
                result = quoteService.FinaliseQuote(key, viewModel);
            }
            else
            {
                result.Status = "Error";
                result.Exception = new QuoteDetailsException(new Exception("Customer object is not valid"));
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
            QuoteDetailsResult result = quoteService.DeletePhoneFromQuote(key, phoneId);
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
            QuoteDetailsResult quoteDetailsResult = quoteService.GetQuoteDetailsByQuoteReferenceId(key);
            return Json(quoteDetailsResult, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [OutputCache(Duration = (int)TimeEnum.oneweek, VaryByParam = "none", Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult GetStates()
        {
            var phoneModels = quoteService.GetAllStates();
            var result = (from s in phoneModels
                          select new
                          {
                              id = s.ID,
                              name = s.StateNameShort
                          }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
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
            var result = (from s in paymentTypes
                          select new
                          {
                              id = s.ID,
                              name = s.PaymentTypeName
                          }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
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
            var result = (from s in postageMethods
                          select new
                          {
                              Id = s.Id,
                              PostageMethodName = s.PostageMethodName,
                              Description = s.Description
                          }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
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
