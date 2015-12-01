using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Core.DTO;

namespace TradeYourPhone.Core.Models
{
    [MetadataType(typeof(PhoneConditionPriceMD))]
    partial class PhoneConditionPrice
    {
        public void UpdateFromDTO(PhoneConditionPriceDTO phoneConditionPriceDTO)
        {
            PhoneModelId = phoneConditionPriceDTO.PhoneModelId;
            PhoneConditionId = phoneConditionPriceDTO.PhoneConditionId;
            OfferAmount = phoneConditionPriceDTO.OfferAmount;
        }
    }

    public class PhoneConditionPriceMD
    {

    }
}
