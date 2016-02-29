tradeYourPhoneControllers.controller('IndexCtrl', function ($route, $scope, PhoneModelService, $location, $cookies, $q) {
    var variation = 0;
    if (typeof cxApi != "undefined") {
        variation = cxApi.getChosenVariation();
    }

    $scope.cssFile = "Site" + variation;

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