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

        private UserBrowser GetBrowser(TargetOS targetOS, TargetBrowser targetBrowser, string browserVersion = "latest")
        {
            return new UserBrowser()
                .SetBaseUrl(_config.Url)
                .SetTargetDevice(TargetDevice.Desktop)
                .SetTargetBrowser(targetBrowser)
                .SetDriver(new DriverSetup(GetSauceLabsSettings(), GetDriverOptions(targetOS, targetBrowser, browserVersion), GetSauceLabsOptions()));
        }


        private static DriverOptions GetDriverOptions(TargetOS targetOS, TargetBrowser targetBrowser, string browserVersion)
        {
            return new DriverOptions
            {
                TargetDevice = TargetDevice.Desktop,
                TargetOS = targetOS,
                TargetBrowser = targetBrowser,
                TargetBrowserVersion = browserVersion
            };
        }

        [Test]
        public void Latest_Win_Chrome()
        {
            _browser = GetBrowser(TargetOS.Windows, TargetBrowser.Chrome);
            RunTest();
        }

        [Test]
        public void Latest_Win_Edge()
        {
            _browser = GetBrowser(TargetOS.Windows, TargetBrowser.Edge);
            RunTest();
        }

        [Test]
        public void Latest_Win_EdgeChromium()
        {
            _browser = GetBrowser(TargetOS.Windows, TargetBrowser.EdgeChromium);
            RunTest();
        }

        [Test]
        public void Latest_Win_Firefox()
        {
            _browser = GetBrowser(TargetOS.Windows, TargetBrowser.Firefox);
            RunTest();
        }

        [Test]
        public void Latest_Win_IE11()
        {
            _browser = GetBrowser(TargetOS.Windows, TargetBrowser.Ie11);
            RunIETest();
        }

        [Test]
        public void Latest_MacOS_Chrome()
        {
            _browser = GetBrowser(TargetOS.MacOs, TargetBrowser.Chrome);
            RunTest();
        }

        [Test]
        public void Latest_MacOS_Firefox()
        {
            _browser = GetBrowser(TargetOS.MacOs, TargetBrowser.Firefox);
            RunTest();
        }

        [Test]
        public void Latest_MacOS_Safari()
        {
            _browser = GetBrowser(TargetOS.MacOs, TargetBrowser.Safari);
            RunTest();
        }

        [Test]
        public void Latest_MacOS_EdgeChromium()
        {
            _browser = GetBrowser(TargetOS.MacOs, TargetBrowser.EdgeChromium);
            RunTest();
        }

        [Test]
        public void Beta_Win_Chrome()
        {
            _browser = GetBrowser(TargetOS.Windows, TargetBrowser.Chrome, "Beta");
            RunTest();
        }

        [Test]
        public void Beta_Win_EdgeChromium()
        {
            _browser = GetBrowser(TargetOS.Windows, TargetBrowser.EdgeChromium, "Beta");
            RunTest();
        }

        [Test]
        public void Beta_Win_Firefox()
        {
            _browser = GetBrowser(TargetOS.Windows, TargetBrowser.Firefox, "Beta");
            RunTest();
        }

        [Test]
        public void Beta_MacOS_Chrome()
        {
            _browser = GetBrowser(TargetOS.MacOs, TargetBrowser.Chrome, "Beta");
            RunTest();
        }

        [Test]
        public void Beta_MacOS_Firefox()
        {
            _browser = GetBrowser(TargetOS.MacOs, TargetBrowser.Firefox, "Beta");
            RunTest();
        }

        [Test]
        public void Beta_MacOS_EdgeChromium()
        {
            _browser = GetBrowser(TargetOS.MacOs, TargetBrowser.EdgeChromium, "Beta");
            RunTest();
        }

        private void RunTest()
        {
            _browser.LaunchBrowser();
            _browser.NavigateToPage(_config.Url);
            //Thread.Sleep(TimeSpan.FromSeconds(4));    /* unable to see why waiting for 4 seconds on a specific device is necessary. WaitTime logic more ideal? */
            _browser.Driver.Title.Should().Contain("Sign in to your account");
        }

        private void RunIETest()
        {
            _browser.LaunchBrowser();
            _browser.NavigateToPage(_config.Url);
            //Thread.Sleep(TimeSpan.FromSeconds(4));    /* unable to see why waiting for 4 seconds on a specific browser is necessary. WaitTime logic more ideal? */
            _browser.Driver.Title.Should().Contain("Video Hearings");
        }

        [TearDown]
        public void LogResult()
        {
            _browser?.BrowserTearDown();

            if (_browser != null)
                DriverManager.LogTestResult(_sauceLabsSettings != null, _browser.Driver, TestContext.CurrentContext.Result.FailCount == 0);
        }

        [TearDown]
        public void CloseDriver()
        {
            _browser?.BrowserTearDown();
        }
    }
}
