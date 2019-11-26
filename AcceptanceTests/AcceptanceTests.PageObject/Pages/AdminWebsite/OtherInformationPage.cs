using AcceptanceTests.PageObject.Pages.Common;
using Coypu;

namespace AcceptanceTests.PageObject.Pages.AdminWebsite
{
    public class OtherInformationPage : UserJourneyPage
    {
        public OtherInformationPage(BrowserSession driver, string path) : base(driver, path)
        {
            HeadingText = "Other Information";
        }
    }
}
