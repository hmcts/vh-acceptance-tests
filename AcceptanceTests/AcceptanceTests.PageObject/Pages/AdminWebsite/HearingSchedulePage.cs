using System.Collections.Generic;
using AcceptanceTests.Driver.DriverExtensions;
using AcceptanceTests.Model.FormData;
using AcceptanceTests.Model.Hearing;
using AcceptanceTests.PageObject.Components.DropdownLists;
using AcceptanceTests.PageObject.Components.Forms;
using AcceptanceTests.PageObject.Components.Inputs;
using AcceptanceTests.PageObject.Pages.Common;
using Coypu;
using OpenQA.Selenium;

namespace AcceptanceTests.PageObject.Pages.AdminWebsite
{
    public class HearingSchedulePage : UserJourneyPage
    {
        public DateTimeInputComponent DateTimeInputComponent(bool runningWithSaucelabs) => new DateTimeInputComponent(WrappedDriver, runningWithSaucelabs);
        public DropdownList HearingVenue() =>  new DropdownList(WrappedDriver, "Hearing Venue", "courtAddress");
        public void Room(string room) => InputDriverExtension.ClearValuesAndEnterText(WrappedDriver, By.Id("court-room"), room);

        public HearingSchedulePage(BrowserSession driver, string path, bool runningWithSaucelabs) : base(driver, path)
        {
            HeadingText = "Time and location";
            _pageFormList = new List<IFormComponent>
            {
                DateTimeInputComponent(runningWithSaucelabs)
            };
        }

        override
        public void FillDetails(object formData)
        {
            var hearingScheduleData = (HearingSchedule)formData;
            if(hearingScheduleData == null)
            {
                hearingScheduleData = new HearingSchedule().GenerateFakeHearingSchedule();
            }

            base.FillDetails(hearingScheduleData);
            SelectHearingVenue(hearingScheduleData.HearingVenue);
            Room(hearingScheduleData.Room);
        }


        public void SelectHearingVenue(HearingVenue option) {
            if (option == null)
                HearingVenue().SelectFirst();
            else
                HearingVenue().SelectOption(option.SelectedHearingVenue);
        }
    }
}
