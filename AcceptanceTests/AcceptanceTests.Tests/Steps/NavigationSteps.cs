using AcceptanceTests.Model.Context;
using Coypu;
using TechTalk.SpecFlow;

namespace AcceptanceTests.Tests.Steps
{
    [Binding]
    public class NavigationSteps : StepsBase
    {
        public NavigationSteps(AppContextManager appContextManager, ITestContext testContext, BrowserSession driver)
            : base(appContextManager, testContext, driver)
        {
        }

        [When(@"I see the '(.*)' page")]
        public void WhenISeeThePage(string pageName)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
