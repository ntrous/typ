using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Core.Models;

namespace TradeYourPhone.Core.ViewModels
{
    public class PriceScraperViewModel
    {
        public IList<ScrapedPhoneModel> PhoneModels { get; set; }
    }

    public class ScrapedPhoneModel
    {
        public bool Selected { get; set; }
        public int ID { get; set; }
        public string ModelName { get; set; }
        public int PhoneMakeId { get; set; }

        public List<ScrapedPhonePrice> PhonePrices { get; set; }

        /// <summary>
        /// Takes a PhoneModel and populates a ScrapedPhoneModel object
        /// </summary>
        /// <param name="phoneModel"></param>
        public void PopulateFromPhoneModel(PhoneModel phoneModel)
        {
            ID = phoneModel.ID;
            ModelName = phoneModel.PhoneMake.MakeName + " " + phoneModel.ModelName;
            PhoneMakeId = phoneModel.PhoneMakeId;

            PhonePrices = new List<ScrapedPhonePrice>();
            foreach (var price in phoneModel.PhoneConditionPrices)
            {
                ScrapedPhonePrice phonePrice = new ScrapedPhonePrice
                {
                    ID = price.ID,
                    PhoneModelId = price.PhoneModelId,
                    PhoneConditionId = price.PhoneConditionId,
                    OldAmount = price.OfferAmount
                };

                PhonePrices.Add(phonePrice);
            }
        }
    }

    public class ScrapedPhonePrice
    {
        public int ID { get; set; }
        public int PhoneModelId { get; set; }
        public int PhoneConditionId { get; set; }
        public decimal OldAmount { get; set; }
        public decimal CompetitorAmount { get; set; }
        public decimal NewAmount { get; set; }
        public decimal PercentageDifference { get; set; }
    }
}
