using System.Linq;
using System.Net;
using System.Web.Mvc;
using TradeYourPhone.Core.Models;
using TradeYourPhone.Core.Services.Interface;
using TradeYourPhone.Web.ViewModels;
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
            PhoneIndexViewModel phoneIndexViewModel = viewModel ?? new PhoneIndexViewModel();
            phoneIndexViewModel.PageSize = 20;
            var phones = phoneService.SearchPhones(phoneIndexViewModel.PhoneId, phoneIndexViewModel.PhoneMakeId, phoneIndexViewModel.PhoneModelId, phoneIndexViewModel.PhoneStatusId);

            viewModel.PhoneMakeParm = string.IsNullOrEmpty(viewModel.SortOrder) ? "phoneMake_desc" : "";
            viewModel.PhoneModelParm = viewModel.SortOrder == "phoneModel_asc" ? "phoneModel_desc" : "phoneModel_asc";
            viewModel.PhoneConditionParm = viewModel.SortOrder == "phoneCondition_asc" ? "phoneCondition_desc" : "phoneCondition_asc";
            viewModel.PhoneStatusParm = viewModel.SortOrder == "phoneStatus_asc" ? "phoneStatus_desc" : "phoneStatus_asc";
            viewModel.PurchaseAmountParm = viewModel.SortOrder == "purchaseAmount_asc" ? "purchaseAmount_desc" : "purchaseAmount_asc";
            viewModel.SaleAmountParm = viewModel.SortOrder == "saleAmount_asc" ? "saleAmount_desc" : "saleAmount_asc";
            viewModel.PhoneIdParm = viewModel.SortOrder == "phoneId_asc" ? "phoneId_desc" : "phoneId_asc";
            phones = phoneService.GetSortedPhones(phones, viewModel.SortOrder);
            phoneIndexViewModel.TotalPhones = phones.Count;

            if (phoneIndexViewModel.PageNumber == 0 || (phoneIndexViewModel.PageNumber > (int)System.Math.Ceiling(((double)phoneIndexViewModel.TotalPhones / (double)phoneIndexViewModel.PageSize))))
            { phoneIndexViewModel.PageNumber = 1; }

            var pagedPhones = phones.ToPagedList(phoneIndexViewModel.PageNumber, phoneIndexViewModel.PageSize);

            phoneIndexViewModel.MapPhones(pagedPhones);
            phoneIndexViewModel.MapPhoneMakes(phoneService.GetAllPhoneMakes().ToList());
            phoneIndexViewModel.MapPhoneModels(phoneService.GetAllPhoneModels().ToList());
            phoneIndexViewModel.MapPhoneStatuses(phoneService.GetAllPhoneStatuses().ToList());

            return Json(phoneIndexViewModel, JsonRequestBehavior.AllowGet);
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
        public ActionResult CreatePhone(PhoneDTO phoneDto)
        {
            Phone phone = new Phone();
            phone.UpdateFromDTO(phoneDto);
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
