var ContactPage = require('./ContactPage.js');
var contactPage;


describe('Contact Page', function () {

    beforeEach(function () {
        browser.driver.manage().deleteAllCookies();
        browser.get('/Contact');
        contactPage = new ContactPage();
    });

    describe('When opening the Contact Page', function () {

        it('Heading should display', function () {
            expect(contactPage.heading.isDisplayed()).toEqual(true);
        });

        it('Contact Form Fields should display', function () {
            expect(contactPage.contactForm.isDisplayed()).toEqual(true);
            expect(contactPage.nameField.isDisplayed()).toEqual(true);
            expect(contactPage.emailField.isDisplayed()).toEqual(true);
            expect(contactPage.subjectSelect.isDisplayed()).toEqual(true);
            expect(contactPage.messageField.isDisplayed()).toEqual(true);
            expect(contactPage.sendBtn.isDisplayed()).toEqual(true);
        });

        it('Contact Form labels should display', function () {
            expect(contactPage.nameLabel.isDisplayed()).toEqual(true);
            expect(contactPage.emailLabel.isDisplayed()).toEqual(true);
            expect(contactPage.subjectLabel.isDisplayed()).toEqual(true);
            expect(contactPage.messageLabel.isDisplayed()).toEqual(true);
        });

        it('Contact Form should have correct labels', function () {
            expect(contactPage.nameLabel.getText()).toEqual('Name');
            expect(contactPage.emailLabel.getText()).toEqual('Email');
            expect(contactPage.subjectLabel.getText()).toEqual('Subject');
            expect(contactPage.messageLabel.getText()).toEqual('Message');
            expect(contactPage.sendBtn.getText()).toContain('Send Email');
        });

        it('Contact Details should display', function () {
            expect(contactPage.contactDetailsSection.isDisplayed()).toEqual(true);
            expect(contactPage.facebook.isDisplayed()).toEqual(true);
            expect(contactPage.email.isDisplayed()).toEqual(true);
            expect(contactPage.phone.isDisplayed()).toEqual(true);
            expect(contactPage.address.isDisplayed()).toEqual(true);
        });

        it('Contact Details text should be correct', function () {
            expect(contactPage.facebook.getText()).toEqual('Trade Your Phone');
            expect(contactPage.email.getText()).toEqual('support@tradeyourphone.com.au');
            expect(contactPage.phone.getText()).toEqual('0484 591 716');
            expect(contactPage.address.getText()).toContain('25 Wills Street');
        });

        it('Contact Details links should be correct', function () {
            expect(contactPage.facebook.getAttribute('href')).toEqual('http://www.facebook.com/tradeyourphoneau');
            expect(contactPage.email.getAttribute('href')).toEqual('mailto:support@tradeyourphone.com.au');
            expect(contactPage.phone.getAttribute('href')).toEqual('tel:0484591716');
        });

        it('Submit button should be disabled', function () {
            expect(contactPage.sendBtn.getAttribute('disabled')).toEqual('true');
        });

        it('Mail sent notification should not display', function () {
            expect(contactPage.mailSentNotification.isDisplayed()).toEqual(false);
        });

        it('Subject should be defaulted to "Question"', function () {
            expect(contactPage.subjectSelect.getAttribute('value')).toEqual('0');
        });

        it('Subject should have correct options', function () {
            expect(contactPage.subjectDropDown.count()).toEqual(4);
            expect(contactPage.subjectDropDown.getText()).toEqual(['I have a Question', 'I have a Complaint', 'I would like more information', 'Other']);
        });


        describe('When submitting a form with incorrect email', function () {

            beforeEach(function () {
                contactPage.enterContactForm('Test Name', 'test', 'I would like more information', 'Message', true);
            });

            it('Should display errors and not allow user to submit', function () {
                expect(contactPage.sendBtn.getAttribute('disabled')).toEqual('true');
                expect(contactPage.errorBlock.getText()).toEqual(['', '', 'Please enter a valid email', '', '']);

            });
        });

        describe('When submitting a form with empty fields', function () {

            beforeEach(function () {
                contactPage.enterContactForm(' ', ' ', 'I have a Question', ' ', true);
            });

            it('Should display errors and not allow user to submit', function () {
                expect(contactPage.sendBtn.getAttribute('disabled')).toEqual('true');
                expect(contactPage.errorBlock.getText()).toEqual(['Your Name is required', 'Your email address is required', '', '', 'A message is required']);

            });
        });

        describe('When submitting a form with correct details', function () {

            beforeEach(function () {
                contactPage.enterContactForm('Test Name', 'ntrous@gmail.com', 'Other', 'Message', false);
            });

            it('Should submit, clear fields and show notification', function () {
                expect(contactPage.nameField.getAttribute('value')).toEqual('');
                expect(contactPage.emailField.getAttribute('value')).toEqual('');
                expect(contactPage.subjectSelect.getAttribute('value')).toEqual('0');
                expect(contactPage.messageField.getAttribute('value')).toEqual('');
                expect(contactPage.sendBtn.getAttribute('disabled')).toEqual('true');
                expect(contactPage.mailSentNotification.isDisplayed()).toEqual(true);
                expect(contactPage.mailSentNotification.getText()).toContain('Mail Sent!');
            });
        });
    });
});
