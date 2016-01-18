tradeYourPhoneAdminControllers.controller('loginController', ['$scope', '$location', 'authService', 'localStorageService', function ($scope, $location, authService, localStorageService) {

    $scope.loginData = {
        userName: "",
        password: ""
    };

    $scope.message = "";

    $scope.login = function () {
        $scope.spinner = true;
        authService.login($scope.loginData).then(function (response) {
            $scope.spinner = false;
            $location.path('/Admin');
        },
         function (err) {
             $scope.spinner = false;
             $scope.message = err.error_description;
         });
    };

}]);