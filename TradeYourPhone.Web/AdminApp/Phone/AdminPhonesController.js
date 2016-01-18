tradeYourPhoneAdminControllers.controller('AdminPhonesCtrl', function ($scope, $location, AdminPhoneService, phoneIndexViewModel) {

    $location.path("/Admin/Phones");

    AdminPhoneService.StoreCurrentPhoneIndexViewModel(phoneIndexViewModel);
    $scope.phoneIndexViewModel = AdminPhoneService.GetCurrentPhoneIndexViewModel();

    $scope.updatePage = function (phoneIndexViewModel) {
        $scope.spinner = true;
        phoneIndexViewModel.PhoneModels = null;
        phoneIndexViewModel.PhoneMakes = null;
        AdminPhoneService.GetPhones(phoneIndexViewModel).then(function (response) {
            AdminPhoneService.StoreCurrentPhoneIndexViewModel(response);
            $scope.phoneIndexViewModel = AdminPhoneService.GetCurrentPhoneIndexViewModel();
            $scope.spinner = false;
        });
    };

    $scope.showPhone = function (id) {
        $location.path('/Admin/Phone/' + id);
    };
});