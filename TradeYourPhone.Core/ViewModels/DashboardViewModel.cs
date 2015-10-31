using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeYourPhone.Core.ViewModels
{
    public class DashboardViewModel
    {
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime DateFrom { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime DateTo { get; set; }

        public int NoOfCreatedQuotes { get; set; }
        public int NoOfFinalisedQuotes { get; set; }
        public int NoOfCompletedQuotes { get; set; }

        public decimal TotalAmountToBePaid { get; set; }
        public decimal TotalAmountPaid { get; set; }

        public int TotalDevicesSold { get; set; }
        public decimal TotalAmountSold { get; set; }
    }
}
