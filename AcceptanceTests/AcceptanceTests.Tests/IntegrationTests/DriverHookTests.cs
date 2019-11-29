﻿using System;
using System.IO;
using System.Reflection;
using AcceptanceTests.Driver;
using AcceptanceTests.Driver.Settings;
using AcceptanceTests.Model.Context;
using AcceptanceTests.Tests.SpecflowTests.Common;
using AcceptanceTests.Tests.SpecflowTests.Common.Hooks;
using Coypu;
using FluentAssertions;
using NUnit.Framework;
using Protractor;
using TechTalk.SpecFlow;

namespace AcceptanceTests.Tests.IntegrationTests
{
    public class DriverHookTests : HookTestsBase
    {
        private string _path;
        private ITestContext _testContext;
        private BrowserSession _driver;
        private SauceLabsSettings _saucelabsSettings;
        private ScenarioInfo _scenarioInfo;
        private string _buildName = "AcceptanceTests.Tests: Driver Hook tests";

        [SetUp]
        public void TestSetUp()
        {
            SetUp();
            _path = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/SpecflowTests/";
            _testContext = _appContextManager.SetUpTestContext(_path);
            _saucelabsSettings = SaucelabsHook.GetSauceLabsSettings(_appContextManager.ConfigRoot);
            _scenarioInfo = new ScenarioInfo("AcceptanceTests.Tests: Driver Hook tests", "Integration Test InitDriverHookDefaultValuesTest", new string[] { });

        }

        [TearDown]
        public void TearDown()
        {
            if (_driver != null)
                _driver.Dispose();

            var drivers = _objectContainer.ResolveAll<BrowserSession>();

            foreach (var driver in drivers)
                driver.Dispose();
        }

        [Test]
        public void InitDriverHookLocalBrowserDefaultValuesTest()
        {
            var targetBrowser = NUnitParamReader.GetTargetBrowser();
            var blockCamAndMicrophone = false;
            var driverManager = new DriverManager();
            _driver = driverManager.Init(_testContext.BaseUrl, targetBrowser.ToString(),
                                        NUnitParamReader.GetTargetPlatform(),
                                        _scenarioInfo, _buildName, blockCamAndMicrophone, new SauceLabsSettings());

            _driver.Should().NotBeNull();
            _driver.Driver.Native.As<NgWebDriver>().WrappedDriver.ToString().Should().Contain(targetBrowser.ToString());
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
            _driver = driverManager.Init(_testContext.BaseUrl, targetBrowser.ToString(),
                                        NUnitParamReader.GetTargetPlatform(),
                                        _scenarioInfo, _buildName, blockCamAndMicrophone, new SauceLabsSettings());

            _driver.Should().NotBeNull();
            _driver.Driver.Native.As<NgWebDriver>().WrappedDriver.ToString().Should().Contain(targetBrowser.ToString());
        }

        [TestCase("AdminWebsite", "Chrome")]
        [TestCase("AdminWebsite", "Safari")]
        [TestCase("AdminWebsite", "Firefox")]
        [TestCase("ServiceWebsite", "Chrome")]
        [TestCase("ServiceWebsite", "Safari")]
        [TestCase("ServiceWebsite", "Firefox")]
        public void InitDriverHookRemoteBrowserCorrectWebsiteLaunchesTest(string app, string browser)
        {
            _testContext = _appContextManager.SetUpTestContext(_path, app);
            _testContext.CurrentApp.Should().Be(app, "Because no NUnit parameter should have been given for this test to run.");

            var targetBrowser = NUnitParamReader.GetTargetBrowser(browser);
            var scenarioInfo = new ScenarioInfo("AcceptanceTests.Tests: Driver Hook tests", "Integration Test InitDriverHookDefaultValuesTest", new string[] { });

            var blockCamAndMicrophone = false;
            var driverManager = new DriverManager();
            _driver = driverManager.Init(_testContext.BaseUrl, targetBrowser.ToString(),
                                        NUnitParamReader.GetTargetPlatform(),
                                        scenarioInfo, _buildName, blockCamAndMicrophone, _saucelabsSettings);

            _driver.Should().NotBeNull();
            _driver.Visit(_testContext.BaseUrl);

            try
            {
                _driver.Location.ToString().Should().Contain(_testContext.BaseUrl);
            }
            catch (Exception)
            {
                _driver.Location.ToString().Should().Contain("https://login.microsoftonline.com/");
            }
            
        }
    }
}
