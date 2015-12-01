var QuotePage = require('./QuotePage.js');
var quotePage;
var phoneToTest;


describe('ReviewTab', function () {

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
            expect(quotePage.quoteTabSection.isDisplayed()).toEqual(true);
            expect(quotePage.quoteTabSectionHeading.isDisplayed()).toEqual(true);
            expect(quotePage.quoteTabSectionHeading.getText()).toContain('Trade your phone for cash now');
            // all tabs should be there, 3 for headings and 3 for content
            expect(quotePage.quoteTabs.count()).toEqual(6);
            // review tab should be active
            expect(quotePage.reviewTabHeading.getAttribute('class')).toContain('active');
            expect(quotePage.reviewTabContent.getAttribute('class')).toContain('active');
            expect(quotePage.reviewTabHeading.getAttribute('heading')).toEqual('Review Your Quote');

            expect(quotePage.detailsTabHeading.isDisplayed()).toEqual(true);
            expect(quotePage.detailsTabHeading.getAttribute('heading')).toEqual('Enter Your Details');

            expect(quotePage.summaryTabHeading.isDisplayed()).toEqual(true);
            expect(quotePage.summaryTabHeading.getAttribute('heading')).toEqual('Review Your Summary');

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
                expect(quotePage.quoteTabSection.isDisplayed()).toEqual(false);
                expect(quotePage.noPhoneHeading.isDisplayed()).toEqual(true);
                expect(quotePage.noPhoneHeading.getText()).toEqual('Select Your Phone');
            });
        });
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
        });
    });
});