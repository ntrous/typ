using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TradeYourPhone.Core.Models;
using TradeYourPhone.Core.Scraper;
using TradeYourPhone.Core.Services.Interface;
using TradeYourPhone.Core.ViewModels;

namespace TradeYourPhone.Core.Services.Implementation
{
    public class ScraperService : IScraperService
    {
        private IPhoneService phoneService;
        private IScraper mobileMonsterScraper;

        public ScraperService(IPhoneService phoneService, IScraper mobileMonsterScraper)
        {
            this.phoneService = phoneService;
            this.mobileMonsterScraper = mobileMonsterScraper;
        }

        /// <summary>
        /// Gets current list of prices for all Phone Models
        /// </summary>
        /// <returns></returns>
        public PriceScraperViewModel GetCurrentPhoneModelPrices()
        {
            PriceScraperViewModel viewModel = new PriceScraperViewModel
            {
                PhoneModels = new List<ScrapedPhoneModel>()
            };

            foreach (var model in phoneService.GetAllPhoneModels())
            {
                ScrapedPhoneModel phoneModel = new ScrapedPhoneModel();
                phoneModel.PopulateFromPhoneModel(model);
                viewModel.PhoneModels.Add(phoneModel);
            }

            return viewModel;
        }

        /// <summary>
        /// Gets scraped prices for all the 'Selected' Phone Models
        /// </summary>
        /// <param name="scrapedPhoneModels"></param>
        /// <returns></returns>
        public PriceScraperViewModel GetScrapedPrices(PriceScraperViewModel viewModel)
        {
            foreach(var phoneModel in viewModel.PhoneModels.Where(p => p.Selected == true))
            {
                // Add a random delay so bot appears more human when doing http requests
                Random r = new Random();
                int rInt = r.Next(1000, 5000);
                Thread.Sleep(rInt);

                ScrapedPhoneModel mobileMonsterScrapedPhone = mobileMonsterScraper.ScrapeForPhone(phoneModel);
            }

            return viewModel;
        }
    }
}
