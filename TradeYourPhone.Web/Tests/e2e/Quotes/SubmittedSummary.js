var QuotePage = require('./QuotePage.js');
var quotePage;
var android;


describe('Submitted Summary Section', function () {

    beforeEach(function () {
        browser.driver.manage().deleteAllCookies();
        browser.get('/');
        quotePage = new QuotePage();
        android = 'Samsung Galaxy S6 Edge (64GB)';
    });
    

    describe('When submitting an Android and choosing Post Yourself', function () {

        beforeEach(function () {
            quotePage.sellPhone(android, 'New', 1).then(function () {             
                 quotePage.enterDetails("John Smith", "ntrous@gmail.com", "0421577380", "1 test street", "test", "NSW", "1234", "Paypal", "", "", "ntrous@gmail.com", "Post Yourself", false);
            }); 
        });

        it('it should display the Submitted Summary Section', function () {
            // Summary Section and Heading should be displayed
            expect(quotePage.quoteSubmittedSection.isDisplayed()).toEqual(true);
            expect(quotePage.submittedHeading.isDisplayed()).toEqual(true);
            expect(quotePage.submittedHeading.getText()).toEqual('Thank you for your Quote');
        });

        it('it should display the Quote Number', function () {
            expect(quotePage.quoteNumHeading.isDisplayed()).toEqual(true);
            expect(quotePage.quoteNumHeading.getText()).toContain('Thank you for submitting your quote, your reference number is ');
            expect(quotePage.quoteNumHeading.getText()).not.toEqual('Thank you for submitting your quote, your reference number is ');
        });

    
        it('the What Happens Next Section should be displayed', function () {
            expect(quotePage.whatHappensNextTitle.isDisplayed()).toEqual(true);
            expect(quotePage.whatHappensNextTitle.getText()).toContain('What happens next?');
            expect(quotePage.whatHappensNextText.isDisplayed()).toEqual(true);
            expect(quotePage.whatHappensNextText.getText()).toContain('You will be receiving an email shortly with instructions on how to send your phone to us.');
        });


        it('The Postage Section should be displayed', function () {
            expect(quotePage.postageInfoHeading.isDisplayed()).toEqual(true);
            expect(quotePage.postageInfoHeading.getText()).toContain('Postage Information');
            expect(quotePage.submittedWaitingDelivery.isDisplayed()).toEqual(true);
            expect(quotePage.submittedWaitingDelivery.getText()).toContain('You have chosen to send your phone(s) to us yourself. Simply package your phone up in a satchel or a box and write the below free reply-paid address on the front.');
            expect(quotePage.submittedRequiresSatchel.isDisplayed()).toEqual(false);
        });


        it('The Start Another Quote button should be displayed', function () {
            expect(quotePage.startAnotherQuote.isDisplayed()).toEqual(true);
        });

        describe('When starting another quote', function () {

            beforeEach(function () {
                quotePage.startAnotherQuote.click();
            });

            it('the whole site should be reset', function () {
                expect(quotePage.quoteSection.isPresent()).toEqual(false);
                expect(quotePage.quoteSubmittedSection.isPresent()).toEqual(false);
                expect(quotePage.noPhoneHeading.isDisplayed()).toEqual(true);
                expect(quotePage.noPhoneHeading.getText()).toEqual('What Phone Do You Want To Sell?');
            });
        });
    });
       
    describe('When submitting an Android, and choosing Free Satchel', function () {

        beforeEach(function () {
            quotePage.sellPhone(android, 'New', 1).then(function () {
                quotePage.enterDetails("John Smith", "ntrous@gmail.com", "0421577380", "1 test street", "test", "NSW", "1234", "Bank Transfer", "123456", "123456789", "", "Free Satchel", false);
            });
           
            it('the Submitted Summary section should display Requires Satchel Text', function () {
                expect(quotePage.quoteSubmittedSection.isDisplayed()).toEqual(true);
                expect(quotePage.submittedRequiresSatchel.isDisplayed()).toEqual(true);
                expect(quotePage.submittedRequiresSatchel.getText()).toContain('You have chosen to send your phone to us via the free satchel method. Your free AusPost satchel will arrive in the mail shortly.');
                expect(quotePage.submittedWaitingDelivery.isDisplayed()).toEqual(false);
            });

            
        });
    });
});