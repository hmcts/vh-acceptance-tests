using Coypu;

namespace AcceptanceTests.Driver.DriverExtensions
{
    public class ButtonDriverExtension
    {
        public static void ClickElement(BrowserSession driver, string elementLocator) => driver.RetryUntilTimeout(() => driver.ClickButton(elementLocator));
        public static void ClickCheckboxElement(BrowserSession driver, string elementLocator) => driver.RetryUntilTimeout(() => driver.Check(elementLocator));
    }
}
