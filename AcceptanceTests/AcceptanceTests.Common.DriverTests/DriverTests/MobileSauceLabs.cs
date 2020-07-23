using System;
using System.Threading;
using AcceptanceTests.Common.Configuration;
using AcceptanceTests.Common.Driver.Drivers;
using AcceptanceTests.Common.Driver.Enums;
using AcceptanceTests.Common.Driver.Settings;
using AcceptanceTests.DriverTests.Helpers;
using FluentAssertions;
using NUnit.Framework;

namespace AcceptanceTests.DriverTests.DriverTests
{
    public class MobileSauceLabs
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
                Name = $"Driver Tests - Mobiles"
            };
        }

        private UserBrowser GetBrowser(TargetOS targetOS, TargetBrowser targetBrowser)
        {
            return new UserBrowser()
                .SetBaseUrl(_config.Url)
                .SetTargetDevice(TargetDevice.Mobile)
                .SetTargetBrowser(targetBrowser)
                .SetDriver(new DriverSetup(GetSauceLabsSettings(), GetDriverOptions(targetOS, targetBrowser), GetSauceLabsOptions()));
        }

        private static DriverOptions GetDriverOptions(TargetOS targetOS, TargetBrowser targetBrowser)
        {
            return new DriverOptions
            {
                TargetDevice = TargetDevice.Mobile,
                TargetOS = targetOS,
                TargetBrowser = targetBrowser,
                TargetDeviceOrientation = TargetDeviceOrientation.PORTRAIT
            };
        }

        [Test]
        public void IOS_Mobile()
        {
            _browser = GetBrowser(TargetOS.iOS, TargetBrowser.Safari);
            RunTest();
        }

        [Test]
        public void Android_Mobile()
        {
            _browser = GetBrowser(TargetOS.Android, TargetBrowser.Chrome);
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
