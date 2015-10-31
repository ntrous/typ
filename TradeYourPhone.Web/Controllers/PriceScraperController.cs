using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradeYourPhone.Core.Scraper;
using TradeYourPhone.Core.Services.Interface;
using TradeYourPhone.Core.ViewModels;

namespace TradeYourPhone.Web.Controllers
{
    [Authorize]
    public class PriceScraperController : Controller
    {
        private IScraperService scraperService;

        public PriceScraperController(IScraperService scraperService)
        {
            this.scraperService = scraperService;
        }

        // GET: PriceScraper
        public ActionResult Index()
        {
            var phoneModels = scraperService.GetCurrentPhoneModelPrices();

            return View(phoneModels);
        }

        [HttpPost]
        public ActionResult Index(PriceScraperViewModel viewModel)
        {
            viewModel = scraperService.GetScrapedPrices(viewModel);

            return View(viewModel);
        }
    }
}