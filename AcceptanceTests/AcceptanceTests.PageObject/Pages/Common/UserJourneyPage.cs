using AcceptanceTests.Driver.Drivers;
using Coypu;
using OpenQA.Selenium;

namespace AcceptanceTests.PageObject.Pages
{
    public class UserJourneyPage : Page
    {
        public UserJourneyPage(BrowserSession driver, string url) : base(driver)
        {
            Path = url;
        }

        public virtual void Continue()
        {
            IsPageLoaded();
            DriverExtension.WaitUntilElementVisible(WrappedDriver, By.Id("next")).Click();
        }
    }
}
