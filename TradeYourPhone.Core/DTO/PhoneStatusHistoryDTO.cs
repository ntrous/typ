using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Core.Models;

namespace TradeYourPhone.Core.DTO
{
    public class PhoneStatusHistoryDTO
    {
        public int Id { get; set; }
        public int PhoneId { get; set; }
        public string PhoneStatus { get; set; }
        public string StatusDate { get; set; }
        public string CreatedBy { get; set; }

        public AspNetUser AspNetUser { get; set; }

        public void Map(PhoneStatusHistory statusHistory)
        {
            Id = statusHistory.Id;
            PhoneId = statusHistory.PhoneId;
            PhoneStatus = statusHistory.PhoneStatus.PhoneStatus;
            StatusDate = statusHistory.StatusDate.ToString("G");
            CreatedBy = statusHistory.AspNetUser?.UserName;
        }
    }
}
