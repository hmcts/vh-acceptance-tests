using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace AcceptanceTests.Common.Driver.Strategies
{
    internal class InternetExplorerDriverStrategy : Drivers
    {
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
            var ieOptions = new InternetExplorerOptions() { PlatformName = "Windows 10", BrowserVersion = "latest" };
            ieOptions.AddAdditionalCapability("sauce:options", SauceOptions);
            return new RemoteWebDriver(Uri, ieOptions);
        }

        public override IWebDriver InitialiseForLocal()
        {
            var ieOptions = new InternetExplorerOptions
            {
                IgnoreZoomLevel = true,
                BrowserAttachTimeout = LocalTimeout,
                IntroduceInstabilityByIgnoringProtectedModeSettings = true
            };
            return new InternetExplorerDriver(BuildPath, ieOptions, LocalTimeout);
        }
    }
}
