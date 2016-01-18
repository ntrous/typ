tradeYourPhoneAdminControllers.controller('AdminQuoteCtrl', function ($scope, $location, AdminQuoteService, quoteDetailsViewModel) {

    $scope.QuoteDetails = quoteDetailsViewModel;

    $scope.SaveQuoteDetails = function (quote) {
        $scope.spinner = true;
        _.forEach($scope.QuoteDetails.Quote.Phones, function (i, key) {
            i.PhoneChecklist = JSON.stringify(i.PhoneChecklist);
        });
       
        AdminQuoteService.SaveQuoteDetails(quote).then(function(response) {
            $scope.QuoteDetails = response;
            $scope.MapPhoneChecklist();
            $scope.result = 'Saved!';
            $scope.spinner = false;
        });
    }

    $scope.MapPhoneChecklist = function () {
        if ($scope.QuoteDetails && $scope.QuoteDetails.Quote && $scope.QuoteDetails.Quote.Phones) {
            _.forEach($scope.QuoteDetails.Quote.Phones, function (i, key) {
                i.PhoneChecklist = JSON.parse(i.PhoneChecklist);
            });
        }
    }

    $scope.MapPhoneChecklist();

});