using AcceptanceTests.Common.Model.FormData;
using AcceptanceTests.Common.PageObject.Components.DropdownLists;
using AcceptanceTests.Common.PageObject.Components.Inputs;
using AcceptanceTests.Driver.DriverExtensions;
using Coypu;
using OpenQA.Selenium;

namespace AcceptanceTests.Common.PageObject.Components.Forms
{
    public class HearingScheduleFormComponent : Component, IFormComponent
    {
        private readonly bool _runningWithSaucelabs;
        public DateTimeInputComponent DateTimeInputComponent(bool runningWithSaucelabs) => new DateTimeInputComponent(WrappedDriver, runningWithSaucelabs);
        public DropdownList HearingVenue => new DropdownList(WrappedDriver, "Hearing Venue", "courtAddress");
        public void Room(string room) => InputDriverExtension.ClearValuesAndEnterText(WrappedDriver, By.Id("court-room"), room);

        public HearingScheduleFormComponent(BrowserSession driver, bool runningWithSaucelabs) : base(driver)
        {
            _runningWithSaucelabs = runningWithSaucelabs;
        }

        public void FillFormDetails(IFormData formData)
        {
            var hearingScheduleData = (HearingSchedule)formData;
            if (hearingScheduleData == null)
            {
                hearingScheduleData = (HearingSchedule)new HearingSchedule().GenerateFake();
            }

            DateTimeInputComponent(_runningWithSaucelabs).FillFormDetails(hearingScheduleData.DateTime);
            HearingVenue.FillFormDetails(hearingScheduleData.HearingVenue);
            Room(hearingScheduleData.Room);
        }
    }
}
