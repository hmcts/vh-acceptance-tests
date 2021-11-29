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
using NUnit.Framework.Internal;

namespace AcceptanceTests.Tests.DriverTests
{
    public class MobileLocal
    {
        private TestConfiguration _config;
        private UserBrowser _browser;

        [SetUp]
        public void GetUserSecrets()
        {
            _config = Hooks.GetUserSecrets();
        }

        private UserBrowser GetBrowser(TargetOS targetOS, TargetBrowser targetBrowser, string deviceName)
        {
            return new UserBrowser()
                .SetBaseUrl(_config.Url)
                .SetTargetDevice(TargetDevice.Mobile)
                .SetTargetBrowser(targetBrowser)
                .SetDriver(new DriverSetup(new SauceLabsSettingsConfig(), GetDriverOptions(targetOS, targetBrowser, deviceName), new SauceLabsOptions()));
        }

        private static DriverOptions GetDriverOptions(TargetOS targetOS, TargetBrowser targetBrowser, string deviceName)
        {
            return new DriverOptions
            {
                TargetDevice = TargetDevice.Mobile,
                TargetOS = targetOS,
                TargetBrowser = targetBrowser,
                TargetDeviceName = deviceName,
                TargetDeviceOrientation = TargetDeviceOrientation.PORTRAIT
            };
        }

        [Test]
        public void IOS_Mobile()
        {
            _browser = GetBrowser(TargetOS.iOS, TargetBrowser.Safari, _config.IOSMobileDeviceName);
            RunTest();
        }

        [Test]
        public void Android_Mobile()
        {
            _browser = GetBrowser(TargetOS.Android, TargetBrowser.Chrome, _config.AndroidMobileDeviceName);
            RunTest();
        }

        private void RunTest()
        {
            _browser.LaunchBrowser();
            _browser.NavigateToPage(_config.Url);
            //Thread.Sleep(TimeSpan.FromSeconds(4));    /* unable to see why waiting for 4 seconds on a specific device is necessary. WaitTime logic more ideal? */
            _browser.Driver.Title.Should().Contain("Sign in to your account");
        }

        [TearDown]
        public void CloseDriver()
        {
            _browser?.BrowserTearDown();
        }
    }
}
