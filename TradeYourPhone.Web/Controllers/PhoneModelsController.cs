using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TradeYourPhone.Core.DTO;
using TradeYourPhone.Core.Enums;
using TradeYourPhone.Core.Models;
using TradeYourPhone.Core.Services.Interface;
using TradeYourPhone.Web.ViewModels;

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
        public ActionResult GetPhoneModelsForAdminView()
        {
            var viewModel = new PhoneModelIndexViewModel();
            var models = phoneService.GetAllPhoneModels();
            models = models.OrderBy(m => m.ModelName);
            viewModel.MapPhoneModels(models.ToList());

            return Json(viewModel, JsonRequestBehavior.AllowGet);
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
            var viewModel = new PhoneModelViewModel();
            var model = phoneService.GetPhoneModelById(id);
            viewModel.Model = new PhoneModelDTO();
            viewModel.Model.Map(model);
            viewModel.MapPhoneMakes(phoneService.GetAllPhoneMakes().ToList());

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetCreatePhoneModelViewModel()
        {
            CreatePhoneModelViewModel viewModel = new CreatePhoneModelViewModel();
            viewModel.Model = new PhoneModelDTO {PhoneConditionPrices = new List<PhoneConditionPriceDTO>()};

            var conditions = phoneService.GetAllPhoneConditions().ToList();
            foreach (var condition in conditions)
            {
                PhoneConditionDTO conditionDTO = new PhoneConditionDTO();
                conditionDTO.Map(condition);
                viewModel.Model.PhoneConditionPrices.Add(new PhoneConditionPriceDTO { PhoneConditionId = condition.ID, PhoneCondition = conditionDTO });
            }

            viewModel.MapPhoneMakes(phoneService.GetAllPhoneMakes().ToList());
            viewModel.MapPhoneConditions(phoneService.GetAllPhoneConditions().ToList());

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
                PhoneModel model = new PhoneModel();
                model.UpdateFromDTO(phoneModelViewModel.Model);
                success = phoneService.CreatePhoneModel(model);
            }
            else
            {
                PhoneModel model = phoneService.GetPhoneModelById(phoneModelViewModel.Model.ID);
                model.UpdateFromDTO(phoneModelViewModel.Model);
                success = phoneService.ModifyPhoneModel(model);
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
            var viewModel = new PhoneModelsViewModel();
            var phoneModels = phoneService.GetAllPhoneModels();
            viewModel.MapPhoneModels(phoneModels.ToList());

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get all Phone Models by Make Name
        /// </summary>
        /// <returns></returns>
        
        [AcceptVerbs(HttpVerbs.Get)]
        [OutputCache(Duration = (int)TimeEnum.oneweek, VaryByParam = "none", Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult GetPhoneModelsByMakeName(string makeName)
        {
            var phoneModels = phoneService.GetPhoneModelsByMakeName(makeName);

            var viewModel = new PhoneModelsViewModel();
            viewModel.MapPhoneModels(phoneModels.ToList());

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get all Phone Models by Make Name
        /// </summary>
        /// <returns></returns>
        
        [AcceptVerbs(HttpVerbs.Get)]
        [OutputCache(Duration = (int)TimeEnum.oneweek, VaryByParam = "none", Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult GetMostPopularPhoneModels(int limit)
        {
            var viewModel = new PhoneModelsViewModel();
            var phoneModels = phoneService.GetMostPopularPhoneModels(limit);
            viewModel.MapPhoneModels(phoneModels.ToList());

            return Json(viewModel, JsonRequestBehavior.AllowGet);
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
