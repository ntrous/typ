tradeYourPhoneServices.service('AdminConfigurationService', function ($log, $http, $q, $cookies) {
    this.GetConfigurationData = function () {
        return $http({ method: "GET", url: '/service/Configuration/GetConfigurationData' })
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

    this.SaveConfigurationData = function (configurationData) {
        return $http({ method: "POST", url: '/service/Configuration/SaveConfigurationData', data: configurationData })
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