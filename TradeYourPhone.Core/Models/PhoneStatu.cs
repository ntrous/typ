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
    
    public partial class PhoneStatu
    {
        public PhoneStatu()
        {
            this.Phones = new HashSet<Phone>();
            this.PhoneStatusHistories = new HashSet<PhoneStatusHistory>();
        }
    
        public int Id { get; set; }
        public string PhoneStatus { get; set; }
        public int SortOrder { get; set; }
    
        public virtual ICollection<Phone> Phones { get; set; }
        public virtual ICollection<PhoneStatusHistory> PhoneStatusHistories { get; set; }
    }
}
