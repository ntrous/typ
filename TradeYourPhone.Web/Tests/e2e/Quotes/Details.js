var QuotePage = require('./QuotePage.js');
var quotePage;
var android;
var iphone;


describe('Details Section', function () {

    beforeEach(function () {
        browser.driver.manage().deleteAllCookies();
        browser.get('/');
        quotePage = new QuotePage();
        android = 'Samsung Galaxy S6 Edge (64GB)';
        iphone = 'Apple iPhone 6s Plus (128GB)';
    });

    describe('When adding an Andriod phone to cart', function () {

        beforeEach(function () {        
            quotePage.sellPhone(android, 'New', 1);
        });

        it('Should display the Details Section', function () {   
            // personal details fields
            expect(quotePage.fullNameField.isDisplayed()).toEqual(true);
            expect(quotePage.emailField.isDisplayed()).toEqual(true);
            expect(quotePage.mobileField.isDisplayed()).toEqual(true);

            // Address fields
            expect(quotePage.streetField.isDisplayed()).toEqual(true);
            expect(quotePage.suburbField.isDisplayed()).toEqual(true);
            expect(quotePage.stateSelect.isDisplayed()).toEqual(true);
            // State should be defaulted to VIC
            expect(quotePage.stateSelect.getAttribute('value')).toEqual('1');
            expect(quotePage.stateDropDown.count()).toEqual(8);
            expect(quotePage.stateDropDown.getText()).toEqual(['VIC', 'NSW', 'WA', 'TAS', 'QLD', 'SA', 'ACT', 'NT']);
            expect(quotePage.postcodeField.isDisplayed()).toEqual(true);

            // Payment fields
            expect(quotePage.paymentTypeSelect.isDisplayed()).toEqual(true);
            // payment type defualts to bank transfer
            expect(quotePage.paymentTypeSelect.getAttribute('value')).toEqual('1');
            expect(quotePage.paymentTypeDropDown.count()).toEqual(2);
            expect(quotePage.paymentTypeDropDown.getText()).toEqual(['Bank Transfer', 'Paypal']);
            expect(quotePage.bsbField.isDisplayed()).toEqual(true);
            expect(quotePage.accountNumField.isDisplayed()).toEqual(true);
            // paypal fields should not be displayed
            expect(quotePage.paypalEmailField.isPresent()).toEqual(false);
            expect(quotePage.paypalSameAsPersonalCheck.isPresent()).toEqual(false);

            // Postage Options
            expect(quotePage.freeSatchelBtn.isDisplayed()).toEqual(true);
            expect(quotePage.postYourselfBtn.isDisplayed()).toEqual(true);
            // post yourself option should be on by default
            expect(quotePage.postYourselfBtn.getAttribute('class')).toContain('active');
            expect(quotePage.postageText.isDisplayed()).toEqual(true);
            expect(quotePage.postageText.getText()).toEqual('The fastest way to get your phone to us. Use your own packaging and post to our free reply paid address.');
        });

        it('it should display the Summary Section', function () {
            // Summary Section and Heading should be displayed
            expect(quotePage.summarySection.isDisplayed()).toEqual(true);
            expect(quotePage.summaryTitle.isDisplayed()).toEqual(true);
            expect(quotePage.summaryTitle.getText()).toEqual('Your Quote Summary');
        
            expect(quotePage.addAnotherPhone.isDisplayed()).toEqual(true);
            expect(quotePage.addAnotherPhone.getText()).toEqual('Add Another Phone');
            // defualts to Bank account for payment
            // should display that it needs bank details
            expect(quotePage.bankSummary.isDisplayed()).toEqual(true);
            expect(quotePage.bankSummaryHeading.getText()).toContain('into your bank account');
            expect(quotePage.bankSummaryText.getText()).toContain('BSB: Please enter your BSB');
            expect(quotePage.bankSummaryText.getText()).toContain('Account Number: Please enter your Account Number');
            // paypal summary should be hidden
            expect(quotePage.paypalSummary.isPresent()).toEqual(false);
            expect(quotePage.appleWarning.isPresent()).toEqual(false);
            // defualts to post yourself for postage
            expect(quotePage.postageSummary.isDisplayed()).toEqual(true);
            expect(quotePage.postageSummaryHeading.getText()).toContain('You have chosen the Post Yourself postage method');
            expect(quotePage.postageSummaryText.getText()).toContain('We will send you complete postage instructions after you have submitted your quote.');
            //Terms and conditions should be displayed
            expect(quotePage.agreeTCLabel.isDisplayed()).toEqual(true);
            expect(quotePage.agreeTCLabel.getText()).toContain('By clicking Submit you agree to the Terms and Conditions');
            // submit button should be displayed
            expect(quotePage.submitBtn.isDisplayed()).toEqual(true);
            expect(quotePage.submitBtn.getText()).toContain('Submit');
        });

        describe('When changing postage type to Free Satchel', function () {

            beforeEach(function () {
                quotePage.freeSatchelBtn.click();
            });

            it('Free Satchel option should be active', function () {
                // freesatchel is active and correct text is shown
                expect(quotePage.freeSatchelBtn.getAttribute('class')).toContain('active');
                expect(quotePage.postageText.getText()).toEqual('A free reply-paid Tracked AusPost satchel will be sent to you. Simply put your phone inside and send it back to us. All for free!');
            });

            it('Postage Summary should display Free Satchel', function () {
                // Postage summary updates
                expect(quotePage.postageSummaryHeading.getText()).toContain('You have chosen the Free Satchel postage method');
            });
        });

        describe('When changing payment type to paypal', function () {

            beforeEach(function () {
                quotePage.selectPaymentType('Paypal');
            });

            it('paypal option should be active', function () {
                // paypal is active and correct inputs are shown
                expect(quotePage.paypalEmailField.isDisplayed()).toEqual(true);
                expect(quotePage.paypalSameAsPersonalCheck.isDisplayed()).toEqual(true);
            });

            it('paypal summary should be displayed', function () {
                // Payment summary should update
                expect(quotePage.paypalSummary.isDisplayed()).toEqual(true);
                expect(quotePage.paypalSummaryHeading.getText()).toContain('into your Paypal Account');
                expect(quotePage.paypalSummaryText.getText()).toContain('Account: Please enter your Paypal Account Email');
                expect(quotePage.bankSummary.isPresent()).toEqual(false);
            });

            describe('When checking that paypal is the same as email', function () {

                beforeEach(function () {
                    quotePage.emailField.sendKeys('test@test.com');
                    quotePage.paypalSameAsPersonalCheck.click();
                });

                it('paypal field should be populated to same email', function () {
                    // paypal checkbox is checked
                    expect(quotePage.paypalSameAsPersonalCheck.getAttribute('value')).toEqual('true');
                    // paypal email field is populated with email and disabled
                    expect(quotePage.paypalEmailField.getAttribute('value')).toEqual('test@test.com');
                    expect(quotePage.paypalEmailField.getAttribute('readonly')).toEqual('true');      
                });

                it('Paypal summary should update to added account', function () {
                    // Paypal summary should update
                    expect(quotePage.paypalSummaryText.getText()).toContain('Account: test@test.com');
                });

                describe('When unchecking the paypal checkbox', function () {

                    beforeEach(function () {
                        quotePage.paypalSameAsPersonalCheck.click();
                    });

                    it('paypal field should be empty and enabled', function () {
                        // paypal email field is empty and enabled
                        expect(quotePage.paypalEmailField.getAttribute('value')).toEqual('');
                        expect(quotePage.paypalEmailField.getAttribute('readonly')).toEqual(null);
                    });

                    it('Paypal summary should update to no account', function () {
                        // Paypal summary should update
                        expect(quotePage.paypalSummaryText.getText()).toContain('Account: Please enter your Paypal Account Email');
                    });
                });
            });
        });

        describe('When adding BSB and account details that are valid', function () {

            beforeEach(function () {
                quotePage.bsbField.sendKeys('123456');
                quotePage.accountNumField.sendKeys('6767565');
            });

            it('should be displayed in the Payment Summary', function () {
                expect(quotePage.bankSummaryHeading.getText()).toContain('into your bank account');
                expect(quotePage.bankSummaryText.getText()).toContain('BSB: 123456');
                expect(quotePage.bankSummaryText.getText()).toContain('Account Number: 6767565');
            });
        });

        describe('When adding BSB and account details that are invalid', function () {

            beforeEach(function () {
                quotePage.bsbField.sendKeys('12345623');
                quotePage.accountNumField.sendKeys('676756523232323232');
            });

            it('should not be displayed in the Payment Summary', function () {
                expect(quotePage.bankSummaryHeading.getText()).toContain('into your bank account');
                expect(quotePage.bankSummaryText.getText()).toContain('BSB: Please enter your BSB');
                expect(quotePage.bankSummaryText.getText()).toContain('Account Number: Please enter your Account Number');
            });
        });

        describe('When adding all details with wrong email, number and paypal and clicking Submit', function () {

            beforeEach(function () {
                quotePage.enterDetails("John Smith", "Wrong", "1123", "1 test street", "test", "NSW", "test", "Paypal", "", "", "test", "Post Yourself", true);
            });

            it('should cause an error and not continue', function () {
                expect(quotePage.errorBlock.getText()).toEqual(['Please fix validation errors below', '', '', 'Please enter a valid email', '', 'Please enter a valid mobile number', '', '', '', '', 'Please enter a valid postcode', '', '', 'Please enter a valid email', 'Click here to fix validation errors above to Submit']);

            });
        });

        describe('When adding all details with wrong bsb and acct no, bank transfer and clicking Submit', function () {

            beforeEach(function () {
                quotePage.enterDetails("John Smith", "test@test.com", "0421577380", "1 test street", "test", "NSW", "1234", "Bank Transfer", "abc", "def", "", "Free Satchel", true);
              
            });

            it('should cause an error and not continue', function () {
                expect(quotePage.errorBlock.getText()).toEqual(['Please fix validation errors below', '', '', '', '', '', '', '', '', '', '', '', '', 'Please enter a valid BSB', '', 'Please enter a valid Account Number', 'Click here to fix validation errors above to Submit']);

            });
        });

        describe('When clicking continue with no details', function () {

            beforeEach(function () {
                quotePage.submitBtn.click();
            });

            it('should cause an error and not continue', function () {
                expect(quotePage.errorBlock.getText()).toEqual(['Please fix validation errors below', 'Your full name is required', 'Email is required', '', 'Mobile is required', '', 'Street is required', 'Suburb is required', '', 'Postcode is required', '', '', 'BSB is required', '', 'Account number is required', '', 'Click here to fix validation errors above to Submit']);
            });

            describe('When changing payment type to paypal', function () {

                beforeEach(function () {
                    quotePage.selectPaymentType('Paypal');
                });

                it('paypal error should be active', function () {
                    expect(quotePage.errorBlock.getText()).toEqual(['Please fix validation errors below', 'Your full name is required', 'Email is required', '', 'Mobile is required', '', 'Street is required', 'Suburb is required', '', 'Postcode is required', '', '', 'Paypal Email is required', '', 'Click here to fix validation errors above to Submit']);

                });
            });

        });

        describe('When adding all details correctly choosing Paypal and Post Yourself, and clicking Submit', function () {

            beforeEach(function () {
                quotePage.enterDetails("John Smith", "test@test.com", "0421577380", "1 test street", "test", "NSW", "1234", "Paypal", "", "", "test@test.com", "Post Yourself", false);
            });

            it('it should display the submitted summary section', function () {
                expect(quotePage.quoteSubmittedSection.isDisplayed()).toEqual(true);
            });
        });
    });

    describe('When adding an iPhone phone to cart', function () {

        beforeEach(function () {
            quotePage.sellPhone(iphone, 'New', 1);
        });

        it('it should display icloud lock text', function () {
            expect(quotePage.appleWarning.isDisplayed()).toEqual(true);
            expect(quotePage.appleWarningText.getText()).toContain("All Apple devices must have 'Find my iPhone' removed before sending. Please see the Apple website for instructions.");
        });
    });

});