using System.Collections.Generic;
using AcceptanceTests.Common.Model.Page;

namespace AcceptanceTests.Common.PageObject.Pages.Common
{
    public class UserJourney
    {
        public DashboardOption DashboardOption { get; set; }
        public List<UserJourneyPage> Pages { get; }

        public UserJourney(List<UserJourneyPage> pages, DashboardOption dashboardOption)
        {
            Pages = pages;
            DashboardOption = dashboardOption;
        }
    }
}
