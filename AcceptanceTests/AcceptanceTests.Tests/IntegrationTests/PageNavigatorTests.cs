using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using AcceptanceTests.PageObject.Helpers;
using AcceptanceTests.PageObject.Pages.AdminWebsite;
using AcceptanceTests.Tests.SpecflowTests.AdminWebsite.Navigation;
using AcceptanceTests.Tests.SpecflowTests.AdminWebsite.UserJourneys;
using AcceptanceTests.Tests.SpecflowTests.Common;
using AcceptanceTests.Tests.SpecflowTests.Common.Hooks;
using Coypu;
using FluentAssertions;
using NUnit.Framework;

namespace AcceptanceTests.Tests.IntegrationTests
{
    public class PageNavigatorTests
    {
        internal BrowserSession _driver;
        private DriverHook _driverHook;
        private TestSetUpHook _testSetUpHook;

        [SetUp]
        public void SetUp()
        {
            var path = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/SpecflowTests";
            var testContext = _testSetUpHook.OneTimeSetup(path);
            _driverHook = new DriverHook(null, null, new AppContextManager());
            _driver = _driverHook.SetUpDriver(testContext);
        }

        [Test]
        public void CompleteAddParticipantUserJourneyTest()
        {
            var userJourney = UserJourneysManager.CreateAddParticipantUserJourneysMapping(_driver);
            PageNavigator.CompleteJourney(userJourney);
            PageNavigator.CurrentPage.GetType().Should().Be(typeof(AddParticipantsPage));
        }
    }
}
