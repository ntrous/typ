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
        public ActionResult Index(QuoteIndexViewModel viewModel)
        {
            int pageSize = 20;
            QuoteIndexViewModel quoteIndexViewModel = viewModel ?? new QuoteIndexViewModel();
            var quotes = quoteService.SearchQuotes(quoteIndexViewModel.referenceId, quoteIndexViewModel.email, quoteIndexViewModel.lastName, quoteIndexViewModel.firstName, quoteIndexViewModel.statusId);
            quotes = quoteService.GetSortedQuotes(quotes, quoteIndexViewModel);
            if (quotes.Count <= pageSize)
            {
                quoteIndexViewModel.page = 1;
            }

            quoteIndexViewModel.Quotes = quotes.ToPagedList(quoteIndexViewModel.page ?? 1, pageSize);
            quoteIndexViewModel.QuoteStatuses = new SelectList(quoteService.GetAllQuoteStatuses(), "ID", "QuoteStatusName");

            return View(quoteIndexViewModel);
        }

        // GET: Quotes/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = quoteService.GetQuoteById((int)id);
            QuoteDetailsViewModel vm = SetupQuoteDetailsViewModel();
            vm.quote = quote;
            vm.CurrentQuoteStatus = quote.QuoteStatusId;
            vm.phones = quote.Phones.ToList();
            if (quote == null)
            {
                return HttpNotFound();
            }
            return View(vm);
        }

        // POST: Quotes/Details/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(QuoteDetailsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.quote.Phones = viewModel.phones;
                viewModel.UserId = User.Identity.GetUserId();
                bool modified = quoteService.ModifyQuote(viewModel);
                if (!modified)
                {
                    ModelState.AddModelError("Quote", "Error");
                    return View(SetupQuoteDetailsViewModel(viewModel));
                }
                return RedirectToAction("Index");
            }
            var phones = quoteService.GetQuoteById(viewModel.quote.ID).Phones;
            viewModel.phones = phones.ToList();
            return View(SetupQuoteDetailsViewModel(viewModel));
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
            if(viewModel == null)
            {
                newViewModel = new QuoteDetailsViewModel();
            }
            else
            {
                newViewModel = viewModel;
            }

            newViewModel.PhoneStatuses = new SelectList(phoneService.GetAllPhoneStatuses(), "Id", "PhoneStatus");
            newViewModel.PhoneMakes = new SelectList(phoneService.GetAllPhoneMakes(), "ID", "MakeName");
            newViewModel.PhoneModels = new SelectList(phoneService.GetAllPhoneModels(), "ID", "ModelName");
            newViewModel.Conditions = new SelectList(phoneService.GetAllPhoneConditions(), "ID", "Condition");
            newViewModel.QuoteStatuses = new SelectList(quoteService.GetAllQuoteStatuses(), "ID", "QuoteStatusName");
            newViewModel.PostageMethods = new SelectList(quoteService.GetAllPostageMethods(), "ID", "PostageMethodName");
            newViewModel.PaymentTypes = new SelectList(quoteService.GetAllPaymentTypes(), "ID", "PaymentTypeName");
            newViewModel.States = new SelectList(quoteService.GetAllStates(), "ID", "StateNameShort");
            newViewModel.Countries = new SelectList(quoteService.GetAllCountries(), "ID", "CountryName");
            return newViewModel;
        }
    }
}
