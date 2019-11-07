using Coypu;
using BoDi;
using AcceptanceTests.Driver;
using TechTalk.SpecFlow;
using AcceptanceTests.Driver.Settings;
using AcceptanceTests.Model.Context;
using AcceptanceTests.Configuration;
using System;
using AcceptanceTests.Driver.Support;

namespace AcceptanceTests.Tests.Hooks
{
    [Binding]
    public sealed class DriverHook
    {
        private BrowserSession _driver;
        private readonly ITestContext _testContext;
        private readonly SauceLabsSettings _saucelabsSettings;
        private readonly ScenarioContext _scenarioContext;
        private readonly IObjectContainer _objectContainer;

        public DriverHook(SauceLabsSettings saucelabsSettings,
            ScenarioContext scenarioContext, ITestContext testContext, IObjectContainer objectContainer)
        {
            _saucelabsSettings = saucelabsSettings;
            _scenarioContext = scenarioContext;
            _testContext = testContext;
            _objectContainer = objectContainer;
        }

        private string GetTargetBrowser()
        {
            var browser = _testContext.TargetBrowser;
            var targetBrowser = NUnitParamReader.GetTargetBrowser(browser).ToString();
            return targetBrowser;
        }

        public BrowserSession InitDriver()
        {
            var blockCamAndMicrophone = ScenarioManager.HasTag(ScenarioTags.BlockCameraAndMic.ToString(), _scenarioContext);
            var buildName = AppContextManager.GetBuildName();
            var driverManager = new DriverManager();
            _driver = driverManager.Init(_testContext.BaseUrl, GetTargetBrowser(),
                                        NUnitParamReader.GetTargetPlatform(),
                                        _scenarioContext.ScenarioInfo, buildName, blockCamAndMicrophone, _saucelabsSettings);

            _objectContainer.RegisterInstanceAs(_driver);
            return _driver;
        }

        [BeforeScenario(Order = 2)]
        public void BeforeScenario()
        {
            _driver = InitDriver();
            try
            {
                _driver.Visit(_testContext.BaseUrl);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error visiting BaseUrl {_testContext.BaseUrl}: {ex.GetBaseException()}");
            }
            
        }

        [AfterScenario(Order = 0)]
        public void AfterScenario()
        {
            if (_saucelabsSettings.RunWithSaucelabs)
            {
                bool passed = _scenarioContext.TestError == null;
                SaucelabsHook.LogPassed(passed, _driver);
            }

            TearDownSession();
        }

        public void TearDownSession()
        {
            if (_driver != null)
                _driver.Dispose();

            var drivers = _objectContainer.ResolveAll<BrowserSession>();

            foreach (var driver in drivers)
                driver.Dispose();
        }
    }
}
