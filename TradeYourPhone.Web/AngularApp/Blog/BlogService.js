tradeYourPhoneServices.service('BlogService', function ($log, $http) {
    
    // Get all blog posts
    this.GetAllBlogPosts = function () {
        return $http.get('https://tradeyourphone.com.au/wpblog/wp-json/posts')
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

    // Get blog post by slug
    this.GetBlogPostBySlug = function (slug) {
        return $http.get('https://tradeyourphone.com.au/wpblog/wp-json/posts?filter[name]=' + slug)
            .then(
                function (response) {
                    return response.data[0];
                },
                function (httpError) {
                    // translate the error
                    throw httpError.status + " : " +
                        httpError.data;
                }
             );
    }

    // Get Blog title
    //TODO: Pass in query to only return title to minimise json size
    this.GetBlogTitle = function (slug) {
        this.GetBlogPostBySlug(slug).then(function (response) {
            return response.title;
        });
    }

    // Get blog post by slug
    this.GetNLatestBlogPosts = function (limit) {
        return $http.get('https://tradeyourphone.com.au/wpblog/wp-json/posts?filter[orderby]=date&filter[posts_per_page]=' + limit)
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