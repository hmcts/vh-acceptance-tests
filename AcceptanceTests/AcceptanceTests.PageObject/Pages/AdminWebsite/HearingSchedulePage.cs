using AcceptanceTests.PageObject.Pages.Common;
using Coypu;

namespace AcceptanceTests.PageObject.Pages.AdminWebsite
{
    public class HearingSchedulePage : UserJourneyPage
    {
        public HearingSchedulePage(BrowserSession driver, string path, string headingText) : base(driver, path, headingText)
        {
        }
    }
}
