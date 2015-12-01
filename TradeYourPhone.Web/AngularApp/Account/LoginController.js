tradeYourPhoneControllers.controller('loginController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {

    $scope.loginData = {
        userName: "",
        password: ""
    };

    $scope.message = "";

    $scope.login = function () {
        $scope.spinner = true;
        authService.login($scope.loginData).then(function (response) {
            $scope.spinner = false;
            $location.path('/');
        },
         function (err) {
             $scope.spinner = false;
             $scope.message = err.error_description;
         });
    };

}]);