using System;
using AcceptanceTests.Driver.Support;
using Coypu;
using Coypu.Drivers;
using AcceptanceTests.Model;
using AcceptanceTests.Driver.Settings;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;

namespace AcceptanceTests.Driver
{
    public class DriverManager
    {
        public BrowserSession Init(string baseUrl, string targetBrowser, PlatformSupport platform, ScenarioInfo scenarioInfo,
                                        string buildName, bool blockCameraAndMicTag, SauceLabsSettings saucelabsSettings)
        {
            IDriver driver = null;
            var parsedBrowser = EnumParser.ParseText<BrowserSupport>(targetBrowser);
            saucelabsSettings.SetRemoteUrl(platform);

            switch (platform)
            {
                case PlatformSupport.Desktop:
                    driver = InitDesktopDriver(scenarioInfo, buildName, blockCameraAndMicTag, saucelabsSettings, parsedBrowser);
                    break;
                case PlatformSupport.Mobile:
                    throw new PlatformNotSupportedException($"Platform {platform} is not currently supported");
            }

            return InitDriver(driver, baseUrl, Browser.Parse(parsedBrowser.ToString()));
        }

        public BrowserSession InitDriver(IDriver driver, string baseUrl, Browser targetBrowser)
        {
            var sessionConfiguration = new SessionConfiguration()
            {
                Driver = Type.GetType($"{driver}, Coypu"),
                Browser = targetBrowser,
                AppHost = baseUrl,
                Timeout = TimeSpan.FromSeconds(10),
                RetryInterval = TimeSpan.FromSeconds(1)
            };

            return new BrowserSession(sessionConfiguration, driver);
        }

        private IDriver InitDesktopDriver(ScenarioInfo scenarioInfo, string buildName, bool blockCameraAndMicTag, SauceLabsSettings saucelabsSettings, BrowserSupport parsedBrowser)
        {
            IDriver driver;
            if (saucelabsSettings.RunWithSaucelabs)
            {
                var browserSettings = saucelabsSettings.GetFirstOrDefaultBrowserSettingsBySupportedBrowser(parsedBrowser);
                var capabilities = DesktopDriver.GetDesktopDriverCapabilities(browserSettings, scenarioInfo, buildName, blockCameraAndMicTag);
                driver = GetDesktopRemoteDriver(capabilities, parsedBrowser, saucelabsSettings);
            }
            else
            {

                var options = DesktopDriver.GetDesktopLocalDriverOptions(parsedBrowser, blockCameraAndMicTag);
                driver = GetDesktopLocalDriver(options, parsedBrowser);
            }

            return driver;
        }

        public IDriver GetDesktopLocalDriver(DriverOptions options, BrowserSupport targetBrowser)
        {
            var driver = DesktopDriver.InitDesktopLocalBrowser(targetBrowser, options);
            var seleniumDriver = new NgDriverCoypu(driver, Browser.Parse(targetBrowser.ToString()));
            
            return seleniumDriver;
        }
#pragma warning disable 618
        private IDriver GetDesktopRemoteDriver(DesiredCapabilities capabilities, BrowserSupport targetBrowser, SauceLabsSettings saucelabsSettings)
        {
            var commandTimeout = TimeSpan.FromMinutes(1.5);
            var seleniumDriver = new SauceLabsDriver(Browser.Parse(targetBrowser.ToString()), capabilities, saucelabsSettings.RemoteServerUrl, commandTimeout);
            return seleniumDriver;
        }
#pragma warning restore 618

        private IDriver GetDesktopRemoteDriver(DriverOptions options, BrowserSupport targetBrowser, SauceLabsSettings saucelabsSettings)
        {
            var seleniumDriver = new SauceLabsDriver(Browser.Parse(targetBrowser.ToString()), options, saucelabsSettings.RemoteServerUrl);
            return seleniumDriver;
        }
    }
}
