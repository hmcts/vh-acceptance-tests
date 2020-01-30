using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;

namespace AcceptanceTests.Common.Driver.Strategies
{
    internal class EdgeDriverStrategy : Drivers
    {
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
            var edgeOptions = new EdgeOptions() { PlatformName = "Windows 10", BrowserVersion = "18.17763" };
            edgeOptions.AddAdditionalCapability(CapabilityType.UnexpectedAlertBehavior, "accept");
            edgeOptions.AddAdditionalCapability(CapabilityType.UnhandledPromptBehavior, UnhandledPromptBehavior.Accept);

            if (!BlockedCamAndMic)
            {
                edgeOptions.AddAdditionalCapability("permissions.default.microphone", true);
                edgeOptions.AddAdditionalCapability("permissions.default.camera", true);
            }
            edgeOptions.AddAdditionalCapability("avoidProxy", true);
            edgeOptions.AddAdditionalCapability("sauce:options", SauceOptions);
            return new RemoteWebDriver(Uri, edgeOptions);
        }

        public override IWebDriver InitialiseForLocal()
        {
            var edgeOptions = new EdgeOptions
            {
                UnhandledPromptBehavior = UnhandledPromptBehavior.Accept,
                UseInPrivateBrowsing = true
            };
            edgeOptions.AddAdditionalCapability(CapabilityType.UnexpectedAlertBehavior, "accept");
            edgeOptions.AddAdditionalCapability("permissions.default.microphone", true);
            edgeOptions.AddAdditionalCapability("permissions.default.camera", true);
            return new EdgeDriver("C:\\Windows\\system32\\", edgeOptions, LocalTimeout);
        }
    }
}
