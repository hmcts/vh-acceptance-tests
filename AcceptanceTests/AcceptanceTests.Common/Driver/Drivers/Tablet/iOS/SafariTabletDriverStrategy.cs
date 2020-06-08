using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Remote;

namespace AcceptanceTests.Common.Driver.Drivers.Tablet.iOS
{
    internal class SafariTabletDriverStrategy : Drivers
    {
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability(MobileCapabilityType.AppiumVersion, AppiumVersion);
            options.AddAdditionalCapability(MobileCapabilityType.Orientation, Orientation.ToString());
            options.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, PlatformVersion);
            options.AddAdditionalCapability(MobileCapabilityType.DeviceName, DeviceName);
            options.AddAdditionalCapability(MobileCapabilityType.PlatformName, "iOS");
            options.AddAdditionalCapability(MobileCapabilityType.BrowserName, "safari");
            options.AddAdditionalCapability("autoAcceptAlerts", true);

            foreach (var (key, value) in SauceOptions)
            {
                options.AddAdditionalCapability(key, value);
            }

            return new RemoteWebDriver(Uri, options.ToCapabilities());
        }

        public override IWebDriver InitialiseForLocal()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability(MobileCapabilityType.DeviceName, DeviceName);
            options.AddAdditionalCapability(MobileCapabilityType.PlatformName, "iOS");
            options.AddAdditionalCapability(MobileCapabilityType.BrowserName, "Safari");
            options.AddAdditionalCapability(MobileCapabilityType.Orientation, Orientation.ToString());
            options.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "13.5");
            options.AddAdditionalCapability(MobileCapabilityType.FullReset, true);
            return new RemoteWebDriver(Uri, options.ToCapabilities(), LocalTimeout);
        }
    }
}
