using System;
using AcceptanceTests.Driver.Support;
using Coypu;
using Coypu.Drivers;
using AcceptanceTests.Model;
using AcceptanceTests.Driver.Settings;
using OpenQA.Selenium;

namespace AcceptanceTests.Driver
{
    public class DriverManager
    {
        public BrowserSession Init(string baseUrl, string targetBrowser, PlatformSupport platform, string scenarioTitle,
                                        bool blockCameraAndMicTag, SauceLabsSettings saucelabsSettings)
        {
            IDriver driver = null;
            var parsedBrowser = EnumParser.ParseText<BrowserSupport>(targetBrowser);
            saucelabsSettings.SetRemoteUrl(platform);

            switch (platform)
            {
                case PlatformSupport.Desktop:
                    var options = DesktopDriver.GetDesktopDriverOptions(parsedBrowser, scenarioTitle, blockCameraAndMicTag);
                    driver = GetDesktopDriver(options, parsedBrowser, saucelabsSettings);
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

        public IDriver GetDesktopDriver(DriverOptions options, BrowserSupport parsedBrowser, SauceLabsSettings saucelabsSettings)
        {
            IDriver seleniumDriver;
            var browser = Browser.Parse(parsedBrowser.ToString());

            if (saucelabsSettings.RunWithSaucelabs)
            {
                seleniumDriver = InitSauceLabsDriver(browser, options, saucelabsSettings.RemoteServerUrl);
            } else
            {

                var driver = DesktopDriver.InitDesktopBrowser(parsedBrowser, options);
                seleniumDriver = new NgDriverCoypu(driver, browser);
            }
            return seleniumDriver;
        }

        public IDriver InitSauceLabsDriver(Browser browser, DriverOptions options, string remoteUrl)
        {
            return new SauceLabsDriver(browser, options, remoteUrl); 
        }
    }
}
