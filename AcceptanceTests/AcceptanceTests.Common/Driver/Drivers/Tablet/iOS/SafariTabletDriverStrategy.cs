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
            options.AddAdditionalOption(MobileCapabilityType.AppiumVersion, AppiumVersion);
            options.AddAdditionalOption(MobileCapabilityType.Orientation, Orientation.ToString());
            options.AddAdditionalOption(MobileCapabilityType.PlatformVersion, PlatformVersion);
            options.AddAdditionalOption(MobileCapabilityType.DeviceName, DeviceName);
            options.AddAdditionalOption(MobileCapabilityType.PlatformName, "iOS");
            options.AddAdditionalOption(MobileCapabilityType.BrowserName, "safari");
            options.AddAdditionalOption(IOSMobileCapabilityType.AutoAcceptAlerts, true);
            options.AddAdditionalOption(IOSMobileCapabilityType.SafariAllowPopups, true);

            foreach (var (key, value) in SauceOptions)
            {
                options.AddAdditionalOption(key, value);
            }

            return options;
        }

        private SafariOptions ConfigureSauceLabsRealDevice()
        {
            var options = new SafariOptions();
            options.AddAdditionalOption(MobileCapabilityType.AppiumVersion, AppiumVersion);
            options.AddAdditionalOption(MobileCapabilityType.PlatformName, "iOS");
            options.AddAdditionalOption(MobileCapabilityType.DeviceName, "iPad.*");
            options.AddAdditionalOption(MobileCapabilityType.PlatformVersion, PlatformVersion);
            options.AddAdditionalOption("testobject_api_key", RealDeviceApiKey);
            options.AddAdditionalOption("tabletOnly", true);
            options.AddAdditionalOption("autoGrantPermissions", true);
            options.AddAdditionalOption(IOSMobileCapabilityType.AutoAcceptAlerts, true);
            options.AddAdditionalOption(MobileCapabilityType.BrowserName, "Safari");
            LocalAppiumTimeout *= 9;
            options.AddAdditionalOption("newCommandTimeout", LocalAppiumTimeout.TotalSeconds); ;
            options.AddAdditionalOption(MobileCapabilityType.NewCommandTimeout, LocalAppiumTimeout.TotalSeconds);
            options.AddAdditionalOption(IOSMobileCapabilityType.LaunchTimeout, LocalAppiumTimeout.TotalSeconds);
            
            foreach (var (key, value) in SauceOptions)
            {
                options.AddAdditionalOption(key, value);
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
            options.AddAdditionalOption(MobileCapabilityType.DeviceName, DeviceName);
            options.AddAdditionalOption(MobileCapabilityType.PlatformName, "iOS");
            options.AddAdditionalOption(MobileCapabilityType.BrowserName, "Safari");
            options.AddAdditionalOption(MobileCapabilityType.Orientation, Orientation.ToString());
            options.AddAdditionalOption(MobileCapabilityType.PlatformVersion, PlatformVersion);
            options.AddAdditionalOption(IOSMobileCapabilityType.AutoAcceptAlerts, true);
            options.AddAdditionalOption(IOSMobileCapabilityType.SafariAllowPopups, true);

            if (ResetDeviceBetweenTests)
            {
                LocalAppiumTimeout *= 4;
                options.AddAdditionalOption(MobileCapabilityType.FullReset, true);
            }

            return options;
        }

        private SafariOptions ConfigureLocalRealDevice()
        {
            var options = new SafariOptions();
            options.AddAdditionalOption(MobileCapabilityType.Orientation, "LANDSCAPE");
            options.AddAdditionalOption(MobileCapabilityType.PlatformName, "iOS");
            options.AddAdditionalOption(MobileCapabilityType.BrowserName, "Safari");
            options.AddAdditionalOption(MobileCapabilityType.PlatformVersion, PlatformVersion);
            options.AddAdditionalOption("safari:deviceType", "iPad");
            options.AddAdditionalOption(MobileCapabilityType.DeviceName, DeviceName);
            options.AddAdditionalOption(MobileCapabilityType.Udid, UUID);
            options.AddAdditionalOption(MobileCapabilityType.FullReset, false);
            return options;
        }
    }
}
