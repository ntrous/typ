﻿tradeYourPhoneServices.service('PhoneModelService', function ($http) {
    var currentPhoneModel = null;

    this.StoreCurrentPhoneModel = function(value) {
        currentPhoneModel = value;
    }

    this.GetCurrentPhoneModel = function() {
        return currentPhoneModel;
    }

    this.GetPhoneModels = function () {
        return $http.get('/PhoneModels/GetPhoneModels', { cache: true })
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

    this.GetPhoneModelsByMakeName = function (makeName) {
        return $http.get('/PhoneModels/GetPhoneModelsByMakeName?makeName=' + makeName, { cache: true })
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

    this.GetMostPopularPhoneModels = function (limit) {
        return $http.get('/PhoneModels/GetMostPopularPhoneModels?limit=' + limit, { cache: true })
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