tradeYourPhoneControllers.controller('SellYourIphoneCtrl',
  function ($scope, $http, PhoneModelService, $q, $location, $route, phoneModels) {

      $scope.goToQuote = function (phone) {
          $location.path('/Home');
          PhoneModelService.StoreCurrentPhoneModel(phone);
          $scope.index = {};
          $route.reload();
          $location.hash('main');
      }

      $scope.phoneModels = phoneModels;
  })