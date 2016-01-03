tradeYourPhoneControllers.controller('QuoteCtrl1', function ($scope, QuoteService, PhoneModelService, $location, $cookies, $q, $anchorScroll, $analytics, $timeout, phoneModels, phoneConditions, quote) {

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

        for (var i = 0; i < $scope.phoneModels.length; i++) {
            if ($scope.phoneModels[i].Id === modelId) {
                var model = $scope.phoneModels[i];
                for (var i = 0; i < model.ConditionPrices.length; i++) {
                    if (model.ConditionPrices[i].PhoneConditionId === conditionId) {
                        $scope.phoneOffer = model.ConditionPrices[i].OfferAmount;
                        break;
                    }
                }
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
            var currentPostageMethod = $scope.quote.PostageMethod;
            $scope.quote = response;
            $scope.quote.PostageMethod = currentPostageMethod;
            $scope.resetInputs();
            $scope.btnSpinner = false;
        });

    }

    $scope.GetQuoteDetails = function (key) {
        var deferredPromise = $q.defer();
        if (key) {
            QuoteService.GetQuoteDetails($scope.quoteKey).then(function (response) {
                if (response.Status === "OK") {
                    $scope.quote = response.QuoteDetails;
                    if (response.QuoteDetails.Customer) {
                        $scope.customer = response.QuoteDetails.Customer;
                    }
                    deferredPromise.resolve();
                }
                else if (response.Exception.Message === "500") {
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
        $timeout(function () {
            $location.hash("main");
            $anchorScroll();
        });
        $scope.customer = { paymentType: '' };
        $scope.detailsFormSubmitted = false;
        $scope.quote = [];
        $scope.quote.PostageMethod = $scope.postageMethods[1];
        $scope.GetPaymentTypes();
        $scope.GetStates();
        $scope.status = '';
        $scope.submittedQuoteDetails = null;
    }


    $scope.deletePhoneFromQuote = function (phoneId, index) {
        $scope.deleteIndex = index;
        $scope.deleteSpinner = true;
        QuoteService.DeleteQuotePhone($scope.quoteKey, phoneId).then(function (response) {
            if (response.Status === "OK") {
                var currentPostageMethod = $scope.quote.PostageMethod;
                $scope.quote = response.QuoteDetails;
                $scope.quote.PostageMethod = currentPostageMethod;
            }

            if ($scope.quote.Phones.length === 0) {
                $timeout(function () {
                    $location.hash("main");
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
            $scope.customer.postageState = $scope.states[0];
            deferredPromise.resolve();
        });
        return deferredPromise.promise;
    }
    $scope.GetPaymentTypes = function () {
        QuoteService.GetPaymentTypes().then(function (response) {
            $scope.paymentTypes = response;
            $scope.customer.paymentType = $scope.paymentTypes[0];
        });
    }

    $scope.GetPostageMethods = function () {
        QuoteService.GetPostageMethods().then(function (response) {
            $scope.postageMethods = response;
            $scope.quote.PostageMethod = $scope.postageMethods[1];
        });
    }

    $scope.FinaliseQuote = function (form, customerValue, quote) {   
        $scope.detailsFormSubmitted = true;
        if (form.$valid) {
            $timeout(function () {
                $location.hash("main");
                $anchorScroll();
            });
            $scope.spinner = true;
           
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
                        StateId: customerValue.postageState.ID
                    },
                    PaymentDetail: {
                        BSB: customerValue.bsb,
                        AccountNumber: customerValue.accountNum,
                        PaypalEmail: customerValue.paypalEmail,
                        PaymentTypeId: customerValue.paymentType.ID
                    }
                },
                PostageMethodId: quote.PostageMethod.Id,
                AgreedToTerms: true
            };

            QuoteService.FinaliseQuote($scope.quoteKey, viewModel).then(function(response) {
                if (response.Status = "OK") {
                    $timeout(function () {
                        $location.hash("QuoteSubmitted");
                        $anchorScroll();
                    });
                    $scope.result = $scope.quoteKey;
                    $scope.status = response.QuoteDetails.QuoteStatus;
                    delete $cookies['tradeYourPhoneCookie'];
                    $scope.quoteKey = undefined;               
                    $scope.customer = { paymentType: '' };
                    $scope.spinner = false;
                    $scope.quote = [];
                    $scope.QuoteSubmitClicked();
                }
            });
        }
    }

    $scope.GoToErrors = function() {
        $timeout(function () {
            $location.hash("Details");
            $anchorScroll();
        });
    }

    $scope.setCondition = function (item) {
        $scope.condition.id = 2;
        if (!$scope.phoneModels) {
            $scope.GetPhoneModels().then(function (response) {
                $scope.GetPhoneOffer(item.Id, $scope.condition.id);
            });
        } else {
            $scope.GetPhoneOffer(item.Id, $scope.condition.id);
        }

    }


    $scope.DisplayBankTransfer = function () {
        if ($scope.customer.paymentType) {
            return $scope.customer.paymentType.PaymentTypeName === 'Bank Transfer';
        }
        else {
            return false;
        }
    }

    $scope.DisplayPaypal = function () {
        if ($scope.customer.paymentType) {
            return $scope.customer.paymentType.PaymentTypeName === 'Paypal';
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

    $scope.QuoteSubmitClicked = function () {
        window.google_trackConversion({
            google_conversion_id: 943138204,
            google_conversion_label: "6-nXCJCZ_WAQnMvcwQM",
            google_conversion_value: 50.00,
            google_conversion_currency: "AUD",
            google_remarketing_only: false
    });
}

    $scope.init = function () {
        $scope.phoneModels = phoneModels;
        $scope.phoneConditions = phoneConditions;
        $scope.status = '';
        $scope.condition = { id: '' };
        $scope.customer = { paymentType: '' };

        $q.all([
            $scope.GetPaymentTypes(),
            $scope.GetStates()
        ]).then(function () {
            $scope.quote = quote;
            if (!$scope.quote) {
                $scope.quote = { PostageMethod: null };
            }

            $scope.GetPostageMethods();
            
            if ($scope.quote && $scope.quote.Customer) {
                $scope.customer = quote.Customer;
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