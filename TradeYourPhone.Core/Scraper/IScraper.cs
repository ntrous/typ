using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Core.Models.DomainModels;
using TradeYourPhone.Core.ViewModels;

namespace TradeYourPhone.Core.Scraper
{
    public interface IScraper
    {
        ScrapedPhoneModel ScrapeForPhone(ScrapedPhoneModel phoneModel);
    }
}
