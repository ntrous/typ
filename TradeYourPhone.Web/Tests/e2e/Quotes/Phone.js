var QuotePage = require('./QuotePage.js');
var quotePage;
var phoneToTest;


describe('PhoneTab', function () {

    beforeEach(function () {
        browser.driver.manage().deleteAllCookies();
        browser.get('/');
        quotePage = new QuotePage();
        phoneToTest = 'Samsung Galaxy S6 Edge (64GB)';

    });

    it('Should have the correct a title', function () {
        // Less than one phone so Select Phone title should be used and shown
        expect(quotePage.noPhoneHeading.isDisplayed()).toEqual(true);
        expect(quotePage.noPhoneHeading.getText()).toEqual('What Phone Do You Want To Sell?');
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
            expect(quotePage.phoneConditionHeading.getText()).toEqual('What Condition is it in?');
            expect(quotePage.phoneConditions.count()).toEqual(3);
            expect(quotePage.btnNew.isDisplayed()).toEqual(true);
            expect(quotePage.btnGood.isDisplayed()).toEqual(true);
            expect(quotePage.btnFaulty.isDisplayed()).toEqual(true);
            // Good condition should be default
            expect(quotePage.btnGood.getAttribute('class')).toContain('active');
        });

        describe('When deleting the phone text', function () {

            beforeEach(function () {
                quotePage.phoneInput.clear();
            });

            it('Phone Conditons and sell button should hide', function () {
                // Condition and sell area should not be displayed
                expect(quotePage.sellPhoneArea.isPresent()).toEqual(false);
                expect(quotePage.phoneConditionArea.isPresent()).toEqual(false);
                expect(quotePage.phoneInput.getAttribute('value')).toEqual('');
            });
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
            expect(quotePage.sellPhoneHeading.getText()).toEqual("We'll Buy Your Phone For");
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
            expect(quotePage.quoteSection.isDisplayed()).toEqual(true);
            expect(quotePage.quoteSectionHeading.isDisplayed()).toEqual(true);
            expect(quotePage.quoteSectionHeading.getText()).toContain('Trade your phone for cash now');
           
           // There is one phone so 'You have 1 phone in your cart, want to add another phone?' title should be used and shown
            expect(quotePage.phonesHeading.isDisplayed()).toEqual(true);
            expect(quotePage.noPhoneHeading.isDisplayed()).toEqual(false);
            expect(quotePage.phonesHeading.getText()).toEqual('Add another phone');
            expect(quotePage.phoneInput.getAttribute('value')).toEqual('');
            // Condition and sell area should not be displayed
            expect(quotePage.sellPhoneArea.isPresent()).toEqual(false);
            expect(quotePage.phoneConditionArea.isPresent()).toEqual(false);
        });
    });
});

   