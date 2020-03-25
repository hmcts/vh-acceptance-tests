using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;

namespace AcceptanceTests.Common.Driver.Strategies.Desktop.Mac
{
    internal class SafariDriverStrategy : Drivers
    {
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
            var browserOptions = new SafariOptions(){
                PlatformName = MacPlatform,
                BrowserVersion = "13.0"
            };
            browserOptions.AddAdditionalCapability("sauce:options", SauceOptions);
            return new RemoteWebDriver(Uri, browserOptions);
        }

        public override IWebDriver InitialiseForLocal()
        {
            var browserOptions = new SafariOptions()
            {
                PlatformName = MacPlatform,
                BrowserVersion = "13.0.5",
                UnhandledPromptBehavior = UnhandledPromptBehavior.Accept
            };
            return new SafariDriver(BuildPath, browserOptions, LocalTimeout);
        }
    }
}
