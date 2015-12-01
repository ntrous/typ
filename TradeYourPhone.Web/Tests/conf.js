
exports.config = {
    seleniumAddress: 'http://localhost:4444/wd/hub',
    baseUrl: 'http://localhost:53130',
    // ----- What tests to run -----
    specs: [
        //'e2e/*/*.js'
        'e2e/*/Blog.js'
    ],

    multiCapabilities: [
        {
            'browserName': 'chrome'
        }
        //, {
        //    'browserName': 'firefox'
        //}
    ],

    // ----- The test framework -----

    framework: 'jasmine',

    // ----- Options to be passed to minijasminenode -----
    jasmineNodeOpts: {
        // onComplete will be called just before the driver quits.
        onComplete: null,
        // If true, display spec names.
        isVerbose: false,
        // If true, print colors to the terminal.
        showColors: true,
        // If true, include stack traces in failures.
        includeStackTrace: true,
        // Default time to wait in ms before a test fails.
        defaultTimeoutInterval: 30000
    }
};