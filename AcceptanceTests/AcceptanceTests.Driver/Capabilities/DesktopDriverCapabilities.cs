using System;
using System.Collections.Generic;
using AcceptanceTests.Driver.Support;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;

namespace AcceptanceTests.Driver.Capabilities
{
    public static class DesktopDriverCapabilities
    {
        //TODO: Fix this -> invalid argument: cannot parse capability goog:chromeOptions from invalid argument: unrecognised chrome options
        public static DriverOptions GetDesktopDriverAdditionalOptions(DriverOptions driverOptions, string scenarioTitle)
        {
            driverOptions.AddAdditionalCapability("name", scenarioTitle);
            driverOptions.AddAdditionalCapability("build", $"{Environment.GetEnvironmentVariable("Build_DefinitionName")} {Environment.GetEnvironmentVariable("RELEASE_RELEASENAME")}");
            
            return driverOptions;
        }

        internal static DriverOptions GetDriverCapabilities(BrowserSupport targetBrowser, bool blockCameraAndMic)
        {
            DriverOptions driverOptions = null;
            switch (targetBrowser)
            {
                case BrowserSupport.Chrome:
                    driverOptions = GetChromeCapabilities(blockCameraAndMic);
                    break;
                case BrowserSupport.Safari:
                    driverOptions = GetSafariCapabilities();
                    break;
                case BrowserSupport.Edge:
                    driverOptions = GetEdgeCapabilities();
                    break;
                case BrowserSupport.Firefox:
                    driverOptions = GetFirefoxCapabilities(blockCameraAndMic);
                    break;
            }

            return driverOptions;
        }

        public static DriverOptions GetFirefoxCapabilities(bool blockCameraAndMic)
        {
            var firefoxOptions = new FirefoxOptions();
            if (!blockCameraAndMic)
            {
                firefoxOptions.AddArgument("use-fake-ui-for-media-stream");
                firefoxOptions.AddArgument("use-fake-device-for-media-stream");
                firefoxOptions.AddArgument("media.navigator.streams.fake");
            }
            firefoxOptions.PlatformName =  "Windows 10";
            firefoxOptions.BrowserVersion = "latest";
            return firefoxOptions;
        }

        public static DriverOptions GetEdgeCapabilities()
        {
            var edgeOptions = new EdgeOptions();
            edgeOptions.PlatformName = "Windows 10";
            edgeOptions.BrowserVersion = "latest";
            return edgeOptions;
        }

        public static DriverOptions GetChromeCapabilities(bool blockCameraAndMic)
        {
            var chromeOptions = new ChromeOptions();

            if (!blockCameraAndMic)
            {
                var args = new List<string>
                            {
                                "use-fake-ui-for-media-stream",
                                "use-fake-device-for-media-stream",
                                "autoAcceptAlerts"
                            };
                chromeOptions.AddArguments(args);
            }
            chromeOptions.BrowserVersion = "latest";
            chromeOptions.PlatformName = "Windows 10";
            
            return chromeOptions;
        }

        public static DriverOptions GetSafariCapabilities()
        {
            var safariOptions = new SafariOptions();
            //safariOptions.PlatformName = "Mac OSX 10.13";
            //safariOptions.BrowserVersion = "12.0";
            return safariOptions;
        }
    }
}
