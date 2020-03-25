using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;

namespace AcceptanceTests.Common.Driver.Strategies.Desktop.Windows
{
    internal class EdgeChroniumDriverStrategy : Drivers
    {
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
            var browserOptions = new EdgeOptions() { PlatformName = "Windows 10", BrowserVersion = "latest" };
            browserOptions.AddAdditionalCapability("dom.webnotifications.enabled", 1);
            if (!BlockedCamAndMic)
            {
                browserOptions.AddAdditionalCapability("permissions.default.microphone", 1);
                browserOptions.AddAdditionalCapability("permissions.default.camera", 1);
            }
            browserOptions.AddAdditionalCapability("avoidProxy", true);
            browserOptions.AddAdditionalCapability("sauce:options", SauceOptions);
            return new RemoteWebDriver(Uri, browserOptions);
        }

        public override IWebDriver InitialiseForLocal()
        {
            var browserOptions = new EdgeOptions
            {
                UnhandledPromptBehavior = UnhandledPromptBehavior.Accept,
                UseInPrivateBrowsing = true
            };
            return new EdgeDriver("C:\\Windows\\system32\\", browserOptions, LocalTimeout);
        }
    }
}
