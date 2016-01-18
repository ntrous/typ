tradeYourPhoneAdminControllers.controller('AdminPhoneCtrl', function ($scope, $location, AdminPhoneService, phoneDetailsViewModel, _) {

    $scope.SavePhoneDetails = function (phone) {
        $scope.spinner = true;
        phone.PhoneChecklist = JSON.stringify($scope.phoneChecklist);
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

    $scope.MapPhoneChecklist = function () {
        if ($scope.phoneDetailsViewModel.Phone && $scope.phoneDetailsViewModel.Phone.PhoneChecklist) {
            $scope.phoneChecklist = JSON.parse($scope.phoneDetailsViewModel.Phone.PhoneChecklist);
        }
    }

   
    $scope.phoneDetailsViewModel = phoneDetailsViewModel;
    $scope.phoneModels = [];
    console.log($scope.phoneDetailsViewModel.Phone);
    $scope.phoneChecklist = {};
    $scope.MapPhoneChecklist();
    $scope.result = null;

    if (phoneDetailsViewModel.Phone) {
        $scope.GetPhoneModelsByMakeId(phoneDetailsViewModel.Phone.PhoneMakeId);
    }

   


   

});