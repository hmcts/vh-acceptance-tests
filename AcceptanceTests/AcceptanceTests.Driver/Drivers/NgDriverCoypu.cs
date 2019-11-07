using Protractor;
using Coypu.Drivers;
using Coypu.Drivers.Selenium;
using OpenQA.Selenium;
using System;

namespace AcceptanceTests.Driver
{
    public class NgDriverCoypu : SeleniumWebDriver
    {
        public NgDriverCoypu(IWebDriver driver, Browser browser)
            : base(CustomWebDriver(driver), browser)
        {
        }
       
        private static NgWebDriver CustomWebDriver(IWebDriver driver)
        {
            var ngdriver = new NgWebDriver(driver);
            ngdriver.IgnoreSynchronization = true;
            TryMaximize(ngdriver);
            return ngdriver;
        }

        private static void TryMaximize(NgWebDriver ngdriver)
        {
            try
            {
                ngdriver.Manage().Window.Maximize();
            }
            catch (NotImplementedException e)
            {
                Console.WriteLine("Skipping maximize, not supported on current platform: " + e.Message);
            }
        }
    }
}
