using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace AcceptanceTests.Common.Driver.Strategies.Desktop.Windows
{
    internal class InternetExplorerDriverStrategy : Drivers
    {
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
            var browserOptions = new InternetExplorerOptions() { PlatformName = "Windows 10", BrowserVersion = "latest" };
            browserOptions.AddAdditionalCapability("sauce:options", SauceOptions, true);
            return new RemoteWebDriver(Uri, browserOptions);
        }

        public override IWebDriver InitialiseForLocal()
        {
            var browserOptions = new InternetExplorerOptions
            {
                IgnoreZoomLevel = true,
                BrowserAttachTimeout = LocalTimeout,
                IntroduceInstabilityByIgnoringProtectedModeSettings = true
            };
            return new InternetExplorerDriver(BuildPath, browserOptions, LocalTimeout);
        }
    }
}
