using AcceptanceTests.Driver.DriverExtensions;
using AcceptanceTests.PageObject.Pages.Common;
using Coypu;
using OpenQA.Selenium;

namespace AcceptanceTests.PageObject.Pages.AdminWebsite
{
    public class HearingSchedulePage : UserJourneyPage
    {
        public void Room(string value) => InputDriverExtension.ClearTextAndEnterText(WrappedDriver, By.Id("court-room"), value);

        public HearingSchedulePage(BrowserSession driver, string path, string headingText) : base(driver, path, headingText)
        {
        }
    }
}
