using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace AcceptanceTests.Common.Driver.Strategies.Mobile.Android
{
    internal class ChromeAndroidMobileDriverStrategy : Drivers
    {
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
            var options = new ChromeOptions();
            options.AddAdditionalCapability("appiumVersion", "1.9.1");
            options.AddAdditionalCapability("deviceName", "Samsung Galaxy S9 Plus FHD GoogleAPI Emulator");
            options.AddAdditionalCapability("deviceOrientation", "portrait");
            options.AddAdditionalCapability("browserName", "Chrome");
            options.AddAdditionalCapability("platformVersion", "8.1");
            options.AddAdditionalCapability("platformName", "Android");
            return new RemoteWebDriver(Uri, options);
        }

        public override IWebDriver InitialiseForLocal()
        {
            var options = new ChromeOptions();
            options.AddAdditionalCapability("appiumVersion", "1.9.1");
            options.AddAdditionalCapability("deviceName", "Samsung Galaxy S9 Plus FHD GoogleAPI Emulator");
            options.AddAdditionalCapability("deviceOrientation", "portrait");
            options.AddAdditionalCapability("browserName", "Chrome");
            options.AddAdditionalCapability("platformVersion", "8.1");
            options.AddAdditionalCapability("platformName", "Android");
            return new AndroidDriver<AppiumWebElement>(options, LocalTimeout);
        }
    }
}
