
exports.config = {
    seleniumAddress: "http://localhost:4444/wd/hub",
    baseUrl: "http://typtest.azurewebsites.net",
    // ----- What tests to run -----
    specs: [
        "e2e/Quotes/*.js",
        "e2e/Home/*.js",
        "e2e/Contact/*.js",
        "e2e/Support/*.js",
        "e2e/About/*.js",
        "e2e/Blog/*.js"

    ],

    multiCapabilities: [
        {
            "browserName": "chrome"
        }
        //, {
        //    'browserName': 'firefox'
        //}
    ],

    // ----- The test framework -----

    framework: "jasmine",

    // ----- Options to be passed to minijasminenode -----
    jasmineNodeOpts: {
        // onComplete will be called just before the driver quits.
        onComplete: null,
        // If true, display spec names.
        isVerbose: true,
        // If true, print colors to the terminal.
        showColors: true,
        // If true, include stack traces in failures.
        includeStackTrace: true,
        // Default time to wait in ms before a test fails.
        defaultTimeoutInterval: 30000
    }
};