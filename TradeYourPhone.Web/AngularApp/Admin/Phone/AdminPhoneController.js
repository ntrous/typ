tradeYourPhoneControllers.controller('AdminPhoneCtrl', function ($scope, $location, AdminPhoneService, phoneDetailsViewModel, _) {

    $scope.SavePhoneDetails = function (phone) {
        $scope.spinner = true;
        AdminPhoneService.SavePhoneDetails(phone).then(function (response) {
            $scope.phoneDetailsViewModel.Phone = response;
            $scope.result = 'Saved!';
            $scope.spinner = false;
        })
    }

    $scope.CreatePhone = function (form, phone) {
        $scope.createPhoneCalled = true;
        $scope.result = null;

        if (form.$valid) {
            $scope.spinner = true;
            AdminPhoneService.CreatePhone(phone).then(function(response) {
                $scope.spinner = false;
                $scope.result = 'Saved!';
            });
        }
    }

    $scope.GetPhoneModelsByMakeId = function(id) {
        $scope.phoneModels = _.where($scope.phoneDetailsViewModel.PhoneModels, { 'PhoneMakeId': id });
    }

    $scope.phoneDetailsViewModel = phoneDetailsViewModel;
    $scope.phoneModels = [];
    $scope.result = null;

    if (phoneDetailsViewModel.Phone) {
        $scope.GetPhoneModelsByMakeId(phoneDetailsViewModel.Phone.PhoneMakeId);
    }

});