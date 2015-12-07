var QuotePage = function () {

    // Phone Input
    this.phoneInput = element(by.id('search'));
    // For when there are no phones / no quote added
    this.noPhoneHeading = element(by.css('.phoneSelectionOverlay > div > h3[ng-hide="quote.Phones.length>0&&!WaitingForDelivery()&&!RequiresSatchel()"]'));
    // For when there are phones and a quote added
    this.phonesHeading = element(by.css('.phoneSelectionOverlay > div > h3[ng-if="quote.Phones.length>0&&!WaitingForDelivery()&&!RequiresSatchel()"]'));
    this.phoneSearchList = element(by.css('.phoneSelectionOverlay ul'));
    this.phoneSearchListItems = element.all(by.css('.phoneSelectionOverlay ul > li'));


    // Phone Condition
    this.phoneConditionArea = element(by.css('div[ng-if="search.model!=null"]'));
    this.phoneConditionHeading = element(by.css('div[ng-if="search.model!=null"] > div > h3'));
    this.phoneConditions = element.all(by.repeater('phoneCondition in phoneConditions'));
    // Condition Buttons
    this.btnNew = element(by.css('.phoneSelectionOverlay button[ng-model="condition.id"]:nth-of-type(1)'));
    this.btnGood = element(by.css('.phoneSelectionOverlay button[ng-model="condition.id"]:nth-of-type(2)'));
    this.btnFaulty = element(by.css('.phoneSelectionOverlay button[ng-model="condition.id"]:nth-of-type(3)'));
    // Condition Text 
    this.textNew = element(by.css('div[ng-show="condition.id == 1"]'));
    this.textGood = element(by.css('div[ng-show="condition.id == 2"]'));
    this.textFaulty = element(by.css('div[ng-show="condition.id == 3"]'));


    // Sell Button and Price
    this.sellPhoneArea = element(by.css('div[ng-if="phoneOffer!=null"]'));
    this.sellPhoneHeading = element(by.css('div[ng-if="phoneOffer!=null"] > h3.mainheading'));
    this.phonePrice = element(by.css('div[ng-if="phoneOffer!=null"] > h2'));
    this.btnSellPhone = element(by.css('button[ng-click="addPhoneToQuote(search.model.Id, condition.id)"]'));


    // Quote Section
    this.quoteTabSection = element(by.id('Quote'));
    this.quoteTabSectionHeading = element(by.css('#Quote>h1'));
    this.quoteTabs = element.all(by.repeater('tab in tabs'));
    // Tabs
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
    this.deleteBtn = this.reviewTabContent.element(by.css('button[ng-click="deletePhoneFromQuote(q.Id, $index)"]'));


    // Details Section
    // Personal Details
    this.fullNameField = element(by.model('customer.fullname'));
    this.emailField = element(by.model('customer.email'));
    this.mobileField = element(by.model('customer.mobile'));
    // Address Details
    this.streetField = element(by.model('customer.postageStreet'));
    this.suburbField = element(by.model('customer.postageSuburb'));
    this.stateSelect = element(by.model('customer.postageState'));
    this.stateDropDown = element.all(by.css('#state>option'));
    this.postcodeField = element(by.model('customer.postagePostcode'));
    // Payment Details
    this.paymentTypeSelect = element(by.model('customer.paymentType'));
    this.paymentTypeDropDown = element.all(by.css('#paymentType>option'));
    this.bsbField = element(by.model('customer.bsb'));
    this.accountNumField = element(by.model('customer.accountNum'));
    this.paypalEmailField = element(by.model('customer.paypalEmail'));
    this.paypalSameAsPersonalCheck = element(by.model('customer.paypalSameAsPersonal'));
    //Postage Details
    this.freeSatchelBtn = element(by.xpath('//button[contains(., "Free Satchel")]'));
    this.postYourselfBtn = element(by.xpath('//button[contains(., "Post Yourself")]'));
    this.postageText = element(by.css('p.postageOptionsText'));
    // Details Buttons
    this.goToSummary = element(by.css('button[ng-click="SaveQuote(details,customer, quote)"]'));
    this.backToReview = element(by.css('button.back-btn[ng-click="goToTab(\'Quote\', 0)"]'));
    // Errors
    this.errorBlock = element.all(by.css('p.help-block'));


    // Summary Tab 
    this.summarySection = element(by.css('div[ng-hide="WaitingForDelivery()||RequiresSatchel()"]'));
    this.summaryTitle = element(by.css('div[ng-hide="WaitingForDelivery()||RequiresSatchel()"] > h3'));
    // Personal Details
    this.personalDetails = element(by.css('div[ng-hide="WaitingForDelivery()||RequiresSatchel()"]>div:nth-of-type(2)'));
    this.personalDetailsTitle = this.personalDetails.element(by.css('h3'));
    this.summaryName = this.personalDetails.element(by.css('p:nth-of-type(1)'));
    this.summaryEmail = this.personalDetails.element(by.css('p:nth-of-type(2)'));
    this.summaryPhone = this.personalDetails.element(by.css('p:nth-of-type(3)'));
    // Address Details
    this.addressDetails = element(by.css('div[ng-hide="WaitingForDelivery()||RequiresSatchel()"]>div:nth-of-type(3)'));
    this.addressDetailsTitle = this.addressDetails.element(by.css('h3'));
    this.addressLine1 = this.addressDetails.element(by.css('p:nth-of-type(1)'));
    this.addressLine2 = this.addressDetails.element(by.css('p:nth-of-type(2)'));
    this.addressLine3 = this.addressDetails.element(by.css('p:nth-of-type(3)'));
    // Payment Details
    this.paymentDetails = element(by.css('div[ng-hide="WaitingForDelivery()||RequiresSatchel()"]>div:nth-of-type(4)'));
    this.paymentDetailsTitle = this.paymentDetails.element(by.css('h3'));
    this.paidAmount = this.paymentDetails.element(by.css('p:nth-of-type(1)'));
    this.chosenType = this.paymentDetails.element(by.css('p:nth-of-type(2)'));
    this.paymentDetailsEntered = this.paymentDetails.element(by.css('p:nth-of-type(3)'));
    // Postage Details
    this.postageDetails = element(by.css('div[ng-hide="WaitingForDelivery()||RequiresSatchel()"]>div:nth-of-type(6)'));
    this.postageDetailsTitle = this.postageDetails.element(by.css('h3'));
    this.postageInfo = element(by.css('div[ng-hide="WaitingForDelivery()||RequiresSatchel()"]>div:nth-of-type(6)>p:nth-of-type(1)'));
    this.postagePost = this.postageDetails.element(by.css('div[ng-if="DisplayPostage()"]'));
    this.postageSatchel = this.postageDetails.element(by.css('div[ng-if="DisplaySatchel()"]'));
    // Summary Table
    this.phoneSummaryTable = this.summarySection.element(by.css('table'));
    this.phoneSummaryTableRows = this.phoneSummaryTable.all(by.css('tbody > tr'));
    // T&C, clear iCloud Warning
    this.cleariCloudCheck = this.summarySection.element(by.css('div[ng-show="DoesQuoteContainApple()"]'));
    this.agreeTCLabel = this.summarySection.element(by.css('p.tc'));
    // Summary Buttons
    this.summarySubmit = element(by.css('button[ng-click="FinaliseQuote(customer, quote)"]'));
    this.backToDetailsBtn = element(by.css('button.back-btn[ng-click="goToTab(\'Quote\', 1)"]'));


    // Submitted Summary Section
    // Quote Number Text
    this.submittedSummarySection = element(by.css('div[ng-show="WaitingForDelivery()||RequiresSatchel()"]'));
    this.submittedQuoteNumberTitle = element(by.css('div[ng-show="WaitingForDelivery()||RequiresSatchel()"]>h3:nth-of-type(1)'));
    // What Happens Next
    this.submittedWhatHappensNextTitle = element(by.css('div[ng-show="WaitingForDelivery()||RequiresSatchel()"]>h3:nth-of-type(2)'));
    this.submittedWhatHappensNextText = element(by.css('div[ng-show="WaitingForDelivery()||RequiresSatchel()"]>p:nth-of-type(1)'));
    // Postage Details
    this.submittedPostageTitle = element(by.css('div[ng-show="WaitingForDelivery()||RequiresSatchel()"]>h3:nth-of-type(3)'));
    this.submittedWaitingDelivery = element(by.css('div[ng-show="WaitingForDelivery()"]'));
    this.submittedRequiresSatchel = element(by.css('div[ng-show="RequiresSatchel()"]'));
    // Reset Quote Button
    this.startAnotherQuote = element(by.css('button[ng-click="ResetQuote()"]'));


  

    // FUNCTION NAME: searchForPhone
    // PARAMETERS: phoneName: string - phone name you want to search
    // DESCRIPTION: Searches for the phone you pass in and waits till the list is displayed before returning
    this.searchForPhone = function (phoneName) {
       return this.phoneInput.sendKeys(phoneName).then(function () {
        browser.wait(function () {
            return element(by.css('.phoneSelectionOverlay ul')).isDisplayed();
            }, 3000);
        });
    };

    // FUNCTION NAME: selectFirstPhone
    // PARAMETERS: phoneName: string - phone name you want to search
    // DESCRIPTION: searches for the phone you pass in and selects the first item in the dropdown displayed and waits till the condition area is displayed before returning
    this.selectFirstPhone = function (phoneName) {
        return this.searchForPhone(phoneName).then(function () {
            return element(by.css('.phoneSelectionOverlay ul > li:nth-of-type(1)')).click().then(function () {
                browser.wait(function () {
                    return element(by.css('div[ng-if="search.model!=null"]')).isDisplayed();
                }, 3600);
            });
        });
    };

    // FUNCTION NAME: selectCondition
    // PARAMETERS:  phoneName: string - phone name you want to search,
    //              condition: string - condition name you want selected
    //              conditionId: int - condition id you want selected
    // DESCRIPTION: searches and selects the phone you pass in, selects the passed in condition and waits till the correct condition text is displayed before returning
    this.selectCondition = function (phoneName, condition, conditionId) {
        return this.selectFirstPhone(phoneName).then(function () {
            return element(by.xpath('//button[contains(., "'+ condition +'")]')).click()
                .then(function () {
                browser.wait(function () {
                    return element(by.css('div[ng-show="condition.id == ' + conditionId + '"]')).isDisplayed();
                }, 3000);
            });
        });
    };
    
    // FUNCTION NAME: sellPhone
    // PARAMETERS:  phoneName: string - phone name you want to search,
    //              condition: string - condition name you want selected
    //              conditionId: int - condition id you want selected
    // DESCRIPTION: searches and selects the phone you pass in, selects the passed in condition and then clicks the 'Sell This Phone' button and waits till the Quote Section is displayed before returning
    this.sellPhone = function (phoneName, condition, conditionId) {
        return this.selectCondition(phoneName, condition, conditionId).then(function () { 
            return element(by.css('button[ng-click="addPhoneToQuote(search.model.Id, condition.id)"]')).click()
            .then(function () {
                browser.wait(function () {
                    return element(by.id('Quote')).isDisplayed();
                }, 3000);
            });
        });
    }

    // FUNCTION NAME: goToNextTab
    // PARAMETERS: tabId: int - current tab number
    // DESCRIPTION: clicks the 'Next' button and waits till the tab is shown before returning
    this.goToNextTab = function (tabId) {
        var nextTab = tabId + 1;
        return element(by.css('#Quote .tab-content .tab-pane:nth-of-type('+ tabId +') button[ng-click="goToTab(\'Quote\', ' + tabId + ')"]')).click()
        .then(function () {
            browser.wait(function () {
                return element(by.css('#Quote .tab-content .tab-pane:nth-of-type('+ nextTab +')')).isDisplayed();
            }, 3000);
        });
    }

    // FUNCTION NAME: deletePhoneFromReview
    // PARAMETERS: row: int - row index you want to delete
    // DESCRIPTION: clicks the delete button from the requested row index from the Review Table
    this.deletePhoneFromReview = function (row) {
        return this.reviewTable.element(by.css('tbody tr:nth-of-type(' + row + ') button[ng-click="deletePhoneFromQuote(q.Id, $index)"]')).click();
    }

    // FUNCTION NAME: selectPaymentType
    // PARAMETERS: type: string - payment type to select
    // DESCRIPTION: clicks the Payment Type dropdown and selects the type requested
    this.selectPaymentType = function (type) {
        return this.paymentTypeSelect.click().then(function() {
            return element(by.css('#paymentType > option[label="' + type + '"]')).click();
        });
    }

    // FUNCTION NAME: selectState
    // PARAMETERS: state: string - state to select
    // DESCRIPTION: clicks the State dropdown and selects the state requested
    this.selectState = function (state) {
        return this.stateSelect.click().then(function() {
            return element(by.css('#state > option[label="' + state + '"]')).click();
        });
    }

    // FUNCTION NAME: enterDetails
    // PARAMETERS:  name: string - name to enter
    //              email: string - email to enter
    //              mobile: string - mobile to enter
    //              street: string - steet address to enter
    //              suburb: string - suburb to enter
    //              state: string - state to select
    //              postcode: string - postcode to enter
    //              paymentType: string - payment type to select
    //              bsb: string - bsb to enter ? if none type 'none'
    //              accountNo: string - account number to enter ? if none type 'none'
    //              paypalEmail: string - paypal email to enter ? if none type 'none'
    //              postageType: string - postage type to enter
    //              errors: boolean - if there are expected errors or not
    // DESCRIPTION: enters all the details and depending on what payment type is chosen the relevent fields will be populated, then depending on if errors are expected or not it will either return upon submit or wait till the next tab is displayed
    this.enterDetails = function (name, email, mobile, street, suburb, state, postcode, paymentType, bsb, accountno, paypalEmail, postageType, errors) {
        this.fullNameField.sendKeys(name);
        this.emailField.sendKeys(email);
        this.mobileField.sendKeys(mobile);
        this.streetField.sendKeys(street);
        this.suburbField.sendKeys(suburb);
        this.selectState(state);
        this.postcodeField.sendKeys(postcode);
        this.selectPaymentType(paymentType).then(function () {
            if (paymentType == 'Bank Transfer') {
                element(by.model('customer.bsb')).sendKeys(bsb);
                element(by.model('customer.accountNum')).sendKeys(accountno);

            } else if (paymentType == 'Paypal') {
                element(by.model('customer.paypalEmail')).sendKeys(paypalEmail);
            }
        }).then(function () {
            return element(by.xpath('//button[contains(., "' + postageType + '")]')).click().then(function () {
                if (errors) {
                    return element(by.css('button[ng-click="SaveQuote(details,customer, quote)"]')).click();
                } else {
                    return element(by.css('button[ng-click="SaveQuote(details,customer, quote)"]')).click()
                   .then(function () {
                       browser.wait(function () {
                           return element(by.css('#Quote .tab-content .tab-pane:nth-of-type(3)')).isDisplayed();
                       }, 6000);
                   });
                }
            });
        });
    }

    // FUNCTION NAME: getSummaryTableCellText
    // PARAMETERS:  type: string - expects either a 'head' or 'body' - if you want the cell from the thead or the tbody
    //              row: int - what row you want to select the cell from
    //              column: int - what column you want to select from
    // DESCRIPTION: returns the cell text requested from the Summary Table in the Summary Tab
    this.getSummaryTableCellText = function (type, row, column) {
        if (type == 'head') {
            return this.summarySection.element(by.css('table > thead > tr > th:nth-of-type(' + column + ')')).getText();
        } else if (type == 'body') {
            return this.summarySection.element(by.css('table > tbody > tr:nth-of-type(' + row + ') td:nth-of-type(' + column + ')')).getText();
        }
        else {
            return;
        }
    }

};
module.exports = QuotePage;