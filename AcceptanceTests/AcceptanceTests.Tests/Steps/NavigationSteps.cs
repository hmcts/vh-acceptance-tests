using AcceptanceTests.Driver.Support;
using AcceptanceTests.Model;
using AcceptanceTests.Model.Context;
using Coypu;
using FluentAssertions;
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

        [Then(@"I am redirected to the '(.*)' automatically")]
        public void ThenParticipantShouldBeRedirectedToVideoWeb(string targetApp)
        {
            _appContextManager.SwitchTargetAppContext(targetApp, _testContext);
            _driver.Location.ToString().Should().Contain(_testContext.BaseUrl);
        }
    }
}
