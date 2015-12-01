var BlogPage = require('./BlogPage.js');
var mockModule = require('../../Mocks/mocks.js');
var blogPage;

describe('Blog', function () {

    beforeEach(function () {
       browser.addMockModule('httpBackendMock', mockModule.httpBackendMock);
        browser.driver.manage().deleteAllCookies();
        browser.get('/Blog');
        blogPage = new BlogPage();
        
    });

    describe('when opening blog page', function () {

        beforeEach(function () {
           
        });

        it('should display Heading', function () {
            expect(blogPage.heading.isDisplayed()).toEqual(true);
            expect(blogPage.heading.getText()).toEqual('Trade Your Phone Blog');
        });
    });
});