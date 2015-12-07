using TradeYourPhone.Core.Models;
using System.Collections.Generic;

namespace TradeYourPhone.Core.Services.Interface
{
    public interface IPhoneService
    {
        IEnumerable<PhoneMake> GetAllPhoneMakes();
        PhoneMake GetPhoneMakeById(int phoneMakeId);
        int GetPhoneMakeIdByModelId(int phoneModelId);
        bool CreatePhoneMake(string phoneMake);
        bool ModifyPhoneMake(PhoneMake phoneMake);
        //bool DeletePhoneMakeById(int phoneMakeId);

        IEnumerable<PhoneModel> GetAllPhoneModels();
        IList<PhoneModel> GetPhoneModelsByMakeName(string makeName);
        PhoneModel GetPhoneModelById(int phoneModelId);
        IEnumerable<PhoneModel> GetPhoneModelsByMakeId(int phoneMakeId);
        IList<PhoneModel> GetMostPopularPhoneModels(int limit);
        bool CreatePhoneModel(PhoneModel phoneModel);
        bool ModifyPhoneModel(PhoneModel phoneModel);
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
        Phone ModifyPhone(Phone phone, string userId);
        bool DeletePhoneById(int phoneId);
        List<Phone> SearchPhones(string phoneId, int phoneMakeId, int phoneModelId, int phoneStatusId);
        List<Phone> GetSortedPhones(List<Phone> phonesToSort, string sortOrder);

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