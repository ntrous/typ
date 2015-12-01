var ContactPage = function () {

    this.heading = element(by.css('h1'));

    this.contactForm = element(by.css('form[name="contact"]'));

    this.nameLabel = element(by.css('label[for="contactName"]'));
    this.nameField = element(by.id('contactName'));

    this.emailLabel = element(by.css('label[for="contactEmail"]'));
    this.emailField = element(by.id('contactEmail'));

    this.subjectLabel = element(by.css('label[for="contactSubject"]'));
    this.subjectSelect = element(by.id('contactSubject'));
    this.subjectDropDown = element.all(by.css('#contactSubject>option'));

    this.messageLabel = element(by.css('label[for="contactMessage"]'));
    this.messageField = element(by.id('contactMessage'));

    this.sendBtn = element(by.css('button[ng-click="SendEmail(name, from, subject, message)"]'));

    this.mailSentNotification = element(by.css('div[ng-show="mailSent"]'));

    this.contactDetailsSection = element(by.css('.contact-details'));
    this.facebook = element(by.css('.contact-details>div:nth-of-type(1) a'));
    this.email = element(by.css('.contact-details>div:nth-of-type(2) a'));
    this.phone = element(by.css('.contact-details>div:nth-of-type(3) a'));
    this.address = element(by.css('.contact-details>div:nth-of-type(4)>div:nth-of-type(2)'));

    // Errors
    this.errorBlock = element.all(by.css('p.help-block'));


    // FUNCTION NAME: selectSubject
    // PARAMETERS: subject: string - subject to select
    // DESCRIPTION: clicks the Subject dropdown and selects the subject requested
    this.selectSubject = function (subject) {
        return this.subjectSelect.click().then(function () {
            return element(by.css('#contactSubject > option[label="' + subject + '"]')).click();
        })
    }

    // FUNCTION NAME: enterContactForm
    // PARAMETERS:  name: string - name to enter
    //              email: string - email to enter
    //              subject: string - subject to select
    //              message: string - message to enter
    //              errors: boolean - if there are expected errors on submit
    // DESCRIPTION: enters the contact from with the mapped paramters and depending on whether there are errors expected returns when mail is sent or not
    this.enterContactForm = function (name, email, subject, message, errors) {
        var submit = element(by.css('button[ng-click="SendEmail(name, from, subject, message)"]'));
        this.nameField.sendKeys(name);
        this.emailField.sendKeys(email);
        this.messageField.sendKeys(message);
        this.selectSubject(subject);
         if(errors) {
            return;
        } else {
            return element(by.css('button[ng-click="SendEmail(name, from, subject, message)"]')).click()
           .then(function () {
               browser.wait(function () {
                   return element(by.css('div[ng-show="mailSent"]>p')).isDisplayed();
               }, 6000);
           });
        }
       
    }

};
module.exports = ContactPage;
