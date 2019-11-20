using AcceptanceTests.Model.Context;
using AcceptanceTests.SpecflowTests.Common;
using AcceptanceTests.SpecflowTests.Common.Steps;
using Coypu;
using TechTalk.SpecFlow;

namespace AcceptanceTests.SpecflowTests.AdminWebsite.Steps
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
