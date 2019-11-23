using System;
using AcceptanceTests.PageObject.Pages.Common;
using TechTalk.SpecFlow;

namespace AcceptanceTests.PageObject.Helpers
{
    public class PageNavigator
    {
        public readonly ScenarioContext _scenarioContext;

        public PageNavigator(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        public static UserJourneyPage CurrentPage { get; set; }

        public static void CompleteJourney(UserJourney userJourney)
        {
            foreach (var page in userJourney.Pages)
            {
                var journeyPage = page;
                try
                {
                    Console.WriteLine($"Current page: {page.HeadingText}");
                    journeyPage.FillDetails(default);
                    journeyPage.Continue();
                }
                catch(Exception)
                {
                    Console.WriteLine("Page has no forms to be filled.");
                    Console.WriteLine("Continuing to next page.");
                    journeyPage.Continue();
                }
            }
        }
    }
}
