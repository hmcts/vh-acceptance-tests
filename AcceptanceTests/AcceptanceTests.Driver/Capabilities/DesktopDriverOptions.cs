using System.Collections.Generic;
using AcceptanceTests.Driver.Settings;
using AcceptanceTests.Driver.Support;
using AcceptanceTests.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using TechTalk.SpecFlow;

namespace AcceptanceTests.Driver.Capabilities
{
    public static class DesktopDriverOptions
    {
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

        public static DriverOptions GetRemoteDesktopOptions(BrowserSettings browserSettings, ScenarioInfo scenarioInfo, string buildName, bool blockCameraAndMic)
        {
            var targetBrowser = EnumParser.ParseText<BrowserSupport>(browserSettings.BrowserName);
            DriverOptions driverOptions = GetLocalDriverOptions(targetBrowser, blockCameraAndMic);
            driverOptions.PlatformName = browserSettings.Platform;
            driverOptions.BrowserVersion = browserSettings.Version;

            var sauceOptions = new Dictionary<string, object>();
            sauceOptions.Add("build", buildName);
            sauceOptions.Add("name", scenarioInfo.Title);
            sauceOptions.Add("tags", scenarioInfo.Tags);
            driverOptions.AddAdditionalCapability("sauce:options", sauceOptions);

            return driverOptions;
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
