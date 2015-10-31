tradeYourPhoneControllers.controller('BlogPostCtrl', function ($scope, $routeParams, BlogService, blogPost) {

    $scope.blogPost = [];
    $scope.blogPost = blogPost;

});