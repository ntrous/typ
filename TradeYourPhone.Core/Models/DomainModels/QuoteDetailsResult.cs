using TradeYourPhone.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeYourPhone.Core.Models.DomainModels
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
            InnerMessage = ex.InnerException != null ? ex.InnerException.Message : null;
        }   
    }
}
