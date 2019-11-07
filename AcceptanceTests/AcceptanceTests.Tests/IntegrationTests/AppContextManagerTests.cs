using System;
using AcceptanceTests.Model.Context;
using AcceptanceTests.Model.User;
using FluentAssertions;
using NUnit.Framework;

namespace AcceptanceTests.Tests.IntegrationTests
{
    public class AppContextManagerTests
    {
        AppContextManager _appContextManager;

        [SetUp]
        public void SetUp()
        {
            _appContextManager = new AppContextManager();

        }

        [TestCase("AdminWebsite")]
        [TestCase("ServiceWebsite")]
        public void SetUpTestContextWithInjectedAppTest(string app)
        {
            var testContext = _appContextManager.SetUpTestContext(app);
            testContext.CurrentApp.Should().Be(app, "Because no NUnit parameter should have been given for this test to run.");
        }

        [TestCase("Admin Website", "Admin Website")]
        [TestCase("Admin Website", "Service Website")]
        [TestCase ("Service Website", "Service Website")]
        [TestCase("Service Website", "Admin Website")]
        public void SwitchAppContextTest(string targetApp, string currentApp)
        {
            ITestContext testContext = new TestContextBase();
            testContext.CurrentApp = currentApp;
            testContext.UserContext = new UserContext();
            testContext.UserContext.CurrentUser = new TestUser();
            testContext.UserContext.CurrentUser.Username = "test@test.com";
            testContext.UserContext.CurrentUser.Username = "dummypassword";
            testContext = _appContextManager.SwitchTargetAppContext(targetApp, testContext);
            testContext.CurrentApp.Should().Equals(targetApp);
        }
    }
}
