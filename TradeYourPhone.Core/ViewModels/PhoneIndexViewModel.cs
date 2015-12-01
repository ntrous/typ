using TradeYourPhone.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using PagedList;
using TradeYourPhone.Core.DTO;

namespace TradeYourPhone.Core.ViewModels
{
    public class PhoneIndexViewModel
    {
        public List<PhoneDetail> Phones { get; set; }
        public List<PhoneMakeDTO> PhoneMakes { get; set; }
        public List<PhoneModelDTO> PhoneModels { get; set; }
        public List<PhoneStatusDTO> PhoneStatuses { get; set; }
        public string PhoneId { get; set; }
        public int PhoneMakeId { get; set; }
        public int PhoneModelId { get; set; }
        public int PhoneStatusId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalPhones { get; set; }
        public string SortOrder { get; set; }
        public string PhoneIdParm { get; set; }
        public string PhoneMakeParm { get; set; }
        public string PhoneModelParm { get; set; }
        public string PhoneConditionParm { get; set; }
        public string PhoneStatusParm { get; set; }
        public string PurchaseAmountParm { get; set; }
        public string SaleAmountParm { get; set; }

        public void MapPhones(IPagedList<Phone> phones)
        {
            Phones = new List<PhoneDetail>();
            foreach(var phone in phones)
            {
                PhoneDetail phoneDetail = new PhoneDetail();
                phoneDetail.Map(phone);
                Phones.Add(phoneDetail);
            }
        }

        public void MapPhoneMakes(IList<PhoneMake> phoneMakes)
        {
            PhoneMakes = new List<PhoneMakeDTO>();
            PhoneMakeDTO defaultOption = new PhoneMakeDTO
            {
                ID = 0,
                Name = "All"
            };
            PhoneMakes.Add(defaultOption);

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
            PhoneModelDTO defaultOption = new PhoneModelDTO
            {
                ID = 0,
                Name = "All"
            };
            PhoneModels.Add(defaultOption);

            foreach (var model in phoneModels)
            {
                PhoneModelDTO phoneModel = new PhoneModelDTO();
                phoneModel.Map(model);
                PhoneModels.Add(phoneModel);
            }
        }

        public void MapPhoneStatuses(IList<PhoneStatu> phoneStatuses)
        {
            PhoneStatuses = new List<PhoneStatusDTO>();
            PhoneStatusDTO defaultOption = new PhoneStatusDTO
            {
                ID = 0,
                Name = "All"
            };
            PhoneStatuses.Add(defaultOption);

            foreach (var status in phoneStatuses)
            {
                PhoneStatusDTO phoneStatus = new PhoneStatusDTO();
                phoneStatus.Map(status);
                PhoneStatuses.Add(phoneStatus);
            }
        }
    }

    public class PhoneDetail
    {
        public int Id { get; set; }
        public string PhoneMake { get; set; }
        public string PhoneModel { get; set; }
        public string PhoneCondition { get; set; }
        public string PhoneStatus { get; set; }
        public string PurchaseAmount { get; set; }
        public string SaleAmount { get; set; }

        public void Map(Phone phone)
        {
            Id = phone.Id;
            PhoneMake = phone.PhoneMake.MakeName;
            PhoneModel = phone.PhoneModel.ModelName;
            PhoneCondition = phone.PhoneCondition.Condition;
            PhoneStatus = phone.PhoneStatu.PhoneStatus;
            PurchaseAmount = phone.PurchaseAmount.HasValue ? phone.PurchaseAmount.Value.ToString("c") : string.Empty;
            SaleAmount = phone.SaleAmount.HasValue ? phone.SaleAmount.Value.ToString("c") : string.Empty;
        }
    }
}
