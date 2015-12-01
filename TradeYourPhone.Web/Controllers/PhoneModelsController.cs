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
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetPhoneModelsForView()
        {
            var phoneModelsViewModel = phoneService.GetPhoneModelsForAdminView();
            return Json(phoneModelsViewModel, JsonRequestBehavior.AllowGet);
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

        // GET: PhoneModels/Edit/5
        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetPhoneModel(int id)
        {
            var viewModel = phoneService.GetPhoneModelForAdminView(id);

            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetCreatePhoneModelViewModel()
        {
            var viewModel = phoneService.GetCreatePhoneModelViewModel();

            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        // POST: PhoneModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SavePhoneModel(PhoneModelViewModel phoneModelViewModel)
        {
            bool success = false;
            if (phoneModelViewModel.Model.ID == 0)
            {
                success = phoneService.CreatePhoneModel(phoneModelViewModel);
            }
            else
            {
                success = phoneService.ModifyPhoneModel(phoneModelViewModel);
            }

            return Json(success, JsonRequestBehavior.AllowGet);
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
