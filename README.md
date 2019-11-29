# vh-acceptance-tests

This project is a common acceptance test framework with specflow integration for Admin Website, Service Website and Video Website. Note that Video Website testing might require multiple browser instances at once and that's not supported yet.

## What is it?
It's a set on individual packages to support acceptance test automation using Selenium Webdriver for Video Hearings applications.

## What can I do with it?
Use individual packages to integrate into other test projects. Run tests on desktop Safari, Chrome, Firefox and Edge. 

## How do I select which application and browser to test on?
In the project root folder, create a file appsettings.json with the target application and browser as follows:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Saucelabs": {
    "Username": "",
    "AccessKey": "",
    "MobileAccessKey": ""
  },
  "TargetApp": "AdminWebsite", // choose from AdminWebsite or ServiceWebsite
  "TargetBrowser": "Chrome" // choose from Safari, Edge, Firefox or Chrome
}
```

## Where can I see what are the browser configurations?
Browser configurations can be found in the application specific folders under resources/[targetApp] as per example below:
AcceptanceTests.Tests/resources/adminwebsite/adminwebsite.appsettings.json

And their content:
```json
{
  "WebsiteUrl": "https://localhost:5400/",
  "VhBrowserSettings": [
    {
      "BrowserName": "Chrome",
      "Platform": "Windows 10",
      "Version": "78.0"
    },
    {
      "BrowserName": "Firefox",
      "Platform": "Windows 10",
      "Version": "latest"
    },
    {
      "BrowserName": "Safari",
      "Platform": "macOS 10.14",
      "Version": "12.0"
    },
    {
      "BrowserName": "Edge",
      "Platform": "Windows 10",
      "Version": "18.17763"
    }
  ]
}
```
## How do I know I'm pointing to the correct test environment?
Under the same file you find the browser settings you can see WebsiteUrl, e.g. AcceptanceTests.Tests/resources/adminwebsite/adminwebsite.appsettings.json

## What is not supported?
Mobile devices are not yet supported, although the feature is implemented this feature is untested. Safari is also not working at the moment and needs further investigation. This framework also doesn't support testing that might require multiple browser instances at once.

## What do I need to run these locally?
Checkout the projects and ensure you have one of the supported and tested browsers installed: Chrome, Firefox or Edge.

## What do I need to run these in Saucelabs?
You'll need to update the [appsettings.json](https://github.com/hmcts/vh-acceptance-tests/blob/feature/initial-project-structure/AcceptanceTests/AcceptanceTests.Tests/appsettings.json) with "Username" and "AccessKey" values for your saucelabs account:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Saucelabs": {
    "Username": "",
    "AccessKey": "",
    "MobileAccessKey": ""
  },
  "TargetApp": "AdminWebsite", // choose from AdminWebsite or ServiceWebsite
  "TargetBrowser": "Edge" // choose from Safari, Edge, Firefox or Chrome
}
```
## Where do I see the test results?
You need to login to your saucelabs account and check for your tests on the dashboard. To make it easier to filter, you can pass in Scenario tags using specflow feature files:
```gherkin
@smoketest
@adminWebsite
@signIn
@vhOfficer
Scenario Outline: VH Officer sign in to the Admin Website then see two panels
    Given I am registered as 'VH Officer' in the Video Hearings Azure AD
    When I sign in to the 'Admin Website' using my account details 
    Then the '<panel_title>' panel is displayed 
    Examples:
    |panel_title|
    |Book a video hearing|
    |Questionnaire results|
```
## How do I consume these packages?
You can create a new test project using the same structure from https://github.com/hmcts/vh-admin-web/tree/feature/nuget-integration/AdminWebsite/AdminWebsite.AcceptanceTests.NuGet, adding AcceptanceTests.Configuration, Driver, Model and PageObject NuGet Packages pointing at our internal NuGet packages manager or use the AcceptanceTests.Test package which already has the specflow and driver support needed. Ensure your project structure makes use of all Hooks and mimics the folder structure for Features, Steps and Resources.

## How do I write an automated test?
If page objects need to be created, you should create them in the AcceptanceTests.PageObject NuGet package. It's important to keep the same structure so the code remains clean and scalable.

## How do I release an automated test?
You need to ensure that all tests are passing locally. It there are framework changes, that need to go through a PR approval and vh-acceptance-tests pipeline checks. New functionality should be acompanied by unit/integration tests.

## Past Challenges
- Platform incompatibility in SauceLabs
- Lack of support for Specflow on Mac
- Windows slow tests
- Safari 12 protocol updates requiring handle to be passed
- Mismatch Edge vs. MicrosoftEdge browser name in SauceLabs
- Ability to fetch elements on the page using different locators - still happening on Safari

## Resources
### Saucelabs
https://support.saucelabs.com/hc/en-us/community/topics/115000100487-Automated-Browser-Testing-
https://support.saucelabs.com/hc/en-us/sections/205416527-Browser-Tips-and-Troubleshooting
