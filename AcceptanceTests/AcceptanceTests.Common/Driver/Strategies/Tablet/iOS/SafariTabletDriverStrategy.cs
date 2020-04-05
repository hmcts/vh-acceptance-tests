using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;

namespace AcceptanceTests.Common.Driver.Strategies.Tablet.iOS
{
    internal class SafariTabletDriverStrategy : Drivers
    {
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
            var options = new SafariOptions();
            options.AddAdditionalCapability("appiumVersion", "1.16.0");
            options.AddAdditionalCapability("deviceName", "iPad Pro (9.7 inch) Simulator");
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
            options.AddAdditionalCapability("deviceName", "iPad Pro (9.7 inch) Simulator");
            options.AddAdditionalCapability("deviceOrientation", "portrait");
            options.AddAdditionalCapability("platformVersion", "13.2");
            options.AddAdditionalCapability("platformName", "iOS");
            options.AddAdditionalCapability("browserName", "Safari");
            return new IOSDriver<AppiumWebElement>(options, LocalTimeout);
        }
    }
}
