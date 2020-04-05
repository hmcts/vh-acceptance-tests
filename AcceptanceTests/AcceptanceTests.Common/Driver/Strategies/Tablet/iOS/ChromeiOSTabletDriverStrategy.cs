using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace AcceptanceTests.Common.Driver.Strategies.Tablet.iOS
{
    internal class ChromeiOSTabletDriverStrategy : Drivers
    {
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
            var options = new ChromeOptions();
            options.AddAdditionalCapability("appiumVersion", "1.16.0");
            options.AddAdditionalCapability("deviceName", "iPad Pro (9.7 inch) Simulator");
            options.AddAdditionalCapability("deviceOrientation", "portrait");
            options.AddAdditionalCapability("platformVersion", "13.2");
            options.AddAdditionalCapability("platformName", "iOS");
            options.AddAdditionalCapability("browserName", "Chrome");
            return new RemoteWebDriver(Uri, options);
        }

        public override IWebDriver InitialiseForLocal()
        {
            var options = new ChromeOptions();
            options.AddAdditionalCapability("appiumVersion", "1.16.0");
            options.AddAdditionalCapability("deviceName", "iPad Pro (9.7 inch) Simulator");
            options.AddAdditionalCapability("deviceOrientation", "portrait");
            options.AddAdditionalCapability("platformVersion", "13.2");
            options.AddAdditionalCapability("platformName", "iOS");
            options.AddAdditionalCapability("browserName", "Chrome");
            return new AndroidDriver<AppiumWebElement>(options, LocalTimeout);
        }
    }
}
