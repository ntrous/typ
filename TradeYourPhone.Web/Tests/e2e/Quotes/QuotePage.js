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
    this.quoteSection = element(by.id('Quote'));
    this.quoteSectionHeading = element(by.css('#Quote h2.quoteHeading'));
   
    // Phones To Sell
    this.phonesToSellSection = element(by.id('phonesToSell'));
    this.phonesToSellHeading = this.phonesToSellSection.element(by.css('h3'));
    this.reviewTable = this.phonesToSellSection.element(by.css('table'));
    this.reviewTableRows = element.all(by.css('#phonesToSell table tbody tr'));
    this.phoneInTable = this.reviewTable.element(by.css('tbody tr:nth-of-type(1) > td:nth-of-type(4)'));
    this.conditionInTable = this.reviewTable.element(by.css('tbody tr:nth-of-type(1) > td:nth-of-type(5)'));
    this.deleteBtn = this.phonesToSellSection.element(by.css('button[ng-click="deletePhoneFromQuote(q.Id, $index)"]'));


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
    
    // Errors
    this.errorBlock = element.all(by.css('p.help-block'));


    // Summary Section 
    this.summarySection = element(by.id('quoteSummary'));
    this.summaryTitle = this.summarySection.element(by.css('.phoneHeading > h3'));

    this.addAnotherPhone = this.summarySection.element(by.css('h3[ng-click="GoToAddPhone()"]'));

    this.bankSummary = this.summarySection.element(by.css('div[ng-if="DisplayBankTransfer()"]'));
    this.bankSummaryHeading = this.bankSummary.element(by.css('h3'));
    this.bankSummaryText = this.bankSummary.element(by.css('p'));

    this.paypalSummary = this.summarySection.element(by.css('div[ng-if="DisplayPaypal()"]'));
    this.paypalSummaryHeading = this.paypalSummary.element(by.css('h3'));
    this.paypalSummaryText = this.paypalSummary.element(by.css('p'));

    this.appleWarning = this.summarySection.element(by.css('div[ng-if="DoesQuoteContainApple()"]'));
    this.appleWarningText = this.appleWarning.element(by.css('h3'));

    this.postageSummary = element(by.id('postageSummary'));
    this.postageSummaryHeading = this.postageSummary.element(by.css('h3'));
    this.postageSummaryText = this.postageSummary.element(by.css('p'));

    this.agreeTCLabel = this.summarySection.element(by.css('p.tc'));
    
    //Submit button
    this.submitBtn = this.quoteSection.element(by.css('button[ng-click="FinaliseQuote(details, customer, quote)"]'));


    // Submitted Summary Section
    this.quoteSubmittedSection = element(by.id('QuoteSubmitted'));
    this.submittedHeading = this.quoteSubmittedSection.element(by.css('h2.quoteHeading'));
    // Quote Number Text
    this.quoteNumHeading = this.quoteSubmittedSection.element(by.css('#SubmittedSection h3.centre'));
    // What Happens Next
    this.whatHappensNextTitle = this.quoteSubmittedSection.element(by.css('#SubmittedSection > div:nth-of-type(2) > h3'));
    this.whatHappensNextText = this.quoteSubmittedSection.element(by.css('#SubmittedSection > p:nth-of-type(1)'));
    // Postage Details
    this.postageInfoHeading = this.quoteSubmittedSection.element(by.css('#SubmittedSection > div:nth-of-type(3) > h3'));
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
                    return element(by.css('button[ng-click="FinaliseQuote(details, customer, quote)"]')).click();
                } else {
                    return element(by.css('button[ng-click="FinaliseQuote(details, customer, quote)"]')).click()
                   .then(function () {
                       browser.wait(function () {
                           return element(by.css('#QuoteSubmitted')).isDisplayed();
                       }, 6000);
                   });
                }
            });
        });
    }

};
module.exports = QuotePage;