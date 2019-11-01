using AcceptanceTests.Model.Context;
using Coypu;
using TechTalk.SpecFlow;

namespace AcceptanceTests.Tests.Steps
{
    public class VideoHearingsSteps : StepsBase
    {

        public VideoHearingsSteps(AppContextManager appContextManager, ScenarioContext scenarioContext, ITestContext testContext, BrowserSession driver)
            : base(appContextManager, scenarioContext, testContext, driver)
        {
        }

        [Given(@"I don't have any upcoming video hearings scheduled")]
        public void GivenIDontHaveAnyUpcomingVideoHearingsScheduled()
        {
            _scenarioContext.Pending();
        }
    }
}
