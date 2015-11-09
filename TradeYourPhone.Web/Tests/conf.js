
exports.config = {
    seleniumAddress: 'http://localhost:4444/wd/hub',
    // ----- What tests to run -----
    specs: [
        'e2e/*/*.js',
    ],

    multiCapabilities: [
        {
            'browserName': 'chrome'
        }
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