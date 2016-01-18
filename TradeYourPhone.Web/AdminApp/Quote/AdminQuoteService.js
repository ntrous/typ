tradeYourPhoneAdminServices.service('AdminQuoteService', function ($log, $http, $q, $cookies) {

    var currentQuoteViewModel = null;

    this.StoreCurrentQuoteViewModel = function (value) {
        currentQuoteViewModel = value;
    }

    this.GetCurrentQuoteViewModel = function () {
        return currentQuoteViewModel;
    }

    this.GetQuotes = function (quoteIndexViewModel) {
        return $http({ method: "POST", url: '/service/Quotes/Index', data: quoteIndexViewModel, cache: false })
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

    this.GetQuote = function (id) {
        return $http({ method: "GET", url: '/service/Quotes/GetQuote?id=' + id, cache: false })
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

    this.SaveQuoteDetails = function (quote) {
        return $http({ method: "POST", url: '/service/Quotes/SaveQuoteDetails', data: quote, cache: false })
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