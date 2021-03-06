﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TradeYourPhone.Core.DTO;

namespace TradeYourPhone.Core.Models
{
    [MetadataType(typeof(PhoneMD))]
    partial class Phone
    {
        public void UpdateFromDTO(PhoneDTO phoneDTO)
        {
            if (PhoneStatusId != phoneDTO.PhoneStatusId)
            {
                PhoneStatusId = phoneDTO.PhoneStatusId;
            }

            PhoneMakeId = phoneDTO.PhoneMakeId;
            PhoneModelId = phoneDTO.PhoneModelId;
            PhoneConditionId = phoneDTO.PhoneConditionId;
            IMEI = phoneDTO.IMEI;
            PurchaseAmount = Convert.ToDecimal(phoneDTO.PurchaseAmount);
            PhoneChecklist = phoneDTO.PhoneChecklist;
            PhoneNotes = phoneDTO.PhoneNotes;
            PhoneDescription = phoneDTO.PhoneDescription;
            if (!string.IsNullOrEmpty(phoneDTO.SaleAmount))
            {
                SaleAmount = Convert.ToDecimal(phoneDTO.SaleAmount);
            }
            else
            {
                SaleAmount = null;
            }
        }
    }

    public class PhoneMD
    {
        [Required]
        public int PhoneMakeId { get; set; }

        [Required]
        public int PhoneModelId { get; set; }

        [Required]
        public int PhoneConditionId { get; set; }

        [Required]
        public Nullable<decimal> PurchaseAmount { get; set; }

        public Nullable<decimal> SaleAmount { get; set; }

        [Required]
        public int PhoneStatusId { get; set; }
    }
}