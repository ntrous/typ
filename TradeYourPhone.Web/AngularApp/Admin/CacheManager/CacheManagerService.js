tradeYourPhoneServices.service('CacheManagerService', function ($log, $http, $q, $cookies) {
    this.ClearCache = function (cacheManagerViewModel) {
        return $http({ method: "POST", url: '/service/Cache/ClearCache', data: cacheManagerViewModel, cache: false })
            .then(
                function (response) {
                    return response.data;
                },
                function (httpError) {
                    // translate the error
                    throw httpError.status + " : " +
                        httpError.data;
                }
             );
    }
});