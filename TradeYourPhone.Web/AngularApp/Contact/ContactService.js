tradeYourPhoneServices.service('ContactUsService', function ($http) {
    this.SendEmail = function (name, from, subject, message) {
        return $http.post('/service/Email/SendEmail', { name: name, from: from, subject: subject, message: message })
       .then(
               function (response) {
                   return response.data;
               },
               function (httpError) {
                   // translate the error
                   throw httpError.status + " : " +
                       httpError.data;
               });
    }
});
