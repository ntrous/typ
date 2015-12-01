tradeYourPhoneControllers.controller('AdminPhoneModelsCtrl', function ($scope, $location, AdminPhoneModelsService, phoneModelsViewModel) {

    $scope.phoneModelsViewModel = phoneModelsViewModel;

    $scope.showPhoneModel = function (id) {
        $location.path('/PhoneModel/' + id);
    }
});