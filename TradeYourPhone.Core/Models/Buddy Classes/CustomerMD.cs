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
        public string FirstName
        {
            get
            {
                if (FullName.Contains(" "))
                {
                    return FullName.Substring(0, FullName.IndexOf(" "));
                }
                else
                {
                    return FullName;
                }
            }
        }

        public string LastName
        {
            get
            {
                if (FullName.Contains(" "))
                {
                    return FullName.Substring(FullName.IndexOf(" ")+1);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public void UpdateFromDTO(CustomerDTO customerDTO)
        {
            FullName = customerDTO.FullName;
            Email = customerDTO.Email;
            PhoneNumber = customerDTO.PhoneNumber;
            PaymentDetail.UpdateFromDTO(customerDTO.PaymentDetail);
            Address.UpdateFromDTO(customerDTO.Address);
        }

        public void UpdateFromCustomerObj(Customer customer)
        {
            FullName = customer.FullName;
            Email = customer.Email;
            PhoneNumber = customer.PhoneNumber;
            PaymentDetail.UpdateFromDTO(customer.PaymentDetail);
            Address.UpdateFromDTO(customer.Address);
        }
    }

    public class CustomerMD
    {
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}