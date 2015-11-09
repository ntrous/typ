

var QuotePage = function () {

    // Phone Input
    this.phoneInput = element(by.id('search'));
    this.noPhoneHeading = element(by.css('.phoneSelectionOverlay > div > h3[ng-hide="quote.Phones.length>0&&!WaitingForDelivery()&&!RequiresSatchel()"]'));
    this.phonesHeading = element(by.css('.phoneSelectionOverlay > div > h3[ng-if="quote.Phones.length>0&&!WaitingForDelivery()&&!RequiresSatchel()"]'));
    this.phoneSearchList = element(by.css('.phoneSelectionOverlay ul'));
    this.phoneSearchListItems = element.all(by.css('.phoneSelectionOverlay ul > li'));

    // Phone Condition
    this.phoneConditionArea = element(by.css('div[ng-if="search.model!=null"]'));
    this.phoneConditionHeading = element(by.css('div[ng-if="search.model!=null"] > div > h3'));
    this.phoneConditions = element.all(by.repeater('phoneCondition in phoneConditions'));
    this.btnNew = element(by.css('.phoneSelectionOverlay button[ng-model="condition.id"]:nth-of-type(1)'));
    this.btnGood = element(by.css('.phoneSelectionOverlay button[ng-model="condition.id"]:nth-of-type(2)'));
    this.btnFaulty = element(by.css('.phoneSelectionOverlay button[ng-model="condition.id"]:nth-of-type(3)'));
    this.textNew = element(by.css('div[ng-show="condition.id == 1"]'));
    this.textGood = element(by.css('div[ng-show="condition.id == 2"]'));
    this.textFaulty = element(by.css('div[ng-show="condition.id == 3"]'));

    // Sell Button and Price
    this.sellPhoneArea = element(by.css('div[ng-if="phoneOffer!=null"]'));
    this.sellPhoneHeading = element(by.css('div[ng-if="phoneOffer!=null"] > h3.mainheading'));
    this.phonePrice = element(by.css('div[ng-if="phoneOffer!=null"] > h2'));
    this.btnSellPhone = element(by.css('button[ng-click="addPhoneToQuote(search.model.id, condition.id)"]'));

    // Quote Section
    this.quoteTabSection = element(by.id('Quote'));
    this.quoteTabSectionHeading = element(by.css('#Quote>h1'));
    this.quoteTabs = element.all(by.repeater('tab in tabs'));
    //tabs
    this.reviewTabHeading = element(by.css('#Quote .tabset ul li:nth-of-type(1)'));
    this.reviewTabContent = element(by.css('#Quote .tab-content .tab-pane:nth-of-type(1)'));
    this.detailsTabHeading = element(by.css('#Quote .tabset ul li:nth-of-type(2)'));
    this.detailsTabContent = element(by.css('#Quote .tab-content .tab-pane:nth-of-type(2)'));
    this.summaryTabHeading = element(by.css('#Quote .tabset ul li:nth-of-type(3)'));
    this.summaryTabContent = element(by.css('#Quote .tab-content .tab-pane:nth-of-type(3)'));

    // Review Your Quote Section
    this.reviewTable = this.reviewTabContent.element(by.css('table'));
    this.reviewTableRows = element.all(by.css('#Quote .tab-content .tab-pane:nth-of-type(1) table tbody tr'));
    this.phoneInTable = this.reviewTable.element(by.css('tbody tr:nth-of-type(1) > td:nth-of-type(4)'));
    this.conditionInTable = this.reviewTable.element(by.css('tbody tr:nth-of-type(1) > td:nth-of-type(5)'));
   

    this.searchForPhone = function (phoneName) {
       return this.phoneInput.sendKeys(phoneName).then(function () {
        browser.wait(function () {
            return element(by.css('.phoneSelectionOverlay ul')).isDisplayed();
            }, 3000);
        });
    };

    this.selectFirstPhone = function (phoneName) {
        return this.searchForPhone(phoneName).then(function () {
            return element(by.css('.phoneSelectionOverlay ul > li:nth-of-type(1)')).click().then(function () {
                browser.wait(function () {
                    return element(by.css('div[ng-if="search.model!=null"]')).isDisplayed();
                }, 3600);
            });
        });
    };

    this.selectCondition = function (phoneName, condition, id) {
        return this.selectFirstPhone(phoneName).then(function () {
            return element(by.xpath('//button[contains(., "'+ condition +'")]')).click()
                .then(function () {
                browser.wait(function () {
                    return element(by.css('div[ng-show="condition.id == '+ id +'"]')).isDisplayed();
                }, 3000);
            });
        });
    };
    
    this.sellPhone = function (phoneName, condition, conditionId) {
        return this.selectCondition(phoneName, condition, conditionId).then(function () { 
            return element(by.css('button[ng-click="addPhoneToQuote(search.model.id, condition.id)"]')).click()
            .then(function () {
                browser.wait(function () {
                    return element(by.id('Quote')).isDisplayed();
                }, 3000);
            });
        });
    }

    this.goToNextTab = function (tabId) {
        var nextTab = tabId + 1;
        return element(by.css('#Quote .tab-content .tab-pane:nth-of-type('+ tabId +') button[ng-click="goToTab(\'Quote\', ' + tabId + ')"]')).click()
        .then(function () {
            browser.wait(function () {
                return element(by.css('#Quote .tab-content .tab-pane:nth-of-type('+ nextTab +')')).isDisplayed();
            }, 3000);
        });
    }

};
module.exports = QuotePage;