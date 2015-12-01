var SupportPage = function () {

    this.heading = element(by.css('h1'));

    this.supportText = element(by.css('div.faq-text > p'));
    this.contactLink = this.supportText.element(by.css('a'));

    this.supportSections = element.all(by.css('div.faq-text > div'));

    this.generalSection = element(by.css('#GeneralFAQ'));
    this.generalSectionHeading = this.generalSection.element(by.css('h3'));
    this.generalSectionPanels = element.all(by.css('#GeneralFAQ accordion .panel'));

    this.handsetSection = element(by.css('#HandsetFAQ'));
    this.handsetSectionHeading = this.handsetSection.element(by.css('h3'));
    this.handsetSectionPanels = element.all(by.css('#HandsetFAQ accordion .panel'));

    this.postageSection = element(by.css('#PostageFAQ'));
    this.postageSectionHeading = this.postageSection.element(by.css('h3'));
    this.postageSectionPanels = element.all(by.css('#PostageFAQ accordion .panel'));


    // FUNCTION NAME: openPanel
    // PARAMETERS:  section: string - section you want the panle from (choose from 3)
    //              panelNum: int - panel number you want             
    // DESCRIPTION: opens the panel requested and waits till its open before returning
    this.openPanel = function (section, panelNum) {     
        return element(by.css('#' + section + ' accordion > div > div:nth-of-type(' + panelNum + ') > .panel-heading a')).click()
            .then(function () {
            browser.wait(function () {
                return element(by.css('#' + section + ' accordion > div > div:nth-of-type(' + panelNum + ') .panel-body')).isDisplayed();
            }, 6000);
        });
    }

    // FUNCTION NAME: isOpen
    // PARAMETERS:  sectionNum: int - section you want the panle from (choose from 3)
    //              panelNum: int - panel number you want             
    // DESCRIPTION: closes the panel requested
    this.closePanel = function (section, panelNum) {
        return element(by.css('#' + section + ' accordion > div > div:nth-of-type(' + panelNum + ') > .panel-heading a')).click();
    }

    // FUNCTION NAME: getPanel
    // PARAMETERS:  sectionNum: int - section you want the panle from (choose from 3)
    //              panelNum: int - panel number you want             
    // DESCRIPTION: returns if the panel is open
    this.getPanel = function (section, panelNum) {
        return element(by.css('#' + section + ' accordion > div > div:nth-of-type(' + panelNum + ')'));
    }

};
module.exports = SupportPage;
