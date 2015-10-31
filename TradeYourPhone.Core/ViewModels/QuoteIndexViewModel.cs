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
    public class QuoteIndexViewModel
    {
        public PagedList.IPagedList<Quote> Quotes { get; set; }
        public SelectList QuoteStatuses { get; set; }
        public string referenceId { get; set; }
        public string email { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public int statusId { get; set; }
        public string currentFilter { get; set; }
        public int? page { get; set; }
        public string sortOrder { get; set; }
        public string StatusSortParm { get; set; }
        public string NameSortParm { get; set; }
        public string EmailSortParm { get; set; }
        public string CreatedDateSortParm { get; set; }
        public string QuoteFinalisedDateSortParm { get; set; }
    }
}
