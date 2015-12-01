using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradeYourPhone.Core.Services.Interface;
using TradeYourPhone.Core.ViewModels;
using TradeYourPhone.Web.ActionResults;

namespace TradeYourPhone.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private IReportingService reportingService;

        public DashboardController(IReportingService reportingService)
        {
            this.reportingService = reportingService;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetDashboardData(DashboardViewModel viewModel)
        {
            JsonNetResult result = new JsonNetResult();
            result.SerializerSettings.DateFormatString = "yyyy-MM-dd";
            result.Data = reportingService.GetDashboardData(viewModel.DateFrom, viewModel.DateTo);
            return result;
        }
    }
}