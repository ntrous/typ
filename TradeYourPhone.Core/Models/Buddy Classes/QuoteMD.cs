using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TradeYourPhone.Core.Models
{
    [MetadataType(typeof(QuoteMD))]
    partial class Quote
    {

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