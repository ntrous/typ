using TradeYourPhone.Core.Models;
using TradeYourPhone.Core.Repositories.Interface;
using TradeYourPhone.Core.Utilities;
using TradeYourPhone.Core.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TradeYourPhone.Core.Enums;

namespace TradeYourPhone.Core.Services.Implementation
{
    public class PhoneService : IPhoneService
    {
        private IUnitOfWork unitOfWork;

        public PhoneService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #region PhoneMakes

        public IEnumerable<PhoneMake> GetAllPhoneMakes()
        {
            return unitOfWork.PhoneMakeRepository.Get().OrderBy(x => x.MakeName);
        }

        public PhoneMake GetPhoneMakeById(int phoneMakeId)
        {
            PhoneMake phoneMake = unitOfWork.PhoneMakeRepository.GetByID(phoneMakeId);
            return phoneMake;
        }

        public int GetPhoneMakeIdByModelId(int phoneModelId)
        {
            int phoneMakeId = unitOfWork.PhoneModelRepository.GetByID(phoneModelId).PhoneMakeId;
            return phoneMakeId;
        }

        public bool CreatePhoneMake(string phoneMake)
        {
            if (!DoesPhoneMakeExist(phoneMake))
            {
                PhoneMake make = new PhoneMake
                {
                    MakeName = Util.UppercaseFirst(phoneMake)
                };
                unitOfWork.PhoneMakeRepository.Insert(make);
                unitOfWork.Save();
                return true;
            }
            return false;
        }

        public bool ModifyPhoneMake(PhoneMake phoneMake)
        {
            if (!DoesPhoneMakeExist(phoneMake.MakeName))
            {
                PhoneMake originalMake = GetPhoneMakeById(phoneMake.ID);
                originalMake.MakeName = Util.UppercaseFirst(phoneMake.MakeName);
                unitOfWork.PhoneMakeRepository.Update(originalMake);
                unitOfWork.Save();
                return true;
            }
            return false;
        }

        private bool DoesPhoneMakeExist(string phoneMake)
        {
            IEnumerable<PhoneMake> phoneMakes = GetAllPhoneMakes().Where(x => x.MakeName.ToLower() == phoneMake.ToLower());
            if (phoneMakes.Any()) { return true; }
            return false;
        }

        #endregion

        #region PhoneModels

        public IEnumerable<PhoneModel> GetAllPhoneModels()
        {
            var models = unitOfWork.PhoneModelRepository.Get().OrderByDescending(p => p.ModelName, new NaturalSortComparer<string>());
            return models;
        }

        /// <summary>
        /// Gets subset of phone models for main site
        /// </summary>
        /// <param name="makeName"></param>
        /// <returns></returns>
        public IList<PhoneModel> GetPhoneModelsByMakeName(string makeName)
        {
            Expression<Func<PhoneModel, bool>> filterExp = (x => x.PhoneMake.MakeName == makeName);
            var models = unitOfWork.PhoneModelRepository.Get(filterExp).OrderByDescending(p => p.ModelName, new NaturalSortComparer<string>());

            return models.ToList();
        }

        public PhoneModel GetPhoneModelById(int phoneModelId)
        {
            PhoneModel phoneModel = unitOfWork.PhoneModelRepository.GetByID(phoneModelId);
            return phoneModel;
        }

        public IEnumerable<PhoneModel> GetPhoneModelsByMakeId(int phoneMakeId)
        {
            Expression<Func<PhoneModel, bool>> filterExp = (x => x.PhoneMakeId == phoneMakeId);
            IEnumerable<PhoneModel> phoneModels = unitOfWork.PhoneModelRepository.Get(filter: filterExp);
            return phoneModels;
        }

