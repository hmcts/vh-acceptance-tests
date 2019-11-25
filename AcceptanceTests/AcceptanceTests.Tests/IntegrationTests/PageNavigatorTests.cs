using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using AcceptanceTests.Model.Context;
using AcceptanceTests.Model.Role;
using AcceptanceTests.PageObject.Helpers;
using AcceptanceTests.PageObject.Pages.AdminWebsite;
using AcceptanceTests.Tests.SpecflowTests.AdminWebsite.Navigation;
using AcceptanceTests.Tests.SpecflowTests.AdminWebsite.UserJourneys;
using AcceptanceTests.Tests.SpecflowTests.Common;
using AcceptanceTests.Tests.SpecflowTests.Common.Hooks;
using BoDi;
using Coypu;
using FluentAssertions;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace AcceptanceTests.Tests.IntegrationTests
{
    public class PageNavigatorTests : HookTestsBase
    {
        internal BrowserSession _driver;
        private DriverHook _driverHook;
        private TestSetUpHook _testSetUpHook;

        private ITestContext SetUpHooks(string path)
        { 
            _testSetUpHook = new TestSetUpHook(_objectContainer, _appContextManager);
            var testContext = _testSetUpHook.OneTimeSetup(path);
            var scenarioInfo = new ScenarioInfo("AcceptanceTests.Tests: PageNavigatorTests", "IntegrationTests", new string[] { });
            _driverHook = new DriverHook(scenarioInfo, _objectContainer, _appContextManager);
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

        [Test]
        public void CompleteCreateHearingDetailsUserJourneyTest()
        {
            var userJourney = UserJourneyManager.CreateHearingDetailsUserJourney(_driver);
            PageNavigator.CompleteJourney(userJourney);
            PageNavigator.CurrentPage.GetType().Should().Be(typeof(HearingDetailsPage));
        }

        [Test]
        public void CompleteAddParticipantUserJourneyTest()
        {
            var userJourney = UserJourneyManager.CreateAddParticipantUserJourney(_driver);
            PageNavigator.CompleteJourney(userJourney);
            PageNavigator.CurrentPage.GetType().Should().Be(typeof(AddParticipantsPage));
        }
    }
}
