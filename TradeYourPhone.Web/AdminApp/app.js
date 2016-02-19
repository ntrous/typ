var tradeYourPhoneAdminApp = angular.module('typAdmin', [
  'ui.bootstrap',
  'ngRoute',
  'ngCookies',
  'typAdmin.controllers',
  'typAdmin.service',
  'typAdmin.common',
  'typAdmin.directives',
  'LocalStorageModule',
  'naif.base64'
]).constant('_', window._);

var tradeYourPhoneAdminControllers = angular.module('typAdmin.controllers', ['typAdmin.service']);
var tradeYourPhoneAdminServices = angular.module('typAdmin.service', []);
var tradeYourPhoneAdminCommon = angular.module('typAdmin.common', []);

tradeYourPhoneAdminApp.config(['$routeProvider', '$locationProvider',
    function ($routeProvider, $locationProvider) {
        var variation = 0;
        if (typeof cxApi != "undefined") {
            variation = cxApi.chooseVariation();
        }

        $routeProvider
        .when('/Admin', {
            templateUrl: '../AdminApp/Dashboard/Dashboard.html',
            controller: 'AdminDashboardCtrl',
            title: 'Dashboard - Trade Your Phone',
            caseInsensitiveMatch: true,
            resolve: {
                dashboardViewModel: function (AdminDashboardService) {
                    return AdminDashboardService.GetDashboardData();
                }
            }
        }).
            when('/Admin/Login', {
                templateUrl: '../AdminApp/Account/Login.html',
                controller: 'loginController',
                title: 'Login - Trade Your Phone',
                caseInsensitiveMatch: true
            }).
            when('/Admin/Quotes/:reset?', {
                templateUrl: '../AdminApp/Quote/Quotes.html',
                controller: 'AdminQuotesCtrl',
                title: 'Quotes - Trade Your Phone',
                caseInsensitiveMatch: true,
                resolve: {
                    quoteViewModel: function (AdminQuoteService, $route) {
                        if (!$route.current.params.reset) {
                            var currentModel = AdminQuoteService.GetCurrentQuoteViewModel();
                            if (currentModel === null) {
                                return AdminQuoteService.GetQuotes();
                            } else {
                                return currentModel;
                            }
                        } else {
                            return AdminQuoteService.GetQuotes();
                        }
                    }
                }
            }).
            when('/Admin/Quote/:id', {
                templateUrl: '../AdminApp/Quote/Quote.html',
                controller: 'AdminQuoteCtrl',
                title: 'Quote - Trade Your Phone',
                caseInsensitiveMatch: true,
                resolve: {
                    quoteDetailsViewModel: function (AdminQuoteService, $route) {
                        return AdminQuoteService.GetQuote($route.current.params.id);
                    }
                }
            }).
            when('/Admin/Phones/:reset?', {
                templateUrl: '../AdminApp/Phone/Phones.html',
                controller: 'AdminPhonesCtrl',
                title: 'Phones - Trade Your Phone',
                caseInsensitiveMatch: true,
                resolve: {
                    phoneIndexViewModel: function (AdminPhoneService, $route) {
                        if (!$route.current.params.reset) {
                            var currentModel = AdminPhoneService.GetCurrentPhoneIndexViewModel();
                            if (currentModel === null) {
                                return AdminPhoneService.GetPhones();
                            } else {
                                return currentModel;
                            }
                        } else {
                            return AdminPhoneService.GetPhones();
                        }
                    }
                }
            }).
            when('/Admin/Phone/:id', {
                templateUrl: '../AdminApp/Phone/Phone.html',
                controller: 'AdminPhoneCtrl',
                title: 'Phone - Trade Your Phone',
                caseInsensitiveMatch: true,
                resolve: {
                    phoneDetailsViewModel: function (AdminPhoneService, $route) {
                        return AdminPhoneService.GetPhone($route.current.params.id);
                    }
                }
            }).
            when('/Admin/CreatePhone', {
                templateUrl: '../AdminApp/Phone/CreatePhone.html',
                controller: 'AdminPhoneCtrl',
                title: 'Create Phone - Trade Your Phone',
                caseInsensitiveMatch: true,
                resolve: {
                    phoneDetailsViewModel: function (AdminPhoneService) {
                        return AdminPhoneService.GetPhoneReferenceData();
                    }
                }
            }).
            when('/Admin/PhoneMakes', {
                templateUrl: '../AdminApp/PhoneMakes/PhoneMakes.html',
                controller: 'AdminPhoneMakesCtrl',
                title: 'Phone Makes - Trade Your Phone',
                caseInsensitiveMatch: true,
                resolve: {
                    phoneMakesViewModel: function (AdminPhoneMakesService) {
                        return AdminPhoneMakesService.GetPhoneMakes();
                    }
                }
            }).
            when('/Admin/CreatePhoneMake', {
                templateUrl: '../AdminApp/PhoneMakes/PhoneMake.html',
                controller: 'AdminPhoneMakeCtrl',
                title: 'Create Phone Make - Trade Your Phone',
                caseInsensitiveMatch: true,
                resolve: {
                    phoneMake: function () {
                        return null;
                    }
                }
            }).
            when('/Admin/PhoneMake/:id', {
                templateUrl: '../AdminApp/PhoneMakes/PhoneMake.html',
                controller: 'AdminPhoneMakeCtrl',
                title: 'Phone Make - Trade Your Phone',
                caseInsensitiveMatch: true,
                resolve: {
                    phoneMake: function (AdminPhoneMakesService, $route) {
                        return AdminPhoneMakesService.GetPhoneMake($route.current.params.id);
                    }
                }
            }).
            when('/Admin/PhoneModels', {
                templateUrl: '../AdminApp/PhoneModels/PhoneModels.html',
                controller: 'AdminPhoneModelsCtrl',
                title: 'Phone Models - Trade Your Phone',
                caseInsensitiveMatch: true,
                resolve: {
                    phoneModelsViewModel: function (AdminPhoneModelsService) {
                        return AdminPhoneModelsService.GetPhoneModelsForView();
                    }
                }
            }).
            when('/Admin/CreatePhoneModel', {
                templateUrl: '../AdminApp/PhoneModels/PhoneModel.html',
                controller: 'AdminPhoneModelCtrl',
                title: 'Phone Models - Trade Your Phone',
                caseInsensitiveMatch: true,
                resolve: {
                    phoneModelViewModel: function (AdminPhoneModelsService) {
                        return AdminPhoneModelsService.GetCreatePhoneModelViewModel();
                    }
                }
            }).
            when('/Admin/PhoneModel/:id', {
                templateUrl: '../AdminApp/PhoneModels/PhoneModel.html',
                controller: 'AdminPhoneModelCtrl',
                title: 'Phone Model - Trade Your Phone',
                caseInsensitiveMatch: true,
                resolve: {
                    phoneModelViewModel: function (AdminPhoneModelsService, $route) {
                        return AdminPhoneModelsService.GetPhoneModel($route.current.params.id);
                    }
                }
            }).
            when('/Admin/Dashboard', {
                templateUrl: '../AdminApp/Dashboard/Dashboard.html',
                controller: 'AdminDashboardCtrl',
                title: 'Dashboard - Trade Your Phone',
                caseInsensitiveMatch: true,
                resolve: {
                    dashboardViewModel: function (AdminDashboardService) {
                        return AdminDashboardService.GetDashboardData();
                    }
                }
            }).
            when('/Admin/Configuration', {
                templateUrl: '../AdminApp/Configuration/Configuration.html',
                controller: 'AdminConfigurationCtrl',
                title: 'Configuration Data - Trade Your Phone',
                caseInsensitiveMatch: true,
                resolve: {
                    ConfigurationData: function (AdminConfigurationService) {
                        return AdminConfigurationService.GetConfigurationData();
                    }
                }
            }).
            when('/Admin/CacheManager', {
                templateUrl: '../AdminApp/CacheManager/CacheManager.html',
                controller: 'CacheManagerCtrl',
                title: 'Cache Manager - Trade Your Phone',
                caseInsensitiveMatch: true
            }).
          otherwise({
              redirectTo: '/Admin'
          });

        $locationProvider.html5Mode(true);
    }
]);

tradeYourPhoneAdminApp.run(['$rootScope', '$routeParams', function ($rootScope, $routeParams) {
    $rootScope.$on('$routeChangeSuccess', function (event, current, previous) {
        if (current.hasOwnProperty('$$route')) {
            $rootScope.title = current.$$route.title;
            $rootScope.description = current.$$route.description;
        }
        else {
            $rootScope.title = 'Trade Your Phone - Sell your old phone for cash';
            $rootScope.description = 'Sell your mobile phone for cash today. Guaranteed best price and free shipping.';
        }
    });
}]);

tradeYourPhoneAdminApp.run(['authService', function (authService) {
    authService.fillAuthData();
}]);

tradeYourPhoneAdminApp.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});


