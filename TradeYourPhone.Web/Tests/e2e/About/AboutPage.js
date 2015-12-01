var AboutPage = function () {

    this.heading = element(by.css('h1'));

    this.aboutText = element(by.css('.faq-text > div:nth-of-type(1) > p'));

    this.phoneImg = element(by.css('div.phone-for-the-ladies'));

    this.typProcess = element(by.css('div.phone-saying'));
    this.submitQuoteHeading = element(by.css('div.phone-saying > div:nth-of-type(1) > h3'));
    this.sendPhoneHeading = element(by.css('div.phone-saying > div:nth-of-type(3) > h3'));
    this.getPaidHeading = element(by.css('div.phone-saying > div:nth-of-type(5) > h3'));

    this.sellPhoneSubHeading = element(by.css('.faq-text > h3'));

    this.sellPhoneText = element(by.css('.faq-text > div:nth-of-type(4) > p'));

    this.completeQuoteLink = this.aboutText.element(by.css('a'));
    this.quoteLink = element(by.css('.faq-text > div > ul a'));
};
module.exports = AboutPage;
