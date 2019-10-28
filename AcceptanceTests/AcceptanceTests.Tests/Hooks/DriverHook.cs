using Coypu;
using BoDi;
using AcceptanceTests.Driver;
using TechTalk.SpecFlow;
using AcceptanceTests.Driver.Settings;
using AcceptanceTests.Model.Context;

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

        public BrowserSession InitDriver()
        {
            var scenarioTitle = ScenarioManager.GetScenarioTitle(_scenarioContext);
            var blockCamAndMicrophone = ScenarioManager.HasTag(ScenarioTags.BlockCameraAndMic.ToString(), _scenarioContext);
            var driverManager = new DriverManager();
            _driver = driverManager.Init(_testContext.BaseUrl, NUnitParamReader.GetTargetBrowser().ToString(),
                                        NUnitParamReader.GetTargetPlatform(),
                                        scenarioTitle, blockCamAndMicrophone, _saucelabsSettings);

            _objectContainer.RegisterInstanceAs(_driver);
            return _driver;
        }

        [BeforeScenario(Order = 2)]
        public void BeforeScenario()
        {
            _driver = InitDriver();
            _driver.Visit(_testContext.BaseUrl);
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
