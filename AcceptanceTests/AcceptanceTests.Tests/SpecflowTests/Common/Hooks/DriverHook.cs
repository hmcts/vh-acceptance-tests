using Coypu;
using BoDi;
using AcceptanceTests.Driver;
using TechTalk.SpecFlow;
using AcceptanceTests.Driver.Settings;
using AcceptanceTests.Model.Context;
using System;
using AcceptanceTests.SpecflowTests.Common.Scenario;

namespace AcceptanceTests.Tests.SpecflowTests.Common.Hooks
{
    public class DriverHook
    {
        private BrowserSession _driver;
        private ITestContext _testContext;
        private SauceLabsSettings _saucelabsSettings;
        private readonly ScenarioContext _scenarioContext;
        private ScenarioInfo _scenarioInfo;
        private readonly IObjectContainer _objectContainer;
        private readonly AppContextManager _appContextManager;

        public DriverHook(ScenarioContext scenarioContext, IObjectContainer objectContainer, AppContextManager appContextManager)
        {
            _scenarioContext = scenarioContext;
            _scenarioInfo = scenarioContext.ScenarioInfo;
            _objectContainer = objectContainer;
            _appContextManager = appContextManager;
        }

        public DriverHook(ScenarioInfo scenarioInfo, IObjectContainer objectContainer, AppContextManager appContextManager)
        {
            _scenarioInfo = scenarioInfo;
            _objectContainer = objectContainer;
            _appContextManager = appContextManager;
        }

        protected string GetTargetBrowser()
        {
            var browser = _testContext.TargetBrowser;
            var targetBrowser = NUnitParamReader.GetTargetBrowser(browser).ToString();
            return targetBrowser;
        }

        public BrowserSession InitDriver()
        {
            var blockCamAndMicrophone = ScenarioManager.HasTag(ScenarioTags.BlockCameraAndMic.ToString(), _scenarioInfo);
            var buildName = AppContextManager.GetBuildName();
            var driverManager = new DriverManager();
            _driver = driverManager.Init(_testContext.BaseUrl, GetTargetBrowser(),
                                        NUnitParamReader.GetTargetPlatform(),
                                        _scenarioInfo, buildName, blockCamAndMicrophone, _saucelabsSettings);

            _objectContainer.RegisterInstanceAs(_driver);
            return _driver;
        }

        public BrowserSession SetUpDriver(ITestContext testContext)
        {
            _testContext = testContext;
            _saucelabsSettings = SaucelabsHook.GetSauceLabsSettings(_appContextManager.ConfigRoot);

            _driver = InitDriver();
            try
            {
                _driver.Visit(_testContext.BaseUrl);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error visiting BaseUrl {_testContext.BaseUrl}: {ex.GetBaseException()}");
            }
            return _driver;
        }

        public void UpdateSauceLabsResults()
        {
            if (_saucelabsSettings.RunWithSaucelabs)
            {
                bool passed = _scenarioContext.TestError == null;
                SaucelabsHook.LogPassed(passed, _driver);
            }
        }

        public void TearDownSession()
        {
            UpdateSauceLabsResults();

            if (_driver != null)
                _driver.Dispose();

            var drivers = _objectContainer.ResolveAll<BrowserSession>();

            foreach (var driver in drivers)
                driver.Dispose();
        }
    }
}
