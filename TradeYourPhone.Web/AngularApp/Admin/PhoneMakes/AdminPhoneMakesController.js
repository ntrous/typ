tradeYourPhoneControllers.controller('AdminPhoneMakesCtrl', function ($scope, $location, AdminPhoneMakesService, phoneMakesViewModel) {

    $scope.phoneMakesViewModel = phoneMakesViewModel;

    $scope.showPhoneMake = function (id) {
        $location.path('/Admin/PhoneMake/' + id);
    }
});