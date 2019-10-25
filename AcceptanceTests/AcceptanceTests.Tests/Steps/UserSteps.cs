﻿using AcceptanceTests.Model.Context;
using AcceptanceTests.Tests.Helpers;
using Coypu;
using TechTalk.SpecFlow;

namespace AcceptanceTests.Tests.Steps
{
    [Binding]
    public class UserSteps : StepsBase
    {
        public UserSteps(AppContextManager appContextManager, ITestContext testContext, BrowserSession driver)
            : base(appContextManager, testContext, driver)
        {
        }

        [Given(@"I am on the '(.*)' as an authorised '(.*)' user")]
        public void GivenIAmOnTheAsAnAuthorisedUser(string targetApp, string role)
        {
            _testContext = _appContextManager.SwitchTargetAppContext(targetApp, _testContext);
            _testContext.UserContext.CurrentUser = UserHelper.SetCurrentUser(_testContext, role);
            SignInHelper.SignIn(_driver, _testContext);
        }

        [Given(@"I am registered as '(.*)' in the Video Hearings Azure AD")]
        public void GivenIAmRegisteredAsInTheVideoHearingsAzureAD(string role)
        {
            _testContext.UserContext.CurrentUser = UserHelper.SetCurrentUser(_testContext, role);
        }
    }
}