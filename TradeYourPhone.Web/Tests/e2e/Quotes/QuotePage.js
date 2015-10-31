

var QuotePage = function () {
    this.phoneInput = element(by.id('search'));
    this.phoneHeading = element(by.css('.phoneSelectionOverlay > div > h3'));
    this.phoneSearchList = element(by.css('.phoneSelectionOverlay ul'));
    this.phoneSearchListItems = element.all(by.css('.phoneSelectionOverlay ul > li'));

    this.searchforPhone = function (phoneName) {
         this.phoneInput.sendKeys(phoneName);

    browser.wait(function () {
        return (this.phoneSearchList).isDisplayed();
    }, 3000);
    
    //            return browser.isElementPresent(this.phoneSearchList);
    
      
    };
    

};
module.exports = QuotePage;