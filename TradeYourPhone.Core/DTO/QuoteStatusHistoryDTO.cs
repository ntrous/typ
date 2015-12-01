using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Core.Enums;
using TradeYourPhone.Core.Models;

namespace TradeYourPhone.Core.DTO
{
    public class QuoteStatusHistoryDTO
    {
        public int Id { get; set; }
        public int QuoteId { get; set; }
        public string QuoteStatus { get; set; }
        public string StatusDate { get; set; }
        public string CreatedBy { get; set; }

        public AspNetUser AspNetUser { get; set; }

        public void Map(QuoteStatusHistory statusHistory)
        {
            Id = statusHistory.Id;
            QuoteId = statusHistory.QuoteId;
            QuoteStatus = statusHistory.QuoteStatus.QuoteStatusName;
            StatusDate = statusHistory.StatusDate.ToString("G");
            CreatedBy = statusHistory.AspNetUser?.UserName;
        }
    }
}
