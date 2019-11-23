using System.Collections.Generic;
using AcceptanceTests.PageObject.Pages.AdminWebsite;
using AcceptanceTests.PageObject.Pages.Common;
using AcceptanceTests.Tests.SpecflowTests.AdminWebsite.Navigation;
using Coypu;

namespace AcceptanceTests.Tests.SpecflowTests.AdminWebsite.UserJourneys
{
    public class UserJourneysManager
    {
        public static UserJourney CreateHearingDetailsUserJourney(BrowserSession driver)
        {
            var pages = new List<UserJourneyPage>();
            pages.Add(new DashboardPage(driver, PageUri.DashboardPage, ""));
            pages.Add(new HearingDetailsPage(driver, PageUri.HearingDetailsPage, ""));
            var userJourney = new UserJourney(pages);
            return userJourney;
        }

        public static List<UserJourneyPage>  CreateAddParticipantUserJourney(BrowserSession driver)
        {
            var userJourney = new List<UserJourneyPage>();
            userJourney.Add(new DashboardPage(driver, PageUri.DashboardPage, ""));
            userJourney.Add(new HearingDetailsPage(driver, PageUri.HearingDetailsPage, ""));
            userJourney.Add(new BookingListPage(driver, PageUri.BookingListPage, ""));
            userJourney.Add(new HearingSchedulePage(driver, PageUri.HearingSchedulePage, ""));
            userJourney.Add(new AssignJudgePage(driver, PageUri.AssignJudgePage, ""));
            userJourney.Add(new AddParticipantsPage(driver, PageUri.AddParticipantsPage, ""));
            return userJourney;
        }
    }
}
