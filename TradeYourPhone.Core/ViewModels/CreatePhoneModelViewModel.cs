using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Core.DTO;
using TradeYourPhone.Core.Models;

namespace TradeYourPhone.Core.ViewModels
{
    public class CreatePhoneModelViewModel
    {
        public PhoneModelDTO Model { get; set; }
        public IList<PhoneMakeDTO> PhoneMakes { get; set; }
        public IList<PhoneConditionDTO> PhoneConditions { get; set; }

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

        public void MapPhoneConditions(IList<PhoneCondition> phoneConditions)
        {
            PhoneConditions = new List<PhoneConditionDTO>();
            foreach (var condition in phoneConditions)
            {
                PhoneConditionDTO phoneCondition = new PhoneConditionDTO();
                phoneCondition.Map(condition);
                PhoneConditions.Add(phoneCondition);
            }
        }
    }
}
