using TradeYourPhone.Core.Models;

namespace TradeYourPhone.Web.ViewModels
{
    public class CustomerDetail
    {
        public string fullname { get; set; }
        public string email { get; set; }
        public string emailConfirm { get; set; }
        public string mobile { get; set; }
        public string postageStreet { get; set; }
        public string postageSuburb { get; set; }
        public State postageState { get; set; }
        public string postagePostcode { get; set; }
        public PaymentType paymentType { get; set; }
        public string bsb { get; set; }
        public string accountNum { get; set; }
        public string paypalEmail { get; set; }
        public bool paypalSameAsPersonal { get; set; }
    }
}
