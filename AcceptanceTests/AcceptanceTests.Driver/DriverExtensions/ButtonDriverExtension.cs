using Coypu;
using OpenQA.Selenium;
using Protractor;

namespace AcceptanceTests.Driver.DriverExtensions
{
    public class ButtonDriverExtension
    {
        public static void ClickElement(BrowserSession driver, By elementLocator)
        {
            var element = WaitDriverExtension.WaitUntilElementClickable(driver, elementLocator);
            element.Click();
        }

        public static void ClickCheckboxElement(BrowserSession driver, By elementLocator)
        {
            var ngDriver = (NgWebDriver)driver.Native;
            var element = ngDriver.FindElement(elementLocator);
            element.Click();
        }
    }
}
