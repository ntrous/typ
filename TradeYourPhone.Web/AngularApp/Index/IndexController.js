tradeYourPhoneControllers.controller('IndexCtrl', function ($route, $scope, PhoneModelService, $location, $cookies, $q) {
    $scope.GetPhoneModels = function () {
        PhoneModelService.GetPhoneModels().then(function (response) {
            $scope.phoneModels = response;
        });
    }

    $scope.GoToQuote = function () {
        $location.path('/Home');
        PhoneModelService.StoreCurrentPhoneModel($scope.index.model);
        $scope.index = {};
        $route.reload();
    }

    $scope.GetPhoneModels();
});