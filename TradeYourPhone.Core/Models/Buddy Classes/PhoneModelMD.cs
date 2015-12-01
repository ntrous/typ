using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TradeYourPhone.Core.DTO;

namespace TradeYourPhone.Core.Models
{
    [MetadataType(typeof(PhoneModelMD))]
    partial class PhoneModel
    {
        public string PrimaryImageString
        {
            get
            {
                if (PrimaryImage != null)
                {
                    return Convert.ToBase64String(PrimaryImage);
                }
                return string.Empty;
            }
            set
            {
                PrimaryImage = Convert.FromBase64String(value);
            }
        }

        public void UpdateFromDTO(PhoneModelDTO phoneModelDTO)
        {
            ModelName = phoneModelDTO.Name;
            PhoneMakeId = phoneModelDTO.PhoneMakeId;
            PrimaryImageString = phoneModelDTO.PrimaryImageString;

            foreach (var conditionPrice in PhoneConditionPrices)
            {
                conditionPrice.UpdateFromDTO(phoneModelDTO.PhoneConditionPrices.Where(p => p.ID == conditionPrice.ID).First());
            }

            if (PhoneConditionPrices.Count == 0 && phoneModelDTO.PhoneConditionPrices.Count > 0)
            {
                PhoneConditionPrices = new List<PhoneConditionPrice>();
                foreach (var price in phoneModelDTO.PhoneConditionPrices)
                {
                    PhoneConditionPrice conditionPrice = new PhoneConditionPrice();
                    conditionPrice.UpdateFromDTO(price);
                    PhoneConditionPrices.Add(conditionPrice);
                }
            }
        }
        public class PhoneModelMD
        {

        }
    }
}