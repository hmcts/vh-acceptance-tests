using System;
using System.Threading;
using AcceptanceTests.Common.Configuration;
using AcceptanceTests.Common.Driver;
using AcceptanceTests.Common.Driver.Drivers;
using AcceptanceTests.Common.Driver.Enums;
using AcceptanceTests.Common.Driver.Settings;
using AcceptanceTests.Tests.Helpers;
using FluentAssertions;
using NUnit.Framework;

namespace AcceptanceTests.Tests.DriverTests
{
    public class TabletSauceLabs
    {
        private TestConfiguration _config;
        private UserBrowser _browser;
        private SauceLabsSettingsConfig _sauceLabsSettings;

        [SetUp]
        public void GetUserSecrets()
        {
            _config = Hooks.GetUserSecrets();
        }

        private SauceLabsSettingsConfig GetSauceLabsSettings()
        {
            _sauceLabsSettings = new SauceLabsSettingsConfig()
            {
                Username = _config.SauceLabsUsername,
                AccessKey = _config.SauceLabsAccessKey,
                RemoteServerUrl = _config.RemoteServer
            };
            return _sauceLabsSettings;
        }

        private static SauceLabsOptions GetSauceLabsOptions()
        {
            return new SauceLabsOptions()
            {
                Name = $"Driver Tests - Tablets"
            };
        }

        [Test]
        public void IOS_Tablet()
        {
            var driverOptions = new DriverOptions
            {
                TargetDevice = TargetDevice.Tablet,
                TargetOS = TargetOS.iOS,
                TargetBrowser = TargetBrowser.Safari,
                TargetDeviceOrientation = TargetDeviceOrientation.LANDSCAPE
            };

            _browser = new UserBrowser()
                .SetBaseUrl(_config.Url)
                .SetTargetDevice(TargetDevice.Tablet)
                .SetTargetBrowser(TargetBrowser.Safari)
                .SetDriver(new DriverSetup(GetSauceLabsSettings(), driverOptions, GetSauceLabsOptions()));

            RunTest();
        }

        [Test]
        public void Android_Tablet()
        {
            var driverOptions = new DriverOptions
            {
                TargetDevice = TargetDevice.Tablet,
                TargetOS = TargetOS.Android,
                TargetBrowser = TargetBrowser.Chrome,
                TargetDeviceOrientation = TargetDeviceOrientation.PORTRAIT
            };

            _browser = new UserBrowser()
                .SetBaseUrl(_config.Url)
                .SetTargetDevice(TargetDevice.Tablet)
                .SetTargetBrowser(TargetBrowser.Chrome)
                .SetDriver(new DriverSetup(GetSauceLabsSettings(), driverOptions, GetSauceLabsOptions()));

            RunTest();
        }

        private void RunTest()
        {
            _browser.LaunchBrowser();
            _browser.NavigateToPage(_config.Url);
            Thread.Sleep(TimeSpan.FromSeconds(4));
            _browser.Driver.Title.Should().Contain("Sign in to your account");
        }

        [TearDown]
        public void LogResult()
        {
            _browser.BrowserTearDown();

            DriverManager.LogTestResult(_sauceLabsSettings != null, _browser.Driver, TestContext.CurrentContext.Result.FailCount == 0);
        }

        [TearDown]
        public void CloseDriver()
        {
            _browser?.BrowserTearDown();
        }
    }
}
