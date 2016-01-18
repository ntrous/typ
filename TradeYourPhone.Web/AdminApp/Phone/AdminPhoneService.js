tradeYourPhoneAdminServices.service('AdminPhoneService', function ($log, $http, $q, $cookies) {

    var currentPhoneIndexViewModel = null;

    this.StoreCurrentPhoneIndexViewModel = function (value) {
        currentPhoneIndexViewModel = value;
    }

    this.GetCurrentPhoneIndexViewModel = function () {
        return currentPhoneIndexViewModel;
    }

    this.GetPhones = function (phoneIndexViewModel) {
        return $http({ method: "POST", url: '/service/Phones/GetPhones', data: phoneIndexViewModel, cache: false })
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

    this.GetPhone = function (id) {
        return $http({ method: "GET", url: '/service/Phones/GetPhone?id=' + id, cache: false })
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

    this.SavePhoneDetails = function (phoneIndexViewModel) {
        return $http({ method: "POST", url: '/service/Phones/SavePhoneDetails', data: phoneIndexViewModel, cache: false })
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

    this.GetPhoneReferenceData = function (id) {
        return $http({ method: "GET", url: '/service/Phones/GetPhoneReferenceData', cache: false })
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

    this.CreatePhone = function (phone) {
        return $http({ method: "POST", url: '/service/Phones/CreatePhone', data: phone, cache: false })
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