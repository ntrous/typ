tradeYourPhoneAdminServices.service('AdminPhoneMakesService', function ($log, $http, $q, $cookies) {
    this.GetPhoneMakes = function () {
        return $http({ method: "GET", url: '/service/PhoneMakes/GetPhoneMakes', cache: false })
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

    this.GetPhoneMake = function (id) {
        return $http({ method: "GET", url: '/service/PhoneMakes/GetPhoneMake/' + id, cache: false })
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

    this.SavePhoneMake = function (phoneMakeViewModel) {
        return $http({ method: "POST", url: '/service/PhoneMakes/SavePhoneMake', data: phoneMakeViewModel, cache: false })
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