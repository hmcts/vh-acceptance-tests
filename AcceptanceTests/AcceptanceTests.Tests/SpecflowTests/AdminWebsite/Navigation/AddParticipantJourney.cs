using System.Collections.Generic;
using AcceptanceTests.PageObject.Pages.AdminWebsite;
using AcceptanceTests.PageObject.Pages.Common;
using Coypu;

namespace AcceptanceTests.Tests.SpecflowTests.AdminWebsite.Navigation
{
    public class AddParticipantJourney
    {
        public class AddParticipantsJourney : UserJourneyPage
        {
            public AddParticipantsJourney(BrowserSession driver) : base(driver)
            {
                Pages.Add(new DashboardPage(WrappedDriver, PageUri.DashboardPage));
                Pages.Add(new HearingDetailsPage(WrappedDriver, PageUri.HearingDetailsPage));
                Pages.Add(new BookingListPage(WrappedDriver, PageUri.BookingListPage));
                Pages.Add(new HearingSchedulePage(WrappedDriver, PageUri.HearingSchedulePage));
                Pages.Add(new AssignJudgePage(WrappedDriver, PageUri.AssignJudgePage));
                Pages.Add(new AddParticipantsPage(WrappedDriver, PageUri.AddParticipantsPage));
            }
        }
    }
}
