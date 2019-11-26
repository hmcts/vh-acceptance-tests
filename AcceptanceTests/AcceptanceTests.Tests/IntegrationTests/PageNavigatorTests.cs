using System.IO;
using System.Reflection;
using AcceptanceTests.Model.Context;
using AcceptanceTests.Model.User;
using AcceptanceTests.PageObject.Helpers;
using AcceptanceTests.PageObject.Pages.AdminWebsite;
using AcceptanceTests.Tests.SpecflowTests.AdminWebsite.UserJourneys;
using AcceptanceTests.Tests.SpecflowTests.Common.Hooks;
using Coypu;
using FluentAssertions;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace AcceptanceTests.Tests.IntegrationTests
{
    public class PageNavigatorTests : HookTestsBase
    {
        private BrowserSession _driver;
        private DriverHook _driverHook;
        private TestSetUpHook _testSetUpHook;

        private ITestContext SetUpHooks(string path)
        { 
            _testSetUpHook = new TestSetUpHook(_objectContainer, _appContextManager);
            var testContext = _testSetUpHook.OneTimeSetup(path);
            _driverHook = new DriverHook(_objectContainer, _appContextManager);
            var scenarioInfo = new ScenarioInfo("AcceptanceTests.Tests: PageNavigatorTests", "IntegrationTests", new string[] { });
            _driverHook.SetScenarioInfo(scenarioInfo);
            return testContext;
        }

        [SetUp]
        public void TestSetUp()
        {
            SetUp();
            var path = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/SpecflowTests";
            var testContext = SetUpHooks(path);
            _driver = _driverHook.SetUpDriver(testContext);
            testContext.UserContext.CurrentUser = UserHelper.SetCurrentUser(testContext, UserRole.VhOfficer.ToString());
            SignInHelper.SignIn(_driver, testContext);
        }

        [TearDown]
        public void TearDown()
        {
            _driverHook.TearDownSession();
        }

        [Test]
        [Category("Local")]
        public void CompleteCreateHearingDetailsUserJourneyTest()
        {
            var userJourney = UserJourneyManager.CreateHearingDetailsUserJourney(_driver);
            PageNavigator.CompleteJourney(userJourney);
            PageNavigator.CurrentPage.GetType().Should().Be(typeof(HearingDetailsPage));
        }

        [Test]
        [Category("Local")]
        public void CompleteCreateHearingScheduleUserJourneyTest()
        {
            var userJourney = UserJourneyManager.CreateHearingScheduleUserJourney(_driver, false);
            PageNavigator.CompleteJourney(userJourney);
            PageNavigator.CurrentPage.GetType().Should().Be(typeof(HearingSchedulePage));
        }

        [Test]
        [Category("Local")]
        public void CompleteCreateAssignJudgeUserJourneyTest()
        {
            var userJourney = UserJourneyManager.CreateAssignJudgeUserJourney(_driver, false);
            PageNavigator.CompleteJourney(userJourney);
            PageNavigator.CurrentPage.GetType().Should().Be(typeof(AssignJudgePage));
        }

        [Test]
        [Category("Local")]
        public void CompleteCreateOtherInformationUserJourneyTest()
        {
            var userJourney = UserJourneyManager.CreateOtherInformationJourney(_driver, false);
            PageNavigator.CompleteJourney(userJourney);
            PageNavigator.CurrentPage.GetType().Should().Be(typeof(OtherInformationPage));
        }

        [Test]
        [Category("Local")]
        public void CompleteAddParticipantUserJourneyTest()
        {
            var userJourney = UserJourneyManager.CreateAddParticipantUserJourney(_driver, false);
            PageNavigator.CompleteJourney(userJourney);
            PageNavigator.CurrentPage.GetType().Should().Be(typeof(AddParticipantsPage));
        }
    }
}
