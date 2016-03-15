tradeYourPhoneControllers.controller('IndexCtrl', function ($route, $scope, PhoneModelService, $location, $cookies, $q) {
    $scope.variation = 0;

    $scope.cssFile = "Site" + $scope.variation;

    $scope.GoToQuote = function () {
        $location.path('/');
        PhoneModelService.StoreCurrentPhoneModel($scope.index.model);
        $scope.index = {};
        $route.reload();
    }

    $scope.toggleDropdown = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.status.isopen = !$scope.status.isopen;
    };
});