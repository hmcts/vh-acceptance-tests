using Protractor;
using Coypu.Drivers;
using Coypu.Drivers.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace AcceptanceTests.Driver
{
    // TODO: fix or remove this
    public class NgDriverCoypu : SeleniumWebDriver
    {
        public NgDriverCoypu(IWebDriver driver, Browser browser)
            : base(CustomWebDriver(driver), browser)
        {
        }
       
        private static NgWebDriver CustomWebDriver(IWebDriver driver)
        {
            return new NgWebDriver(driver);
        }
    }
}
