using TradeYourPhone.Core.Models;
using System.Collections.Generic;
using TradeYourPhone.Core.DTO;

namespace TradeYourPhone.Web.ViewModels
{
    public class QuoteDetailsViewModel
    {
        public QuoteDTO Quote { get; set; }

        // UserId of the person updating the quote details
        public string UserId { get; set; }

        public List<QuoteStatusDTO> QuoteStatuses { get; set; }
        public List<PostageMethod> PostageMethods { get; set; }
        public List<PaymentType> PaymentTypes { get; set; }
        public List<State> States { get; set; }
        public List<Country> Countries { get; set; }
        public List<PhoneStatusDTO> PhoneStatuses { get; set; }
        public List<PhoneMakeDTO> PhoneMakes { get; set; }
        public List<PhoneModelDTO> PhoneModels { get; set; }
        public List<PhoneConditionDTO> Conditions { get; set; }

        public void MapQuote(Quote quote)
        {
            Quote = new QuoteDTO();
            Quote.MapToDTO(quote);
        }

        public void MapQuoteStatuses(IList<QuoteStatus> quoteStatuses)
        {
            QuoteStatuses = new List<QuoteStatusDTO>();
            foreach(var status in quoteStatuses)
            {
                QuoteStatusDTO quoteStatus = new QuoteStatusDTO();
                quoteStatus.Map(status);
                QuoteStatuses.Add(quoteStatus);
            }
        }

        public void MapPhoneStatuses(IList<PhoneStatu> phoneStatuses)
        {
            PhoneStatuses = new List<PhoneStatusDTO>();
            foreach (var status in phoneStatuses)
            {
                PhoneStatusDTO phoneStatus = new PhoneStatusDTO();
                phoneStatus.Map(status);
                PhoneStatuses.Add(phoneStatus);
            }
        }

        public void MapPhoneMakes(IList<PhoneMake> phoneMakes)
        {
            PhoneMakes = new List<PhoneMakeDTO>();
            foreach (var make in phoneMakes)
            {
                PhoneMakeDTO phoneMake = new PhoneMakeDTO();
                phoneMake.Map(make);
                PhoneMakes.Add(phoneMake);
            }
        }

        public void MapPhoneModels(IList<PhoneModel> phoneModels)
        {
            PhoneModels = new List<PhoneModelDTO>();
            foreach (var model in phoneModels)
            {
                PhoneModelDTO phoneModel = new PhoneModelDTO();
                phoneModel.Map(model);
                PhoneModels.Add(phoneModel);
            }
        }

        public void MapPhoneConditions(IList<PhoneCondition> phoneConditions)
        {
            Conditions = new List<PhoneConditionDTO>();
            foreach (var condition in phoneConditions)
            {
                PhoneConditionDTO phoneCondition = new PhoneConditionDTO();
                phoneCondition.Map(condition);
                Conditions.Add(phoneCondition);
            }
        }
    }
}