tradeYourPhoneControllers.controller('AdminConfigurationCtrl', function ($scope, $location, AdminConfigurationService, ConfigurationData) {

    $scope.configurationData = ConfigurationData;

    $scope.SaveConfigData = function (configurationData) {
        $scope.result = '';
        $scope.spinner = true;
        AdminConfigurationService.SaveConfigurationData(configurationData).then(function(response) {
            $scope.result = 'Success!';
            $scope.spinner = false;
        });
    }
});