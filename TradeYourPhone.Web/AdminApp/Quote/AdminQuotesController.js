tradeYourPhoneAdminControllers.controller('AdminQuotesCtrl', function ($scope, $location, AdminQuoteService, quoteViewModel) {

    $location.path("/Admin/Quotes");

    AdminQuoteService.StoreCurrentQuoteViewModel(quoteViewModel);
    $scope.quoteViewModel = AdminQuoteService.GetCurrentQuoteViewModel();

    $scope.updatePage = function (quoteViewModel) {
        $scope.spinner = true;
        AdminQuoteService.GetQuotes(quoteViewModel).then(function (response) {
            AdminQuoteService.StoreCurrentQuoteViewModel(response);
            $scope.quoteViewModel = AdminQuoteService.GetCurrentQuoteViewModel();
            $scope.spinner = false;
        });
    };

    $scope.showQuote = function (id) {
        $location.path('/Admin/Quote/' + id);
    };
});