using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TradeYourPhone.Core.Services.Interface;
using TradeYourPhone.Core.Models;

namespace TradeYourPhone.Web.Controllers
{
    public class PhoneConditionPricesController : Controller
    {
        private IPhoneService phoneService;

        public PhoneConditionPricesController(IPhoneService phoneService)
        {
            this.phoneService = phoneService;
        }

        // GET: PhoneConditionPrices
        [Authorize]
        public ActionResult Index()
        {
            var phoneConditionPrices = phoneService.GetAllPhoneConditionPrices();
            return View(phoneConditionPrices.ToList());
        }

        // GET: PhoneConditionPrices/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhoneConditionPrice phoneConditionPrice = phoneService.GetPhoneConditionPriceById((int)id);
            if (phoneConditionPrice == null)
            {
                return HttpNotFound();
            }
            return View(phoneConditionPrice);
        }

        // GET: PhoneConditionPrices/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.PhoneConditionId = new SelectList(phoneService.GetAllPhoneConditions(), "ID", "Condition");
            ViewBag.PhoneModelId = new SelectList(phoneService.GetAllPhoneModels(), "ID", "ModelName");
            return View();
        }

        // POST: PhoneConditionPrices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PhoneModelId,PhoneConditionId,OfferAmount")] PhoneConditionPrice phoneConditionPrice)
        {
            if (ModelState.IsValid)
            {
                bool created = phoneService.CreatePhoneConditionPrice(phoneConditionPrice);
                if (!created)
                {
                    ModelState.AddModelError("PhoneConditionPrice", "Phone Condition Price already exists");
                    return View(phoneConditionPrice);
                }
                return RedirectToAction("Index");
            }

            ViewBag.PhoneConditionId = new SelectList(phoneService.GetAllPhoneConditions(), "ID", "Condition", phoneConditionPrice.PhoneConditionId);
            ViewBag.PhoneModelId = new SelectList(phoneService.GetAllPhoneModels(), "ID", "ModelName", phoneConditionPrice.PhoneModelId);
            return View(phoneConditionPrice);
        }

        // GET: PhoneConditionPrices/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhoneConditionPrice phoneConditionPrice = phoneService.GetPhoneConditionPriceById((int)id);
            if (phoneConditionPrice == null)
            {
                return HttpNotFound();
            }
            ViewBag.PhoneConditionId = new SelectList(phoneService.GetAllPhoneConditions(), "ID", "Condition", phoneConditionPrice.PhoneConditionId);
            ViewBag.PhoneModelId = new SelectList(phoneService.GetAllPhoneModels(), "ID", "ModelName", phoneConditionPrice.PhoneModelId);
            return View(phoneConditionPrice);
        }

        // POST: PhoneConditionPrices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PhoneModelId,PhoneConditionId,OfferAmount")] PhoneConditionPrice phoneConditionPrice)
        {
            if (ModelState.IsValid)
            {
                bool modified = phoneService.ModifyPhoneConditionPrice(phoneConditionPrice);
                if (!modified)
                {
                    ModelState.AddModelError("PhoneConditionPrice", "Phone Condition Price already exists");
                    return View(phoneConditionPrice);
                }
                return RedirectToAction("Index");
            }
            ViewBag.PhoneConditionId = new SelectList(phoneService.GetAllPhoneConditions(), "ID", "Condition", phoneConditionPrice.PhoneConditionId);
            ViewBag.PhoneModelId = new SelectList(phoneService.GetAllPhoneModels(), "ID", "ModelName", phoneConditionPrice.PhoneModelId);
            return View(phoneConditionPrice);
        }

        // GET: PhoneConditionPrices/Delete/5
        //[Authorize]
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    PhoneConditionPrice phoneConditionPrice = phoneService.GetPhoneConditionPriceById((int)id);
        //    if (phoneConditionPrice == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(phoneConditionPrice);
        //}

        //// POST: PhoneConditionPrices/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //[Authorize]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    phoneService.DeletePhoneConditionPriceById(id);
        //    return RedirectToAction("Index");
        //}

        /// <summary>
        /// Get the Price for the selected model and condition
        /// </summary>
        /// <param name="PhoneModelId"></param>
        /// <param name="PhoneConditionId"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetPhoneConditionPrice(int PhoneModelId, int PhoneConditionId)
        {
            var phoneConditionPrice = phoneService.GetPhoneConditionPrice(PhoneModelId, PhoneConditionId);
            if (phoneConditionPrice != null)
            {
                return Json(phoneConditionPrice.OfferAmount, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("N/A", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
