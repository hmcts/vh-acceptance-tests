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
    }
}
