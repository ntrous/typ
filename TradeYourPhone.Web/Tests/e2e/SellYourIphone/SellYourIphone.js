var SellYourIphonePage = require('./SellYourIphonePage.js');
var sellYourIphonePage;


describe('SellYourIphone', function () {

    beforeEach(function () {
        browser.driver.manage().deleteAllCookies();
        browser.get('/sell-your-iphone');
        sellYourIphonePage = new SellYourIphonePage();
    });

    describe('When opening the Sell Your iPhone Page', function () {

        it('should display the top banner', function () {
            expect(sellYourIphonePage.heading.isDisplayed()).toEqual(true);
            expect(sellYourIphonePage.heading.getText()).toEqual('Sell Your iPhone Here!');
            expect(sellYourIphonePage.subHeading.isDisplayed()).toEqual(true);
            expect(sellYourIphonePage.subHeading.getText()).toEqual('Get the best price for your old iPhone and free shipping!');
            expect(sellYourIphonePage.headingImage.isDisplayed()).toEqual(true);
        });

        it('should display instructional text', function () {
            expect(sellYourIphonePage.instructionHeading.isDisplayed()).toEqual(true);
            expect(sellYourIphonePage.instructionHeading.getText()).toEqual('Click the iPhone you wish to sell below');
            expect(sellYourIphonePage.instructionText.isDisplayed()).toEqual(true);
            expect(sellYourIphonePage.instructionText.getText()).toContain('The prices listed are what we will pay you for your iPhone');
        });

        it('should display rows of phones', function () {
            expect(sellYourIphonePage.phoneRows.count()).toBeGreaterThan(0);
            expect(sellYourIphonePage.countRowTiles(0)).toEqual(4);
        });
    });
});