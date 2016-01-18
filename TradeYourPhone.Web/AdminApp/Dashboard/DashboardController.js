tradeYourPhoneAdminControllers.controller('AdminDashboardCtrl', function ($scope, $location, AdminDashboardService, dashboardViewModel) {

    dashboardViewModel.DateFrom = new Date(dashboardViewModel.DateFrom);
    dashboardViewModel.DateTo = new Date(dashboardViewModel.DateTo);
    $scope.dashboardViewModel = dashboardViewModel;

    $scope.UpdateDashboard = function (dashboardViewModel) {
        $scope.spinner = true;
        AdminDashboardService.GetDashboardData(dashboardViewModel).then(function (response) {
            response.DateFrom = new Date(dashboardViewModel.DateFrom);
            response.DateTo = new Date(dashboardViewModel.DateTo);
            $scope.dashboardViewModel = response;
            $scope.spinner = false;
        });
    };

});