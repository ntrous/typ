
using TradeYourPhone.Core.Models;
using TradeYourPhone.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradeYourPhone.Core.Services.Interface
{
    public interface IPhoneService
    {
        IEnumerable<PhoneMake> GetAllPhoneMakes();
        PhoneMake GetPhoneMakeById(int phoneMakeId);
        int GetPhoneMakeIdByModelId(int phoneModelId);
        bool CreatePhoneMake(PhoneMake phoneMake);
        bool ModifyPhoneMake(PhoneMake phoneMake);
        //bool DeletePhoneMakeById(int phoneMakeId);

        IEnumerable<PhoneModel> GetAllPhoneModels();
        IList<PhoneViewModel> GetAllPhoneModelsForView();
        IList<PhoneViewModel> GetPhoneModelsForViewByMakeName(string makeName);
        PhoneModel GetPhoneModelById(int phoneModelId);
        IEnumerable<PhoneModel> GetPhoneModelsByMakeId(int phoneMakeId);
        IList<PhoneViewModel> GetMostPopularPhoneModels(int limit);
        bool CreatePhoneModel(PhoneModelViewModel phoneModelViewModel);
        bool ModifyPhoneModel(PhoneModelViewModel phoneModelViewModel);
        //bool DeletePhoneModelById(int phoneModelId);

        IEnumerable<PhoneCondition> GetAllPhoneConditions();
        PhoneCondition GetPhoneConditionById(int phoneConditionId);
        bool CreatePhoneCondition(PhoneCondition phoneCondition);
        bool ModifyPhoneCondition(PhoneCondition phoneCondition);
        //bool DeletePhoneConditionById(int phoneConditionId);

        IEnumerable<PhoneConditionPrice> GetAllPhoneConditionPrices();
        PhoneConditionPrice GetPhoneConditionPriceById(int phoneConditionPriceId);
        PhoneConditionPrice GetPhoneConditionPrice(int PhoneModelId, int PhoneConditionId);
        bool CreatePhoneConditionPrice(PhoneConditionPrice phoneConditionPrice);
        bool ModifyPhoneConditionPrice(PhoneConditionPrice phoneConditionPrice);
        //bool DeletePhoneConditionPriceById(int phoneConditionPriceId);

        IEnumerable<Phone> GetAllPhones();
        Phone GetPhoneById(int phoneId);
        bool CreatePhone(Phone phone);
        bool ModifyPhone(Phone phone, string userId);
        bool ModifyPhones(IList<Phone> phones, string userId);
        bool DeletePhoneById(int phoneId);
        List<Phone> SearchPhones(string phoneId, int phoneMakeId, int phoneModelId, int phoneStatusId);
        List<Phone> GetSortedPhones(List<Phone> phonesToSort, PhoneIndexViewModel viewModel);

        IEnumerable<PhoneStatu> GetAllPhoneStatuses();

        /// <summary>
        /// If the status has changed a new Status history record will be added
        /// </summary>
        /// <param name="phoneId"></param>
        /// <param name="oldPhoneStatusId"></param>
        /// <param name="newPhoneStatusId"></param>
        /// <param name="UserId">UserId of the user updating the Phone record</param>
        void UpdatePhoneStatusHistory(int phoneId, int oldPhoneStatusId, int newPhoneStatusId, string UserId);
    }
}