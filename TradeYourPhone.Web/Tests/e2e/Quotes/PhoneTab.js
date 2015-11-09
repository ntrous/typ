var QuotePage = require('./QuotePage.js');
var quotePage;
var phoneToTest;


describe('PhoneTab', function () {

    beforeEach(function () {
        browser.get('http://localhost:53130/');
        quotePage = new QuotePage();
        phoneToTest = 'Samsung Galaxy S6 Edge (64GB)';

    });

    it('Should have the correct a title', function () {
        // Less than one phone so Select Phone title should be used and shown
        expect(quotePage.noPhoneHeading.isDisplayed()).toEqual(true);
        expect(quotePage.noPhoneHeading.getText()).toEqual('Select Your Phone');
    });

    it('Should display the phone input box', function () {
        // Select Phone search box should be displayed
        expect(quotePage.phoneInput.isDisplayed()).toEqual(true);
    });

    it('Should not display the phone condition and sell button areas', function () {
        // Condition and sell area should not be displayed
        expect(quotePage.sellPhoneArea.isPresent()).toEqual(false);
        expect(quotePage.phoneConditionArea.isPresent()).toEqual(false);
    });

    describe('When entering a new phone', function () {

        beforeEach(function () {
            quotePage.searchForPhone('Samsung Galaxy S6 Edge (64GB)');

        });

        it('Should display correct phone', function () {
            expect(quotePage.phoneSearchList.isDisplayed()).toEqual(true);
            expect(quotePage.phoneSearchListItems.count()).toEqual(1);
        });

    });

    describe('When selecting a phone', function () {

        beforeEach(function () {
            quotePage.selectFirstPhone('Samsung Galaxy S6 Edge (64GB)');

        });

        it('Should select the correct phone', function () {
            expect(quotePage.phoneInput.getAttribute('value')).toEqual(phoneToTest);
        });


        it('Should show all conditions', function () {
            expect(quotePage.phoneConditionArea.isDisplayed()).toEqual(true);
            expect(quotePage.phoneConditionHeading.isDisplayed()).toEqual(true);
            expect(quotePage.phoneConditionHeading.getText()).toEqual('Choose the Condition');
            expect(quotePage.phoneConditions.count()).toEqual(3);
            expect(quotePage.btnNew.isDisplayed()).toEqual(true);
            expect(quotePage.btnGood.isDisplayed()).toEqual(true);
            expect(quotePage.btnFaulty.isDisplayed()).toEqual(true);
        });
    });

    describe('When selecting New condition', function () {

        beforeEach(function () {
            quotePage.selectCondition(phoneToTest, 'New', 1);

        });

        it('Should select the New condition', function () {
            expect(quotePage.btnNew.getAttribute('class')).toContain('active');
            expect(quotePage.textNew.isDisplayed()).toEqual(true);
        });

        it('Should display the sell phone area', function () {
            expect(quotePage.sellPhoneArea.isDisplayed()).toEqual(true);
            expect(quotePage.sellPhoneHeading.isDisplayed()).toEqual(true);
            expect(quotePage.sellPhoneHeading.getText()).toEqual('Sell Your Phone For');
            expect(quotePage.phonePrice.isDisplayed()).toEqual(true);
            expect(quotePage.phonePrice.getText()).not.toEqual('');
            expect(quotePage.btnSellPhone.isDisplayed()).toEqual(true);
            expect(quotePage.btnSellPhone.getText()).toContain('Sell This Phone');
        });
    });

    describe('When selecting Good condition', function () {

        beforeEach(function () {
            quotePage.selectCondition(phoneToTest, 'Good', 2);

        });

        it('Should select the New condition', function () {
            expect(quotePage.btnGood.getAttribute('class')).toContain('active');
            expect(quotePage.textGood.isDisplayed()).toEqual(true);
        });
    });

    describe('When selecting Faulty condition', function () {

        beforeEach(function () {
            quotePage.selectCondition(phoneToTest, 'Faulty', 3);

        });

        it('Should select the New condition', function () {
            expect(quotePage.btnFaulty.getAttribute('class')).toContain('active');
            expect(quotePage.textFaulty.isDisplayed()).toEqual(true);
        });
    });

    describe('When adding a phone to cart', function () {

        beforeEach(function () {
            quotePage.sellPhone(phoneToTest, 'New', 1);
        });

        it('Should display Quote Tab Section and reset top panel', function () {
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
           // There is one phone so 'You have 1 phone in your cart, want to add another phone?' title should be used and shown
            expect(quotePage.phonesHeading.isDisplayed()).toEqual(true);
            expect(quotePage.noPhoneHeading.isDisplayed()).toEqual(false);
            expect(quotePage.phonesHeading.getText()).toEqual('You have 1 phone in your cart, want to add another phone?');
            expect(quotePage.phoneInput.getAttribute('value')).toEqual('');
            // Condition and sell area should not be displayed
            expect(quotePage.sellPhoneArea.isPresent()).toEqual(false);
            expect(quotePage.phoneConditionArea.isPresent()).toEqual(false);

            //table should show
            expect(quotePage.reviewTable.isDisplayed()).toEqual(true);
            expect(quotePage.reviewTableRows.count()).toEqual(1);
            expect(quotePage.phoneInTable.getText()).toEqual(phoneToTest);
            expect(quotePage.conditionInTable.getText()).toEqual('New');
           
        });
    });

    describe('When clicking continue on review tab', function () {

        beforeEach(function () {
            quotePage.sellPhone(phoneToTest, 'New', 1).then(function () {
                quotePage.goToNextTab(1);
            });
        });

        it('Should display the Details Tab', function () {
            expect(quotePage.detailsTabHeading.getAttribute('class')).toContain('active');
            expect(quotePage.detailsTabContent.getAttribute('class')).toContain('active');
        });
    });

});