using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeYourPhone.Web.ViewModels
{
    public class DashboardViewModel
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public int NoOfCreatedQuotes { get; set; }
        public int NoOfFinalisedQuotes { get; set; }
        public int NoOfCompletedQuotes { get; set; }
        public decimal PercentageOfCompletedQuotes { get; set; }
        public decimal PercentageOfCompletedSatchelQuotes { get; set; }

        public decimal TotalAmountToPay { get; set; }
        public decimal TotalAmountPaid { get; set; }
        public decimal TotalProfit { get; set; }

        public int TotalDevicesSold { get; set; }
        public decimal TotalIncomeAmount { get; set; }
        public decimal TotalAssetWorth { get; set; } 
    }
}
