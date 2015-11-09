tradeYourPhoneControllers.controller('BlogPostCtrl', function ($scope, $routeParams, BlogService, blogPost, latestPosts, phoneModels, PhoneModelService, $location, $route) {

    $scope.blogPost = [];
    $scope.blogPost = blogPost;
    $scope.latestPosts = latestPosts;
    $scope.phoneModels = phoneModels;


    $scope.goToQuote = function (phone) {
        $location.path('/Home');
        PhoneModelService.StoreCurrentPhoneModel(phone);
        $scope.index = {};
        $route.reload();
        $location.hash('main');
    }

});