var QuotePage = require('./QuotePage.js');
var quotePage;
var phoneToTest;


describe('DetailsTab', function () {

    beforeEach(function () {
        browser.driver.manage().deleteAllCookies();
        browser.get('/');
        quotePage = new QuotePage();
        phoneToTest = 'Samsung Galaxy S6 Edge (64GB)';
    });

    describe('When clicking continue on review tab', function () {

        beforeEach(function () {        
            quotePage.sellPhone(phoneToTest, 'New', 1).then(function () {
                quotePage.goToNextTab(1);
            });
        });

        it('Should display the Details Tab', function () {
            // details tab should be active
            expect(quotePage.detailsTabHeading.getAttribute('class')).toContain('active');
            expect(quotePage.detailsTabContent.getAttribute('class')).toContain('active');

            // personal details fields
            expect(quotePage.fullNameField.isDisplayed()).toEqual(true);
            expect(quotePage.emailField.isDisplayed()).toEqual(true);
            expect(quotePage.mobileField.isDisplayed()).toEqual(true);

            // Address fields
            expect(quotePage.streetField.isDisplayed()).toEqual(true);
            expect(quotePage.suburbField.isDisplayed()).toEqual(true);
            expect(quotePage.stateSelect.isDisplayed()).toEqual(true);
            // State should be defaulted to VIC
            expect(quotePage.stateSelect.getAttribute('value')).toEqual('0');
            expect(quotePage.stateDropDown.count()).toEqual(8);
            expect(quotePage.stateDropDown.getText()).toEqual(['VIC', 'NSW', 'WA', 'TAS', 'QLD', 'SA', 'ACT', 'NT']);
            expect(quotePage.postcodeField.isDisplayed()).toEqual(true);

            // Payment fields
            expect(quotePage.paymentTypeSelect.isDisplayed()).toEqual(true);
            // payment type defualts to bank transfer
            expect(quotePage.paymentTypeSelect.getAttribute('value')).toEqual('0');
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

            // Go to summary button
            expect(quotePage.goToSummary.isDisplayed()).toEqual(true);
            expect(quotePage.goToSummary.getText()).toEqual('Go To Summary');

            // Back Button
            expect(quotePage.backToReview.isDisplayed()).toEqual(true);
            expect(quotePage.backToReview.getText()).toEqual('Go Back');
        });

        describe('When clicking the Go Back button', function () {

            beforeEach(function () {
                quotePage.backToReview.click();
            });

            it('the Review tab should be displayed', function () {
                expect(quotePage.reviewTabHeading.getAttribute('class')).toContain('active');
                expect(quotePage.reviewTabContent.getAttribute('class')).toContain('active');
            });
        });

        describe('When changing postage type to Free Satchel', function () {

            beforeEach(function () {
                quotePage.freeSatchelBtn.click();
            });

            it('Free Satchel option should be active', function () {
                // freesatchel is active and correct text is shown
                expect(quotePage.freeSatchelBtn.getAttribute('class')).toContain('active');
                expect(quotePage.postageText.getText()).toEqual('A free reply-paid AusPost satchel will be sent to you. Simply put your phone inside and send it back to us. All for free!');
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

                describe('When unchecking the paypal checkbox', function () {

                    beforeEach(function () {
                        quotePage.paypalSameAsPersonalCheck.click();
                    });

                    it('paypal field should be empty and enabled', function () {
                        // paypal email field is empty and enabled
                        expect(quotePage.paypalEmailField.getAttribute('value')).toEqual('');
                        expect(quotePage.paypalEmailField.getAttribute('readonly')).toEqual(null);
                    });
                });
            });
        });


        describe('When adding all details with wrong email, number and paypal and clicking continue', function () {

            beforeEach(function () {
                quotePage.enterDetails("John Smith", "Wrong", "1123", "1 test street", "test", "NSW", "test", "Paypal", "", "", "test", "Post Yourself", true);
            });

            it('should cause an error and not continue', function () {
                expect(quotePage.errorBlock.getText()).toEqual(['', '', 'Please enter a valid email', '', 'Please enter a valid mobile number', '', '', '', '', 'Please enter a valid postcode', '', '', 'Please enter a valid email', 'Please fix validation errors']);

            });
        });

        describe('When adding all details with wrong bsb and acct no, bank transfer and clicking continue', function () {

            beforeEach(function () {
                quotePage.enterDetails("John Smith", "test@test.com", "0421577380", "1 test street", "test", "NSW", "1234", "Bank Transfer", "abc", "def", "", "Free Satchel", true);
            });

            it('should cause an error and not continue', function () {
                expect(quotePage.errorBlock.getText()).toEqual(['', '', '', '', '', '', '', '', '', '', '', '', 'Please enter a valid BSB', '', 'Please enter a valid Account Number', 'Please fix validation errors']);

            });
        });

        describe('When clicking continue with no details', function () {

            beforeEach(function () {
                quotePage.goToSummary.click();
            });

            it('should cause an error and not continue', function () {
                expect(quotePage.errorBlock.getText()).toEqual(['Your full name is required', 'Email is required', '', 'Mobile is required', '', 'Street is required', 'Suburb is required', '', 'Postcode is required', '', '', 'BSB is required', '', 'Account number is required', '', 'Please fix validation errors']);
            });

            describe('When changing payment type to paypal', function () {

                beforeEach(function () {
                    quotePage.selectPaymentType('Paypal');
                });

                it('paypal error should be active', function () {
                    expect(quotePage.errorBlock.getText()).toEqual(['Your full name is required', 'Email is required', '', 'Mobile is required', '', 'Street is required', 'Suburb is required', '', 'Postcode is required', '', '', 'Paypal Email is required', '', 'Please fix validation errors']);

                });
            });

        });

        describe('When adding all details correctly choosing Paypal and Post Yourself, and clicking continue', function () {

            beforeEach(function () {
                quotePage.enterDetails("John Smith", "test@test.com", "0421577380", "1 test street", "test", "NSW", "1234", "Paypal", "", "", "test@test.com", "Post Yourself", false);
            });

            it('it should go to the summary page', function () {
                // summary tab should be active
                expect(quotePage.summaryTabHeading.getAttribute('class')).toContain('active');
                expect(quotePage.summaryTabContent.getAttribute('class')).toContain('active');
            });
        });
    });

});