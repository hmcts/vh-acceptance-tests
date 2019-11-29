using System.Collections.Generic;
using AcceptanceTests.PageObject.Components.Forms;
using AcceptanceTests.PageObject.Pages.Common;
using Coypu;

namespace AcceptanceTests.PageObject.Pages.AdminWebsite
{
    public class HearingDetailsPage : UserJourneyPage
    {
        IFormComponent _hearingDetailsForm;

        public HearingDetailsPage(BrowserSession driver, string path) : base(driver, path)
        {
            HeadingText = "Hearing details";
            _hearingDetailsForm = new HearingDetailsFormComponent(driver);
            _pageFormList = new List<IFormComponent>
            {
                _hearingDetailsForm
            };
        }
    }
}
