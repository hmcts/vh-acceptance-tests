using TechTalk.SpecFlow;
using BoDi;
using System.Reflection;
using System.IO;

namespace AcceptanceTests.SpecflowTests.Common.Hooks
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
            var targetApp = _appContextManager.GetTargetApp();
            var path = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/SpecflowTests/{targetApp}/Resources";
            OneTimeSetup(path);
        }

        public void OneTimeSetup(string path)
        {
            var testContext = _appContextManager.SetUpTestContext(path);
            _objectContainer.RegisterInstanceAs(testContext);

            var _saucelabsSettings = SaucelabsHook.GetSauceLabsSettings(_appContextManager.ConfigRoot);
            _objectContainer.RegisterInstanceAs(_saucelabsSettings);
        }
    }
}
