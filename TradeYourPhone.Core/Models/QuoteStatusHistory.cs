//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TradeYourPhone.Core.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class QuoteStatusHistory
    {
        public int Id { get; set; }
        public int QuoteId { get; set; }
        public int QuoteStatusId { get; set; }
        public System.DateTime StatusDate { get; set; }
        public string CreatedBy { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Quote Quote { get; set; }
        public virtual QuoteStatus QuoteStatus { get; set; }
    }
}
