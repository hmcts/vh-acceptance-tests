using AcceptanceTests.Driver.DriverExtensions;
using AcceptanceTests.Model.FormData;
using AcceptanceTests.PageObject.Components.DropdownLists;
using AcceptanceTests.PageObject.Components.Inputs;
using Coypu;
using OpenQA.Selenium;

namespace AcceptanceTests.PageObject.Components.Forms
{
    public class HearingScheduleFormComponent : Component, IFormComponent
    {
        private bool _runningWithSaucelabs;
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
