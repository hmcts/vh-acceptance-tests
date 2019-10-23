using TechTalk.SpecFlow;
using AcceptanceTests.Model.Context;
using Coypu;
using AcceptanceTests.PageObject.Page;
using FluentAssertions;

namespace AcceptanceTests.Tests.Steps
{
    [Binding]
    public class SignInSteps
    {
        private readonly AppContextManager _appContextManager;
        private ITestContext _testContext;
        private readonly BrowserSession _driver;

        public SignInSteps(AppContextManager appContextManager, ITestContext testContext, BrowserSession driver)
        {
            _appContextManager = appContextManager;
            _testContext = testContext;
            _driver = driver;
        }

        [Given("I am registered as '(.*)' in the Video Hearings Azure AD")]
        public void GivenIamRegisteredAsRoleInTheVideoHearingsAD(string role)
        {
            var currentUser = _testContext.UserContext.GetFirstOrDefaultUserByRole(role);
            currentUser.Username = _testContext.UserContext.GetUsername(currentUser);
            _testContext.UserContext.CurrentUser = currentUser;
        }

        [When("I sign in to the '(.*)' using my account details")]
        public void WhenISignInToTheWebsiteUsingMyAccountDetails(string targetApp)
        {
            _testContext = _appContextManager.SwitchTargetAppContext(targetApp, _testContext);
            SignInPage page = new SignInPage(_driver);
            page.Visit();
            page.SignInAs(_testContext.UserContext.CurrentUser.Username,
                            _testContext.UserContext.TestUserSecrets.TestUserPassword);
        }
    }
}
