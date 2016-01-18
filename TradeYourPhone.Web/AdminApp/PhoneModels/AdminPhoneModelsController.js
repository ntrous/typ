tradeYourPhoneAdminControllers.controller('AdminPhoneModelsCtrl', function ($scope, $location, AdminPhoneModelsService, phoneModelsViewModel) {

    $scope.phoneModelsViewModel = phoneModelsViewModel;

    $scope.showPhoneModel = function (id) {
        $location.path('/Admin/PhoneModel/' + id);
    }
});