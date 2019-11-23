using System.Collections.Generic;

namespace AcceptanceTests.PageObject.Pages.Common
{
    public class UserJourney
    {
        public List<UserJourneyPage> Pages { get; }

        public UserJourney(List<UserJourneyPage> pages)
        {
            Pages = pages;
        }
    }
}