        public IList<PhoneModel> GetMostPopularPhoneModels(int limit)
        {
            var models = unitOfWork.PhoneModelRepository.Get().OrderByDescending(p => p.ModelName, new NaturalSortComparer<string>());
            var phoneStatuses = new[] { (int)PhoneStatusEnum.Paid, (int)PhoneStatusEnum.Listed, (int)PhoneStatusEnum.ReadyForSale, (int)PhoneStatusEnum.Sold, };
            IEnumerable<PhoneModel> topModels = models.Where(m => m.Phones.Any(p => phoneStatuses.Contains(p.PhoneStatusId))).OrderByDescending(m => m.Phones.Count(p => phoneStatuses.Contains(p.PhoneStatusId)));

            if (limit != 0)
            {
                topModels = topModels.Take(limit);
            }

            return topModels.ToList();
        }

        public bool CreatePhoneModel(PhoneModel phoneModel)
        {
            if (!DoesPhoneModelExistForMake(phoneModel.PhoneMakeId, phoneModel.ModelName))
            {
                if (!string.IsNullOrEmpty(phoneModel.PrimaryImageString))
                {
                    byte[] imageData = null;
                    imageData = ImageManager.CompressImage(Convert.FromBase64String(phoneModel.PrimaryImageString), "100");

                    phoneModel.PrimaryImageString = Convert.ToBase64String(imageData);
                }

                unitOfWork.PhoneModelRepository.Insert(phoneModel);
                unitOfWork.Save();

                return true;
            }
            return false;
        }

        public bool ModifyPhoneModel(PhoneModel phoneModel)
        {
            if (DoesPhoneModelExistForMake(phoneModel.PhoneMakeId, phoneModel.ModelName, phoneModel.ID))
            {
                return false;
            }

            if (!string.IsNullOrEmpty(phoneModel.PrimaryImageString))
            {
                byte[] imageData = null;
                imageData = ImageManager.CompressImage(Convert.FromBase64String(phoneModel.PrimaryImageString), "100");

                phoneModel.PrimaryImageString = Convert.ToBase64String(imageData);
            }

            unitOfWork.PhoneModelRepository.Update(phoneModel);
            unitOfWork.Save();
            return true;
        }

        /// <summary>
        /// Checks if the Phone Model name provided already exists for the given Phone Make.
        /// Has optional parameter so the search can exclude a given model from the search.
        /// </summary>
        /// <param name="phoneMakeId">The PhoneMakeId for the Phone Make to check against</param>
        /// <param name="phoneModelName">The PhoneModelName to check for</param>
        /// <param name="excludeModelId">Optional param that when provided excludes checking against the Id provided</param>
        /// <returns></returns>
        private bool DoesPhoneModelExistForMake(int phoneMakeId, string phoneModelName, int excludeModelId = 0)
        {
            IEnumerable<PhoneModel> phoneModels = GetAllPhoneModels().Where(x => x.ID != excludeModelId && x.PhoneMakeId == phoneMakeId && string.Equals(x.ModelName, phoneModelName, StringComparison.CurrentCultureIgnoreCase));
            if (phoneModels.Any()) { return true; }
            return false;
        }

        #endregion

        #region PhoneConditions

        public IEnumerable<PhoneCondition> GetAllPhoneConditions()
        {
            return unitOfWork.PhoneConditionRepository.Get();
        }

        public PhoneCondition GetPhoneConditionById(int phoneConditionId)
        {
            PhoneCondition phoneCondition = unitOfWork.PhoneConditionRepository.GetByID(phoneConditionId);
            return phoneCondition;
        }

        public bool CreatePhoneCondition(PhoneCondition phoneCondition)
        {
            if (!DoesPhoneConditionExist(phoneCondition.Condition))
            {
                phoneCondition.Condition = Util.UppercaseFirst(phoneCondition.Condition);
                unitOfWork.PhoneConditionRepository.Insert(phoneCondition);
                unitOfWork.Save();
                return true;
            }
            return false;
        }

        public bool ModifyPhoneCondition(PhoneCondition phoneCondition)
        {
            if (!DoesPhoneConditionExist(phoneCondition.Condition))
            {
                PhoneCondition originalCondition = GetPhoneConditionById(phoneCondition.ID);
                originalCondition.Condition = Util.UppercaseFirst(phoneCondition.Condition);
                unitOfWork.PhoneConditionRepository.Update(originalCondition);
                unitOfWork.Save();
                return true;
            }
            return false;
        }

