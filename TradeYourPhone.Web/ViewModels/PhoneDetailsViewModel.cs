using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Core.DTO;
using TradeYourPhone.Core.Models;

namespace TradeYourPhone.Web.ViewModels
{
    public class PhoneDetailsViewModel
    {
        public PhoneDTO Phone { get; set; }

        public List<PhoneStatusDTO> PhoneStatuses { get; set; }
        public List<PhoneMakeDTO> PhoneMakes { get; set; }
        public List<PhoneModelDTO> PhoneModels { get; set; }
        public List<PhoneConditionDTO> Conditions { get; set; }

        public void MapPhone(Phone phone)
        {
            Phone = new PhoneDTO();
            Phone.MapToDTO(phone);
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
