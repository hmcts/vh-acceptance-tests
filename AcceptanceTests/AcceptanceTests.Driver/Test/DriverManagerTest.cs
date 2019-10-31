using System;
using AcceptanceTests.Driver.Settings;
using AcceptanceTests.Driver.Support;
using Coypu;
using FluentAssertions;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace AcceptanceTests.Driver.Test
{
    [TestFixture]
    public class DriverManagerTest
    {
        private const string BaseUrl = "https://vh-admin-web-api-dev.hearings.reform.hmcts.net/";
        private BrowserSession _session;
        private SauceLabsSettings _saucelabsSettings;
        private ScenarioInfo _scenarioInfo;
        private string _buildName = "Acceptance Tests: Driver Tests";
        private const bool BlockCamAndMicrophone = false;

        private const PlatformSupport defaultPlatform = PlatformSupport.Desktop;

        [SetUp]
        public void SetUp()
        {
            _saucelabsSettings = new SauceLabsSettings();
            var tags = new string[] { "integrationTests", "DriverManagerTest" };
            _scenarioInfo = new ScenarioInfo("Acceptance Test Framework : Driver Tests", "Testing driver integration tests", tags);
        }

        [TearDown]
        public void TearDown()
        {
            if (_session != null)
                _session.Dispose();
        }

        [TestCase("Firefox")]
        [TestCase("Safari")]
        [TestCase("Chrome")]
        public void InitLocalBrowserDesktopSuccessTest(string browser)
        {
            var driverManager = new DriverManager();
            _session = driverManager.Init(BaseUrl, browser, defaultPlatform, _scenarioInfo, _buildName, BlockCamAndMicrophone, _saucelabsSettings);
            _session.Should().NotBeNull();
            _session.Driver.Window.Title.Should().NotBeNull();
        }

        [TestCase("Firefox")]
        [TestCase("Safari")]
        [TestCase("Chrome")]
        public void InitLocalBrowserDesktopBlockCamAndMicSuccessTest(string browser)
        {
            var driverManager = new DriverManager();
            _session = driverManager.Init(BaseUrl, browser, defaultPlatform, _scenarioInfo, _buildName, true, _saucelabsSettings);
            _session.Should().NotBeNull();
            _session.Driver.Window.Title.Should().NotBeNull();
        }

        [TestCase("InternetExplorer")]
        [TestCase("Opera")]
        public void InitBrowserNotSupportedDesktopThrowsExceptionTest(string browser)
        {
            var driverManager = new DriverManager();
            Assert.Throws<NotSupportedException>(() =>
            {
                driverManager.Init(BaseUrl, browser, defaultPlatform, _scenarioInfo, _buildName, BlockCamAndMicrophone, _saucelabsSettings);

            });
        }

        [Test]
        public void InitBrowserNotSupportedMobileThrowsExceptionTest()
        {
            var driverManager = new DriverManager();
            Assert.Throws<PlatformNotSupportedException>(() =>
            {
                driverManager.Init(BaseUrl, BrowserSupport.Chrome.ToString(), PlatformSupport.Mobile, _scenarioInfo, _buildName, BlockCamAndMicrophone, _saucelabsSettings);
            });
        }
    }
}
