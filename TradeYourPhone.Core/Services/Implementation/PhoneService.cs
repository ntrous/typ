using TradeYourPhone.Core;
using TradeYourPhone.Core.Models;
using TradeYourPhone.Core.Repositories.Interface;
using TradeYourPhone.Core.Utilities;
using TradeYourPhone.Core.Services.Interface;
using TradeYourPhone.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
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

        public bool CreatePhoneMake(PhoneMake phoneMake)
        {
            if (!DoesPhoneMakeExist(phoneMake.MakeName))
            {
                phoneMake.MakeName = Util.UppercaseFirst(phoneMake.MakeName);
                unitOfWork.PhoneMakeRepository.Insert(phoneMake);
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

        //public bool DeletePhoneMakeById(int phoneMakeId)
        //{
        //    unitOfWork.PhoneMakeRepository.Delete(phoneMakeId);
        //    unitOfWork.Save();
        //    return true;
        //}

        private bool DoesPhoneMakeExist(string phoneMake)
        {
            IEnumerable<PhoneMake> phoneMakes = GetAllPhoneMakes().Where(x => x.MakeName.ToLower() == phoneMake.ToLower());
            if (phoneMakes.Count() > 0) { return true; }
            return false;
        }

        #endregion

        #region PhoneModels

        public IEnumerable<PhoneModel> GetAllPhoneModels()
        {
            var models = unitOfWork.PhoneModelRepository.Get();
            return models.OrderBy(m => m.ModelName);
        }

        public IList<PhoneViewModel> GetAllPhoneModelsForView()
        {
            var models = unitOfWork.PhoneModelRepository.Get().OrderByDescending(p => p.ModelName, new NaturalSortComparer<string>());
            var phoneModels = PreparePhoneModelsForView(models);

            return phoneModels;
        }

        public IList<PhoneViewModel> GetPhoneModelsForViewByMakeName(string makeName)
            {
            Expression<Func<PhoneModel, bool>> filterExp = (x => x.PhoneMake.MakeName == makeName);
            var models = unitOfWork.PhoneModelRepository.Get(filterExp).OrderByDescending(p => p.ModelName, new NaturalSortComparer<string>());
            var phoneModels = PreparePhoneModelsForView(models);

            return phoneModels;
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

        public IList<PhoneViewModel> GetMostPopularPhoneModels(int limit)
                    {
            var models = unitOfWork.PhoneModelRepository.Get().OrderByDescending(p => p.ModelName, new NaturalSortComparer<string>());
            var phoneStatuses = new[] { (int)PhoneStatusEnum.Paid, (int)PhoneStatusEnum.Listed, (int)PhoneStatusEnum.ReadyForSale, (int)PhoneStatusEnum.Sold, };
            IEnumerable<PhoneModel> topModels = models.Where(m => m.Phones.Any(p => phoneStatuses.Contains(p.PhoneStatusId))).OrderByDescending(m => m.Phones.Where(p => phoneStatuses.Contains(p.PhoneStatusId)).Count());

            if(limit != 0)
            {
                topModels = topModels.Take(limit);
            }

            var phoneModels = PreparePhoneModelsForView(topModels);

            return phoneModels;
        }

        /// <summary>
        /// Prepares PhoneModel objects into PhoneViewModel
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        private List<PhoneViewModel> PreparePhoneModelsForView(IEnumerable<PhoneModel> models)
        {
            List<PhoneViewModel> phoneModels = new List<PhoneViewModel>();

            foreach (var model in models)
            {
                PhoneViewModel newModel = new PhoneViewModel
                {
                    id = model.ID.ToString(),
                    name = model.PhoneMake.MakeName + " " + model.ModelName,
                    image = model.PrimaryImageString,
                    conditionPrices = new List<Condition>()
                };

                foreach (var conditionPrice in model.PhoneConditionPrices)
                {
                    Condition condition = new Condition
                    {
                        PhoneConditionId = conditionPrice.PhoneConditionId,
                        OfferAmount = conditionPrice.OfferAmount
                    };
                    newModel.conditionPrices.Add(condition);
                };

                phoneModels.Add(newModel);
            }

            return phoneModels;
        }

        public bool CreatePhoneModel(PhoneModelViewModel phoneModelViewModel)
        {
            if (!DoesPhoneModelExistForMake(phoneModelViewModel.Model.PhoneMakeId, phoneModelViewModel.Model.ModelName))
            {
                if (phoneModelViewModel.PrimaryImage != null && phoneModelViewModel.PrimaryImage.ContentLength != 0)
                {
                    byte[] imageData = null;
                    imageData = ImageManager.CompressImage(phoneModelViewModel.PrimaryImage.InputStream, "100");

                    phoneModelViewModel.Model.PrimaryImage = imageData;
                }
                unitOfWork.PhoneModelRepository.Insert(phoneModelViewModel.Model);
                unitOfWork.Save();

                foreach (var condition in phoneModelViewModel.ConditionPrices)
                {
                    condition.PhoneModelId = phoneModelViewModel.Model.ID;
                    unitOfWork.PhoneConditionPriceRepository.Insert(condition);
                }
                unitOfWork.Save();
                return true;
            }
            return false;
        }

        public bool ModifyPhoneModel(PhoneModelViewModel phoneModelViewModel)
        {
            PhoneModel originalModel = GetPhoneModelById(phoneModelViewModel.Model.ID);
            if (!DoesPhoneModelExistForMake(phoneModelViewModel.Model.PhoneMakeId, phoneModelViewModel.Model.ModelName, phoneModelViewModel.Model.ID))
            {
                originalModel.ModelName = phoneModelViewModel.Model.ModelName;
            }

            if (phoneModelViewModel.PrimaryImage != null && phoneModelViewModel.PrimaryImage.ContentLength != 0)
            {
                byte[] imageData = null;
                imageData = ImageManager.CompressImage(phoneModelViewModel.PrimaryImage.InputStream, "100");

                originalModel.PrimaryImage = imageData;
            }

            unitOfWork.PhoneModelRepository.Update(originalModel);
            unitOfWork.Save();

            foreach (var condition in phoneModelViewModel.ConditionPrices)
            {
                if (condition.ID == 0)
                {
                    unitOfWork.PhoneConditionPriceRepository.Insert(condition);
                }
                else
                {
                    unitOfWork.PhoneConditionPriceRepository.Update(condition);
                }
            }

            unitOfWork.Save();
            return true;
        }

        //public bool DeletePhoneModelById(int phoneModelId)
        //{
        //    unitOfWork.PhoneModelRepository.Delete(phoneModelId);
        //    unitOfWork.Save();
        //    return true;
        //}

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
            IEnumerable<PhoneModel> phoneModels = GetAllPhoneModels().Where(x => x.ID != excludeModelId && x.PhoneMakeId == phoneMakeId && x.ModelName.ToLower() == phoneModelName.ToLower());
            if (phoneModels.Count() > 0) { return true; }
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

        //public bool DeletePhoneConditionById(int phoneConditionId)
        //{
        //    unitOfWork.PhoneConditionRepository.Delete(phoneConditionId);
        //    unitOfWork.Save();
        //    return true;
        //}

        private bool DoesPhoneConditionExist(string phoneCondition)
        {
            IEnumerable<PhoneCondition> phoneConditions = GetAllPhoneConditions().Where(x => x.Condition.ToLower() == phoneCondition.ToLower());
            if (phoneConditions.Count() > 0) { return true; }
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

            var conditionPrices = unitOfWork.PhoneConditionPriceRepository.Get(filter: filterExp).FirstOrDefault();
            return conditionPrices;
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

        //public bool DeletePhoneConditionPriceById(int phoneConditionPriceId)
        //{
        //    unitOfWork.PhoneConditionPriceRepository.Delete(phoneConditionPriceId);
        //    unitOfWork.Save();
        //    return true;
        //}

        #endregion

        #region Phones

        public IEnumerable<Phone> GetAllPhones()
        {
            return unitOfWork.PhoneRepository.Get();
        }

        public Phone GetPhoneById(int phoneId)
        {
            Phone phone = unitOfWork.PhoneRepository.Get(p => p.Id == phoneId, null, "PhoneMake,PhoneModel,PhoneCondition").FirstOrDefault();
            return phone;
        }

        public bool CreatePhone(Phone phone)
        {
            unitOfWork.PhoneRepository.Insert(phone);
            unitOfWork.Save();
            UpdatePhoneStatusHistory(phone.Id, 0, phone.PhoneStatusId, null);
            unitOfWork.Save();
            return true;
        }

        public bool ModifyPhone(Phone phone, string userId)
        {
            var histories = GetPhoneStatusHistory(phone.Id);
            int existingPhoneStatusId = 0;
            if (histories != null && histories.Count() > 0)
            {
                existingPhoneStatusId = histories.OrderByDescending(x => x.StatusDate).First().PhoneStatusId;
            }
            unitOfWork.PhoneRepository.Update(phone);
            UpdatePhoneStatusHistory(phone.Id, existingPhoneStatusId, phone.PhoneStatusId, userId);
            unitOfWork.Save();
            return true;
        }

        public bool ModifyPhones(IList<Phone> phones, string userId)
        {
            foreach (var phone in phones)
            {
                ModifyPhone(phone, userId);
            }
            unitOfWork.Save();
            return true;
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

            if(phoneId != null && phoneId.Length > 0)
            {
                int id = 0;
                bool isInt = int.TryParse(phoneId, out id);
                if (id > 0)
                {
                    predicate = predicate.And(c => c.Id == id);
                }
                else if(!isInt)
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
        public List<Phone> GetSortedPhones(List<Phone> phonesToSort, PhoneIndexViewModel viewModel)
        {
            viewModel.PhoneMakeParm = String.IsNullOrEmpty(viewModel.SortOrder) ? "phoneMake_desc" : "";
            viewModel.PhoneModelParm = viewModel.SortOrder == "phoneModel_asc" ? "phoneModel_desc" : "phoneModel_asc";
            viewModel.PhoneConditionParm = viewModel.SortOrder == "phoneCondition_asc" ? "phoneCondition_desc" : "phoneCondition_asc";
            viewModel.PhoneStatusParm = viewModel.SortOrder == "phoneStatus_asc" ? "phoneStatus_desc" : "phoneStatus_asc";
            viewModel.PurchaseAmountParm = viewModel.SortOrder == "purchaseAmount_asc" ? "purchaseAmount_desc" : "purchaseAmount_asc";
            viewModel.SaleAmountParm = viewModel.SortOrder == "saleAmount_asc" ? "saleAmount_desc" : "saleAmount_asc";
            viewModel.PhoneIdParm = viewModel.SortOrder == "phoneId_asc" ? "phoneId_desc" : "phoneId_asc";

            IEnumerable <Phone> phones = phonesToSort;

            switch (viewModel.SortOrder)
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