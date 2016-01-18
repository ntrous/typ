// html filter (render text as html)
tradeYourPhoneAdminCommon.filter('html', ['$sce', function ($sce) {
    return function (text) {
        return $sce.trustAsHtml(text);
    };
}]);

tradeYourPhoneAdminCommon.filter('capitalize', function () {
    return function (input) {
        return (!!input) ? input.charAt(0).toUpperCase() + input.substr(1).toLowerCase() : '';
    }
});

tradeYourPhoneAdminCommon.filter('ctime', function () {

    return function (jsonDate) {

        var date = new Date(parseInt(jsonDate.substr(6)));
        return date;
    };

});