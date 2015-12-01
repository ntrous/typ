using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TradeYourPhone.Core.Models
{
    [MetadataType(typeof(PaymentDetailMD))]
    partial class PaymentDetail
    {
        public void UpdateFromDTO(PaymentDetail paymentDetail)
        {
            PaymentTypeId = paymentDetail.PaymentTypeId;
            BSB = paymentDetail.BSB;
            AccountNumber = paymentDetail.AccountNumber;
            PaypalEmail = paymentDetail.PaypalEmail;
        }
    }

    public class PaymentDetailMD
    {
        [Display(Name = "Payment Type")]
        public int PaymentTypeId { get; set; }

        [Display(Name = "BSB")]
        public string BSB { get; set; }

        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        [Display(Name = "Paypal Email")]
        public string PaypalEmail { get; set; }
    }
}