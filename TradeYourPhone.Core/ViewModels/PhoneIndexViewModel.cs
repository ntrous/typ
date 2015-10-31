using TradeYourPhone.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using PagedList;

namespace TradeYourPhone.Core.ViewModels
{
    public class PhoneIndexViewModel
    {
        public PagedList.IPagedList<Phone> Phones { get; set; }
        public SelectList PhoneMakes { get; set; }
        public SelectList PhoneModels { get; set; }
        public SelectList PhoneStatuses { get; set; }
        public string PhoneId { get; set; }
        public int PhoneMakeId { get; set; }
        public int PhoneModelId { get; set; }
        public int PhoneStatusId { get; set; }
        public int? page { get; set; }
        public string SortOrder { get; set; }
        public string PhoneIdParm { get; set; }
        public string PhoneMakeParm { get; set; }
        public string PhoneModelParm { get; set; }
        public string PhoneConditionParm { get; set; }
        public string PhoneStatusParm { get; set; }
        public string PurchaseAmountParm { get; set; }
        public string SaleAmountParm { get; set; }
    }
}
