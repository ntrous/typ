using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TradeYourPhone.Core.DTO;

namespace TradeYourPhone.Core.Models
{
    [MetadataType(typeof(CustomerMD))]
    partial class Customer
    {
        public string FullName
        {
            set
            {
                FirstName = value.Substring(0, value.IndexOf(" "));
                LastName = value.Substring(value.IndexOf(" ")+1);
            }

            get
            {
                return FirstName + " " + LastName;
            }
        }

        public void UpdateFromDTO(CustomerDTO customerDTO)
        {
            FirstName = customerDTO.FirstName;
            LastName = customerDTO.LastName;
            Email = customerDTO.Email;
            PhoneNumber = customerDTO.PhoneNumber;
            PaymentDetail.UpdateFromDTO(customerDTO.PaymentDetail);
            Address.UpdateFromDTO(customerDTO.Address);
        }
    }

    public class CustomerMD
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}