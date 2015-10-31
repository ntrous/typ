    var QuotePage = require('./QuotePage.js');
    var quotePage;


describe('PhoneTab', function () {
    
    beforeEach(function () {
        browser.get('http://localhost:53131/');
        quotePage = new QuotePage();

        
    });

    it('Should have the correct a title', function () {
        // Less than one phone so Select Phone title should be used
        expect(quotePage.phoneHeading.getText()).toEqual('Select Your Phone');
    });

    it('Should display the phone input box', function () {
        // Select Phone search box should be displayed
        expect(quotePage.phoneInput.isDisplayed()).toEqual(true);
    });

    describe('When entering a new phone', function () {
    
        beforeEach(function () {
            quotePage.phoneInput.sendKeys('Samsung Galaxy S6 Edge (64GB)');
        
        });

        it('Should have the correct a title', function () {
            expect(quotePage.phoneSearchList.isDisplayed()).toEqual(true);
            expect(quotePage.phoneSearchListItems.count()).toEqual(1);
        });
    });

});