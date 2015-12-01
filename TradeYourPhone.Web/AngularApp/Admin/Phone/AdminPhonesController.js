tradeYourPhoneControllers.controller('AdminPhonesCtrl', function ($scope, $location, AdminPhoneService, phoneIndexViewModel) {

    $scope.phoneIndexViewModel = phoneIndexViewModel;

    $scope.updatePage = function (phoneIndexViewModel) {
        $scope.spinner = true;
        phoneIndexViewModel.PhoneModels = null;
        phoneIndexViewModel.PhoneMakes = null;
        AdminPhoneService.GetPhones(phoneIndexViewModel).then(function (response) {
            $scope.phoneIndexViewModel = response;
            $scope.spinner = false;
        });
    };

    $scope.showPhone = function (id) {
        $location.path('/Phone/' + id);
    };
});