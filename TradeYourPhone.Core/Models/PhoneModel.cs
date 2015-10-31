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
    
    public partial class PhoneModel
    {
        public PhoneModel()
        {
            this.PhoneConditionPrices = new HashSet<PhoneConditionPrice>();
            this.Phones = new HashSet<Phone>();
        }
    
        public int ID { get; set; }
        public string ModelName { get; set; }
        public int PhoneMakeId { get; set; }
        public byte[] PrimaryImage { get; set; }
    
        public virtual ICollection<PhoneConditionPrice> PhoneConditionPrices { get; set; }
        public virtual PhoneMake PhoneMake { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }
    }
}
