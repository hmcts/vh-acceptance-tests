using System;
using AcceptanceTests.Driver;
using AcceptanceTests.Driver.Settings;
using AcceptanceTests.Model.Context;
using AcceptanceTests.Tests.Hooks;
using Coypu;
using FluentAssertions;
using NUnit.Framework;
using Protractor;
using TechTalk.SpecFlow;

namespace AcceptanceTests.Tests.IntegrationTests
{
    public class DriverHookTests : HookTestsBase
    {
        private ITestContext _testContext;
        private BrowserSession _session;
        private SauceLabsSettings _saucelabsSettings;
        private ScenarioInfo _scenarioInfo;
        private string _buildName = "AcceptanceTests.Tests: Driver Hook tests";

        [SetUp]
        public void TestSetUp()
        {
            SetUp();
            _testContext = _appContextManager.SetUpTestContext();
            _saucelabsSettings = SaucelabsHook.GetSauceLabsSettings(_appContextManager.ConfigRoot);
            _scenarioInfo = new ScenarioInfo("AcceptanceTests.Tests: Driver Hook tests", "Integration Test InitDriverHookDefaultValuesTest", new string[] { });

        }

        [TearDown]
        public void TearDown()
        {
            if (_session != null)
                _session.Dispose();

            var drivers = _objectContainer.ResolveAll<BrowserSession>();

            foreach (var driver in drivers)
                driver.Dispose();
        }

        [Test]
        [Category("Local")]
        public void InitDriverHookLocalBrowserDefaultValuesTest()
        {
            var targetBrowser = NUnitParamReader.GetTargetBrowser();
            var blockCamAndMicrophone = false;
            var driverManager = new DriverManager();
            _session = driverManager.Init(_testContext.BaseUrl, targetBrowser.ToString(),
                                        NUnitParamReader.GetTargetPlatform(),
                                        _scenarioInfo, _buildName, blockCamAndMicrophone, new SauceLabsSettings());

            _session.Should().NotBeNull();
            _session.Driver.Native.As<NgWebDriver>().WrappedDriver.ToString().Should().Contain(targetBrowser.ToString());
        }

        [TestCase("Chrome")]
        [TestCase("Safari")]
        [TestCase("Firefox")]
        [Category("Local")]
        public void InitDriverHookLocalBrowserValuesTest(string browser)
        {
            var targetBrowser = NUnitParamReader.GetTargetBrowser(browser);
            var blockCamAndMicrophone = false;
            var driverManager = new DriverManager();
            _session = driverManager.Init(_testContext.BaseUrl, targetBrowser.ToString(),
                                        NUnitParamReader.GetTargetPlatform(),
                                        _scenarioInfo, _buildName, blockCamAndMicrophone, new SauceLabsSettings());

            _session.Should().NotBeNull();
            _session.Driver.Native.As<NgWebDriver>().WrappedDriver.ToString().Should().Contain(targetBrowser.ToString());
        }

        [TestCase("AdminWebsite", "Chrome")]
        [TestCase("AdminWebsite", "Safari")]
        [TestCase("AdminWebsite", "Firefox")]
        [TestCase("ServiceWebsite", "Chrome")]
        [TestCase("ServiceWebsite", "Safari")]
        [TestCase("ServiceWebsite", "Firefox")]
        public void InitDriverHookRemoteBrowserCorrectWebsiteLaunchesTest(string app, string browser)
        {
            _testContext = _appContextManager.SetUpTestContext(app);
            _testContext.CurrentApp.Should().Be(app, "Because no NUnit parameter should have been given for this test to run.");

            var targetBrowser = NUnitParamReader.GetTargetBrowser(browser);
            var scenarioInfo = new ScenarioInfo("AcceptanceTests.Tests: Driver Hook tests", "Integration Test InitDriverHookDefaultValuesTest", new string[] { });

            var blockCamAndMicrophone = false;
            var driverManager = new DriverManager();
            _session = driverManager.Init(_testContext.BaseUrl, targetBrowser.ToString(),
                                        NUnitParamReader.GetTargetPlatform(),
                                        scenarioInfo, _buildName, blockCamAndMicrophone, _saucelabsSettings);

            _session.Should().NotBeNull();
            _session.Visit(_testContext.BaseUrl);

            try
            {
                _session.Location.ToString().Should().Contain(_testContext.BaseUrl);
            }
            catch (Exception)
            {
                _session.Location.ToString().Should().Contain("https://login.microsoftonline.com/");
            }
            
        }
    }
}
