using System;
using System.Linq;
using AcceptanceTests.Driver.DriverExtensions;
using AcceptanceTests.Model.FormData;
using AcceptanceTests.PageObject.Components.Forms;
using Coypu;
using OpenQA.Selenium;

namespace AcceptanceTests.PageObject.Components.Inputs
{
    public class DateTimeInputComponent : Component, IFormComponent
    {
        private bool _runningWithSaucelabs;
        private static By HearingDateTextfield => By.Id("hearingDate");
        private static By HearingStartTimeTextfield => By.XPath("//input[@id='hearingStartTimeHour'or @id='hearingStartTimeMinute']");
        private static By HearingDurationTextfield => By.XPath("//input[@id='hearingDurationHour' or @id='hearingDurationMinute']");
        private static readonly By ErrorDateText = By.Id("hearingDate-error");

        public DateTimeInputComponent(BrowserSession driver, bool runningWithSaucelabs) : base(driver)
        {
            _runningWithSaucelabs = runningWithSaucelabs;
        }


        public void FillFormDetails(IFormData formDataObject)
        {
            HearingSchedule hearingScheduleData = null;
            HearingDateTime dateTimeFormData = null;

            try
            {
                hearingScheduleData = (HearingSchedule)formDataObject;
            }
            catch (Exception)
            {
                dateTimeFormData = (HearingDateTime)formDataObject;
            }

            if (hearingScheduleData == null && dateTimeFormData == null)
            {
                var dateTimeData = new HearingDateTime();
                dateTimeFormData = (HearingDateTime)dateTimeData.GenerateFake(WrappedDriver.GetType().ToString(), _runningWithSaucelabs);
            } else if (hearingScheduleData != null && hearingScheduleData.DateTime != null)
            {
                dateTimeFormData = (HearingDateTime)hearingScheduleData.DateTime;
            }

            HearingDate(WrappedDriver, dateTimeFormData.Date);
            HearingStartTime(WrappedDriver, dateTimeFormData.StartTime);
            HearingDuration(WrappedDriver, dateTimeFormData.Duration);
        }

        public void HearingDate(BrowserSession driver, string currentdate = null)
        {
            InputDriverExtension.EnterValues(driver, HearingDateTextfield, currentdate);
        }

        public void HearingStartTime(BrowserSession driver, string[] currentTime = null)
        {
            var startTime = ListDriverExtension.GetListOfElements("Start Time", driver, HearingStartTimeTextfield, 1).ToArray();
            for (var i = 0; i < startTime.Length; i++)
            {
                InputDriverExtension.ClearWebElementValues(startTime[i]);
                InputDriverExtension.EnterValues(startTime[i], currentTime[i]);
            }
        }
        public void HearingDuration(BrowserSession driver, string hearingDuration)
        {
            var duration = ListDriverExtension.GetListOfElements("Duration", driver, HearingDurationTextfield, 1).ToArray();
            var hearingduration = hearingDuration.Split(':');
            for (var i = 0; i < duration.Length; i++)
            {
                InputDriverExtension.ClearWebElementValues(duration[i]);
                InputDriverExtension.EnterValues(duration[i], hearingduration[i]);
            }
        }
    }
}
