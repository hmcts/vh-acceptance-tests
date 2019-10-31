using System;
using System.Collections.Generic;
using AcceptanceTests.Driver.Settings;
using AcceptanceTests.Driver.Support;
using AcceptanceTests.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using TechTalk.SpecFlow;

namespace AcceptanceTests.Driver.Capabilities
{
    public static class DesktopDriverOptions
    {
        //TODO: Fix this -> invalid argument: cannot parse capability goog:chromeOptions from invalid argument: unrecognised chrome options
        public static DriverOptions GetDesktopDriverAdditionalOptions(DriverOptions driverOptions, string scenarioTitle)
        {
            driverOptions.AddAdditionalCapability("name", scenarioTitle);
            driverOptions.AddAdditionalCapability("build", $"{Environment.GetEnvironmentVariable("Build_DefinitionName")} {Environment.GetEnvironmentVariable("RELEASE_RELEASENAME")}");
            
            return driverOptions;
        }

        internal static DriverOptions GetLocalDriverOptions(BrowserSupport targetBrowser, bool blockCameraAndMic)
        {
            DriverOptions driverOptions = null;
            switch (targetBrowser)
            {
                case BrowserSupport.Chrome:
                    driverOptions = GetChromeOptions(blockCameraAndMic);
                    break;
                case BrowserSupport.Safari:
                    driverOptions = GetSafariOptions();
                    break;
                case BrowserSupport.Edge:
                    driverOptions = GetEdgeOptions();
                    break;
                case BrowserSupport.Firefox:
                    driverOptions = GetFirefoxOptions(blockCameraAndMic);
                    break;
            }

            return driverOptions;
        }

        public static DriverOptions GetRemoteDesktopOptions(BrowserSettings browserSettings, ScenarioInfo scenarioInfo, bool blockCameraAndMic)
        {
            var targetBrowser = EnumParser.ParseText<BrowserSupport>(browserSettings.BrowserName);
            DriverOptions driverOptions = GetLocalDriverOptions(targetBrowser, blockCameraAndMic);
            driverOptions.PlatformName = browserSettings.Platform;
            driverOptions.BrowserVersion = browserSettings.Version;
            //driverOptions.AddAdditionalCapability("name", scenarioInfo.Title);
            //driverOptions.AddAdditionalCapability("tags", scenarioInfo.Tags);
            //driverOptions.AddAdditionalCapability("build", GetBuildName());

            //var sauceOptions = new Dictionary<string, object>();
            //driverOptions.AddAdditionalCapability("sauce:options", sauceOptions);

            return driverOptions;
        }

        public static DriverOptions GetRemoteDesktopCapabilities(BrowserSettings browserSettings, ScenarioInfo scenarioInfo, bool blockCameraAndMic)
        {
            var targetBrowser = EnumParser.ParseText<BrowserSupport>(browserSettings.BrowserName);
            DriverOptions driverOptions = GetLocalDriverOptions(targetBrowser, blockCameraAndMic);
            driverOptions.PlatformName = browserSettings.Platform;
            driverOptions.BrowserVersion = browserSettings.Version;
            //driverOptions.AddAdditionalCapability("name", scenarioInfo.Title);
            //driverOptions.AddAdditionalCapability("tags", scenarioInfo.Tags);
            //driverOptions.AddAdditionalCapability("build", GetBuildName());

            //var sauceOptions = new Dictionary<string, object>();
            //driverOptions.AddAdditionalCapability("sauce:options", sauceOptions);

            return driverOptions;
        }
#pragma warning disable 618
        public static DesiredCapabilities GetDesktopCapabilities(BrowserSettings browserSettings, ScenarioInfo scenarioInfo, bool blockCameraAndMic)
        {
            var targetBrowser = EnumParser.ParseText<BrowserSupport>(browserSettings.BrowserName);
            DriverOptions driverOptions = GetLocalDriverOptions(targetBrowser, blockCameraAndMic);

            switch (targetBrowser)
            {
                case BrowserSupport.Safari:
                    driverOptions.PlatformName = browserSettings.Platform;
                    break;
            }


            var capabilities = new DesiredCapabilities();

            try
            {
                capabilities = (DesiredCapabilities)driverOptions.ToCapabilities();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Could not convert capabilities: {ex.Message}");
            }
            capabilities.AcceptInsecureCerts = true;
            capabilities.SetCapability("browserName", driverOptions.BrowserName);
            capabilities.SetCapability("version", browserSettings.Version);
            capabilities.SetCapability("platform", browserSettings.Platform);
            capabilities.SetCapability("name", scenarioInfo.Title);
            capabilities.SetCapability("tags", scenarioInfo.Tags);
            capabilities.SetCapability("build", GetBuildName());

            return capabilities;

        }
#pragma warning restore 618

        private static object GetBuildName()
        {
            var buildName = $"{ Environment.GetEnvironmentVariable("Build_DefinitionName") } { Environment.GetEnvironmentVariable("RELEASE_RELEASENAME")}";
            if (string.IsNullOrEmpty(buildName))
                buildName = "Acceptance Tests";

            return buildName;
        }

        public static DriverOptions GetFirefoxOptions(bool blockCameraAndMic)
        {
            var firefoxOptions = new FirefoxOptions();
            if (!blockCameraAndMic)
            {
                var args = new List<string>
                            {
                                "use-fake-ui-for-media-stream",
                                "use-fake-device-for-media-stream",
                                "media.navigator.streams.fake",
                                "autoAcceptAlerts"
                            };
                firefoxOptions.AddArguments(args);
            }
            firefoxOptions.AcceptInsecureCertificates = true;
            return firefoxOptions;
        }

        public static DriverOptions GetEdgeOptions()
        {
            var edgeOptions = new EdgeOptions();
            return edgeOptions;
        }

        public static DriverOptions GetChromeOptions(bool blockCameraAndMic)
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
            chromeOptions.AcceptInsecureCertificates = true;
            return chromeOptions;
        }

        public static DriverOptions GetSafariOptions()
        {
            var safariOptions = new SafariOptions();
            return safariOptions;
        }
    }
}
