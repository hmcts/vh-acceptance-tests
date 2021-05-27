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
    public class DesktopLocal
    {
        private TestConfiguration _config;
        private UserBrowser _browser;

        [SetUp]
        public void GetUserSecrets()
        {
            _config = Hooks.GetUserSecrets();
        }

        private UserBrowser GetBrowser(TargetOS targetOS, TargetBrowser targetBrowser)
        {
            return new UserBrowser()
                .SetBaseUrl(_config.Url)
                .SetTargetDevice(TargetDevice.Desktop)
                .SetTargetBrowser(targetBrowser)
                .SetDriver(new DriverSetup(new SauceLabsSettingsConfig(), GetDriverOptions(targetOS, targetBrowser), new SauceLabsOptions()));
        }

        private static DriverOptions GetDriverOptions(TargetOS targetOS, TargetBrowser targetBrowser)
        {
            return new DriverOptions
            {
                TargetBrowserVersion = "latest",
                TargetDevice = TargetDevice.Desktop,
                TargetOS = targetOS,
                TargetBrowser = targetBrowser
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
            _browser.Driver.Title.Should().Contain("Sign in to your account");
        }

        private void RunIETest()
        {
            _browser.LaunchBrowser();
            NUnit.Framework.TestContext.WriteLine($"Browser started, headin to page : {_config.Url}");
            _browser.NavigateToPage(_config.Url);
            Thread.Sleep(TimeSpan.FromSeconds(4));
            _browser.Driver.Title.Should().Contain("Video Hearings");
        }

        [TearDown]
        public void CloseDriver()
        {
            _browser?.BrowserTearDown();
        }
    }
}
