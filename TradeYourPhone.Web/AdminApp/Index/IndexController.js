tradeYourPhoneAdminControllers.controller('IndexCtrl', function ($route, $scope, authService, $location) {

    $scope.logOut = function () {
        authService.logOut();
        $location.path('/Admin/Login');
    }

    $scope.authentication = authService.authentication;
});