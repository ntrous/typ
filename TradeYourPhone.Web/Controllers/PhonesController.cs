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
using TradeYourPhone.Core.DTO;

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
        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetPhones(PhoneIndexViewModel viewModel)
        {
            var phones = phoneService.GetPhones(viewModel);

            return Json(phones, JsonRequestBehavior.AllowGet);
        }

        // GET: Phones/Details/5
        [Authorize]
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

        // POST: Phones/CreatePhone
        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreatePhone(PhoneDTO phone)
        {
            phoneService.CreatePhone(phone);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        // GET: Phones/Edit/5
        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetPhone(int? id)
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
            PhoneDetailsViewModel viewModel = new PhoneDetailsViewModel();
            viewModel.MapPhone(phone);
            viewModel.MapPhoneConditions(phoneService.GetAllPhoneConditions().ToList());
            viewModel.MapPhoneMakes(phoneService.GetAllPhoneMakes().ToList());
            viewModel.MapPhoneModels(phoneService.GetAllPhoneModels().ToList());
            viewModel.MapPhoneStatuses(phoneService.GetAllPhoneStatuses().ToList());

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        // POST: Phones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SavePhoneDetails(PhoneDTO phoneDTO)
        {
            Phone phone = phoneService.GetPhoneById(phoneDTO.Id);
            phone.UpdateFromDTO(phoneDTO);
            var updatedPhone = phoneService.ModifyPhone(phone, User.Identity.GetUserId());
            phoneDTO.MapToDTO(updatedPhone);

            return Json(phoneDTO, JsonRequestBehavior.AllowGet);
        }

        // GET: Phones/Edit/5
        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetPhoneReferenceData()
        {
            PhoneDetailsViewModel viewModel = new PhoneDetailsViewModel();
            viewModel.MapPhoneConditions(phoneService.GetAllPhoneConditions().ToList());
            viewModel.MapPhoneMakes(phoneService.GetAllPhoneMakes().ToList());
            viewModel.MapPhoneModels(phoneService.GetAllPhoneModels().ToList());
            viewModel.MapPhoneStatuses(phoneService.GetAllPhoneStatuses().ToList());

            return Json(viewModel, JsonRequestBehavior.AllowGet);
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
