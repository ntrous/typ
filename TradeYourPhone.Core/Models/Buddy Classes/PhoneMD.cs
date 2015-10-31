using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TradeYourPhone.Core.Models
{
    [MetadataType(typeof(PhoneMD))]
    partial class Phone
    {

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