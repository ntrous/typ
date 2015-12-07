using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TradeYourPhone.Core.DTO;

namespace TradeYourPhone.Core.Models
{
    [MetadataType(typeof(QuoteMD))]
    partial class Quote
    {
        public void UpdateFromDTO(QuoteDTO quoteDTO)
        {
            if (QuoteStatusId != quoteDTO.QuoteStatusId)
            {
                QuoteStatusId = quoteDTO.QuoteStatusId;
            }

            PostageMethodId = quoteDTO.PostageMethodId;
            Notes = quoteDTO.Notes;
            TrackingNumber = quoteDTO.TrackingNumber;

            if (Customer != null)
            {
                Customer.UpdateFromDTO(quoteDTO.Customer);
            }

            foreach (var phone in Phones)
            {
                phone.UpdateFromDTO(quoteDTO.Phones.Where(p => p.Id == phone.Id).First());
            }
        }
    }

    public class QuoteMD
    {
        [Required]
        [Display(Name = "Quote Status")]
        public int QuoteStatusId { get; set; }

        [Display(Name = "Postage Method")]
        public int PostageMethodId { get; set; }

        [Display(Name = "Quote Finalised Date")]
        public Nullable<System.DateTime> QuoteFinalisedDate { get; set; }

        [Display(Name = "Quote Expiry Date")]
        public Nullable<System.DateTime> QuoteExpiryDate { get; set; }

        [Required]
        [Display(Name = "Quote Reference Id")]
        public string QuoteReferenceId { get; set; }
    }
}