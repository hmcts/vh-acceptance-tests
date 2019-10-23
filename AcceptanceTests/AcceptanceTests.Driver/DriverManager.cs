using System;
using AcceptanceTests.Driver.Support;
using Coypu;
using Coypu.Drivers;
using AcceptanceTests.Model;
using AcceptanceTests.Driver.Drivers;
using AcceptanceTests.Driver.Settings;
using OpenQA.Selenium;
using Coypu.Drivers.Selenium;


namespace AcceptanceTests.Driver
{
    public class DriverManager
    {
        public BrowserSession Init(string baseUrl, string targetBrowser, PlatformSupport platform, DeviceSupport device, string scenarioTitle,
                                        bool blockCameraAndMicTag, SauceLabsSettings saucelabsSettings)
        {
            IWebDriver webdriver = null;
            ICapabilities capabilities = null;
            var parsedBrowser = EnumParser.ParseText<BrowserSupport>(targetBrowser);
            saucelabsSettings.SetRemoteUrl(platform);

            switch (platform)
            {
                case PlatformSupport.Desktop:
                    capabilities = DesktopDriver.GetDesktopDriverCapabilities(parsedBrowser, scenarioTitle, blockCameraAndMicTag);
                    var options = DesktopDriver.GetDesktopDriverOptions(parsedBrowser, blockCameraAndMicTag);
                    webdriver = DesktopDriver.InitDesktopLocalBrowser(parsedBrowser, options);
                    break;
                case PlatformSupport.Mobile:
                    capabilities = MobileDriver.GetMobileDriverCapabilities(parsedBrowser, device, scenarioTitle);
                    break;
            }

            var browser = Browser.Parse(parsedBrowser.ToString());
            var driver = GetDriver(webdriver, capabilities, browser, saucelabsSettings);
            return InitDriver(driver, baseUrl, browser);
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

        public IDriver GetDriver(IWebDriver driver, ICapabilities capabilities, Browser browser, SauceLabsSettings saucelabsSettings)
        {
            return saucelabsSettings.RunWithSaucelabs ? InitSauceLabsDriver(browser, capabilities, saucelabsSettings.RemoteServerUrl) : new NgDriverCoypu(driver, browser);
        }

        public IDriver InitSauceLabsDriver(Browser browser, ICapabilities capabilities, string remoteUrl)
        {
            return new SauceLabsDriver(browser, capabilities, remoteUrl); 
        }
    }
}
