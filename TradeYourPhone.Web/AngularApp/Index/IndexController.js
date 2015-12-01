tradeYourPhoneControllers.controller('IndexCtrl', function ($route, $scope, PhoneModelService, authService, $location, $cookies, $q) {
    $scope.GetPhoneModels = function () {
        PhoneModelService.GetPhoneModels().then(function (response) {
            $scope.phoneModels = response;
        });
    }

    $scope.GoToQuote = function () {
        $location.path('/');
        PhoneModelService.StoreCurrentPhoneModel($scope.index.model);
        $scope.index = {};
        $route.reload();
    }

    $scope.logOut = function () {
        authService.logOut();
        $location.path('/');
    }

    $scope.toggleDropdown = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.status.isopen = !$scope.status.isopen;
    };

    $scope.authentication = authService.authentication;

    $scope.GetPhoneModels();
});