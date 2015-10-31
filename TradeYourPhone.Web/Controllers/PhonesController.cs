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
using PagedList;
using Microsoft.AspNet.Identity;

namespace TradeYourPhone.Web.Controllers
{
    [Authorize]
    public class PhonesController : Controller
    {
        private IPhoneService phoneService;

        public PhonesController(IPhoneService phoneService)
        {
            this.phoneService = phoneService;
        }

        // GET: Phones
        public ActionResult Index(PhoneIndexViewModel viewModel)
        {
            int pageSize = 10;
            PhoneIndexViewModel phoneIndexViewModel = viewModel ?? new PhoneIndexViewModel();
            var phones = phoneService.SearchPhones(phoneIndexViewModel.PhoneId, phoneIndexViewModel.PhoneMakeId, phoneIndexViewModel.PhoneModelId, phoneIndexViewModel.PhoneStatusId);
            phones = phoneService.GetSortedPhones(phones, phoneIndexViewModel);
            if (phones.Count <= pageSize)
            {
                phoneIndexViewModel.page = 1;
            }

            phoneIndexViewModel.Phones = phones.ToPagedList(viewModel.page ?? 1, 10);
            phoneIndexViewModel.PhoneMakes = new SelectList(phoneService.GetAllPhoneMakes(), "ID", "MakeName");
            phoneIndexViewModel.PhoneModels = new SelectList(phoneService.GetPhoneModelsByMakeId(phoneIndexViewModel.PhoneMakeId), "ID", "ModelName");
            phoneIndexViewModel.PhoneStatuses = new SelectList(phoneService.GetAllPhoneStatuses(), "ID", "PhoneStatus");

            return View(phoneIndexViewModel);
        }

        // GET: Phones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone phone = phoneService.GetPhoneById((int)id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            return View(phone);
        }

        // GET: Phones/Create
        public ActionResult Create()
        {
            ViewBag.PhoneConditionId = new SelectList(phoneService.GetAllPhoneConditions(), "ID", "Condition");
            ViewBag.PhoneMakeId = new SelectList(phoneService.GetAllPhoneMakes(), "ID", "MakeName");
            ViewBag.PhoneModelId = Enumerable.Empty<SelectListItem>();
            ViewBag.PhoneStatusId = new SelectList(phoneService.GetAllPhoneStatuses(), "Id", "PhoneStatus");
            return View();
        }

        // POST: Phones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,QuoteId,PhoneMakeId,PhoneModelId,PhoneConditionId,PurchaseAmount,SaleAmount,PhoneStatusId")] Phone phone)
        {
            if (ModelState.IsValid)
            {
                phoneService.CreatePhone(phone);
                return RedirectToAction("Index");
            }

            ViewBag.PhoneConditionId = new SelectList(phoneService.GetAllPhoneConditions(), "ID", "Condition");
            ViewBag.PhoneMakeId = new SelectList(phoneService.GetAllPhoneMakes(), "ID", "MakeName");
            ViewBag.PhoneModelId = new SelectList(phoneService.GetAllPhoneModels(), "ID", "ModelName");
            ViewBag.PhoneStatusId = new SelectList(phoneService.GetAllPhoneStatuses(), "Id", "PhoneStatus");
            return View(phone);
        }

        // GET: Phones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone phone = phoneService.GetPhoneById((int)id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            ViewBag.PhoneConditionId = new SelectList(phoneService.GetAllPhoneConditions(), "ID", "Condition", phone.PhoneConditionId);
            ViewBag.PhoneMakeId = new SelectList(phoneService.GetAllPhoneMakes(), "ID", "MakeName", phone.PhoneMakeId);
            ViewBag.PhoneModelId = new SelectList(phoneService.GetPhoneModelsByMakeId(phone.PhoneMakeId), "ID", "ModelName", phone.PhoneModelId);
            ViewBag.PhoneStatusId = new SelectList(phoneService.GetAllPhoneStatuses(), "Id", "PhoneStatus", phone.PhoneStatusId);
            return View(phone);
        }

        // POST: Phones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,QuoteId,PhoneMakeId,PhoneModelId,PhoneConditionId,IMEI,PurchaseAmount,SaleAmount,PhoneStatusId")] Phone phone)
        {
            if (ModelState.IsValid)
            {
                phoneService.ModifyPhone(phone, User.Identity.GetUserId());
                return RedirectToAction("Index");
            }
            ViewBag.PhoneConditionId = new SelectList(phoneService.GetAllPhoneConditions(), "ID", "Condition", phone.PhoneConditionId);
            ViewBag.PhoneMakeId = new SelectList(phoneService.GetAllPhoneMakes(), "ID", "MakeName", phone.PhoneMakeId);
            ViewBag.PhoneModelId = new SelectList(phoneService.GetPhoneModelsByMakeId(phone.PhoneMakeId), "ID", "ModelName", phone.PhoneModelId);
            ViewBag.PhoneStatusId = new SelectList(phoneService.GetAllPhoneStatuses(), "Id", "PhoneStatus", phone.PhoneStatusId);
            return View(phone);
        }

        // GET: Phones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone phone = phoneService.GetPhoneById((int)id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            return View(phone);
        }

        // POST: Phones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            phoneService.DeletePhoneById((int)id);
            return RedirectToAction("Index");
        }
    }
}
