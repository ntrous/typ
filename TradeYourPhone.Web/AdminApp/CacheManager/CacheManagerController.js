tradeYourPhoneAdminControllers.controller('CacheManagerCtrl', function ($scope, $location, CacheManagerService) {

    $scope.cacheManagerViewModel = {};

    $scope.ClearCache = function (cacheManagerViewModel) {
        $scope.spinner = true;
        CacheManagerService.ClearCache(cacheManagerViewModel).then(function (response) {
            if (response === true) {
                $scope.result = 'Cache Cleared!'
            }
            else {
                $scope.result = 'Failed!';
            }

            $scope.spinner = false;
        });
    };

});