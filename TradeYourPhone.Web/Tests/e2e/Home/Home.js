var HomePage = require("./HomePage.js");
var homePage;


describe("Home Page", function () {

    beforeEach(function () {
        browser.driver.manage().deleteAllCookies();
        browser.get(browser.baseUrl);
        homePage = new HomePage();
    });

    describe('When opening the Home Page', function () {

        it('TYP Image should display', function () {
            expect(homePage.typHeading.isDisplayed()).toEqual(true);
        });

        it('Navigation Should Display', function () {
            expect(homePage.navContainer.isDisplayed()).toEqual(true);
            expect(homePage.navItems.count()).toEqual(5);
            expect(homePage.navItems.getText()).toEqual(["Home", "What We Do", "Support", "Contact", "Blog"]);
        });

        it('Phone Selection Area Should Be Displayed', function () {
            expect(homePage.quoteSection.isDisplayed()).toEqual(true);
            expect(homePage.phoneImageBackground.isDisplayed()).toEqual(true);
            expect(homePage.phoneSection.isDisplayed()).toEqual(true);
        });

        it('Quote Tabs Area Should Not Be Displayed', function () {
            expect(homePage.quoteTabsSection.isDisplayed()).toEqual(false);
        });

        it('TYP Info Divs Should Be Displayed', function () {
            expect(homePage.aboutDivs.isDisplayed()).toEqual([true, true, true, true, true, true]);
            expect(homePage.aboutDivs.count()).toEqual(6);
            expect(homePage.aboutDivsTitles.getText()).toEqual(["Payment Your Way", "Free Postage", "Prompt Payment", "Support", "Who Are We", "How It's Done"]);
        });

        it('Sticker Img is Displayed', function () {
            expect(homePage.stickerImg.isDisplayed()).toEqual(true);
        });

        it('Social Media Icons Should Be Displayed', function () {
            expect(homePage.socialIconSection.isDisplayed()).toEqual(true);
            expect(homePage.socialIcons.count()).toEqual(6);

            expect(homePage.googleIcon.isDisplayed()).toEqual(true);
            expect(homePage.googleIcon.getAttribute('href')).toEqual('https://plus.google.com/+TradeYourPhoneBrunswick/about');

            expect(homePage.facebookIcon.isDisplayed()).toEqual(true);
            expect(homePage.facebookIcon.getAttribute('href')).toEqual('https://www.facebook.com/tradeyourphoneau');

            expect(homePage.twitterIcon.isDisplayed()).toEqual(true);
            expect(homePage.twitterIcon.getAttribute('href')).toEqual('https://twitter.com/tradeyourphone');

            expect(homePage.yelpIcon.isDisplayed()).toEqual(true);
            expect(homePage.yelpIcon.getAttribute('href')).toEqual('http://www.yelp.com.au/biz/trade-your-phone-melbourne');

            expect(homePage.phoneIcon.isDisplayed()).toEqual(true);
            expect(homePage.phoneIcon.getAttribute('href')).toEqual('tel:0484591716');

            expect(homePage.mailIcon.isDisplayed()).toEqual(true);
            expect(homePage.mailIcon.getAttribute('href')).toEqual('mailto:support@tradeyourphone.com.au');
        });

        it('Review Link is Displayed', function () {
            expect(homePage.reviewLink.isDisplayed()).toEqual(true);
            expect(homePage.reviewLink.getText()).toContain('Tell Us What You Think By Leaving Us a Review!');
        });
    });
});