var AboutPage = require('./AboutPage.js');
var aboutPage;


describe('About - What We Do', function () {

    beforeEach(function () {
        browser.driver.manage().deleteAllCookies();
        browser.get('WhatWeDo');
        aboutPage = new AboutPage();
    });

    describe('When going to the What We Do Page', function () {

        it('Should have the correct Heading and Sub Heading', function () {
            expect(aboutPage.heading.isDisplayed()).toEqual(true);
            expect(aboutPage.heading.getText()).toEqual('About Trade Your Phone');

            expect(aboutPage.sellPhoneSubHeading.isDisplayed()).toEqual(true);
            expect(aboutPage.sellPhoneSubHeading.getText()).toEqual('How To Sell Your Phone');
        });

        it('Should display text', function () {
            expect(aboutPage.aboutText.isDisplayed()).toEqual(true);
            expect(aboutPage.aboutText.getText()).toContain('We’re passionate about re-allocating and recycling second-hand mobile phones. And we want to help YOU turn your old phone into cash.');

            expect(aboutPage.sellPhoneText.isDisplayed()).toEqual(true);
            expect(aboutPage.sellPhoneText.getText()).toContain('We\'ve made our process as simple as possible!');
        });

        it('Should display phone image', function () {
            expect(aboutPage.phoneImg.isDisplayed()).toEqual(true);
        });

        it('Should display TYP process', function () {
            expect(aboutPage.typProcess.isDisplayed()).toEqual(true);

            expect(aboutPage.submitQuoteHeading.getText()).toContain('Submit Your Quote');
            expect(aboutPage.sendPhoneHeading.getText()).toContain('Send Us Your Phone');
            expect(aboutPage.getPaidHeading.getText()).toContain('Get Paid');
        });

        it('Quote links should be displayed', function () {
            expect(aboutPage.completeQuoteLink.isDisplayed()).toEqual(true);
            expect(aboutPage.quoteLink.isDisplayed()).toEqual(true);

            expect(aboutPage.completeQuoteLink.getText()).toEqual('Complete the quote');
            expect(aboutPage.quoteLink.getText()).toEqual('quote');

            expect(aboutPage.completeQuoteLink.getAttribute('href')).toEqual(browser.baseUrl +'/');
            expect(aboutPage.quoteLink.getAttribute('href')).toEqual(browser.baseUrl +'/');
        });
    });
});