tradeYourPhoneControllers.controller('AdminPhoneModelCtrl', function ($scope, $location, AdminPhoneModelsService, phoneModelViewModel) {

    $scope.phoneModelViewModel = phoneModelViewModel;
    $scope.image = {};

    $scope.SavePhoneModel = function (phoneModelViewModel, form) {
        $scope.savePhoneModelCalled = true;
        if (form.$valid) {
            $scope.spinner = true;
            AdminPhoneModelsService.SavePhoneModel(phoneModelViewModel).then(function (response) {
                $scope.result = 'Saved!';
                $scope.spinner = false;
            });
        }
    }

    $scope.ImageUploaded = function (event, reader, fileList, fileObjs, file, object) {
        phoneModelViewModel.Model.PrimaryImageString = object.base64;
    }
});