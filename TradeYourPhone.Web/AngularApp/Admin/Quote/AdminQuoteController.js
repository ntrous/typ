tradeYourPhoneControllers.controller('AdminQuoteCtrl', function ($scope, $location, AdminQuoteService, quoteDetailsViewModel) {

    $scope.quoteDetailsViewModel = quoteDetailsViewModel;

    $scope.SaveQuoteDetails = function (quote) {
        $scope.spinner = true;
        AdminQuoteService.SaveQuoteDetails(quote).then(function (response) {
            $scope.quoteDetailsViewModel = response;
            $scope.result = 'Saved!';
            $scope.spinner = false;
        })
    }

});