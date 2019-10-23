using AcceptanceTests.Driver.Settings;
using AcceptanceTests.Model.Context;
using AcceptanceTests.Tests.Hooks;
using Coypu;
using FluentAssertions;
using NUnit.Framework;
using Protractor;

namespace AcceptanceTests.Tests.IntegrationTests
{
    public class DriverHookTests: HookTestsBase
    {
        private ITestContext _testContext;
        private SauceLabsSettings _saucelabsSettings;
        private BrowserSession _session;

        [SetUp]
        public void TestSetUp()
        {
            SetUp();
            _testContext = new TestContextBase();
            _testContext.BaseUrl = "https://www.google.com";
            _saucelabsSettings = new SauceLabsSettings();
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
        public void InitDriverHookTests()
        {
            var targetBrowser = NUnitParamReader.GetTargetBrowser();
            var targetApp = NUnitParamReader.GetTargetApp();
            DriverHook hook = new DriverHook(_saucelabsSettings, null, _testContext, _objectContainer);
            _session = hook.InitDriver();
            _session.Should().NotBeNull();
            _session.Driver.Native.As<NgWebDriver>().WrappedDriver.ToString().Should().Contain(targetBrowser.ToString());
            _testContext.CurrentApp.Should().Be(targetApp.ToString());
        }
    }
}
