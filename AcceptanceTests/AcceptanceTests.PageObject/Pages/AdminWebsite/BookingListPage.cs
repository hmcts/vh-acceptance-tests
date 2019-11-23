using AcceptanceTests.PageObject.Pages.Common;
using Coypu;

namespace AcceptanceTests.PageObject.Pages.AdminWebsite
{
    public class BookingListPage : UserJourneyPage
    {
        public BookingListPage(BrowserSession driver, string path, string headingText) : base(driver, path, headingText)
        {
        }
    }
}