        private bool DoesPhoneConditionExist(string phoneCondition)
        {
            IEnumerable<PhoneCondition> phoneConditions = GetAllPhoneConditions().Where(x => string.Equals(x.Condition, phoneCondition, StringComparison.CurrentCultureIgnoreCase));
            if (phoneConditions.Any()) { return true; }
            return false;
        }

        #endregion

        #region PhoneConditionPrices

        public IEnumerable<PhoneConditionPrice> GetAllPhoneConditionPrices()
        {
            return unitOfWork.PhoneConditionPriceRepository.Get();
        }

        public PhoneConditionPrice GetPhoneConditionPriceById(int phoneConditionPriceId)
        {
            PhoneConditionPrice phoneConditionPrice = unitOfWork.PhoneConditionPriceRepository.GetByID(phoneConditionPriceId);
            return phoneConditionPrice;
        }

        public PhoneConditionPrice GetPhoneConditionPrice(int PhoneModelId, int PhoneConditionId)
        {
            if (PhoneModelId == 0 || PhoneConditionId == 0)
            {
                return null;
            }

            Expression<Func<PhoneConditionPrice, bool>> filterExp = (x => x.PhoneModelId == PhoneModelId && x.PhoneConditionId == PhoneConditionId);

            var conditionPrice = unitOfWork.PhoneConditionPriceRepository.Get(filter: filterExp).FirstOrDefault();
            if (conditionPrice == null)
            {
                throw new Exception("PhoneConditionPrice does not exist");
            }
            return conditionPrice;
        }

        public bool CreatePhoneConditionPrice(PhoneConditionPrice phoneConditionPrice)
        {
            unitOfWork.PhoneConditionPriceRepository.Insert(phoneConditionPrice);
            unitOfWork.Save();
            return true;
        }

        public bool ModifyPhoneConditionPrice(PhoneConditionPrice phoneConditionprice)
        {
            PhoneConditionPrice originalConditionPrice = GetPhoneConditionPriceById(phoneConditionprice.ID);
            originalConditionPrice.OfferAmount = phoneConditionprice.OfferAmount;
            originalConditionPrice.PhoneConditionId = phoneConditionprice.PhoneConditionId;
            originalConditionPrice.PhoneModelId = phoneConditionprice.PhoneModelId;
            unitOfWork.PhoneConditionPriceRepository.Update(originalConditionPrice);
            unitOfWork.Save();
            return true;
        }

        #endregion

        #region Phones

        public IEnumerable<Phone> GetAllPhones()
        {
            return unitOfWork.PhoneRepository.Get();
        }

        public Phone GetPhoneById(int phoneId)
        {
            Phone phone = unitOfWork.PhoneRepository.Get(p => p.Id == phoneId, null, "PhoneMake,PhoneModel,PhoneCondition,PhoneStatusHistories").FirstOrDefault();
            return phone;
        }

        public bool CreatePhone(Phone phone)
        {
            unitOfWork.PhoneRepository.Insert(phone, null);
            unitOfWork.Save();
            return true;
        }

        public Phone ModifyPhone(Phone phone, string userId)
        {
            unitOfWork.PhoneRepository.Update(phone, userId);
            unitOfWork.Save();

            return phone;
        }

        /// <summary>
        /// If the status has changed a new Status history record will be added
        /// </summary>
        /// <param name="phoneId"></param>
        /// <param name="oldPhoneStatusId"></param>
        /// <param name="newPhoneStatusId"></param>
        /// <param name="UserId">UserId of the user updating the Phone record</param>
        public void UpdatePhoneStatusHistory(int phoneId, int oldPhoneStatusId, int newPhoneStatusId, string UserId)
        {
            if (oldPhoneStatusId != newPhoneStatusId)
            {
                PhoneStatusHistory record = new PhoneStatusHistory
                {
                    PhoneId = phoneId,
                    PhoneStatusId = newPhoneStatusId,
                    StatusDate = Util.GetAEST(DateTime.Now),
                    CreatedBy = UserId ?? User.SystemUser.Value
                };

                unitOfWork.PhoneStatusHistoryRepository.Insert(record);
            }
        }

