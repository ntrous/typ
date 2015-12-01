var QuotePage = require('./QuotePage.js');
var quotePage;
var android;
var iphone;


describe('SummaryTab', function () {

    beforeEach(function () {
        browser.driver.manage().deleteAllCookies();
        browser.get('/');
        quotePage = new QuotePage();
        android = 'Samsung Galaxy S6 Edge (64GB)';
        iphone = 'Apple iPhone 6s Plus (128GB)';
    });
    


    describe('When selecting an Android and choosing Paypal and Post Yourself', function () {

        beforeEach(function () {
            quotePage.sellPhone(android, 'New', 1).then(function () {
                quotePage.goToNextTab(1).then(function () {
                    quotePage.enterDetails("John Smith", "ntrous@gmail.com", "0421577380", "1 test street", "test", "NSW", "1234", "Paypal", "", "", "ntrous@gmail.com", "Post Yourself", false);
                });
            });
           
        });

        it('it should display the Summary Section', function () {
            // summary tab should be active
            expect(quotePage.summaryTabHeading.getAttribute('class')).toContain('active');
            expect(quotePage.summaryTabContent.getAttribute('class')).toContain('active');
            // Summary Section and Heading should be displayed
            expect(quotePage.summarySection.isDisplayed()).toEqual(true);
            expect(quotePage.summaryTitle.isDisplayed()).toEqual(true);
            expect(quotePage.summaryTitle.getText()).toEqual('Quote Summary');

        });

        it('it should display the Personal Details', function () {
            expect(quotePage.personalDetails.isDisplayed()).toEqual(true);
            expect(quotePage.personalDetailsTitle.getText()).toContain('Personal Details');
            expect(quotePage.summaryName.getText()).toEqual('John Smith');
            expect(quotePage.summaryEmail.getText()).toEqual('ntrous@gmail.com');
            expect(quotePage.summaryPhone.getText()).toEqual('0421577380');
        });

        it('it should display the Address Details', function () {
            expect(quotePage.addressDetails.isDisplayed()).toEqual(true);
            expect(quotePage.addressDetailsTitle.getText()).toContain('Your Address');
            expect(quotePage.addressLine1.getText()).toEqual('1 test street');
            expect(quotePage.addressLine2.getText()).toEqual('test');
            expect(quotePage.addressLine3.getText()).toEqual('NSW 1234');
        });

        it('it should display the Payment Details', function () {
            expect(quotePage.paymentDetails.isDisplayed()).toEqual(true);
            expect(quotePage.paymentDetailsTitle.getText()).toContain('Payment Details');
            expect(quotePage.paidAmount.getText()).toContain('A total amount of');
            expect(quotePage.chosenType.getText()).toEqual('Chosen payment type is: Paypal');
            expect(quotePage.paymentDetailsEntered.getText()).toEqual('Paypal Email: ntrous@gmail.com');
        });

        it('it should display the Postage Details', function () {
            expect(quotePage.postageDetails.isDisplayed()).toEqual(true);
            expect(quotePage.postageDetailsTitle.getText()).toContain('Postage Information');
            expect(quotePage.postageInfo.getText()).toContain('(This will also be emailed to you)');
            expect(quotePage.postagePost.isDisplayed()).toEqual(true);
            expect(quotePage.postageSatchel.isPresent()).toEqual(false);
            expect(quotePage.postagePost.getText()).toContain('You have chosen to send your phone(s) to us yourself. Once you submit your quote, simply package your phone up in a satchel or a box and write the below free reply-paid address and your quote number on the front.');
        });

        it('it should display the Phone Summary Table', function () {
            expect(quotePage.getSummaryTableCellText('head', 1, 4)).toEqual('Phone Type');
            expect(quotePage.getSummaryTableCellText('head', 1, 5)).toEqual('Phone Condition');
            expect(quotePage.getSummaryTableCellText('head', 1, 6)).toEqual('Offer');
            expect(quotePage.getSummaryTableCellText('body', 1, 4)).toEqual(android);
            expect(quotePage.getSummaryTableCellText('body', 1, 5)).toEqual('New');
            expect(quotePage.phoneSummaryTableRows.count()).toEqual(1);
        });

        it('it should display the Summary Conditions and Buttons', function () {
            expect(quotePage.cleariCloudCheck.isDisplayed()).toEqual(false);
            expect(quotePage.agreeTCLabel.isDisplayed()).toEqual(true);
            expect(quotePage.agreeTCLabel.getText()).toContain('I agree to the');
            expect(quotePage.agreeTCLabel.getText()).toContain('Terms and Conditions');
            expect(quotePage.agreeTCCheckbox.isDisplayed()).toEqual(true);
            expect(quotePage.summarySubmit.isDisplayed()).toEqual(true);
            expect(quotePage.summarySubmit.getAttribute('disabled')).toEqual('true');
            expect(quotePage.backToDetailsBtn.isDisplayed()).toEqual(true);
            expect(quotePage.backToDetailsBtn.getText()).toContain('Go Back');
        });

        describe('When clicking the Go Back button', function () {

            beforeEach(function () {
                quotePage.backToDetailsBtn.click();
            });

            it('the Review tab should be displayed', function () {
                expect(quotePage.detailsTabHeading.getAttribute('class')).toContain('active');
                expect(quotePage.detailsTabContent.getAttribute('class')).toContain('active');
            });
        });



        describe('When checking the Terms and Condition', function () {

            beforeEach(function () {
                quotePage.agreeTCCheckbox.click();
            });

            it('the submit button should be enabled', function () {
                expect(quotePage.summarySubmit.getAttribute('disabled')).toEqual(null);
            });

            describe('When submitting the Quote', function () {

                beforeEach(function () {
                    quotePage.summarySubmit.click();
                });

                it('the Submitted Summary section should be displayed', function () {
                    expect(quotePage.submittedSummarySection.isDisplayed()).toEqual(true);

                    // Quote Number
                    expect(quotePage.submittedQuoteNumberTitle.isDisplayed()).toEqual(true);
                    expect(quotePage.submittedQuoteNumberTitle.getText()).toContain('Thank you for submitting your quote, your reference number is ');
                    expect(quotePage.submittedQuoteNumberTitle.getText()).not.toEqual('Thank you for submitting your quote, your reference number is ');

                    // What Happens Next
                    expect(quotePage.submittedWhatHappensNextTitle.isDisplayed()).toEqual(true);
                    expect(quotePage.submittedWhatHappensNextTitle.getText()).toContain('What happens next?');
                    expect(quotePage.submittedWhatHappensNextText.isDisplayed()).toEqual(true);
                    expect(quotePage.submittedWhatHappensNextText.getText()).toContain('You will be receiving an email shortly with instructions on how to send your phone to us.');

                    // Postage Info
                    expect(quotePage.submittedPostageTitle.isDisplayed()).toEqual(true);
                    expect(quotePage.submittedPostageTitle.getText()).toContain('Postage Information');
                    expect(quotePage.submittedWaitingDelivery.isDisplayed()).toEqual(true);
                    expect(quotePage.submittedWaitingDelivery.getText()).toContain('You have chosen to send your phone(s) to us yourself. Simply package your phone up in a satchel or a box and write the below free reply-paid address on the front.');
                    expect(quotePage.submittedRequiresSatchel.isDisplayed()).toEqual(false);

                    // Start another quote button
                    expect(quotePage.startAnotherQuote.isDisplayed()).toEqual(true);
                });

                describe('When starting another quote', function () {

                    beforeEach(function () {
                        quotePage.startAnotherQuote.click();
                    });

                    it('the whole site should be reset', function () {
                        expect(quotePage.quoteTabSection.isDisplayed()).toEqual(false);
                        expect(quotePage.noPhoneHeading.isDisplayed()).toEqual(true);
                        expect(quotePage.noPhoneHeading.getText()).toEqual('Select Your Phone');
                    });
                });
            });
        });
    });

    describe('When selecting an iphone', function () {

        beforeEach(function () {
            quotePage.sellPhone(iphone, 'New', 1).then(function () {
                quotePage.goToNextTab(1).then(function () {
                    quotePage.enterDetails("John Smith", "ntrous@gmail.com", "0421577380", "1 test street", "test", "NSW", "1234", "Paypal", "", "", "ntrous@gmail.com", "Post Yourself", false);
                });
            });
           
        });

        it('it should display icloud lock text', function () {
            expect(quotePage.cleariCloudCheck.isDisplayed()).toEqual(true);
        });
    });

    describe('When selecting an Android, and choosing Bank Transfer and Free Satchel', function () {

        beforeEach(function () {
            quotePage.sellPhone(android, 'New', 1).then(function () {
                quotePage.goToNextTab(1).then(function () {
                    quotePage.enterDetails("John Smith", "ntrous@gmail.com", "0421577380", "1 test street", "test", "NSW", "1234", "Bank Transfer", "123456", "123456789", "", "Free Satchel", false);
                });
            });
           
            it('it should display BSB, Account Number and Free Satchel Text', function () {
                expect(quotePage.chosenType.getText()).toEqual('Chosen payment type is: Bank Transfer');
                expect(quotePage.paymentDetailsEntered.getText()).toContain('BSB: 123456');
                expect(quotePage.paymentDetailsEntered.getText()).toContain('Account Number: 123456789');

                expect(quotePage.postageSatchel.isDisplayed()).toEqual(true);
                expect(quotePage.postageSatchel.getText()).toContain('You have chosen to send your phone to us via the free satchel method. After you submit your quote, your free reply-paid AusPost satchel will arrive in the mail shortly.');
                expect(quotePage.postagePost.isPresent()).toEqual(false);

                expect(quotePage.phoneSummaryTableRows.count()).toEqual(1);

                expect(quotePage.cleariCloudCheck.isDisplayed()).toEqual(false);       
            });

            describe('When submitting the Quote', function () {

                beforeEach(function () {
                    quotePage.summarySubmit.click();
                });

                it('the Submitted Summary section should display Requires Satchel Text', function () {
                    expect(quotePage.submittedSummarySection.isDisplayed()).toEqual(true);
                    expect(quotePage.submittedRequiresSatchel.isDisplayed()).toEqual(true);
                    expect(quotePage.submittedRequiresSatchel.getText()).toContain('You have chosen to send your phone to us via the free satchel method. Your free AusPost satchel will arrive in the mail shortly.');
                    expect(quotePage.submittedWaitingDelivery.isDisplayed()).toEqual(false);
                });
            });
            
        });
    });
});