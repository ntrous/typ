var SupportPage = require('./SupportPage.js');
var supportPage;


describe('Support', function () {

    beforeEach(function () {
        browser.driver.manage().deleteAllCookies();
        browser.get('/Support');
        supportPage = new SupportPage();
    });

    describe('When opening the Support Page', function () {

        it('should display the correct title', function () {
            expect(supportPage.heading.isDisplayed()).toEqual(true);
            expect(supportPage.heading.getText()).toEqual('Support');
        });

        it('should display text and links', function () {
            expect(supportPage.supportText.isDisplayed()).toEqual(true);
            expect(supportPage.supportText.getText()).toContain('Trade Your Phone offers 9am - 7pm support on Monday - Friday,');
            expect(supportPage.contactLink.isDisplayed()).toEqual(true);
            expect(supportPage.contactLink.getText()).toEqual('contact');
            expect(supportPage.contactLink.getAttribute('href')).toEqual(browser.baseUrl + '/Contact');
        });

        it('should have 3 support sections', function () {
            expect(supportPage.supportSections.count()).toEqual(3);
        });

        it('General Section should display', function () {
            expect(supportPage.generalSection.isDisplayed()).toEqual(true);
            expect(supportPage.generalSectionHeading.isDisplayed()).toEqual(true);
            expect(supportPage.generalSectionHeading.getText()).toEqual('General');
            expect(supportPage.generalSectionPanels.count()).toEqual(6);
        });

        it('Handset Section should display', function () {
            expect(supportPage.handsetSection.isDisplayed()).toEqual(true);
            expect(supportPage.handsetSectionHeading.isDisplayed()).toEqual(true);
            expect(supportPage.handsetSectionHeading.getText()).toEqual('Handset');
            expect(supportPage.handsetSectionPanels.count()).toEqual(4);
        });

        it('Postage Section should display', function () {
            expect(supportPage.postageSection.isDisplayed()).toEqual(true);
            expect(supportPage.postageSectionHeading.isDisplayed()).toEqual(true);
            expect(supportPage.postageSectionHeading.getText()).toEqual('Postage');
            expect(supportPage.postageSectionPanels.count()).toEqual(3);
        });

        describe('When opening and closing a panel in General Section', function () {

            beforeEach(function () {
                supportPage.openPanel('GeneralFAQ', 2);
                expect(supportPage.getPanel('GeneralFAQ', 2).getAttribute('class')).toContain('panel-open');
                supportPage.closePanel('GeneralFAQ', 2);
            });

            it('Should close panel', function () {
                expect(supportPage.getPanel('GeneralFAQ', 2).getAttribute('class')).not.toContain('panel-open');
            });
        });

        describe('When opening another panel in General Section', function () {

            beforeEach(function () {
                supportPage.openPanel('GeneralFAQ', 2);
                expect(supportPage.getPanel('GeneralFAQ', 2).getAttribute('class')).toContain('panel-open');
                supportPage.openPanel('GeneralFAQ', 3);
            });

            it('Should close first panel opened', function () {
                expect(supportPage.getPanel('GeneralFAQ', 2).getAttribute('class')).not.toContain('panel-open');
                expect(supportPage.getPanel('GeneralFAQ', 3).getAttribute('class')).toContain('panel-open');
            });
        });

        describe('When opening and closing a panel in Handset Section', function () {

            beforeEach(function () {
                supportPage.openPanel('HandsetFAQ', 3);
                expect(supportPage.getPanel('HandsetFAQ', 3).getAttribute('class')).toContain('panel-open');
                supportPage.closePanel('HandsetFAQ', 3);
            });

            it('Should close panel', function () {
                expect(supportPage.getPanel('HandsetFAQ', 2).getAttribute('class')).not.toContain('panel-open');
            });
        });

        describe('When opening another panel in Handset Section', function () {

            beforeEach(function () {
                supportPage.openPanel('HandsetFAQ', 2);
                expect(supportPage.getPanel('HandsetFAQ', 2).getAttribute('class')).toContain('panel-open');
                supportPage.openPanel('HandsetFAQ', 3);
            });

            it('Should close first panel opened', function () {
                expect(supportPage.getPanel('HandsetFAQ', 2).getAttribute('class')).not.toContain('panel-open');
                expect(supportPage.getPanel('HandsetFAQ', 3).getAttribute('class')).toContain('panel-open');
            });
        });

        describe('When opening and closing a panel in Postage Section', function () {

            beforeEach(function () {
                supportPage.openPanel('PostageFAQ', 2);
                expect(supportPage.getPanel('PostageFAQ', 2).getAttribute('class')).toContain('panel-open');
                supportPage.closePanel('PostageFAQ', 2);
            });

            it('Should close panel', function () {
                expect(supportPage.getPanel('PostageFAQ', 2).getAttribute('class')).not.toContain('panel-open');
            });
        });

        describe('When opening another panel in Postage Section', function () {

            beforeEach(function () {
                supportPage.openPanel('PostageFAQ', 2);
                expect(supportPage.getPanel('PostageFAQ', 2).getAttribute('class')).toContain('panel-open');
                supportPage.openPanel('PostageFAQ', 3);
            });

            it('Should close first panel opened', function () {
                expect(supportPage.getPanel('PostageFAQ', 2).getAttribute('class')).not.toContain('panel-open');
                expect(supportPage.getPanel('PostageFAQ', 3).getAttribute('class')).toContain('panel-open');
            });
        });
    });
});