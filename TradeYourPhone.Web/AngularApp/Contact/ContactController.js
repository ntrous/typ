tradeYourPhoneControllers.controller('ContactCtrl', function ($scope, ContactUsService) {
    $scope.subjects = [
        { subject: "I have a Question" },
        { subject: "I have a Complaint" },
        { subject: "I would like more information" },
        { subject: "Other" }];

    $scope.subject = $scope.subjects[0].subject;

    $scope.SendEmail = function (name, from, subject, message) {
        $scope.sendMailSpinner = true;
        ContactUsService.SendEmail(name, from, subject, message).then(function (response) {
            $scope.mailSent = true;
            $scope.sendMailSpinner = false;
            $scope.name = '';
            $scope.from = '';
            $scope.subject = $scope.subjects[0].subject;
            $scope.message = '';
            $scope.contact.$setPristine();
            $scope.contact.$setUntouched();
        });
    }
    
  })