tradeYourPhoneControllers.controller('BlogCtrl', function ($scope, $routeParams, $location, BlogService, blogPosts) {

    $scope.blogPosts = blogPosts;

    $scope.showBlog = function (slug) {
        $location.path('/blog/' + slug);
    };
});