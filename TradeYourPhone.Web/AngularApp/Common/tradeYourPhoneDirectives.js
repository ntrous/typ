angular.module('typ.directives', [])

    .directive('compareTo', function () {
        return {
            require: "ngModel",
            scope: {
                otherModelValue: "=compareTo"
            },
            link: function (scope, element, attributes, ngModel) {

                ngModel.$validators.compareTo = function (modelValue) {
                    return modelValue == scope.otherModelValue;
                };

                scope.$watch("otherModelValue", function () {
                    ngModel.$validate();
                });
            }
        };
    })
    .directive('routeLoadingIndicator', function ($rootScope, $route) {
        return {
            restrict: 'E',
            template: "<i ng-if='isRouteLoading' class='loading fa fa-cog fa-spin'></i>",
            link: function (scope, elem, attrs) {
                scope.isRouteLoading = false;

                $rootScope.$on('$routeChangeStart', function () {
                    scope.isRouteLoading = true;
                });

                $rootScope.$on('$routeChangeSuccess', function () {
                    scope.isRouteLoading = false;
                });
            }
        };
    });

