tradeYourPhoneControllers.controller('AdminQuotesCtrl', function ($scope, $location, AdminQuoteService, quoteViewModel) {

    $scope.quoteViewModel = quoteViewModel;

    $scope.updatePage = function (quoteViewModel) {
        $scope.spinner = true;
        AdminQuoteService.GetQuotes(quoteViewModel).then(function (response) {
            $scope.quoteViewModel = response;
            $scope.spinner = false;
        });
    };

    $scope.showQuote = function (id) {
        $location.path('/Admin/Quote/' + id);
    };
});