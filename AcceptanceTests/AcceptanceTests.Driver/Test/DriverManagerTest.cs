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
        private const string ScenarioTitle = "Acceptance Test Framework : Driver Tests";
        private const bool BlockCamAndMicrophone = false;

        private const PlatformSupport defaultPlatform = PlatformSupport.Desktop;

        [SetUp]
        public void SetUp()
        {
            _saucelabsSettings = new SauceLabsSettings();
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
        [TestCase("Edge")]
        public void InitLocalBrowserDesktopSuccessTest(string browser)
        {
            var driverManager = new DriverManager();
            _session = driverManager.Init(BaseUrl, browser, defaultPlatform, DeviceSupport.Pc, ScenarioTitle, BlockCamAndMicrophone, _saucelabsSettings);
            _session.Should().NotBeNull();
            _session.Driver.Window.Title.Should().NotBeNull();
        }

        [TestCase("Firefox")]
        [TestCase("Safari")]
        [TestCase("Chrome")]
        [TestCase("Edge")]
        public void InitLocalBrowserDesktopBlockCamAndMicSuccessTest(string browser)
        {
            var driverManager = new DriverManager();
            _session = driverManager.Init(BaseUrl, browser, defaultPlatform, DeviceSupport.Pc, ScenarioTitle, true, _saucelabsSettings);
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
                driverManager.Init(BaseUrl, browser, defaultPlatform, DeviceSupport.Pc, ScenarioTitle, BlockCamAndMicrophone, _saucelabsSettings);

            });
        }

        [TestCase("Ipad")]
        [TestCase("Iphone")]
        public void InitBrowserSupportedMobileSuccessTest(DeviceSupport device)
        {
            var driverManager = new DriverManager();
            _session = driverManager.Init(BaseUrl, BrowserSupport.Safari.ToString(), PlatformSupport.Mobile, device, ScenarioTitle, BlockCamAndMicrophone, _saucelabsSettings);
            _session.Should().NotBeNull();
            _session.Driver.Window.Title.Should().NotBeNull();
        }

        [Test]
        public void InitBrowserNotSupportedMobileThrowsExceptionTest()
        {
            var driverManager = new DriverManager();
            Assert.Throws<NotSupportedException>(() =>
            {
                driverManager.Init(BaseUrl, BrowserSupport.Chrome.ToString(), PlatformSupport.Mobile, DeviceSupport.Samsung, ScenarioTitle, BlockCamAndMicrophone, _saucelabsSettings);
            });
        }
    }
}
