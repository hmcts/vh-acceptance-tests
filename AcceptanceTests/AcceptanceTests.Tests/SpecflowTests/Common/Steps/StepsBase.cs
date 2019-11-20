using AcceptanceTests.Model.Context;
using Coypu;
using TechTalk.SpecFlow;

namespace AcceptanceTests.SpecflowTests.Common.Steps
{
    public class StepsBase
    {
        protected readonly AppContextManager _appContextManager;
        protected ScenarioContext _scenarioContext;
        protected ITestContext _testContext;
        protected readonly BrowserSession _driver;

        public StepsBase(AppContextManager appContextManager, ScenarioContext scenarioContext, ITestContext testContext,
                            BrowserSession driver)
        {
            _appContextManager = appContextManager;
            _scenarioContext = scenarioContext;
            _testContext = testContext;
            _driver = driver;
        }
    }
}
