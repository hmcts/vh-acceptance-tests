using System.Collections.Generic;
using AcceptanceTests.Driver.Settings;
using AcceptanceTests.Driver.Support;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;

namespace AcceptanceTests.Driver.Capabilities
{
    public static class DesktopDriverCapabilities
    {
        //TODO: Fix this -> invalid argument: cannot parse capability goog:chromeOptions from invalid argument: unrecognised chrome options
        public static DesiredCapabilities GetDesktopDriverAdditionalCapabilities(DesiredCapabilities desiredCapabilities,
                                                                                    ScenarioInfo scenarioTitle, string buildName)
        {
            desiredCapabilities.SetCapability("name", scenarioTitle);
            desiredCapabilities.SetCapability("build", buildName);

            var sauceOptions = new Dictionary<string, object>();
            desiredCapabilities.SetCapability("sauce:options", sauceOptions);

            return desiredCapabilities;
        }

        public static DesiredCapabilities GetRemoteDesktopCapabilities(BrowserSupport targetBrowser, BrowserSettings browserSettings,
                                                                            ScenarioInfo scenarioInfo, string buildName, bool blockCameraAndMic)
        {
            var desiredCapabilities = new DesiredCapabilities();
            switch (targetBrowser)
            {
                case BrowserSupport.Chrome:
                    desiredCapabilities = GetChromeCapabilities(blockCameraAndMic);
                    break;
                case BrowserSupport.Safari:
                    desiredCapabilities = GetSafariCapabilities();
                    break;
                case BrowserSupport.Edge:
                    desiredCapabilities = GetEdgeCapabilities();
                    break;
                case BrowserSupport.Firefox:
                    desiredCapabilities = GetFirefoxCapabilities(blockCameraAndMic);
                    break;
            }

            desiredCapabilities.SetCapability("seleniumVersion", "3.11.0");
            desiredCapabilities.SetCapability("platformName", browserSettings.Platform);
            desiredCapabilities.SetCapability("browserName", browserSettings.BrowserName);
            desiredCapabilities.SetCapability("browserVersion", browserSettings.Version);
            desiredCapabilities.SetCapability("platform", browserSettings.Platform);
            desiredCapabilities.SetCapability("version", browserSettings.Version);

            var sauceOptions = new Dictionary<string, object>();
            sauceOptions.Add("build", buildName);
            sauceOptions.Add("name", scenarioInfo.Title);
            sauceOptions.Add("tags", scenarioInfo.Tags);
            desiredCapabilities.SetCapability("sauce:options", sauceOptions);

            return desiredCapabilities;
        }

        public static DesiredCapabilities GetFirefoxCapabilities(bool blockCameraAndMic)
        {
            var capabilities = new DesiredCapabilities();
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
                capabilities.SetCapability("args", firefoxOptions);
            }

            return capabilities;

        }

        public static DesiredCapabilities GetEdgeCapabilities()
        {
            var capabilities = new DesiredCapabilities();
            return capabilities;
        }

        public static DesiredCapabilities GetChromeCapabilities(bool blockCameraAndMic)
        {
            var capabilities = new DesiredCapabilities();
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
                capabilities.SetCapability("args", args);
            }
            
            return capabilities;
        }

        public static DesiredCapabilities GetSafariCapabilities()
        {
            var capabilities = new DesiredCapabilities();
            return capabilities;
        }
    }
}
