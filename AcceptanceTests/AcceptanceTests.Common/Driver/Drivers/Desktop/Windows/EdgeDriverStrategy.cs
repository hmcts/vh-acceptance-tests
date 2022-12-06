using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;

namespace AcceptanceTests.Common.Driver.Drivers.Desktop.Windows
{
    internal class EdgeDriverStrategy : Drivers
    {
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
            var browserOptions = new EdgeOptions()
            {
                PlatformName = "Windows 10", 
                BrowserVersion = BrowserVersion
            };
            browserOptions.AddAdditionalOption("dom.webnotifications.enabled", 1);
            browserOptions.AddAdditionalOption("permissions.default.microphone", 1);
            browserOptions.AddAdditionalOption("permissions.default.camera", 1);
            browserOptions.AddAdditionalOption("avoidProxy", true);
            browserOptions.AddAdditionalOption("sauce:options", SauceOptions);
            return new RemoteWebDriver(Uri, browserOptions);
        }

        public override IWebDriver InitialiseForLocal()
        {
            var browserOptions = new EdgeOptions();
            browserOptions.AddAdditionalOption("dom.webnotifications.enabled", 1);
            browserOptions.AddAdditionalOption("permissions.default.microphone", 1);
            browserOptions.AddAdditionalOption("permissions.default.camera", 1);
            browserOptions.AddAdditionalOption("avoidProxy", true);
            return new EdgeDriver("C:\\Windows\\system32\\", browserOptions, LocalDesktopTimeout);
        }
    }
}
