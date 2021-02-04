using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;

namespace AcceptanceTests.Common.Driver.Drivers.Tablet.iOS
{
    internal class SafariTabletDriverStrategy : Drivers
    {
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
            return RealDevice
                ? new RemoteWebDriver(new Uri(RealDeviceServerUrl), ConfigureSauceLabsRealDevice().ToCapabilities(), TimeSpan.FromMinutes(3)) 
                : new RemoteWebDriver(Uri, ConfigureSauceLabsSimulator());
        }

        private AppiumOptions ConfigureSauceLabsSimulator()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability(MobileCapabilityType.AppiumVersion, AppiumVersion);
            options.AddAdditionalCapability(MobileCapabilityType.Orientation, Orientation.ToString());
            options.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, PlatformVersion);
            options.AddAdditionalCapability(MobileCapabilityType.DeviceName, DeviceName);
            options.AddAdditionalCapability(MobileCapabilityType.PlatformName, "iOS");
            options.AddAdditionalCapability(MobileCapabilityType.BrowserName, "safari");
            options.AddAdditionalCapability(IOSMobileCapabilityType.AutoAcceptAlerts, true);
            options.AddAdditionalCapability(IOSMobileCapabilityType.SafariAllowPopups, true);

            foreach (var (key, value) in SauceOptions)
            {
                options.AddAdditionalCapability(key, value);
            }

            return options;
        }

        private SafariOptions ConfigureSauceLabsRealDevice()
        {
            var options = new SafariOptions();
            options.AddAdditionalCapability(MobileCapabilityType.AppiumVersion, AppiumVersion);
            options.AddAdditionalCapability(MobileCapabilityType.PlatformName, "iOS");
            options.AddAdditionalCapability(MobileCapabilityType.DeviceName, "iPad.*");
            options.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, PlatformVersion);
            options.AddAdditionalCapability("testobject_api_key", RealDeviceApiKey);
            options.AddAdditionalCapability("tabletOnly", true);
            options.AddAdditionalCapability("autoGrantPermissions", true);
            options.AddAdditionalCapability(IOSMobileCapabilityType.AutoAcceptAlerts, true);
            options.AddAdditionalCapability(MobileCapabilityType.BrowserName, "Safari");
            LocalAppiumTimeout *= 9;
            options.AddAdditionalCapability("newCommandTimeout", LocalAppiumTimeout.TotalSeconds); ;
            options.AddAdditionalCapability(MobileCapabilityType.NewCommandTimeout, LocalAppiumTimeout.TotalSeconds);
            options.AddAdditionalCapability(IOSMobileCapabilityType.LaunchTimeout, LocalAppiumTimeout.TotalSeconds);
            
            foreach (var (key, value) in SauceOptions)
            {
                options.AddAdditionalCapability(key, value);
            }

            return options;
        }

        public override IWebDriver InitialiseForLocal()
        {
            return RealDevice ? new SafariDriver("/usr/bin/", ConfigureLocalRealDevice(), LocalAppiumTimeout) : new RemoteWebDriver(Uri, ConfigureLocalSimulator().ToCapabilities(), LocalAppiumTimeout);
        }

        private AppiumOptions ConfigureLocalSimulator()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability(MobileCapabilityType.DeviceName, DeviceName);
            options.AddAdditionalCapability(MobileCapabilityType.PlatformName, "iOS");
            options.AddAdditionalCapability(MobileCapabilityType.BrowserName, "Safari");
            options.AddAdditionalCapability(MobileCapabilityType.Orientation, Orientation.ToString());
            options.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, PlatformVersion);
            options.AddAdditionalCapability(IOSMobileCapabilityType.AutoAcceptAlerts, true);
            options.AddAdditionalCapability(IOSMobileCapabilityType.SafariAllowPopups, true);

            if (ResetDeviceBetweenTests)
            {
                LocalAppiumTimeout *= 4;
                options.AddAdditionalCapability(MobileCapabilityType.FullReset, true);
            }

            return options;
        }

        private SafariOptions ConfigureLocalRealDevice()
        {
            var options = new SafariOptions();
            options.AddAdditionalCapability(MobileCapabilityType.Orientation, "LANDSCAPE");
            options.AddAdditionalCapability(MobileCapabilityType.PlatformName, "iOS");
            options.AddAdditionalCapability(MobileCapabilityType.BrowserName, "Safari");
            options.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, PlatformVersion);
            options.AddAdditionalCapability("safari:deviceType", "iPad");
            options.AddAdditionalCapability(MobileCapabilityType.DeviceName, DeviceName);
            options.AddAdditionalCapability(MobileCapabilityType.Udid, UUID);
            options.AddAdditionalCapability(MobileCapabilityType.FullReset, false);
            return options;
        }
    }
}
