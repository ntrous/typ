tradeYourPhoneAdminServices.service('AdminPhoneModelsService', function ($log, $http, $q, $cookies) {
    this.GetPhoneModelsForView = function () {
        return $http({ method: "GET", url: '/service/PhoneModels/GetPhoneModelsForAdminView', cache: false })
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

    this.GetPhoneModel = function (id) {
        return $http({ method: "GET", url: '/service/PhoneModels/GetPhoneModel/' + id, cache: false })
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

    this.GetCreatePhoneModelViewModel = function () {
        return $http({ method: "GET", url: '/service/PhoneModels/GetCreatePhoneModelViewModel', cache: false })
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

    this.SavePhoneModel = function (phoneModelViewModel) {
        return $http({ method: "POST", url: '/service/PhoneModels/SavePhoneModel', data: phoneModelViewModel, cache: false })
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