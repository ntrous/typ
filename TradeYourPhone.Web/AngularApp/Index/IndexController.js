tradeYourPhoneControllers.controller('IndexCtrl', function ($route, $scope, PhoneModelService, authService, $location, $cookies, $q) {
    $scope.variation = 0;
    if (typeof cxApi != "undefined") {
        $scope.variation = cxApi.getChosenVariation();
    }
    
    if ($scope.variation <= 0) {
        $scope.cssFile = "SiteA";
    } else {
        $scope.cssFile = "SiteB";
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
});