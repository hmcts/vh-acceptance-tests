using System;
using System.Collections.Generic;
using AcceptanceTests.Model.Page;
using AcceptanceTests.PageObject.Pages.AdminWebsite;
using AcceptanceTests.PageObject.Pages.Common;
using AcceptanceTests.Tests.SpecflowTests.AdminWebsite.Navigation;
using Coypu;

namespace AcceptanceTests.Tests.SpecflowTests.AdminWebsite.UserJourneys
{
    public class UserJourneyManager
    {
        public static UserJourney CreateHearingDetailsUserJourney(BrowserSession driver)
        {
            var pages = new List<UserJourneyPage>();
            pages.Add(new DashboardPage(driver, PageUri.DashboardPage));
            pages.Add(new HearingDetailsPage(driver, PageUri.HearingDetailsPage));
            var userJourney = new UserJourney(pages, DashboardOption.BookAvideoHearing);
            return userJourney;
        }

        public static UserJourney CreateHearingScheduleUserJourney(BrowserSession driver)
        {
            var userJourney = CreateHearingDetailsUserJourney(driver);
            userJourney.Pages.Add(new HearingSchedulePage(driver, PageUri.HearingSchedulePage));
            return userJourney;
        }

        public static UserJourney CreateAssignJudgeUserJourney(BrowserSession driver)
        {
            var userJourney = CreateHearingScheduleUserJourney(driver);
            userJourney.Pages.Add(new AssignJudgePage(driver, PageUri.AssignJudgePage));
            return userJourney;
        }

        public static UserJourney CreateOtherInformationJourney(BrowserSession driver)
        {
            var userJourney = CreateAssignJudgeUserJourney(driver);
            userJourney.Pages.Add(new OtherInformationPage(driver, PageUri.OtherInformationPage));
            return userJourney;
        }

        public static UserJourney  CreateAddParticipantUserJourney(BrowserSession driver)
        {
            var userJourney = CreateAssignJudgeUserJourney(driver);
            userJourney.Pages.Add(new AddParticipantsPage(driver, PageUri.AddParticipantsPage));
            return userJourney;
        }
    }
}
