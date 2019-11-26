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
            pages.Add(new HearingDetailsPage(driver, PageUri.HearingDetailsPage, "Hearing details"));
            var userJourney = new UserJourney(pages, DashboardOption.BookAvideoHearing);
            return userJourney;
        }

        public static UserJourney  CreateAddParticipantUserJourney(BrowserSession driver)
        {
            var pages = new List<UserJourneyPage>();
            pages.Add(new DashboardPage(driver, PageUri.DashboardPage));
            pages.Add(new HearingDetailsPage(driver, PageUri.HearingDetailsPage, "Hearing details"));
            pages.Add(new HearingSchedulePage(driver, PageUri.HearingSchedulePage, "Time and location"));
            pages.Add(new AssignJudgePage(driver, PageUri.AssignJudgePage, "Assign Judge"));
            pages.Add(new OtherInformationPage(driver, PageUri.OtherInformationPage));
            pages.Add(new AddParticipantsPage(driver, PageUri.AddParticipantsPage));
            var userJourney = new UserJourney(pages, DashboardOption.BookAvideoHearing);
            return userJourney;
        }
    }
}
