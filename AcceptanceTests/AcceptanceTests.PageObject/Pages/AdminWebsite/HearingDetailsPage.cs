using AcceptanceTests.PageObject.Pages.Common;
using Coypu;

namespace AcceptanceTests.PageObject.Pages.AdminWebsite
{
    public class HearingDetailsPage : UserJourneyPage
    {
        public HearingDetailsPage(BrowserSession driver, string path, string headingText) : base(driver, path, headingText)
        {
        }
    }
}
