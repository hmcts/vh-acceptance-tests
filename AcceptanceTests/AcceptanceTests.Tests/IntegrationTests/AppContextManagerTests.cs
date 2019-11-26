using System;
using System.IO;
using System.Reflection;
using AcceptanceTests.Driver.Support;
using AcceptanceTests.Model;
using AcceptanceTests.Model.Context;
using AcceptanceTests.Model.User;
using AcceptanceTests.Tests.SpecflowTests.Common;
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

        public string GetPath(string targetApp)
        {
            if (string.IsNullOrEmpty(targetApp))
            {
                targetApp = _appContextManager.GetTargetApp().ToString();
            }
            var supportedApp = EnumParser.ParseText<SutSupport>(targetApp);
            return  $"{Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)}/SpecflowTests/";
        }

        [TestCase("AdminWebsite")]
        [TestCase("ServiceWebsite")]
        public void SetUpTestContextWithInjectedAppTest(string app)
        {
            var testContext = _appContextManager.SetUpTestContext(GetPath(app), app);
            testContext.CurrentApp.Should().Be(app.Replace(" ", ""), "Because no NUnit parameter should have been given for this test to run.");
        }

        [TestCase("Admin Website", "Admin Website")]
        [TestCase("Admin Website", "Service Website")]
        [TestCase ("Service Website", "Service Website")]
        [TestCase("Service Website", "Admin Website")]
        public void SwitchAppContextTest(string targetApp, string currentApp)
        {
            ITestContext testContext = InitialiseTestContext(currentApp);
            var expectedUsername = "test@test.com";
            testContext.UserContext.CurrentUser.Username = expectedUsername;
            SetUpTestContextWithInjectedAppTest(currentApp);
            testContext = _appContextManager.SwitchTargetAppContext(targetApp, testContext);

            var expectedApp = testContext.CurrentApp.Replace(" ", "");
            expectedApp.Should().Be(targetApp.Replace(" ", ""));
            testContext.UserContext.CurrentUser.Username.Should().Be(expectedUsername);
        }

        private ITestContext InitialiseTestContext(string currentApp)
        {
            ITestContext testContext = new TestContextBase();
            testContext.CurrentApp = currentApp;
            testContext.UserContext = new UserContext();
            testContext.UserContext.CurrentUser = new TestUser();
            return testContext;
        }
    }
}
