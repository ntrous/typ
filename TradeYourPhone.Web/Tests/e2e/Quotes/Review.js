var QuotePage = require('./QuotePage.js');
var quotePage;
var phoneToTest;


describe('Phones To Sell Section', function () {

    beforeEach(function () {
        browser.driver.manage().deleteAllCookies();
        browser.get('/');
        quotePage = new QuotePage();
        phoneToTest = 'Samsung Galaxy S6 Edge (64GB)';

    });

    describe('When adding a phone to cart', function () {

        beforeEach(function () {
            quotePage.sellPhone(phoneToTest, 'New', 1);
        });

        it('Should display Quote Tab Section', function () {
            // Quote Section should display with heading
            expect(quotePage.quoteSection.isDisplayed()).toEqual(true);
            expect(quotePage.quoteSectionHeading.isDisplayed()).toEqual(true);
            expect(quotePage.quoteSectionHeading.getText()).toContain('Trade your phone for cash now');
          
            // Phones to sell section should display with heading
            expect(quotePage.phonesToSellSection.isDisplayed()).toEqual(true);
            expect(quotePage.phonesToSellHeading.isDisplayed()).toEqual(true);
            expect(quotePage.phonesToSellHeading.getText()).toContain('Phones To Sell');

            //table should show
            expect(quotePage.reviewTable.isDisplayed()).toEqual(true);
            expect(quotePage.reviewTableRows.count()).toEqual(1);
            expect(quotePage.phoneInTable.getText()).toEqual(phoneToTest);
            expect(quotePage.conditionInTable.getText()).toEqual('New');

            // delete button should show
            expect(quotePage.deleteBtn.isDisplayed()).toEqual(true);
        });

        describe('When deleting the phone from multiple phones', function () {

            beforeEach(function () {
                quotePage.sellPhone(phoneToTest, 'New', 1).then(function () {
                    //should add another phone
                    expect(quotePage.reviewTableRows.count()).toEqual(2);
                    // delete first phone
                    quotePage.deletePhoneFromReview(1);
                });
            });

            it('Should delete the phone from a list', function () {
                expect(quotePage.reviewTableRows.count()).toEqual(1);
            });
        });

        describe('When deleting the last phone', function () {

            beforeEach(function () {
                expect(quotePage.reviewTableRows.count()).toEqual(1);
                // delete first phone
                quotePage.deletePhoneFromReview(1);
            });

            it('Should delete the phone and remove quote section', function () {
                expect(quotePage.quoteSection.isPresent()).toEqual(false);
                expect(quotePage.noPhoneHeading.isDisplayed()).toEqual(true);
                expect(quotePage.noPhoneHeading.getText()).toEqual('What Phone Do You Want To Sell?');
            });
        });
    });
});