using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace AcceptanceTests.Common.Driver.Strategies.Desktop.Mac
{
    internal class FirefoxMacDriverStrategy : Drivers
    {
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
            var browserOptions = new FirefoxOptions { PlatformName = MacPlatform, BrowserVersion =BrowserVersions.FirefoxMac, AcceptInsecureCertificates = true };
            browserOptions.SetPreference("media.navigator.streams.fake", true);
            browserOptions.SetPreference("media.navigator.permission.disabled", true);

            if (LoggingEnabled)
                SauceOptions.Add("extendedDebugging", true);

            browserOptions.AddAdditionalCapability("sauce:options", SauceOptions, true);
            return new RemoteWebDriver(Uri, browserOptions);
        }

        public override IWebDriver InitialiseForLocal()
        {
            var geckoService = FirefoxDriverService.CreateDefaultService(BuildPath);
            geckoService.Host = "::1";
            var browserOptions = new FirefoxOptions();
            browserOptions.SetPreference("media.navigator.streams.fake", true);
            browserOptions.SetPreference("media.navigator.permission.disabled", true);

            if (Proxy?.HttpProxy != null)
            {
                browserOptions.Proxy = Proxy;
                browserOptions.AddArgument(ProxyByPassList);
            }

            return new FirefoxDriver(geckoService, browserOptions, LocalTimeout);
        }
    }
}
