using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace AcceptanceTests.Common.Driver.Drivers.Desktop.Mac
{
    internal class FirefoxMacDriverStrategy : Drivers
    {
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
            var options = new FirefoxOptions { PlatformName = MacPlatform, BrowserVersion =BrowserVersion, AcceptInsecureCertificates = true };
            if (LoggingEnabled)
            {
                SauceOptions.Add("extendedDebugging", true);
                options.SetPreference("devtools.chrome.enabled", true);
                options.SetPreference("devtools.debugger.prompt-connection", false);
                options.SetPreference("devtools.debugger.remote-enabled", true);
            }

            options.SetPreference("media.navigator.streams.fake", true);
            options.SetPreference("media.navigator.permission.disabled", true);
            options.SetPreference("media.autoplay.default", 0);
            options.AddAdditionalCapability("sauce:options", SauceOptions, true);
            return new RemoteWebDriver(Uri, options);
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
