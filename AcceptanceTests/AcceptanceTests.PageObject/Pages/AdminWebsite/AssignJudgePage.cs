using AcceptanceTests.PageObject.Pages.Common;
using Coypu;

namespace AcceptanceTests.PageObject.Pages.AdminWebsite
{
    public class AssignJudgePage : UserJourneyPage
    {
        public AssignJudgePage(BrowserSession driver, string path, string headingText) : base(driver, path, headingText)
        {
        }
    }
}
