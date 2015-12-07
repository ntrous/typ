using TradeYourPhone.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using PagedList;
using TradeYourPhone.Core.DTO;

namespace TradeYourPhone.Web.ViewModels
{
    public class QuoteIndexViewModel
    {
        public List<QuoteDetail> Quotes { get; set; }
        public List<QuoteStatusDTO> QuoteStatuses { get; set; }
        public string referenceId { get; set; }
        public string email { get; set; }
        public string fullName { get; set; }
        public int statusId { get; set; }
        public string currentFilter { get; set; }
        public int? page { get; set; }
        public string sortOrder { get; set; }
        public string StatusSortParm { get; set; }
        public string NameSortParm { get; set; }
        public string EmailSortParm { get; set; }
        public string CreatedDateSortParm { get; set; }
        public string QuoteFinalisedDateSortParm { get; set; }
        public int TotalQuotes { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public void MapQuotes(IPagedList<Quote> quotes)
        {
            List<QuoteDetail> quoteList = new List<QuoteDetail>();
            foreach (var quote in quotes)
            {
                QuoteDetail quoteDetail = new QuoteDetail();
                quoteDetail.Map(quote);
                quoteList.Add(quoteDetail);
            }
            Quotes = quoteList;
            PageNumber = quotes.PageCount < quotes.PageNumber ? 0 : quotes.PageNumber;
        }

        public void MapStatuses(IList<QuoteStatus> statuses)
        {
            List<QuoteStatusDTO> quoteStatuses = new List<QuoteStatusDTO>();
            QuoteStatusDTO defaultOption = new QuoteStatusDTO
            {
                ID = 0,
                Name = "All"
            };
            quoteStatuses.Add(defaultOption);

            foreach(var status in statuses)
            {
                QuoteStatusDTO statusDTO = new QuoteStatusDTO();
                statusDTO.Map(status);
                quoteStatuses.Add(statusDTO);
            }
            QuoteStatuses = quoteStatuses;
        }
    }

    public class QuoteDetail
    {
        public int QuoteId { get; set; }
        public string QuoteStatus { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string QuoteCreatedDate { get; set; }
        public string QuoteFinalisedDate { get; set; }
        public string QuoteExpiryDate { get; set; }

        public void Map(Quote quote)
        {
            QuoteId = quote.ID;
            QuoteStatus = quote.QuoteStatus.QuoteStatusName;
            CustomerName = quote.Customer?.FullName;
            CustomerEmail = quote.Customer?.Email;
            QuoteCreatedDate = quote.CreatedDate.HasValue ? quote.CreatedDate.Value.ToString("G") : string.Empty;
            QuoteFinalisedDate = quote.QuoteFinalisedDate.HasValue ? quote.QuoteFinalisedDate.Value.ToString("G") : string.Empty;
            QuoteExpiryDate = quote.QuoteExpiryDate.HasValue ? quote.QuoteExpiryDate.Value.ToString("G") : string.Empty;
        }
    }
}
