using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TradeYourPhone.Core.Models.DomainModels;
using TradeYourPhone.Core.ViewModels;
using static TradeYourPhone.Core.Scraper.PhoneMapper;

namespace TradeYourPhone.Core.Scraper
{
    public class MobileMonsterScraper : IScraper
    {
        public ScrapedPhoneModel ScrapeForPhone(ScrapedPhoneModel phoneModel)
        {
            MappedPhone mappedPhone = MonsterMobileMapping.Where(p => p.PhoneModelId == phoneModel.ID).FirstOrDefault();

            if(mappedPhone != null)
            {
                var phoneRequest = (HttpWebRequest)WebRequest.Create(string.Format("http://mobilemonster.com.au/sell-your-phone{0}", mappedPhone.link));

                var phoneResponse = (HttpWebResponse)phoneRequest.GetResponse();

                var phoneString = new StreamReader(phoneResponse.GetResponseStream()).ReadToEnd();

                int newPosition = phoneString.IndexOf(@"""new"" data-price=");
                string newPrice = Regex.Replace(phoneString.Substring(newPosition + 18, 7), "[^.0-9]", "");
                if (Utilities.Math.IsDecimal(newPrice))
                {
                    var price = phoneModel.PhonePrices.Where(x => x.PhoneConditionId == 1).FirstOrDefault();
                    price.CompetitorAmount = Convert.ToDecimal(newPrice);
                    price.PercentageDifference = Utilities.Math.CalculatePercentageChange(price.OldAmount, price.CompetitorAmount);
                    price.NewAmount = price.CompetitorAmount + (price.CompetitorAmount * (decimal)0.05);
                }

                int goodPosition = phoneString.IndexOf(@"""working"" data-price=");
                string goodPrice = Regex.Replace(phoneString.Substring(goodPosition + 22, 7), "[^.0-9]", "");
                if (Utilities.Math.IsDecimal(goodPrice))
                {
                    var price = phoneModel.PhonePrices.Where(x => x.PhoneConditionId == 2).FirstOrDefault();
                    price.CompetitorAmount = Convert.ToDecimal(goodPrice);
                    price.PercentageDifference = Utilities.Math.CalculatePercentageChange(price.OldAmount, price.CompetitorAmount);
                    price.NewAmount = price.CompetitorAmount + (price.CompetitorAmount * (decimal)0.05);
                }

                int faultyPosition = phoneString.IndexOf(@"""dead"" data-price=");
                string faultyPrice = Regex.Replace(phoneString.Substring(faultyPosition + 19, 7), "[^.0-9]", "");
                if (Utilities.Math.IsDecimal(faultyPrice))
                {
                    var price = phoneModel.PhonePrices.Where(x => x.PhoneConditionId == 3).FirstOrDefault();
                    price.CompetitorAmount = Convert.ToDecimal(faultyPrice);
                    price.PercentageDifference = Utilities.Math.CalculatePercentageChange(price.OldAmount, price.CompetitorAmount);
                    price.NewAmount = price.CompetitorAmount + (price.CompetitorAmount * (decimal)0.05);
                }
            }
            
            return phoneModel;
        }
    }
}
