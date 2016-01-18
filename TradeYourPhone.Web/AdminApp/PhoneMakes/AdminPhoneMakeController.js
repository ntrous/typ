tradeYourPhoneAdminControllers.controller('AdminPhoneMakeCtrl', function ($scope, $location, AdminPhoneMakesService, phoneMake) {

    $scope.phoneMake = phoneMake;

    $scope.SavePhoneMake = function (phoneMake, form) {
        $scope.savePhoneMakeCalled = true;
        if (form.$valid) {
            $scope.spinner = true;
            AdminPhoneMakesService.SavePhoneMake(phoneMake).then(function (response) {
                $scope.result = 'Saved!';
                $scope.spinner = false;
            });
        }
    }
});