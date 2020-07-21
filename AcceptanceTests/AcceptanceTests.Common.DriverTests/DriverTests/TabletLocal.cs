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
    public class TabletLocal
    {
        private TestConfiguration _config;
        private UserBrowser _browser;

        [SetUp]
        public void GetUserSecrets()
        {
            _config = Hooks.GetUserSecrets();
        }

        [Test]
        public void IOS_Tablet_Real_Device()
        {
            var driverOptions = new DriverOptions
            {
                RealDevice = true,
                TargetDevice = TargetDevice.Tablet,
                TargetOS = TargetOS.iOS,
                TargetBrowser = TargetBrowser.Safari,
                TargetDeviceOrientation = TargetDeviceOrientation.LANDSCAPE
            };

            _browser = new UserBrowser()
                .SetBaseUrl(_config.Url)
                .SetTargetDevice(TargetDevice.Tablet)
                .SetTargetBrowser(TargetBrowser.Safari)
                .SetDriver(new DriverSetup(new SauceLabsSettingsConfig(), driverOptions, new SauceLabsOptions()));

            RunTest();
        }

        [Test]
        public void IOS_Tablet_Simulator()
        {
            var driverOptions = new DriverOptions
            {
                RealDevice = false,
                TargetDevice = TargetDevice.Tablet,
                TargetOS = TargetOS.iOS,
                TargetBrowser = TargetBrowser.Safari,
                TargetDeviceName = _config.IOSTabletDeviceName,
                TargetDeviceOrientation = TargetDeviceOrientation.LANDSCAPE
            };

            _browser = new UserBrowser()
                .SetBaseUrl(_config.Url)
                .SetTargetDevice(TargetDevice.Tablet)
                .SetTargetBrowser(TargetBrowser.Safari)
                .SetDriver(new DriverSetup(new SauceLabsSettingsConfig(), driverOptions, new SauceLabsOptions()));

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
                TargetDeviceName = _config.AndroidTabletDeviceName,
                TargetDeviceOrientation = TargetDeviceOrientation.LANDSCAPE
            };

            _browser = new UserBrowser()
                .SetBaseUrl(_config.Url)
                .SetTargetDevice(TargetDevice.Tablet)
                .SetTargetBrowser(TargetBrowser.Chrome)
                .SetDriver(new DriverSetup(new SauceLabsSettingsConfig(), driverOptions, new SauceLabsOptions()));

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
        public void CloseDriver()
        {
            _browser?.BrowserTearDown();
        }
    }
}
