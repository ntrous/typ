using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Core.Models;

namespace TradeYourPhone.Core.DTO
{
    public class QuoteDTO
    {
        public int ID { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public int QuoteStatusId { get; set; }
        public string QuoteFinalisedDate { get; set; }
        public string QuoteExpiryDate { get; set; }
        public string QuoteReferenceId { get; set; }
        public string CreatedDate { get; set; }
        public Nullable<int> PostageMethodId { get; set; }
        public bool AgreedToTerms { get; set; }
        public string Notes { get; set; }
        public string TrackingNumber { get; set; }

        public CustomerDTO Customer { get; set; }
        public PostageMethod PostageMethod { get; set; }
        public ICollection<PhoneDTO> Phones { get; set; }
        public ICollection<QuoteStatusHistoryDTO> QuoteStatusHistories { get; set; }

        public void MapToDTO(Quote quote)
        {
            ID = quote.ID;
            CustomerId = quote.CustomerId;
            QuoteStatusId = quote.QuoteStatusId;
            QuoteFinalisedDate = quote.QuoteFinalisedDate.HasValue ? quote.QuoteFinalisedDate.Value.ToString("G") : string.Empty;
            QuoteExpiryDate = quote.QuoteExpiryDate.HasValue ? quote.QuoteExpiryDate.Value.ToString("G") : string.Empty;
            QuoteReferenceId = quote.QuoteReferenceId;
            CreatedDate = quote.CreatedDate.HasValue ? quote.CreatedDate.Value.ToString("G") : string.Empty;
            PostageMethodId = quote.PostageMethodId;
            AgreedToTerms = quote.AgreedToTerms;
            Notes = quote.Notes;
            TrackingNumber = quote.TrackingNumber;
            Customer = new CustomerDTO();
            Customer.Map(quote.Customer);
            PostageMethod = quote.PostageMethod;

            Phones = new List<PhoneDTO>();
            foreach(var phone in quote.Phones)
            {
                PhoneDTO phoneDto = new PhoneDTO();
                phoneDto.MapToDTO(phone);
                Phones.Add(phoneDto);
            }

            QuoteStatusHistories = new List<QuoteStatusHistoryDTO>();
            foreach (var statusHistory in quote.QuoteStatusHistories.OrderBy(qsh => qsh.StatusDate))
            {
                QuoteStatusHistoryDTO statusHistoryDTO = new QuoteStatusHistoryDTO();
                statusHistoryDTO.Map(statusHistory);
                QuoteStatusHistories.Add(statusHistoryDTO);
            }
        }
    }
}
