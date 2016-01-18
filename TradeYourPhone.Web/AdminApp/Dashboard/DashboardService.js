tradeYourPhoneAdminServices.service('AdminDashboardService', function ($log, $http, $q, $cookies) {
    this.GetDashboardData = function (dashboardViewModel) {
        return $http({ method: "POST", url: '/service/Dashboard/GetDashboardData', data: dashboardViewModel, cache: false })
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