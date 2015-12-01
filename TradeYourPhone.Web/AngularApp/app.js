var tradeYourPhoneApp = angular.module('typ', [
  'ui.bootstrap',
  'ngRoute',
  'ngCookies',
  'typ.controllers',
  'typ.service',
  'typ.common',
  'typ.directives',
  'angulartics',
  'angulartics.google.analytics',
  'LocalStorageModule',
  'naif.base64'
]).constant('_', window._);

var tradeYourPhoneControllers = angular.module('typ.controllers', ['typ.service']);
var tradeYourPhoneServices = angular.module('typ.service', []);
var tradeYourPhoneCommon = angular.module('typ.common', []);

tradeYourPhoneApp.config(['$routeProvider', '$locationProvider',
    function ($routeProvider, $locationProvider) {
        $routeProvider
        .when('/', {
            redirectTo: '/',
            templateUrl: '../AngularApp/Quote/Home.html',
            controller: 'QuoteCtrl',
            reloadOnSearch: false,
            caseInsensitiveMatch: true,
            title: 'Trade Your Phone - Sell Your Old Phone For Cash',
            description: 'Sell your mobile phone for cash today. Get the guaranteed best price and free shipping. Fill out a quote now!',
            resolve: {
                phoneModels: function (PhoneModelService, $route) {
                    return PhoneModelService.GetPhoneModels();
                },
                phoneConditions: function (QuoteService, $route) {
                    return QuoteService.GetPhoneConditions();
                },
                quote: function (QuoteService, $route, $cookies) {
                    var key = $cookies.tradeYourPhoneCookie;
                    if (key) {
                        return QuoteService.GetQuoteDetails($cookies.tradeYourPhoneCookie);
                    }
                },
            }
        }).
          when('/WhatWeDo', {
              templateUrl: '../AngularApp/WhatWeDo/WhatWeDo.html',
              controller: 'WhatWeDoCtrl',
              caseInsensitiveMatch: true,
              title: 'What We Do - Trade Your Phone',
              description: 'We are passionate about re-allocating and recycling second-hand mobile phones. And we want to help YOU turn your old phone into cash.'
          }).
          when('/FAQ', {
              templateUrl: '../AngularApp/Support/FAQ.html',
              controller: 'SupportCtrl',
              caseInsensitiveMatch: true,
              title: 'FAQ - Trade Your Phone',
              description: 'All the questions we frequently get asked, answered here!'
          }).
            when('/Support', {
                templateUrl: '../AngularApp/Support/Support.html',
                controller: 'SupportCtrl',
                caseInsensitiveMatch: true,
                title: 'Support - Trade Your Phone',
                description: 'Trade Your Phone offers 9am - 7pm support on Monday - Friday, however it can be easier to look through the questions below instead of contacting us.'
            }).
          when('/Contact', {
              templateUrl: '../AngularApp/Contact/Contact.html',
              controller: 'ContactCtrl',
              caseInsensitiveMatch: true,
              title: 'Contact - Trade Your Phone',
              description: 'Contact us here via one of our many support channels'
          }).
          when('/Privacy', {
              templateUrl: '../AngularApp/Privacy/Privacy.html',
              caseInsensitiveMatch: true,
              title: 'Privacy - Trade Your Phone',
              description: 'Trade Your Phones Privacy Statement'
          }).
          when('/TermsAndConditions', {
              templateUrl: '../AngularApp/TermsAndConditions/TermsAndConditions.html',
              caseInsensitiveMatch: true,
              title: 'Terms And Conditions - Trade Your Phone',
              description: 'Trade Your Phones Terms And Conditions'
          }).
            when('/CustomerReviews', {
                templateUrl: '../AngularApp/Reviews/CustomerReviews.html',
                caseInsensitiveMatch: true,
                title: 'Customer Reviews - Trade Your Phone',
                description: 'See our customer reviews and how to give us a review'
            }).
            when('/FreeShipping', {
                templateUrl: '../AngularApp/ShippingOptions/ShippingOptions.html',
                caseInsensitiveMatch: true,
                title: 'Free Shipping on all our options - Trade Your Phone',
                description: 'We offer free shipping on all our postage options. No charge to you!'
            }).
            when('/PriceGuarantee', {
                templateUrl: '../AngularApp/PriceGuarantee/PriceGuarantee.html',
                caseInsensitiveMatch: true,
                title: 'Price Guarantee | We will beat any competitors price! - Trade Your Phone',
                description: 'We will beat any australian competitors price by 5% guaranteed!'
            }).
            when('/Sell-Your-Iphone', {
                templateUrl: '../AngularApp/SellYourIphone/SellYourIphone.html',
                controller: 'SellYourIphoneCtrl',
                caseInsensitiveMatch: true,
                title: 'Sell Your iPhone | We buy all iPhones! - Trade Your Phone',
                description: 'We buy all Apple iPhones for the best price guaranteed! Free shipping and no hidden costs. Sell your phone now!',
                resolve: {
                    phoneModels: function (PhoneModelService, $route) {
                        return PhoneModelService.GetPhoneModelsByMakeName("Apple");
                    }
                }
            }).
          when('/Blog', {
              templateUrl: '../AngularApp/Blog/Blog.html',
              controller: 'BlogCtrl',
              caseInsensitiveMatch: true,
              title: 'Blog - Trade Your Phone',
              description: 'Trade Your Phones Blog',
              resolve: {
                  blogPosts: function (BlogService, $route) {
                      return BlogService.GetAllBlogPosts();
                  }
              }
          }).
          when('/Blog/:slug', {
              templateUrl: '../AngularApp/Blog/BlogPost.html',
              controller: 'BlogPostCtrl',
              caseInsensitiveMatch: true,
              title: 'Blog - Trade Your Phone',
              description: 'Trade Your Phones Blog',
              resolve: {
                  blogPost: function (BlogService, $route) {
                      return BlogService.GetBlogPostBySlug($route.current.params.slug);
                  },
                  latestPosts: function (BlogService, $route) {
                      return BlogService.GetNLatestBlogPosts(5);
                  },
                  phoneModels: function (PhoneModelService, $route) {
                      return PhoneModelService.GetMostPopularPhoneModels(5);
                  }
              }
          }).
            when('/Login', {
                templateUrl: '../AngularApp/Account/Login.html',
                controller: 'loginController',
                caseInsensitiveMatch: true
            }).
            when('/Quotes', {
                templateUrl: '../AngularApp/Admin/Quote/Quotes.html',
                controller: 'AdminQuotesCtrl',
                title: 'Quotes - Trade Your Phone',
                caseInsensitiveMatch: true,
                resolve: {
                    quoteViewModel: function (AdminQuoteService, $route) {
                        return AdminQuoteService.GetQuotes();
                    }
                }
            }).
            when('/Quote/:id', {
                templateUrl: '../AngularApp/Admin/Quote/Quote.html',
                controller: 'AdminQuoteCtrl',
                title: 'Quote - Trade Your Phone',
                caseInsensitiveMatch: true,
                resolve: {
                    quoteDetailsViewModel: function (AdminQuoteService, $route) {
                        return AdminQuoteService.GetQuote($route.current.params.id);
                    }
                }
            }).
            when('/Phones', {
                templateUrl: '../AngularApp/Admin/Phone/Phones.html',
                controller: 'AdminPhonesCtrl',
                title: 'Phones - Trade Your Phone',
                caseInsensitiveMatch: true,
                resolve: {
                    phoneIndexViewModel: function (AdminPhoneService, $route) {
                        return AdminPhoneService.GetPhones();
                    }
                }
            }).
            when('/Phone/:id', {
                templateUrl: '../AngularApp/Admin/Phone/Phone.html',
                controller: 'AdminPhoneCtrl',
                title: 'Phone - Trade Your Phone',
                caseInsensitiveMatch: true,
                resolve: {
                    phoneDetailsViewModel: function (AdminPhoneService, $route) {
                        return AdminPhoneService.GetPhone($route.current.params.id);
                    }
                }
            }).
            when('/CreatePhone', {
                templateUrl: '../AngularApp/Admin/Phone/CreatePhone.html',
                controller: 'AdminPhoneCtrl',
                title: 'Create Phone - Trade Your Phone',
                caseInsensitiveMatch: true,
                resolve: {
                    phoneDetailsViewModel: function (AdminPhoneService, $route) {
                        return AdminPhoneService.GetPhoneReferenceData();
                    }
                }
            }).
            when('/PhoneMakes', {
                templateUrl: '../AngularApp/Admin/PhoneMakes/PhoneMakes.html',
                controller: 'AdminPhoneMakesCtrl',
                title: 'Phone Makes - Trade Your Phone',
                caseInsensitiveMatch: true,
                resolve: {
                    phoneMakesViewModel: function (AdminPhoneMakesService, $route) {
                        return AdminPhoneMakesService.GetPhoneMakes();
                    }
                }
            }).
            when('/CreatePhoneMake', {
                templateUrl: '../AngularApp/Admin/PhoneMakes/PhoneMake.html',
                controller: 'AdminPhoneMakeCtrl',
                title: 'Create Phone Make - Trade Your Phone',
                caseInsensitiveMatch: true,
                resolve: {
                    phoneMake: function () {
                        return null;
                    }
                }
            }).
            when('/PhoneMake/:id', {
                templateUrl: '../AngularApp/Admin/PhoneMakes/PhoneMake.html',
                controller: 'AdminPhoneMakeCtrl',
                title: 'Phone Make - Trade Your Phone',
                caseInsensitiveMatch: true,
                resolve: {
                    phoneMake: function (AdminPhoneMakesService, $route) {
                        return AdminPhoneMakesService.GetPhoneMake($route.current.params.id);
                    }
                }
            }).
            when('/PhoneModels', {
                templateUrl: '../AngularApp/Admin/PhoneModels/PhoneModels.html',
                controller: 'AdminPhoneModelsCtrl',
                title: 'Phone Models - Trade Your Phone',
                caseInsensitiveMatch: true,
                resolve: {
                    phoneModelsViewModel: function (AdminPhoneModelsService, $route) {
                        return AdminPhoneModelsService.GetPhoneModelsForView();
                    }
                }
            }).
            when('/CreatePhoneModel', {
                templateUrl: '../AngularApp/Admin/PhoneModels/PhoneModel.html',
                controller: 'AdminPhoneModelCtrl',
                title: 'Phone Models - Trade Your Phone',
                caseInsensitiveMatch: true,
                resolve: {
                    phoneModelViewModel: function (AdminPhoneModelsService, $route) {
                        return AdminPhoneModelsService.GetCreatePhoneModelViewModel();
                    }
                }
            }).
            when('/PhoneModel/:id', {
                templateUrl: '../AngularApp/Admin/PhoneModels/PhoneModel.html',
                controller: 'AdminPhoneModelCtrl',
                title: 'Phone Model - Trade Your Phone',
                caseInsensitiveMatch: true,
                resolve: {
                    phoneModelViewModel: function (AdminPhoneModelsService, $route) {
                        return AdminPhoneModelsService.GetPhoneModel($route.current.params.id);
                    }
                }
            }).
            when('/Dashboard', {
                templateUrl: '../AngularApp/Admin/Dashboard/Dashboard.html',
                controller: 'AdminDashboardCtrl',
                title: 'Dashboard - Trade Your Phone',
                caseInsensitiveMatch: true,
                resolve: {
                    dashboardViewModel: function (AdminDashboardService, $route) {
                        return AdminDashboardService.GetDashboardData();
                    }
                }
            }).
            when('/CacheManager', {
                templateUrl: '../AngularApp/Admin/CacheManager/CacheManager.html',
                controller: 'CacheManagerCtrl',
                title: 'Cache Manager - Trade Your Phone',
                caseInsensitiveMatch: true
            }).
          otherwise({
              redirectTo: '/'
          });

        $locationProvider.html5Mode(true);
    }
]);

tradeYourPhoneApp.run(['$rootScope', '$routeParams', 'BlogService', function ($rootScope, $routeParams, BlogService) {
    $rootScope.$on('$routeChangeSuccess', function (event, current, previous) {
        if (current.hasOwnProperty('$$route')) {
            if (current.params.slug) {
                $rootScope.title = current.locals.blogPost.title + ' - Trade Your Phone';
                $rootScope.description = current.locals.blogPost.excerpt;
            }
            else {
                $rootScope.title = current.$$route.title;
                $rootScope.description = current.$$route.description;
            }

        }
        else {
            $rootScope.title = 'Trade Your Phone - Sell your old phone for cash';
            $rootScope.description = 'Sell your mobile phone for cash today. Guaranteed best price and free shipping.';
        }
    });
}]);

tradeYourPhoneApp.run(['authService', function (authService) {
    authService.fillAuthData();
}]);

tradeYourPhoneApp.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});