        /// <summary>
        /// Gets list of Phone Status History records for the given PhoneId
        /// </summary>
        /// <param name="phoneId"></param>
        /// <returns></returns>
        private IEnumerable<PhoneStatusHistory> GetPhoneStatusHistory(int phoneId)
        {
            return unitOfWork.PhoneStatusHistoryRepository.Get(x => x.PhoneId == phoneId);
        }

        public bool DeletePhoneById(int phoneId)
        {
            unitOfWork.PhoneRepository.Delete(phoneId);
            unitOfWork.Save();
            return true;
        }

        /// <summary>
        /// Search Phones by Phone Make ID and/or Phone Model ID
        /// </summary>
        /// <param name="phoneMakeId"></param>
        /// <param name="phoneModelId"></param>
        /// <returns></returns>
        public List<Phone> SearchPhones(string phoneId, int phoneMakeId, int phoneModelId, int phoneStatusId)
        {
            Expression<Func<Phone, bool>> predicate = c => true;

            if (!string.IsNullOrEmpty(phoneId))
            {
                int id = 0;
                bool isInt = int.TryParse(phoneId, out id);
                if (id > 0)
                {
                    predicate = predicate.And(c => c.Id == id);
                }
                else if (!isInt)
                {
                    return new List<Phone>();
                }
            }
            if (phoneMakeId != 0)
            {
                predicate = predicate.And(c => c.PhoneMakeId == phoneMakeId);
            }
            if (phoneModelId != 0)
            {
                predicate = predicate.And(c => c.PhoneModelId == phoneModelId);
            }
            if (phoneStatusId != 0)
            {
                predicate = predicate.And(c => c.PhoneStatusId == phoneStatusId);
            }

            var content = unitOfWork.PhoneRepository.Get(filter: predicate).ToList();

            return content;
        }

        /// <summary>
        /// Based on sortOrder paramater the phones will be sorted into specific order
        /// </summary>
        /// <param name="phonesToSort"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public List<Phone> GetSortedPhones(List<Phone> phonesToSort, string sortOrder)
        {
            IEnumerable<Phone> phones = phonesToSort;

            switch (sortOrder)
            {
                case "phoneMake_desc":
                    phones = phones.OrderByDescending(s => s.PhoneMakeId).ThenBy(s => s.PhoneModelId);
                    break;
                case "phoneModel_asc":
                    phones = phones.OrderBy(s => s.PhoneModelId);
                    break;
                case "phoneModel_desc":
                    phones = phones.OrderByDescending(s => s.PhoneModelId);
                    break;
                case "phoneCondition_asc":
                    phones = phones.OrderBy(s => s.PhoneConditionId);
                    break;
                case "phoneCondition_desc":
                    phones = phones.OrderByDescending(s => s.PhoneConditionId);
                    break;
                case "phoneStatus_asc":
                    phones = phones.OrderBy(s => s.PhoneStatusId);
                    break;
                case "phoneStatus_desc":
                    phones = phones.OrderByDescending(s => s.PhoneStatusId);
                    break;
                case "purchaseAmount_asc":
                    phones = phones.OrderBy(s => s.PurchaseAmount);
                    break;
                case "purchaseAmount_desc":
                    phones = phones.OrderByDescending(s => s.PurchaseAmount);
                    break;
                case "saleAmount_asc":
                    phones = phones.OrderBy(s => s.SaleAmount);
                    break;
                case "saleAmount_desc":
                    phones = phones.OrderByDescending(s => s.SaleAmount);
                    break;
                case "phoneId_asc":
                    phones = phones.OrderBy(s => s.Id);
                    break;
                case "phoneId_desc":
                    phones = phones.OrderByDescending(s => s.Id);
                    break;
                default:
                    phones = phones.OrderBy(s => s.PhoneMakeId);
                    break;
            }

            return phones.ToList();
        }

        #endregion

        #region PhoneStatus

        public IEnumerable<PhoneStatu> GetAllPhoneStatuses()
        {
            return unitOfWork.PhoneStatusRepository.Get(orderBy: x => x.OrderBy(s => s.SortOrder));
        }

        #endregion
    }
}