using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Remote;

namespace AcceptanceTests.Common.Driver.Drivers.Tablet.Android
{
    internal class ChromeAndroidTabletDriverStrategy : Drivers
    {
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability(MobileCapabilityType.DeviceName, DeviceName);
            options.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            options.AddAdditionalCapability(MobileCapabilityType.BrowserName, "Chrome");
            options.AddAdditionalCapability(MobileCapabilityType.AppiumVersion, AndroidAppiumVersion);
            options.AddAdditionalCapability(MobileCapabilityType.Orientation, Orientation.ToString());
            options.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, AndroidPlatformVersion);

            foreach (var (key, value) in SauceOptions)
            {
                options.AddAdditionalCapability(key, value);
            }

            return new RemoteWebDriver(Uri, options.ToCapabilities());
        }

        public override IWebDriver InitialiseForLocal()
        {
            var chromePath = $"{BuildPath}/chromedriver";
            var options = new AppiumOptions();
            options.AddAdditionalCapability("avd", DeviceName);
            options.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            options.AddAdditionalCapability(MobileCapabilityType.DeviceName, DeviceName);
            options.AddAdditionalCapability(MobileCapabilityType.Orientation, Orientation.ToString());
            options.AddAdditionalCapability(MobileCapabilityType.BrowserName, "Chrome");
            options.AddAdditionalCapability(AndroidMobileCapabilityType.ChromedriverExecutable, chromePath);
            return new RemoteWebDriver(Uri, options.ToCapabilities(), LocalTimeout);
        }
    }
}
