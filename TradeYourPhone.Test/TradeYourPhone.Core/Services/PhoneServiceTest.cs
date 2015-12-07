using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradeYourPhone.Core.Services.Interface;
using System.Collections.Generic;
using TradeYourPhone.Core.Services.Implementation;
using TradeYourPhone.Test.DummyRepositories;
using System.Linq;
using System.Linq.Expressions;
using TradeYourPhone.Test.SetupData;
using TradeYourPhone.Web.ViewModels;
using TradeYourPhone.Test.Repositories;
using TradeYourPhone.Core.Repositories.Implementation;
using TradeYourPhone.Core.Models;
using System.Transactions;
using TradeYourPhone.Core.Enums;
using TradeYourPhone.Core.DTO;

namespace TradeYourPhone.Test
{
    [TestClass]
    public class PhoneServiceTest
    {
        IPhoneService phoneService;
        CreateMockData cmd;
        TransactionScope _trans;

        public PhoneServiceTest()
        {
            cmd = new CreateMockData();
            phoneService = cmd.GetPhoneService();
        }

        [TestInitialize()]
        public void Init()
        {
            _trans = new TransactionScope();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            _trans.Dispose();
        }

        #region PhoneMakeTests
        [TestMethod]
        public void GetAllPhoneMakesTest()
        {
            IEnumerable<PhoneMake> phoneMakes = phoneService.GetAllPhoneMakes();

            var Make = phoneMakes.Select(x => x.MakeName).ElementAt(1);
            Assert.IsTrue(phoneMakes.Count() == 5);
            Assert.AreEqual("HTC", Make);
        }

        [TestMethod]
        public void GetPhoneMakeByIdTest()
        {
            PhoneMake phoneMake = phoneService.GetPhoneMakeById(1);
            Assert.AreEqual("Samsung", phoneMake.MakeName);
        }

        [TestMethod]
        public void GetPhoneMakeByPhoneModelIdTest()
        {
            int id = phoneService.GetPhoneMakeIdByModelId(3);
            Assert.AreEqual(1, id);
        }

        [TestMethod]
        public void CreatePhoneMakeTest()
        {
            phoneService.CreatePhoneMake("LG");
            PhoneMake newMake = phoneService.GetAllPhoneMakes().OrderByDescending(x => x.ID).FirstOrDefault();
            Assert.AreEqual("LG", newMake.MakeName);
        }

        [TestMethod]
        public void ModifyPhoneMakeTest()
        {
            PhoneMake phoneMakeToModify = new PhoneMake
            {
                ID = 2,
                MakeName = "Cracken"
            };
            PhoneMake phoneMake = phoneService.GetPhoneMakeById(2);
            Assert.AreEqual("Sony", phoneMake.MakeName);
            Assert.AreEqual(true, phoneService.ModifyPhoneMake(phoneMakeToModify));
            PhoneMake modifiedMake = phoneService.GetPhoneMakeById(2);
            Assert.AreEqual("Cracken", modifiedMake.MakeName);
        }

        #endregion

        #region PhoneModelTests

        [TestMethod]
        public void GetAllPhoneModelsTest()
        {
            IEnumerable<PhoneModel> phoneModels = phoneService.GetAllPhoneModels();

            var Model = phoneModels.Select(x => x.ModelName).ElementAt(1);
            Assert.IsTrue(phoneModels.Count() == 12);
            Assert.AreEqual("Xperia Z2", Model);
        }

        [TestMethod]
        public void GetPhoneModelsForViewByMakeNameTest()
        {
            IList<PhoneModel> phoneModels = phoneService.GetPhoneModelsByMakeName("Samsung");

            var Model = phoneModels.Select(x => x.ModelName).ElementAt(1);
            Assert.IsTrue(phoneModels.Count() == 2);
            Assert.AreEqual("Galaxy S4", Model);
        }

        [TestMethod]
        public void GetMostPopularPhoneModelsTest()
        {
            IList<PhoneModel> phoneModels = phoneService.GetMostPopularPhoneModels(5);

            var Model = phoneModels.Select(x => x.ModelName).ElementAt(0);
            Assert.IsTrue(phoneModels.Count() == 2);
            Assert.AreEqual("iPhone 5", Model);
        }

