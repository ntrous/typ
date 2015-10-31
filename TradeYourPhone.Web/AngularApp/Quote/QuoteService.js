tradeYourPhoneServices.service('QuoteService', function ($log, $http, $q, $cookies) {

    this.GetPhoneConditions = function () {
        return $http.get('/PhoneConditions/GetPhoneConditions', { cache: true })
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

    this.CreateQuote = function () {
        return $http.get('/Quotes/CreateQuote')
            .then(
                function (response) {
                    $cookies.tradeYourPhoneCookie = response.data;
                    return response.data;
                },
                function (httpError) {
                    // translate the error
                    throw httpError.status + " : " +
                        httpError.data;
                });
    }

    this.AddPhoneToQuote = function (key, modelId, conditionId) {
        return $http.post('/Quotes/AddPhoneToQuoteInSession?key=' + key + '&modelId=' + modelId + '&conditionId=' + conditionId)
            .then(
                function (response) {
                    return response.data;
                },
                function (httpError) {
                    // translate the error
                    throw httpError.status + " : " +
                        httpError.data;
                });
    }

    this.GetQuoteDetails = function (key) {
        return $http.get('/Quotes/GetQuoteDetails?key=' + key)
        .then(
                function (response) {
                    return response.data.QuoteDetails;
                },
                function (httpError) {
                    // translate the error
                    throw httpError.status + " : " +
                        httpError.data;
                });
    }

    this.DeleteQuotePhone = function (key, phoneId) {
        return $http.get('/Quotes/DeleteQuotePhone?key=' + key + '&phoneId=' + phoneId)
        .then(
                function (response) {
                    return response.data;
                },
                function (httpError) {
                    // translate the error
                    throw httpError.status + " : " +
                        httpError.data;
                });
    }

    this.GetStates = function () {
        return $http.get('/Quotes/GetStateNames', { cache: true })
        .then(
                function (response) {
                    return response.data;
                },
                function (httpError) {
                    // translate the error
                    throw httpError.status + " : " +
                        httpError.data;
                });
    }

    this.GetPaymentTypes = function () {
        return $http.get('/Quotes/GetPaymentTypeNames', { cache: true })
        .then(
                function (response) {
                    return response.data;
                },
                function (httpError) {
                    // translate the error
                    throw httpError.status + " : " +
                        httpError.data;
                });
    }

    this.GetPostageMethods = function () {
        return $http.get('/Quotes/GetPostageMethods', { cache: true })
        .then(
                function (response) {
                    return response.data;
                },
                function (httpError) {
                    // translate the error
                    throw httpError.status + " : " +
                        httpError.data;
                });
    }

    this.FinaliseQuote = function (key, viewModel) {
        return $http.post('/Quotes/FinaliseQuote', { key: key, viewModel: viewModel })
       .then(
               function (response) {
                   return response.data;
               },
               function (httpError) {
                   // translate the error
                   throw httpError.status + " : " +
                       httpError.data;
               });
    }

    this.SaveQuote = function (key, viewModel) {
        return $http.post('/Quotes/SaveQuote', { key: key, viewModel: viewModel })
       .then(
               function (response) {
                   return response.data;
               },
               function (httpError) {
                   // translate the error
                   throw httpError.status + " : " +
                       httpError.data;
               });
    }
})