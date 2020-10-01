using System;
using System.Threading;
using AcceptanceTests.Common.Configuration;
using AcceptanceTests.Common.Driver.Drivers;
using AcceptanceTests.Common.Driver.Enums;
using AcceptanceTests.Common.Driver.Settings;
using AcceptanceTests.Tests.Helpers;
using FluentAssertions;
using NUnit.Framework;

namespace AcceptanceTests.Tests.DriverTests
{
    public class DesktopSauceLabs
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
                Name = $"Driver Tests - Desktop"
            };
        }

        private UserBrowser GetBrowser(TargetOS targetOS, TargetBrowser targetBrowser)
        {
            return new UserBrowser()
                .SetBaseUrl(_config.Url)
                .SetTargetDevice(TargetDevice.Desktop)
                .SetTargetBrowser(targetBrowser)
                .SetDriver(new DriverSetup(GetSauceLabsSettings(), GetDriverOptions(targetOS, targetBrowser), GetSauceLabsOptions()));
        }


        private static DriverOptions GetDriverOptions(TargetOS targetOS, TargetBrowser targetBrowser)
        {
            return new DriverOptions
            {
                TargetDevice = TargetDevice.Desktop,
                TargetOS = targetOS,
                TargetBrowser = targetBrowser,
                TargetBrowserVersion = "latest"
            };
        }

        [Test]
        public void Win_Chrome()
        {
            _browser = GetBrowser(TargetOS.Windows, TargetBrowser.Chrome);
            RunTest();
        }

        [Test]
        public void Win_Edge()
        {
            _browser = GetBrowser(TargetOS.Windows, TargetBrowser.Edge);
            RunTest();
        }

        [Test]
        public void Win_EdgeChromium()
        {
            _browser = GetBrowser(TargetOS.Windows, TargetBrowser.EdgeChromium);
            RunTest();
        }

        [Test]
        public void Win_Firefox()
        {
            _browser = GetBrowser(TargetOS.Windows, TargetBrowser.Firefox);
            RunTest();
        }

        [Test]
        public void Win_IE11()
        {
            _browser = GetBrowser(TargetOS.Windows, TargetBrowser.Ie11);
            RunIETest();
        }

        [Test]
        public void MacOS_Chrome()
        {
            _browser = GetBrowser(TargetOS.MacOs, TargetBrowser.Chrome);
            RunTest();
        }

        [Test]
        public void MacOS_Firefox()
        {
            _browser = GetBrowser(TargetOS.MacOs, TargetBrowser.Firefox);
            RunTest();
        }

        [Test]
        public void MacOS_Safari()
        {
            _browser = GetBrowser(TargetOS.MacOs, TargetBrowser.Safari);
            RunTest();
        }

        private void RunTest()
        {
            _browser.LaunchBrowser();
            _browser.NavigateToPage(_config.Url);
            Thread.Sleep(TimeSpan.FromSeconds(4));
            _browser.AngularDriver.Title.Should().Contain("Sign in to your account");
        }

        private void RunIETest()
        {
            _browser.LaunchBrowser();
            _browser.NavigateToPage(_config.Url);
            Thread.Sleep(TimeSpan.FromSeconds(4));
            _browser.AngularDriver.Title.Should().Contain("Video Hearings");
        }

        [TearDown]
        public void LogResult()
        {
            _browser.BrowserTearDown();

            DriverManager.LogTestResult(_sauceLabsSettings != null, _browser.AngularDriver, TestContext.CurrentContext.Result.FailCount == 0);
        }

        [TearDown]
        public void CloseDriver()
        {
            _browser?.BrowserTearDown();
        }
    }
}
