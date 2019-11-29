using System.IO;
using System.Reflection;
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

        public string GetPath()
        {
            return  $"{Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)}/SpecflowTests/";
        }

        [TestCase("AdminWebsite")]
        [TestCase("ServiceWebsite")]
        public void SetUpTestContextWithInjectedAppTest(string app)
        {
            var testContext = _appContextManager.SetUpTestContext(GetPath(), app);
            testContext.CurrentApp.Should().Be(app.Replace(" ", ""), "Because no NUnit parameter should have been given for this test to run.");
        }

        [TestCase("Admin Website", "Admin Website")]
        [TestCase("Admin Website", "Service Website")]
        [TestCase ("Service Website", "Service Website")]
        [TestCase("Service Website", "Admin Website")]
        public void SwitchAppContextTestKeepsTheSameUser(string targetApp, string currentApp)
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

        [TestCase("Admin Website", "Service Website")]
        [TestCase("Service Website", "Admin Website")]
        public void SwitchAppContextChangesToNewUserTest(string targetApp, string currentApp)
        {
            ITestContext testContext = InitialiseTestContext(currentApp);
            var currentUser = CreateTestUser();
            var newUser = CreateTestUser();
            testContext.UserContext.CurrentUser = currentUser;
            SetUpTestContextWithInjectedAppTest(currentApp);
            testContext = _appContextManager.SwitchTargetAppContext(targetApp, testContext, newUser);

            var expectedApp = testContext.CurrentApp.Replace(" ", "");
            expectedApp.Should().Be(targetApp.Replace(" ", ""));
            testContext.UserContext.CurrentUser.Should().Be(newUser);
        }

        [TestCase("Admin Website", "Admin Website")]
        [TestCase("Service Website", "Service Website")]
        public void SwitchAppContextSameAppDoesNotChangeToNewUserTest(string targetApp, string currentApp)
        {
            ITestContext testContext = InitialiseTestContext(currentApp);
            var currentUser = CreateTestUser();
            var newUser = CreateTestUser();
            testContext.UserContext.CurrentUser = currentUser;
            SetUpTestContextWithInjectedAppTest(currentApp);
            testContext = _appContextManager.SwitchTargetAppContext(targetApp, testContext, newUser);

            var expectedApp = testContext.CurrentApp.Replace(" ", "");
            expectedApp.Should().Be(targetApp.Replace(" ", ""));
            testContext.UserContext.CurrentUser.Should().Be(currentUser);
        }

        private ITestContext InitialiseTestContext(string currentApp)
        {
            ITestContext testContext = new TestContextBase();
            testContext.CurrentApp = currentApp;
            testContext.UserContext = new UserContext();
            testContext.UserContext.CurrentUser = new TestUser();
            return testContext;
        }

        private TestUser CreateTestUser()
        {
            var testUser = new TestUser
            {
                Firstname = Faker.Name.First(),
                Lastname = Faker.Name.Last(),
                Username = Faker.Internet.Email()
        };

            return testUser;
        }
    }
}
