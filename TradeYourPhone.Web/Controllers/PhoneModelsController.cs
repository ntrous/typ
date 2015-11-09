using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TradeYourPhone.Core.Enums;
using TradeYourPhone.Core.Models;
using TradeYourPhone.Core.Services.Interface;
using TradeYourPhone.Core.ViewModels;

namespace TradeYourPhone.Web.Controllers
{
    public class PhoneModelsController : Controller
    {
        private IPhoneService phoneService;

        public PhoneModelsController(IPhoneService phoneService)
        {
            this.phoneService = phoneService;
        }

        // GET: PhoneModels
        [Authorize]
        public ActionResult Index()
        {
            var phoneModels = phoneService.GetAllPhoneModels();
            return View(phoneModels.ToList());
        }

        // GET: PhoneModels/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhoneModel phoneModel = phoneService.GetPhoneModelById((int)id);
            if (phoneModel == null)
            {
                return HttpNotFound();
            }
            return View(phoneModel);
        }

        // GET: PhoneModels/Create
        [Authorize]
        public ActionResult Create()
        {
            PhoneModelViewModel viewModel = new PhoneModelViewModel();
            viewModel.PhoneMakes = new SelectList(phoneService.GetAllPhoneMakes(), "ID", "MakeName");
            var conditions = phoneService.GetAllPhoneConditions();
            viewModel.ConditionPrices = new List<PhoneConditionPrice>();
            foreach (var condition in conditions)
            {
                viewModel.ConditionPrices.Add(new PhoneConditionPrice() { PhoneConditionId = condition.ID, PhoneCondition = condition });
            }
            return View(viewModel);
        }

        // POST: PhoneModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PhoneModelViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                bool created = phoneService.CreatePhoneModel(viewModel);
                if (!created)
                {
                    ModelState.AddModelError("PhoneModel", "Phone Model already exists");
                    return View(viewModel);
                }
                return RedirectToAction("Index");
            }

            viewModel.PhoneMakes = new SelectList(phoneService.GetAllPhoneMakes(), "ID", "MakeName");

            return View(viewModel);
        }

        // GET: PhoneModels/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PhoneModelViewModel viewModel = new PhoneModelViewModel();
            viewModel.PhoneMakes = new SelectList(phoneService.GetAllPhoneMakes(), "ID", "MakeName");
            var conditions = phoneService.GetAllPhoneConditions();
            viewModel.ConditionPrices = new List<PhoneConditionPrice>();
            viewModel.Model = phoneService.GetPhoneModelById((int)id);

            viewModel.ConditionPrices = viewModel.Model.PhoneConditionPrices.ToList();
            if(viewModel.ConditionPrices.Count == 0)
            {
                foreach (var condition in conditions)
                {
                    viewModel.ConditionPrices.Add(new PhoneConditionPrice() { PhoneConditionId = condition.ID, PhoneModelId = viewModel.Model.ID, PhoneCondition = condition });
                }
            }

            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }

        // POST: PhoneModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PhoneModelViewModel phoneModelViewModel)
        {
            if (ModelState.IsValid)
            {
                bool modified = phoneService.ModifyPhoneModel(phoneModelViewModel);
                if (!modified)
                {
                    ModelState.AddModelError("PhoneModel", "Phone Model already exists");
                    return View(phoneModelViewModel);
                }
                return RedirectToAction("Index");
            }
            phoneModelViewModel.PhoneMakes = new SelectList(phoneService.GetAllPhoneMakes(), "ID", "MakeName");
            return View(phoneModelViewModel);
        }

        /// <summary>
        /// Get all Phone Models
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        [OutputCache(Duration = (int)TimeEnum.oneweek, VaryByParam = "none", Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult GetPhoneModels()
        {
            var phoneModels = phoneService.GetAllPhoneModelsForView();
            return Json(phoneModels, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get all Phone Models by Make Name
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        [OutputCache(Duration = (int)TimeEnum.oneweek, VaryByParam = "none", Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult GetPhoneModelsByMakeName(string makeName)
        {
            var phoneModels = phoneService.GetPhoneModelsForViewByMakeName(makeName);
            return Json(phoneModels, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get all Phone Models by Make Name
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        [OutputCache(Duration = (int)TimeEnum.oneweek, VaryByParam = "none", Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult GetMostPopularPhoneModels(int limit)
        {
            var phoneModels = phoneService.GetMostPopularPhoneModels(limit);
            return Json(phoneModels, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get all Phone Models by the MakeId
        /// </summary>
        /// <param name="phoneMakeId"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        [OutputCache(Duration = (int)TimeEnum.oneweek, VaryByParam = "none", Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult GetPhoneModelsByMakeId(int phoneMakeId)
        {
            var phoneModels = phoneService.GetPhoneModelsByMakeId(phoneMakeId);
            var result = (from s in phoneModels
                          select new
                          {
                              id = s.ID,
                              name = s.ModelName
                          }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
