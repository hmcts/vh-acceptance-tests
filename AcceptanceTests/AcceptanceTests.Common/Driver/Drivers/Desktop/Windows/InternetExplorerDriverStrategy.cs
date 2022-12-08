using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace AcceptanceTests.Common.Driver.Drivers.Desktop.Windows
{
    internal class InternetExplorerDriverStrategy : Drivers
    {
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
            var browserOptions = new InternetExplorerOptions()
            {
                PlatformName = "Windows 10", 
                BrowserVersion = BrowserVersion
            };
            browserOptions.AddAdditionalOption("sauce:options", SauceOptions);
            return new RemoteWebDriver(Uri, browserOptions);
        }

        public override IWebDriver InitialiseForLocal()
        {
            var browserOptions = new InternetExplorerOptions
            {
                IgnoreZoomLevel = true,
                BrowserAttachTimeout = LocalDesktopTimeout,
                IntroduceInstabilityByIgnoringProtectedModeSettings = true
            };
            return new InternetExplorerDriver(BuildPath, browserOptions, LocalDesktopTimeout);
        }
    }
}
