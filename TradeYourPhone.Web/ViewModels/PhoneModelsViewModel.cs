using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Core.Models;

namespace TradeYourPhone.Web.ViewModels
{
    public class PhoneModelsViewModel
    {
        public IList<PhoneViewModel> PhoneModels { get; set; }

        public void MapPhoneModels(IList<PhoneModel> phoneModels)
        {
            PhoneModels = new List<PhoneViewModel>();
            foreach (var model in phoneModels)
            {
                var phoneModel = new PhoneViewModel();
                phoneModel.MapPhoneModel(model);
                PhoneModels.Add(phoneModel);
            }
        }
    } 

    public class PhoneViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public List<ConditionViewModel> ConditionPrices;

        public void MapPhoneModel(PhoneModel phoneModel)
        {
            Id = phoneModel.ID.ToString();
            Name = phoneModel.PhoneMake.MakeName + " " + phoneModel.ModelName;
            Image = phoneModel.PrimaryImageString;
            ConditionPrices = new List<ConditionViewModel>();
            foreach (var conditionPrice in phoneModel.PhoneConditionPrices)
            {
                var condition = new ConditionViewModel();
                condition.MapCondition(conditionPrice);
                ConditionPrices.Add(condition);
            }
        }
    }

    public class ConditionViewModel
    {
        public string Name { get; set; }
        public int PhoneConditionId { get; set; }
        public decimal OfferAmount { get; set; }

        public void MapCondition(PhoneConditionPrice conditionPrice)
        {
            Name = conditionPrice.PhoneCondition.Condition;
            PhoneConditionId = conditionPrice.PhoneConditionId;
            OfferAmount = conditionPrice.OfferAmount;
        }
    }
}
