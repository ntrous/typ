var HomePage = function () {

    this.typHeading = element(by.css('.pageHeading > div.title img'));
    this.navContainer = element(by.id('typ-navigation'));
    this.navItems = element.all(by.css('#typ-navigation > ul > li'));
    this.activeNavItem = element.all(by.css('#typ-navigation > ul > li.active'));
    this.quoteAndPhoneSection = element(by.css('div.quoteSection'));
    this.phoneImageBackground = element(by.css('div.phoneImageBackground'));
    this.phoneSection = this.phoneImageBackground.element(by.css('.phoneSection'));
    this.quoteSection = element(by.id('Quote'));

    this.aboutDivs = element.all(by.css('.row > .info-box > div'));
    this.aboutDivsTitles = element.all(by.css('.row > .info-box > div >h3:nth-of-type(2)'));

    this.stickerImg = element(by.css('.sticker'));

    this.socialIconSection = element(by.css('.home-icons > div.col-md-6 > div.row'));
    this.socialIcons = element.all(by.css('.home-icons > div.col-md-6 > div.row > a'));

    this.googleIcon = element(by.css('a.google'));
    this.facebookIcon = element(by.css('a.facebook'));
    this.twitterIcon = element(by.css('a.twitter'));
    this.yelpIcon = element(by.css('a.yelp'));
    this.phoneIcon = element(by.css('a.phone'));
    this.mailIcon = element(by.css('a.envelope'));

    this.reviewLink = element(by.css('.home-icons h2.love > a'));

};
module.exports = HomePage;