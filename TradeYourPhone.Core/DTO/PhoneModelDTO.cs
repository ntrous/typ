using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Core.Models;

namespace TradeYourPhone.Core.DTO
{
    public class PhoneModelDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int PhoneMakeId { get; set; }
        public string PhoneMake { get; set; }
        public string PrimaryImageString { get; set; }
        public IList<PhoneConditionPriceDTO> PhoneConditionPrices { get; set; }

        public void Map(PhoneModel model)
        {
            ID = model.ID;
            Name = model.ModelName;
            PhoneMakeId = model.PhoneMakeId;
            PhoneMake = model.PhoneMake.MakeName;
            PrimaryImageString = model.PrimaryImageString;
            PhoneConditionPrices = new List<PhoneConditionPriceDTO>();

            foreach(var price in model.PhoneConditionPrices)
            {
                PhoneConditionPriceDTO conditionPrice = new PhoneConditionPriceDTO();
                conditionPrice.MapToDTO(price);
                PhoneConditionPrices.Add(conditionPrice);
            }
        }
    }
}
