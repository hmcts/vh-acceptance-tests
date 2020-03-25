using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;

namespace AcceptanceTests.Common.Driver.Strategies.Mobile.iOS
{
    internal class SafariIphoneDriverStrategy : Drivers
    {
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
            var options = new SafariOptions();
            options.AddAdditionalCapability("appiumVersion", "1.16.0");
            options.AddAdditionalCapability("deviceName", "iPhone 11 Pro Simulator");
            options.AddAdditionalCapability("deviceOrientation", "portrait");
            options.AddAdditionalCapability("platformVersion", "13.2");
            options.AddAdditionalCapability("platformName", "iOS");
            options.AddAdditionalCapability("browserName", "Safari");
            return new RemoteWebDriver(Uri, options);
        }

        public override IWebDriver InitialiseForLocal()
        {
            var options = new SafariOptions();
            options.AddAdditionalCapability("appiumVersion", "1.16.0");
            options.AddAdditionalCapability("deviceName", "iPhone 11 Pro Simulator");
            options.AddAdditionalCapability("deviceOrientation", "portrait");
            options.AddAdditionalCapability("platformVersion", "13.2");
            options.AddAdditionalCapability("platformName", "iOS");
            options.AddAdditionalCapability("browserName", "Safari");
            return new IOSDriver<AppiumWebElement>(options, LocalTimeout);
        }
    }
}
