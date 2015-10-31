using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradeYourPhone.Core.Services.Interface;
using TradeYourPhone.Core.ViewModels;

namespace TradeYourPhone.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private IQuoteService quoteService;

        public DashboardController(IQuoteService quoteService)
        {
            this.quoteService = quoteService;
        }

        // GET: Dashboard
        public ActionResult Index()
        {
            var viewModel = quoteService.GetDashboardData(null, null);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(DashboardViewModel viewModel)
        {
            viewModel = quoteService.GetDashboardData(viewModel.DateFrom, viewModel.DateTo);
            return View(viewModel);
        }
    }
}