using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TradeYourPhone.Core.DTO;
using TradeYourPhone.Core.Models;
using TradeYourPhone.Core.Services.Interface;
using TradeYourPhone.Core.Utilities;
using TradeYourPhone.Web.ViewModels;

namespace TradeYourPhone.Web.Controllers
{
    public class PhoneMakesController : Controller
    {
        private IPhoneService phoneService;

        public PhoneMakesController(IPhoneService phoneService)
        {
            this.phoneService = phoneService;
        }

        /// <summary>
        /// Get all Phone Makes
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetPhoneMakes()
        {
            var phoneMakes = phoneService.GetAllPhoneMakes();
            var result = (from s in phoneMakes
                          select new
                          {
                              id = s.ID,
                              name = s.MakeName
                          }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult GetPhoneMake(int id)
        {
            var phoneMake = phoneService.GetPhoneMakeById(id);
            PhoneMakeDTO make = new PhoneMakeDTO();
            make.Map(phoneMake);

            return Json(make, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Saves an existing or new Phone Make
        /// </summary>
        /// <param name="phoneModelViewModel"></param>
        /// <returns></returns>
        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SavePhoneMake(PhoneMakeDTO phoneMake)
        {
            bool success = false;
            if (phoneMake.ID == 0)
            {
                success = phoneService.CreatePhoneMake(phoneMake.Name);
            }
            else
            {
                var make = new PhoneMake();
                make.UpdateFromDTO(phoneMake);
                success = phoneService.ModifyPhoneMake(make);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
