using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;

namespace AcceptanceTests.Common.Driver.Strategies
{
    internal class SafariIpadDriverStrategy : Drivers
    {
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
#pragma warning disable 618
            var capabilities = new DesiredCapabilities();
            capabilities.SetCapability("appiumVersion", "1.15.0");
            capabilities.SetCapability("deviceName", "iPad Pro (9.7 inch) Simulator");
            capabilities.SetCapability("deviceOrientation", "portrait");
            capabilities.SetCapability("platformVersion", "13.0");
            capabilities.SetCapability("platformName", "iOS");
            capabilities.SetCapability("browserName", "Safari");
#pragma warning restore 618
            return new RemoteWebDriver(Uri, capabilities);
        }

        public override IWebDriver InitialiseForLocal()
        {
            var browserOptions = new SafariOptions();
            browserOptions.AddAdditionalCapability("appiumVersion", "1.15.0");
            browserOptions.AddAdditionalCapability("deviceName", "iPad Pro (9.7 inch) Simulator");
            browserOptions.AddAdditionalCapability("deviceOrientation", "portrait");
            browserOptions.AddAdditionalCapability("platformVersion", "13.0");
            browserOptions.AddAdditionalCapability("platformName", "iOS");
            browserOptions.AddAdditionalCapability("browserName", "Safari");
            return new AndroidDriver<AppiumWebElement>(browserOptions, LocalTimeout);
        }
    }
}
