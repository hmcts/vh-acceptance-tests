using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;

namespace AcceptanceTests.Common.Driver.Strategies
{
    internal class EdgeDriverStrategy : Drivers
    {
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
            var browserOptions = new EdgeOptions() { PlatformName = "Windows 10", BrowserVersion = "18.17763" };
            browserOptions.AddAdditionalCapability(CapabilityType.UnexpectedAlertBehavior, "accept");
            browserOptions.AddAdditionalCapability(CapabilityType.UnhandledPromptBehavior, UnhandledPromptBehavior.Accept);

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
            browserOptions.AddAdditionalCapability(CapabilityType.UnexpectedAlertBehavior, "accept");
            browserOptions.AddAdditionalCapability("permissions.default.microphone", true);
            browserOptions.AddAdditionalCapability("permissions.default.camera", true);
            return new EdgeDriver("C:\\Windows\\system32\\", browserOptions, LocalTimeout);
        }
    }
}
