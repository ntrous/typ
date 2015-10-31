﻿tradeYourPhoneControllers.controller('QuoteCtrl', function ($scope, QuoteService, PhoneModelService, $location, $cookies, $q, $anchorScroll, $analytics, $timeout, phoneModels, phoneConditions, quote) {

    $scope.isActive = function (viewLocation) {
        return viewLocation == $location.path();
    };

    $scope.resetInputs = function () {
        $scope.search = {};
        $scope.condition = {
            id: ''
        };
        $scope.phoneOffer = null;
    }

    $scope.GetPhoneModels = function () {
        var deferredPromise = $q.defer();
        PhoneModelService.GetPhoneModels().then(function (response) {
            $scope.phoneModels = response;
            deferredPromise.resolve();
        });
        return deferredPromise.promise;
    }

    $scope.GetPhoneConditions = function () {
        var deferredPromise = $q.defer();
        QuoteService.GetPhoneConditions().then(function (response) {
            $scope.phoneConditions = response;
            deferredPromise.resolve();
        });
        return deferredPromise.promise;
    }

    $scope.GetPhoneOffer = function (modelId, conditionId) {
        if (!modelId) { return }

        var modelFound = false;
        for (var i = 0; i < $scope.phoneModels.length; i++) {
            if ($scope.phoneModels[i].id == modelId) {
                var model = $scope.phoneModels[i];
                var conditionPriceFound = false;
                for (var i = 0; i < model.conditionPrices.length; i++) {
                    if (model.conditionPrices[i].PhoneConditionId == conditionId) {
                        $scope.phoneOffer = model.conditionPrices[i].OfferAmount;
                        conditionPriceFound = true;
                        break;
                    }
                }
                modelFound = true;
                break;
            }
        }
    }

    $scope.SetupQuoteKey = function () {
        var deferredPromise = $q.defer();
        if (!$scope.quoteKey) {
            QuoteService.CreateQuote().then(function (response) {
                $scope.quoteKey = response;
                deferredPromise.resolve();
            });
        }
        else {
            deferredPromise.resolve();
        }
        return deferredPromise.promise;
    }

    $scope.addPhone = function (modelId, conditionId) {
        var deferredPromises = $q.defer();
        $scope.SetupQuoteKey().then(function () {
            QuoteService.AddPhoneToQuote($scope.quoteKey, modelId, conditionId).then(function (response) {
                if (response.Status == "OK") {
                    deferredPromises.resolve(response.QuoteDetails);
                }
                else if (response.Exception.Message == "500") {
                    QuoteService.CreateQuote().then(function (response) {
                        $scope.quoteKey = response;
                        $scope.addPhone(modelId, conditionId).then(function (response) {
                            deferredPromises.resolve(response);
                        });
                    });
                }
            });
        });

        return deferredPromises.promise;
    }

    $scope.addPhoneToQuote = function (modelId, conditionId) {
        $scope.btnSpinner = true;
        $scope.addPhone(modelId, conditionId).then(function (response) {
            $scope.quote = response;
            $scope.quote.PostageMethod = $scope.postageMethods[1];
            $scope.resetInputs();
            $scope.btnSpinner = false;
            $scope.goToTab('Quote', 0);
        });

    }

    $scope.GetQuoteDetails = function (key) {
        var deferredPromise = $q.defer();
        if (key) {
            QuoteService.GetQuoteDetails($scope.quoteKey).then(function (response) {
                if (response.Status = "OK") {
                    $scope.quote = response.QuoteDetails;
                    $scope.tabs[0].active = true;
                    if (response.QuoteDetails.Customer) {
                        $scope.customer = response.QuoteDetails.Customer;
                        $scope.tabs[1].disabled = false;
                    }
                    deferredPromise.resolve();
                }
                else if (response.Exception.Message == "500") {
                    QuoteService.CreateQuote().then(function (response) {
                        $scope.quoteKey = response;
                        deferredPromise.resolve();
                    });
                }
            });
        }
        else {
            deferredPromise.resolve();
        }
        return deferredPromise.promise;
    }

    $scope.ResetQuote = function () {
        $scope.goToTab('main', 0)
        $scope.customer = { paymentType: '' };
        $scope.detailsFormSubmitted = false;
        $scope.quote = [];
        $scope.quote.PostageMethod = $scope.postageMethods[1];
        $scope.GetPaymentTypes();
        $scope.GetStates();
        $scope.status = '';
    }


    $scope.deletePhoneFromQuote = function (phoneId, index) {
        $scope.deleteIndex = index;
        $scope.deleteSpinner = true;
        QuoteService.DeleteQuotePhone($scope.quoteKey, phoneId).then(function (response) {
            if (response.Status = "OK") {
                $scope.quote = response.QuoteDetails;
            }

            if ($scope.quote.Phones.length == 0) {
                $timeout(function () {
                    $location.hash('main');
                    $anchorScroll();
                });
            }
            $scope.deleteSpinner = false;
        });
    }

    $scope.GetStates = function () {
        var deferredPromise = $q.defer();
        QuoteService.GetStates().then(function (response) {
            $scope.states = response;
            $scope.customer.postageState = $scope.states[0].name;
            deferredPromise.resolve();
        });
        return deferredPromise.promise;
    }
    $scope.GetPaymentTypes = function () {
        QuoteService.GetPaymentTypes().then(function (response) {
            $scope.paymentTypes = response;
            $scope.customer.paymentType = $scope.paymentTypes[0].name;
        });
    }

    $scope.GetPostageMethods = function () {
        QuoteService.GetPostageMethods().then(function (response) {
            $scope.postageMethods = response;
            $scope.quote.PostageMethod = $scope.postageMethods[1];
        });
    }

    $scope.FinaliseQuote = function (customerValue, quote) {
        $scope.spinner = true;
        $scope.goToTab('Quote', 2);
        var viewModel = {
            customer: {
                FullName: customerValue.fullname,
                Email: customerValue.email,
                PhoneNumber: customerValue.mobile,
                Address: {
                    AddressLine1: customerValue.postageStreet,
                    AddressLine2: customerValue.postageSuburb,
                    PostCode: customerValue.postagePostcode,
                    CountryId: '1', // Australia
                    State: { StateNameShort: customerValue.postageState }
                },
                PaymentDetail: {
                    BSB: customerValue.bsb,
                    AccountNumber: customerValue.accountNum,
                    PaypalEmail: customerValue.paypalEmail,
                    PaymentType: {
                        PaymentTypeName: customerValue.paymentType
                    }
                }
            },
            PostageMethodId: quote.PostageMethod.Id,
            AgreedToTerms: quote.AgreedToTerms
        };

        QuoteService.FinaliseQuote($scope.quoteKey, viewModel).then(function (response) {
            if (response.Status = "OK") {
                $scope.result = $scope.quoteKey
                delete $cookies['tradeYourPhoneCookie'];
                $scope.quoteKey = undefined;
                $scope.status = response.QuoteDetails.QuoteStatus;
                $scope.customer = { paymentType: '' };
                $scope.spinner = false;

                // disable tabs so the user cannot edit previous tabs and quote
                $scope.tabs[1].disabled = true;
                $scope.tabs[0].disabled = true;
            }
        });
    }

    $scope.SaveQuote = function (form, customerValue, quote) {
        $scope.detailsFormSubmitted = true;
        if (form.$valid) {
            $scope.spinner = true;
            $scope.goToTab('Quote', 2);
            var viewModel = {
                customer: {
                    FullName: customerValue.fullname,
                    Email: customerValue.email,
                    PhoneNumber: customerValue.mobile,
                    Address: {
                        AddressLine1: customerValue.postageStreet,
                        AddressLine2: customerValue.postageSuburb,
                        PostCode: customerValue.postagePostcode,
                        CountryId: '1', // Australia
                        State: { StateNameShort: customerValue.postageState }
                    },
                    PaymentDetail: {
                        BSB: customerValue.bsb,
                        AccountNumber: customerValue.accountNum,
                        PaypalEmail: customerValue.paypalEmail,
                        PaymentType: {
                            PaymentTypeName: customerValue.paymentType
                        }
                    }
                },
                PostageMethodId: quote.PostageMethod.Id,
                AgreedToTerms: quote.AgreedToTerms
            };

            QuoteService.SaveQuote($scope.quoteKey, viewModel).then(function (response) {
                if (response.Status = "OK") {

                }
                $scope.spinner = false;
            });
        }
    }

    $scope.setCondition = function (item) {
        $scope.condition.id = 2;
        if (!$scope.phoneModels) {
            $scope.GetPhoneModels().then(function (response) {
                $scope.GetPhoneOffer(item.id, $scope.condition.id)
            });
        } else {
            $scope.GetPhoneOffer(item.id, $scope.condition.id)
        }

    }

    $scope.goToTab = function (location, tab) {
        if (!$scope.tabs[tab].active) {
            $scope.tabs[tab].active = true;
        }
        if ($scope.tabs[tab].disabled) {
            $scope.tabs[tab].disabled = false;
        }

        $timeout(function () {
            $location.hash(location);
            $anchorScroll();
        });
    }


    $scope.DisplayBankTransfer = function () {
        if ($scope.customer.paymentType) {
            return $scope.customer.paymentType == 'Bank Transfer';
        }
        else {
            return false;
        }
    }

    $scope.DisplayPaypal = function () {
        if ($scope.customer.paymentType) {
            return $scope.customer.paymentType == 'Paypal';
        }
        else {
            return false;
        }
    }

    $scope.DoesQuoteContainApple = function () {
        if ($scope.quote) {
            return _.some($scope.quote.Phones, 'PhoneMakeName', 'Apple');
        }
        else {
            return false;
        }
    }

    $scope.WaitingForDelivery = function () {
        return $scope.status == "Waiting For Delivery";
    }

    $scope.RequiresSatchel = function () {
        return $scope.status == "Requires Satchel";
    }

    $scope.DisplaySatchel = function () {
        return $scope.quote && $scope.quote.PostageMethod && $scope.quote.PostageMethod.Id == 1;
    }

    $scope.DisplayPostage = function () {
        return $scope.quote && $scope.quote.PostageMethod && $scope.quote.PostageMethod.Id == 2;
    }

    $scope.updatePaypalEmail = function ($event) {
        var checkbox = $event.target;
        if (checkbox.checked) {
            $scope.customer.paypalEmail = $scope.customer.email;
        }
        else {
            $scope.customer.paypalEmail = '';
        }
    };

    $scope.fireFieldEvent = function (fieldName) {
        $analytics.eventTrack('detailsForm: ' + fieldName, { category: 'QuoteProcess' });
    }

    $scope.init = function () {
        $scope.phoneModels = phoneModels;
        $scope.phoneConditions = phoneConditions;
        $scope.status = '';
        $scope.condition = { id: '' };
        $scope.customer = { paymentType: '' };

        $scope.tabs = [
       { title: 'Review Your Quote', link: 'QuoteTab.html', disabled: false, active: true },
      { title: 'Enter Your Details', link: 'DetailsTab.html', disabled: true, active: false },
      { title: 'Review Your Summary', link: 'SummaryTab.html', disabled: true, active: false }
        ];

        $q.all([
            $scope.GetPaymentTypes(),
            $scope.GetStates()
        ]).then(function () {
            $scope.quote = quote;
            if (!$scope.quote) {
                $scope.quote = { PostageMethod: null };
            }
            $scope.GetPostageMethods();
            $scope.tabs[0].active = true;
            if ($scope.quote && $scope.quote.Customer) {
                $scope.customer = quote.Customer;
                $scope.tabs[1].disabled = false;
            }
        });
    };
    $scope.postageMethod = {};
    $scope.init();
    $scope.quoteKey = $cookies.tradeYourPhoneCookie;
    $scope.detailsFormSubmitted = false;
    $scope.onlyNumbersPattern = /^[0-9]*$/;
    $scope.numberwithSpacesPattern = /^[\d ]+$/;

    $scope.search = {
        model: PhoneModelService.GetCurrentPhoneModel()
    };

    if ($scope.search.model != null) {
        $scope.setCondition($scope.search.model);
    }
});