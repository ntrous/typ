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
    
    public partial class Customer
    {
        public Customer()
        {
            this.Quotes = new HashSet<Quote>();
        }
    
        public int ID { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int AddressId { get; set; }
        public int PaymentDetailsId { get; set; }
        public string FullName { get; set; }
    
        public virtual Address Address { get; set; }
        public virtual PaymentDetail PaymentDetail { get; set; }
        public virtual ICollection<Quote> Quotes { get; set; }
    }
}
