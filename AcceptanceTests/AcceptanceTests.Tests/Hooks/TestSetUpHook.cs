using TechTalk.SpecFlow;
using BoDi;

namespace AcceptanceTests.Tests.Hooks
{
    [Binding]
    public class TestSetUpHook
    {
        private readonly IObjectContainer _objectContainer;
        private readonly AppContextManager _appContextManager;

        public TestSetUpHook(IObjectContainer objectContainer, AppContextManager appContextManager)
        {
            _objectContainer = objectContainer;
            _appContextManager = appContextManager;
        }

        [BeforeScenario(Order = 0)]
        public void OneTimeSetup()
        {
            var testContext = _appContextManager.SetUpTestContext(NUnitParamReader.GetTargetApp());
            _objectContainer.RegisterInstanceAs(testContext);

            var _saucelabsSettings = SaucelabsHook.GetSauceLabsSettings(_appContextManager.ConfigRoot);
            _objectContainer.RegisterInstanceAs(_saucelabsSettings);
        }
    }
}
