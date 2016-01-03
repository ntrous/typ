using System;
using System.Web.Mvc;
using TradeYourPhone.Core.Services.Interface;
using TradeYourPhone.Core.Utilities;
using TradeYourPhone.Web.ViewModels;
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
            var dateFrom = viewModel.DateFrom == DateTime.MinValue ? DateTime.Today : viewModel.DateFrom;
            var dateTo = viewModel.DateTo == DateTime.MinValue ? DateTime.Today : viewModel.DateTo;
            dateTo = dateTo.Date.AddDays(1).AddSeconds(-1);

            var vm = new DashboardViewModel
            {
                DateFrom = Util.GetAEST(dateFrom.Date),
                DateTo = Util.GetAEST(dateTo),
                NoOfCreatedQuotes = reportingService.GetTotalQuotesCreated(dateFrom, dateTo),
                NoOfFinalisedQuotes = reportingService.GetTotalFinalisedQuotes(dateFrom, dateTo),
                TotalAmountToPay = reportingService.GetTotalAmountToBePaid(),
                TotalAmountPaid = reportingService.GetTotalAmountPaid(dateFrom, dateTo),
                NoOfCompletedQuotes = reportingService.GetTotalCompletedQuotes(dateFrom, dateTo),
                TotalDevicesSold = reportingService.GetTotalDevicesSold(dateFrom, dateTo),
                TotalIncomeAmount = reportingService.GetTotalIncomeAmount(dateFrom, dateTo),
                TotalAssetWorth = reportingService.GetTotalAssetWorth(),
                TotalProfit = reportingService.GetTotalProfit(dateFrom, dateTo),
                PercentageOfCompletedQuotes = reportingService.GetPercentageOfQuotesCompleted(50),
                PercentageOfCompletedSatchelQuotes = reportingService.GetPercentageOfSatchelQuotesCompleted(50)
            };

            var result = new JsonNetResult
            {
                SerializerSettings = {DateFormatString = "yyyy-MM-dd"},
                Data = vm
            };
            return result;
        }
    }
}