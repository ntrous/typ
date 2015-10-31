using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Core.Scraper;
using TradeYourPhone.Core.ViewModels;

namespace TradeYourPhone.Core.Services.Interface
{
    public interface IScraperService
    {
        /// <summary>
        /// Gets all current Phone Models and their prices
        /// </summary>
        /// <returns></returns>
        PriceScraperViewModel GetCurrentPhoneModelPrices();

        /// <summary>
        /// Gets the specified Phone Models with the highest scraped prices
        /// </summary>
        /// <param name="scrapedPhoneModels"></param>
        /// <returns></returns>
        PriceScraperViewModel GetScrapedPrices(PriceScraperViewModel viewModel);
    }
}
