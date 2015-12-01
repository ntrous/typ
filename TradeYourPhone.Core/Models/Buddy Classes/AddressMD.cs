using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TradeYourPhone.Core.Models
{
    [MetadataType(typeof(AddressMD))]
    partial class Address
    {
        public void UpdateFromDTO(Address address)
        {
            AddressLine1 = address.AddressLine1;
            AddressLine2 = address.AddressLine2;
            PostCode = address.PostCode;
            StateId = address.StateId;
            CountryId = address.CountryId;
        }
    }

    public class AddressMD
    {
        [Required]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [Required]
        [Display(Name = "State")]
        public int StateId { get; set; }

        [Required]
        [Display(Name = "Post Code")]
        public string PostCode { get; set; }

        [Required]
        [Display(Name = "Country")]
        public int CountryId { get; set; }
    }
}