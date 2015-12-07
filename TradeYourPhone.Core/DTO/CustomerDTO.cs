using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Core.Models;

namespace TradeYourPhone.Core.DTO
{
    public class CustomerDTO
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int AddressId { get; set; }
        public int PaymentDetailsId { get; set; }

        public Address Address { get; set; }
        public PaymentDetail PaymentDetail { get; set; }

        public void Map(Customer customer)
        {
            if (customer != null)
            {
                ID = customer.ID;
                FullName = customer.FullName;
                Email = customer.Email;
                PhoneNumber = customer.PhoneNumber;
                AddressId = customer.AddressId;
                PaymentDetailsId = customer.PaymentDetailsId;
                Address = customer.Address;
                PaymentDetail = customer.PaymentDetail;
            }
        }
    }
}
