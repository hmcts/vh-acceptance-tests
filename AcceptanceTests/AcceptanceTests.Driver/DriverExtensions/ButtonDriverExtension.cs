using Coypu;
using OpenQA.Selenium;

namespace AcceptanceTests.Driver.DriverExtensions
{
    public class ButtonDriverExtension
    {
        public static void ClickElement(BrowserSession driver, By elementLocator)
        {
            var element = WaitDriverExtension.WaitUntilElementClickable(driver, elementLocator);
            element.Click();
        }

        public static void ClickElement(BrowserSession driver, string elementLocator)
        {
            var element = WaitDriverExtension.WaitUntilElementClickable(driver, elementLocator);
            element.Click();
        }

        public static void ClickCheckboxElement(BrowserSession driver, string elementLocator) => driver.RetryUntilTimeout(() => driver.Check(elementLocator));
    }
}
