using System.Collections.Generic;
using System.Linq;
using AcceptanceTests.PageObject.Pages.Common;
using TechTalk.SpecFlow;

namespace AcceptanceTests.PageObject.Helpers
{
    public class PageNavigator
    {
        private UserJourneyPage _userJourney;
        public readonly ScenarioContext _scenarioContext;

        public PageNavigator(ScenarioContext scenarioContext, UserJourneyPage userJourney)
        {
            _scenarioContext = scenarioContext;
            _userJourney = userJourney;
        }

        public Page GetPage(string pageUri)
        {
            return _userJourney.Pages.Single(page => page.Path == pageUri);
        }

        public List<Page> PageList
        {
            get
            {
                return _userJourney.Pages;
            }
        }
    }
}
