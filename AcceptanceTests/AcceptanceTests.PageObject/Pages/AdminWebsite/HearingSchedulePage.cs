using System.Collections.Generic;
using AcceptanceTests.PageObject.Components.Forms;
using AcceptanceTests.PageObject.Pages.Common;
using Coypu;

namespace AcceptanceTests.PageObject.Pages.AdminWebsite
{
    public class HearingSchedulePage : UserJourneyPage
    {
        IFormComponent _hearingScheduleForm;

        public HearingSchedulePage(BrowserSession driver, string path, bool runningWithSaucelabs) : base(driver, path)
        {
            HeadingText = "Time and location";
            _hearingScheduleForm = new HearingScheduleFormComponent(driver, runningWithSaucelabs);
            _pageFormList = new List<IFormComponent>
            {
                _hearingScheduleForm
            };
        }
    }
}
