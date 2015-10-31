// html filter (render text as html)
tradeYourPhoneCommon.filter('html', ['$sce', function ($sce) {
    return function (text) {
        return $sce.trustAsHtml(text);
    };    
}])