        [TestMethod]
        public void GetPhoneModelsForViewByMakeNameNullParamTest()
        {
            IList<PhoneModel> phoneModels = phoneService.GetPhoneModelsByMakeName(null);
            Assert.IsTrue(!phoneModels.Any());
        }

        [TestMethod]
        public void GetPhoneModelByIdTest()
        {
            PhoneModel phoneModel = phoneService.GetPhoneModelById(3);
            Assert.AreEqual("Galaxy S6", phoneModel.ModelName);
        }

        [TestMethod]
        public void GetPhoneModelsByMakeIdTest()
        {
            IEnumerable<PhoneModel> phoneModels = phoneService.GetPhoneModelsByMakeId(3);
            Assert.AreEqual(6, phoneModels.Count());
            Assert.AreEqual(3, phoneModels.ElementAt(0).PhoneMakeId);
        }

        [TestMethod]
        public void CreatePhoneModelTest()
        {
            var model = new PhoneModel
            {
                PhoneMakeId = 4,
                ModelName = "m99",
                PhoneConditionPrices = new List<PhoneConditionPrice>()
                {
                    new PhoneConditionPrice()
                    {
                        PhoneConditionId = 1,
                        PhoneModelId = 1,
                        OfferAmount = 100
                    },
                    new PhoneConditionPrice()
                    {
                        PhoneConditionId = 2,
                        PhoneModelId = 1,
                        OfferAmount = 50
                    },
                    new PhoneConditionPrice()
                    {
                        PhoneConditionId = 3,
                        PhoneModelId = 1,
                        OfferAmount = 10
                    }
                },
            };

            bool result = phoneService.CreatePhoneModel(model);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ModifyPhoneModelTest()
        {
            var model = phoneService.GetPhoneModelById(9);
            model.ModelName = "m7";

            Assert.AreEqual(true, phoneService.ModifyPhoneModel(model));
            var modifiedModel = phoneService.GetPhoneModelById(9);
            Assert.AreEqual("m7", modifiedModel.ModelName);
        }

        #endregion

        #region PhoneConditionTests

        [TestMethod]
        public void GetAllPhoneConditionsTest()
        {
            IEnumerable<PhoneCondition> phoneConditions = phoneService.GetAllPhoneConditions();

            var condition = phoneConditions.Select(x => x.Condition).ElementAt(1);
            Assert.IsTrue(phoneConditions.Count() == 3);
            Assert.AreEqual("Good", condition);
        }

        [TestMethod]
        public void GetPhoneConditionByIdTest()
        {
            PhoneCondition phoneCondition = phoneService.GetPhoneConditionById(3);
            Assert.AreEqual("Faulty", phoneCondition.Condition);
        }

        [TestMethod]
        public void CreatePhoneConditionTest()
        {
            PhoneCondition phoneCondition = new PhoneCondition
            {
                Condition = "Brand New"
            };

            phoneService.CreatePhoneCondition(phoneCondition);
            PhoneCondition newCondition = phoneService.GetAllPhoneConditions().OrderByDescending(x => x.ID).FirstOrDefault();
            Assert.AreEqual("Brand New", newCondition.Condition);
        }

        [TestMethod]
        public void ModifyPhoneConditionTest()
        {
            PhoneCondition condition = phoneService.GetPhoneConditionById(2);
            Assert.AreEqual("Good", condition.Condition);

            PhoneCondition newCondition = new PhoneCondition
            {
                ID = 2,
                Condition = "Random"
            };

            Assert.AreEqual(true, phoneService.ModifyPhoneCondition(newCondition));
            PhoneCondition modifiedCondition = phoneService.GetPhoneConditionById(2);
            Assert.AreEqual("Random", modifiedCondition.Condition);
        }

        //[TestMethod]
        //public void DeletePhoneConditionByIdTest()
        //{
        //    Assert.AreEqual(2, phoneService.GetPhoneConditionById(2).ID);
        //    phoneService.DeletePhoneConditionById(2);
        //    PhoneCondition condition = phoneService.GetPhoneConditionById(2);
        //    Assert.AreEqual(null, condition);
        //}

        #endregion

        #region PhoneConditionPriceTests

        [TestMethod]
        public void GetAllPhoneConditionPricesTest()
        {
            IEnumerable<PhoneConditionPrice> phoneConditionPrices = phoneService.GetAllPhoneConditionPrices();

            var conditionPrice = phoneConditionPrices.Select(x => x.OfferAmount).ElementAt(1);
            Assert.IsTrue(phoneConditionPrices.Count() == 21);
            Assert.AreEqual(20, conditionPrice);
        }

        [TestMethod]
        public void GetPhoneConditionPriceByIdTest()
        {
            PhoneConditionPrice phoneConditionPrice = phoneService.GetPhoneConditionPriceById(3);
            Assert.AreEqual(20, phoneConditionPrice.OfferAmount);
        }

        [TestMethod]
        public void GetPhoneConditionPriceTest()
        {
            var phoneConditionPrices = phoneService.GetPhoneConditionPrice(1, 2);
            Assert.AreEqual((decimal)120.0000, phoneConditionPrices.OfferAmount);
        }

        [TestMethod]
        public void CreatePhoneConditionPriceTest()
        {
            PhoneConditionPrice phoneConditionPrice = new PhoneConditionPrice
            {
                OfferAmount = 350,
                PhoneConditionId = 1,
                PhoneModelId = 1
            };

            phoneService.CreatePhoneConditionPrice(phoneConditionPrice);
            PhoneConditionPrice newConditionPrice = phoneService.GetAllPhoneConditionPrices().OrderByDescending(x => x.ID).FirstOrDefault();
            Assert.AreEqual(350, newConditionPrice.OfferAmount);
        }

        [TestMethod]
        public void ModifyPhoneConditionPriceTest()
        {
            PhoneConditionPrice conditionPrice = phoneService.GetPhoneConditionPriceById(3);
            Assert.AreEqual((decimal)20.0000, conditionPrice.OfferAmount);

            PhoneConditionPrice newConditionPrice = new PhoneConditionPrice
            {
                ID = 3,
                OfferAmount = 50,
                PhoneModelId = 1,
                PhoneConditionId = 3
            };

            Assert.AreEqual(true, phoneService.ModifyPhoneConditionPrice(newConditionPrice));
            PhoneConditionPrice modifiedConditionPrice = phoneService.GetPhoneConditionPriceById(3);
            Assert.AreEqual(50, modifiedConditionPrice.OfferAmount);
        }

        //[TestMethod]
        //public void DeletePhoneConditionPriceByIdTest()
        //{
        //    Assert.AreEqual(2, phoneService.GetPhoneConditionPriceById(2).ID);
        //    phoneService.DeletePhoneConditionPriceById(2);
        //    PhoneConditionPrice conditionPrice = phoneService.GetPhoneConditionPriceById(2);
        //    Assert.AreEqual(null, conditionPrice);
        //}

        #endregion

        #region PhoneTests
        [TestMethod]
        public void GetAllPhonesTest()
        {
            IEnumerable<Phone> phones = phoneService.GetAllPhones();

            var phone = phones.Select(x => x).ElementAt(3);
            Assert.IsTrue(phones.Count() == 10);
            Assert.AreEqual(3, phone.PhoneMakeId);
            Assert.AreEqual(3, phone.PhoneModelId);
            Assert.AreEqual(2, phone.PhoneConditionId);
        }

        [TestMethod]
        public void GetPhoneByIdTest()
        {
            Phone phone = phoneService.GetPhoneById(13);
            Assert.AreEqual(3, phone.PhoneMakeId);
        }

        [TestMethod]
        public void CreatePhoneTest()
        {
            Phone phone = new Phone
            {
                PhoneMakeId = 2,
                PhoneModelId = 3,
                PhoneConditionId = 1,
                PhoneStatusId = 1,
                PurchaseAmount = 100
            };

            phoneService.CreatePhone(phone);
            Phone newPhone = phoneService.GetAllPhones().OrderByDescending(x => x.Id).FirstOrDefault();
            Assert.AreEqual(2, newPhone.PhoneMakeId);
            Assert.AreEqual(3, newPhone.PhoneModelId);
        }

        [TestMethod]
        public void ModifyPhoneTest()
        {
            Phone phone = phoneService.GetPhoneById(8);
            phone.PhoneMakeId = 1;
            phone.PhoneModelId = 4;

            Assert.IsNotNull(phoneService.ModifyPhone(phone, User.SystemUser.Value));
            Phone modifiedPhone = phoneService.GetPhoneById(8);
            Assert.AreEqual(1, modifiedPhone.PhoneMakeId);
            Assert.AreEqual(4, modifiedPhone.PhoneModelId);
        }

        [TestMethod]
        public void DeletePhoneByIdTest()
        {
            Assert.AreEqual(13, phoneService.GetPhoneById(13).Id);
            phoneService.DeletePhoneById(13);
            Phone phone = phoneService.GetPhoneById(13);
            Assert.AreEqual(null, phone);
        }

        [TestMethod]
        public void SearchPhonesByMakeIdTest()
        {
            var phones = phoneService.SearchPhones("0", 3, 0, 0);
            Assert.AreEqual(5, phones.Count);
            Assert.AreEqual("Apple", phones.ElementAt(0).PhoneMake.MakeName);
        }

        [TestMethod]
        public void SearchPhonesByModelIdTest()
        {
            var phones = phoneService.SearchPhones("0", 0, 3, 0);
            Assert.AreEqual(4, phones.Count);
            Assert.AreEqual("Apple", phones.ElementAt(0).PhoneMake.MakeName);
        }

        [TestMethod]
        public void SearchPhonesByMakeIdAndModelIdTest()
        {
            var phones = phoneService.SearchPhones("0", 3, 3, 0);
            Assert.AreEqual(3, phones.Count);
            Assert.AreEqual("Apple", phones.ElementAt(0).PhoneMake.MakeName);
        }

        [TestMethod]
        public void SearchPhonesByMakeIdAndModelIdAndPhoneStatusIdTest()
        {
            var phones = phoneService.SearchPhones("0", 3, 3, 2);
            Assert.AreEqual(2, phones.Count);
            Assert.AreEqual("Apple", phones.ElementAt(0).PhoneMake.MakeName);
        }

        [TestMethod]
        public void SearchPhonesByPhoneStatusIdTest()
        {
            var phones = phoneService.SearchPhones("0", 0, 0, 1);
            Assert.AreEqual(4, phones.Count);
        }

        [TestMethod]
        public void SearchPhonesByIdTest()
        {
            var phones = phoneService.SearchPhones("7", 0, 0, 0);
            Assert.AreEqual(1, phones.Count);
        }

        [TestMethod]
        public void SearchPhonesNullParamTest()
        {
            var phones = phoneService.SearchPhones("0", 0, 0, 0);
            Assert.AreEqual(10, phones.Count);
        }

        [TestMethod]
        public void SearchPhonesNonIntPhoneIdParamTest()
        {
            var phones = phoneService.SearchPhones("gh", 0, 0, 0);
            Assert.AreEqual(0, phones.Count);
        }

        [TestMethod]
        public void SearchPhonesNullPhoneIdParamTest()
        {
            var phones = phoneService.SearchPhones(null, 0, 0, 0);
            Assert.AreEqual(10, phones.Count);
        }

        [TestMethod]
        public void GetSortedPhonesTest()
        {
            var phones = phoneService.GetAllPhones().ToList();
            phones = phoneService.GetSortedPhones(phones, null);
            Assert.AreEqual("Samsung", phones.ElementAt(0).PhoneMake.MakeName);
            Assert.AreEqual("Sony", phones.ElementAt(1).PhoneMake.MakeName);
            Assert.AreEqual("Sony", phones.ElementAt(2).PhoneMake.MakeName);
        }

        [TestMethod]
        public void GetSortedPhonesByMakeDescTest()
        {
            var phones = phoneService.GetAllPhones().ToList();
            phones = phoneService.GetSortedPhones(phones, "phoneMake_desc");
            Assert.AreEqual("HTC", phones.ElementAt(0).PhoneMake.MakeName);
            Assert.AreEqual("Apple", phones.ElementAt(5).PhoneMake.MakeName);
            Assert.AreEqual("Sony", phones.ElementAt(6).PhoneMake.MakeName);
        }

        [TestMethod]
        public void GetSortedPhonesByModelTest()
        {
            var phones = phoneService.GetAllPhones().ToList();
            phones = phoneService.GetSortedPhones(phones, "phoneModel_asc");
            Assert.AreEqual("Galaxy S4", phones.ElementAt(0).PhoneModel.ModelName);
            Assert.AreEqual("Galaxy S6", phones.ElementAt(5).PhoneModel.ModelName);
            Assert.AreEqual("iPhone 5", phones.ElementAt(6).PhoneModel.ModelName);

        }

        [TestMethod]
        public void GetSortedPhonesByModelDescTest()
        {
            var phones = phoneService.GetAllPhones().ToList();
            phones = phoneService.GetSortedPhones(phones, "phoneModel_desc");
            Assert.AreEqual("iPhone 5", phones.ElementAt(0).PhoneModel.ModelName);
            Assert.AreEqual("iPhone 5", phones.ElementAt(1).PhoneModel.ModelName);
            Assert.AreEqual("iPhone 5", phones.ElementAt(2).PhoneModel.ModelName);
        }

        [TestMethod]
        public void GetSortedPhonesByConditionTest()
        {
            var phones = phoneService.GetAllPhones().ToList();
            phones = phoneService.GetSortedPhones(phones, "phoneCondition_asc");
            Assert.AreEqual("New", phones.ElementAt(0).PhoneCondition.Condition);
            Assert.AreEqual("New", phones.ElementAt(5).PhoneCondition.Condition);
            Assert.AreEqual("New", phones.ElementAt(6).PhoneCondition.Condition);
        }

        [TestMethod]
        public void GetSortedPhonesByConditionDescTest()
        {
            var phones = phoneService.GetAllPhones().ToList();
            phones = phoneService.GetSortedPhones(phones, "phoneCondition_desc");
            Assert.AreEqual("Faulty", phones.ElementAt(0).PhoneCondition.Condition);
            Assert.AreEqual("Good", phones.ElementAt(1).PhoneCondition.Condition);
            Assert.AreEqual("New", phones.ElementAt(3).PhoneCondition.Condition);
        }

        [TestMethod]
        public void GetSortedPhonesByStatusTest()
        {
            var phones = phoneService.GetAllPhones().ToList();
            phones = phoneService.GetSortedPhones(phones, "phoneStatus_asc");
            Assert.AreEqual(1, phones.ElementAt(0).PhoneStatusId);
            Assert.AreEqual(1, phones.ElementAt(1).PhoneStatusId);
            Assert.AreEqual(1, phones.ElementAt(2).PhoneStatusId);
            Assert.AreEqual(1, phones.ElementAt(3).PhoneStatusId);
            Assert.AreEqual(2, phones.ElementAt(4).PhoneStatusId);
            Assert.AreEqual(2, phones.ElementAt(5).PhoneStatusId);
            Assert.AreEqual(2, phones.ElementAt(6).PhoneStatusId);
        }

        [TestMethod]
        public void GetSortedPhonesByStatusDescTest()
        {
            var phones = phoneService.GetAllPhones().ToList();
            phones = phoneService.GetSortedPhones(phones, "phoneStatus_desc");
            Assert.AreEqual(9, phones.ElementAt(0).PhoneStatusId);
            Assert.AreEqual(8, phones.ElementAt(1).PhoneStatusId);
            Assert.AreEqual(8, phones.ElementAt(2).PhoneStatusId);
            Assert.AreEqual(2, phones.ElementAt(3).PhoneStatusId);
            Assert.AreEqual(2, phones.ElementAt(4).PhoneStatusId);
            Assert.AreEqual(2, phones.ElementAt(5).PhoneStatusId);
            Assert.AreEqual(1, phones.ElementAt(6).PhoneStatusId);
        }

        [TestMethod]
        public void GetSortedPhonesByPurchaseAmountTest()
        {
            var phones = phoneService.GetAllPhones().ToList();
            phones = phoneService.GetSortedPhones(phones, "purchaseAmount_asc");
            Assert.AreEqual(100, phones.ElementAt(0).PurchaseAmount);
            Assert.AreEqual(120, phones.ElementAt(1).PurchaseAmount);
            Assert.AreEqual(120, phones.ElementAt(2).PurchaseAmount);
            Assert.AreEqual(150, phones.ElementAt(3).PurchaseAmount);
            Assert.AreEqual(180, phones.ElementAt(4).PurchaseAmount);
            Assert.AreEqual(180, phones.ElementAt(5).PurchaseAmount);
            Assert.AreEqual(180, phones.ElementAt(6).PurchaseAmount);
        }

        [TestMethod]
        public void GetSortedPhonesByPurchaseAmountDescTest()
        {
            var phones = phoneService.GetAllPhones().ToList();
            phones = phoneService.GetSortedPhones(phones, "purchaseAmount_desc");
            Assert.AreEqual(150, phones.ElementAt(6).PurchaseAmount);
            Assert.AreEqual(180, phones.ElementAt(5).PurchaseAmount);
            Assert.AreEqual(180, phones.ElementAt(4).PurchaseAmount);
            Assert.AreEqual(180, phones.ElementAt(3).PurchaseAmount);
            Assert.AreEqual(180, phones.ElementAt(2).PurchaseAmount);
            Assert.AreEqual(180, phones.ElementAt(1).PurchaseAmount);
            Assert.AreEqual(270, phones.ElementAt(0).PurchaseAmount);
        }

        [TestMethod]
        public void GetSortedPhonesBySaleAmountTest()
        {
            var phones = phoneService.GetAllPhones().ToList();
            phones = phoneService.GetSortedPhones(phones, "saleAmount_asc");
            Assert.AreEqual(null, phones.ElementAt(0).SaleAmount);
            Assert.AreEqual(100, phones.ElementAt(1).SaleAmount);
            Assert.AreEqual(120, phones.ElementAt(2).SaleAmount);
            Assert.AreEqual(120, phones.ElementAt(3).SaleAmount);
            Assert.AreEqual(150, phones.ElementAt(4).SaleAmount);
            Assert.AreEqual(180, phones.ElementAt(5).SaleAmount);
            Assert.AreEqual(180, phones.ElementAt(6).SaleAmount);
        }

        [TestMethod]
        public void GetSortedPhonesBySaleAmountDescTest()
        {
            var phones = phoneService.GetAllPhones().ToList();
            phones = phoneService.GetSortedPhones(phones, "saleAmount_desc");
            Assert.AreEqual(270, phones.ElementAt(0).SaleAmount);
            Assert.AreEqual(180, phones.ElementAt(1).SaleAmount);
            Assert.AreEqual(180, phones.ElementAt(2).SaleAmount);
            Assert.AreEqual(180, phones.ElementAt(3).SaleAmount);
            Assert.AreEqual(180, phones.ElementAt(4).SaleAmount);
            Assert.AreEqual(150, phones.ElementAt(5).SaleAmount);
            Assert.AreEqual(120, phones.ElementAt(6).SaleAmount);
        }

        [TestMethod]
        public void GetSortedPhonesByPhoneIdTest()
        {
            var phones = phoneService.GetAllPhones().ToList();
            phones = phoneService.GetSortedPhones(phones, "phoneId_asc");
            Assert.AreEqual(100, phones.ElementAt(0).SaleAmount);
            Assert.AreEqual(150, phones.ElementAt(1).SaleAmount);
            Assert.AreEqual(120, phones.ElementAt(2).SaleAmount);
            Assert.AreEqual(120, phones.ElementAt(3).SaleAmount);
            Assert.AreEqual(null, phones.ElementAt(4).SaleAmount);
            Assert.AreEqual(180, phones.ElementAt(5).SaleAmount);
            Assert.AreEqual(180, phones.ElementAt(6).SaleAmount);
        }

        [TestMethod]
        public void GetSortedPhonesByPhoneIdDescTest()
        {
            var phones = phoneService.GetAllPhones().ToList();
            phones = phoneService.GetSortedPhones(phones, "phoneId_desc");
            Assert.AreEqual(270, phones.ElementAt(0).SaleAmount);
            Assert.AreEqual(180, phones.ElementAt(1).SaleAmount);
            Assert.AreEqual(180, phones.ElementAt(2).SaleAmount);
            Assert.AreEqual(180, phones.ElementAt(3).SaleAmount);
            Assert.AreEqual(180, phones.ElementAt(4).SaleAmount);
            Assert.AreEqual(null, phones.ElementAt(5).SaleAmount);
            Assert.AreEqual(120, phones.ElementAt(6).SaleAmount);
        }

        #endregion

        #region PhoneStatusTests

        [TestMethod]
        public void GetAllPhoneStatusesTest()
        {
            IEnumerable<PhoneStatu> phoneStatuses = phoneService.GetAllPhoneStatuses();

            var status = phoneStatuses.Select(x => x).ElementAt(0);
            Assert.IsTrue(phoneStatuses.Count() == 10);
            Assert.AreEqual("New", status.PhoneStatus);
        }

        #endregion
    }
}
