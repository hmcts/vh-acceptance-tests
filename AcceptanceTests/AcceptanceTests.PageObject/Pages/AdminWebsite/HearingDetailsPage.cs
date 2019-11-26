using System.Collections.Generic;
using AcceptanceTests.PageObject.Components;
using AcceptanceTests.PageObject.Pages.Common;
using Coypu;

namespace AcceptanceTests.PageObject.Pages.AdminWebsite
{
    public class HearingDetailsPage : UserJourneyPage
    {
        IFormComponent _hearingDetailsForm;

        public HearingDetailsPage(BrowserSession driver, string path, string headingText) : base(driver, path, headingText)
        {
            _hearingDetailsForm = new HearingDetailsForm(driver);
            _pageFormList = new List<IFormComponent>();
            _pageFormList.Add(_hearingDetailsForm);
        }
    }
}
