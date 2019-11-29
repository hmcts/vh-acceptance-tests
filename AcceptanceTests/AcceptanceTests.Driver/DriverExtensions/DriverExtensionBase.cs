using System;
using Coypu;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Protractor;

namespace AcceptanceTests.Driver.DriverExtensions
{
    public class NativeDriverExtension
    {

        public static IWebDriver GetDriver(BrowserSession driver)
        {
            IWebDriver ngDriver;
            try
            {
                ngDriver = (NgWebDriver)driver.Native;
            }
            catch (InvalidCastException)
            {
                ngDriver = (RemoteWebDriver)driver.Native;
            }

            return ngDriver;
        }
    }
}
