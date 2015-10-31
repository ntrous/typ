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

namespace TradeYourPhone.Web.Controllers
{
    public class PhoneConditionsController : Controller
    {
        private IPhoneService phoneService;

        public PhoneConditionsController(IPhoneService phoneService)
        {
            this.phoneService = phoneService;
        }

        // GET: PhoneConditions
        [Authorize]
        public ActionResult Index()
        {
            return View(phoneService.GetAllPhoneConditions());
        }

        // GET: PhoneConditions/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhoneCondition phoneCondition = phoneService.GetPhoneConditionById((int)id);
            if (phoneCondition == null)
            {
                return HttpNotFound();
            }
            return View(phoneCondition);
        }

        // GET: PhoneConditions/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhoneConditions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Condition")] PhoneCondition phoneCondition)
        {
            if (ModelState.IsValid)
            {
                bool created = phoneService.CreatePhoneCondition(phoneCondition);
                if (!created)
                {
                    ModelState.AddModelError("PhoneCondition", "Phone Condition already exists");
                    return View(phoneCondition);
                }
                return RedirectToAction("Index");
            }

            return View(phoneCondition);
        }

        // GET: PhoneConditions/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhoneCondition phoneCondition = phoneService.GetPhoneConditionById((int)id);
            if (phoneCondition == null)
            {
                return HttpNotFound();
            }
            return View(phoneCondition);
        }

        // POST: PhoneConditions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Condition")] PhoneCondition phoneCondition)
        {
            if (ModelState.IsValid)
            {
                bool modified = phoneService.ModifyPhoneCondition(phoneCondition);
                if (!modified)
                {
                    ModelState.AddModelError("PhoneCondition", "Phone Condition already exists");
                    return View(phoneCondition);
                }
                return RedirectToAction("Index");
            }
            return View(phoneCondition);
        }

        // GET: PhoneConditions/Delete/5
        //[Authorize]
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    PhoneCondition phoneCondition = phoneService.GetPhoneConditionById((int)id);
        //    if (phoneCondition == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(phoneCondition);
        //}

        //// POST: PhoneConditions/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //[Authorize]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    phoneService.DeletePhoneConditionById(id);
        //    return RedirectToAction("Index");
        //}

        /// <summary>
        /// Get all Phone Conditions
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        [OutputCache(Duration = (int)TimeEnum.oneweek, VaryByParam = "none", Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult GetPhoneConditions()
        {
            var phoneConditions = phoneService.GetAllPhoneConditions();
            var result = (from s in phoneConditions
                          select new
                          {
                              id = s.ID,
                              name = s.Condition
                          }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
