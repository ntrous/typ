using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradeYourPhone.Web.ViewModels;

namespace TradeYourPhone.Web.Controllers
{
    [Authorize]
    public class CacheController : Controller
    {
        [HttpPost]
        public ActionResult ClearCache(CacheManagerViewModel viewModel)
        {
            bool result = true;
            try {
                if (viewModel.ClearPhoneModels)
            {
                var urlToRemove = Url.Action("GetPhoneModels", "PhoneModels");
                HttpResponse.RemoveOutputCacheItem(urlToRemove);

                var urlToRemove2 = Url.Action("GetPhoneModelsByMakeName", "PhoneModels");
                HttpResponse.RemoveOutputCacheItem(urlToRemove2);

                var urlToRemove3 = Url.Action("GetPhoneModelsByMakeId", "PhoneModels");
                HttpResponse.RemoveOutputCacheItem(urlToRemove3);

                var urlToRemove4 = Url.Action("GetMostPopularPhoneModels", "PhoneModels");
                HttpResponse.RemoveOutputCacheItem(urlToRemove4);
            }

            if (viewModel.ClearPhoneConditions)
            {
                var urlToRemove = Url.Action("GetPhoneConditions", "PhoneConditions");
                HttpResponse.RemoveOutputCacheItem(urlToRemove);
            }

            if (viewModel.ClearPaymentTypes)
            {
                var urlToRemove = Url.Action("GetPaymentTypes", "Quotes");
                HttpResponse.RemoveOutputCacheItem(urlToRemove);

                var urlToRemove2 = Url.Action("GetPaymentTypeNames", "Quotes");
                HttpResponse.RemoveOutputCacheItem(urlToRemove2);
            }

            if (viewModel.ClearPostageMethods)
            {
                var urlToRemove = Url.Action("GetPostageMethods", "Quotes");
                HttpResponse.RemoveOutputCacheItem(urlToRemove);
            }
            }
            catch(Exception ex)
            {
                result = false;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}