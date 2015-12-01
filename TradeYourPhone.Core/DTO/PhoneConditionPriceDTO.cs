using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Core.Models;

namespace TradeYourPhone.Core.DTO
{
    public class PhoneConditionPriceDTO
    {
        public int ID { get; set; }
        public int PhoneModelId { get; set; }
        public int PhoneConditionId { get; set; }
        public decimal OfferAmount { get; set; }

        public PhoneConditionDTO PhoneCondition { get; set; }

        public void MapToDTO(PhoneConditionPrice conditionPrice)
        {
            ID = conditionPrice.ID;
            PhoneModelId = conditionPrice.PhoneModelId;
            PhoneConditionId = conditionPrice.PhoneConditionId;
            OfferAmount = conditionPrice.OfferAmount;
            PhoneCondition = new PhoneConditionDTO();
            PhoneCondition.Map(conditionPrice.PhoneCondition);
        }
    }
}
