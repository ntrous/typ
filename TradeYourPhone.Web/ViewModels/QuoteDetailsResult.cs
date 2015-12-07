using System;

namespace TradeYourPhone.Web.ViewModels
{
    public class QuoteDetailsResult
    {
        public string Status { get; set; }
        public QuoteDetailsException Exception { get; set; }
        public QuoteDetails QuoteDetails { get; set; }
    }

    public class QuoteDetailsException
    {
        public string Message { get; set; }
        public string InnerMessage { get; set; }

        public QuoteDetailsException(Exception ex)
        {
            Message = ex.Message;
            InnerMessage = ex.InnerException?.Message;
        }   
    }
}
