using System;
using System.Reflection;
using System.Threading;
using AcceptanceTests.Common.Configuration;
using AcceptanceTests.Common.Driver.Browser;
using AcceptanceTests.Common.Driver.Configuration;
using AcceptanceTests.Common.Driver.Drivers;
using AcceptanceTests.Common.Driver.Enums;
using AcceptanceTests.Common.Driver.Support;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace AcceptanceTests.Tests
{
    namespace Appium
    {
        public class TabletDriverTests
        {
            private NugetTestsConfiguration _config;
            private UserBrowser _browser;
            private string _remoteServer;
            private SauceLabsSettingsConfig _sauceLabsSettings;

            [SetUp]
            public void GetUserSecrets()
            {
                var configRootBuilder = new ConfigurationBuilder()
                    .AddJsonFile($"appsettings.json")
                    .AddUserSecrets("7c8eafa9-e05a-410b-aec2-ce368a920a7f");

                var config = configRootBuilder.Build();
                _config = Options.Create(config.GetSection("TestsConfiguration").Get<NugetTestsConfiguration>()).Value;
                _remoteServer = $"http://{_config.SauceLabsUsername}:{_config.SauceLabsAccessKey}{_config.ServerUrl}";
            }

            private SauceLabsSettingsConfig GetSauceLabsSettings()
            {
                _sauceLabsSettings = new SauceLabsSettingsConfig()
                {
                    Username = _config.SauceLabsUsername,
                    AccessKey = _config.SauceLabsAccessKey,
                    RemoteServerUrl = _remoteServer
                };
                return _sauceLabsSettings;
            }

            private static SauceLabsOptions GetSauceLabsOptions()
            {
                return new SauceLabsOptions()
                {
                    Name = $"NuGet Drivers Test"
                };
            }

            [Test]
            public void IOS_Tablet_Local_Test()
            {
                var driverOptions = new DriverOptions
                {
                    TargetDevice = TargetDevice.Tablet,
                    TargetOS = TargetOS.iOS,
                    TargetBrowser = TargetBrowser.Safari,
                    TargetDeviceName = _config.IOSDeviceName,
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
            public void Android_Tablet_Local_Test()
            {
                var driverOptions = new DriverOptions
                {
                    TargetDevice = TargetDevice.Tablet,
                    TargetOS = TargetOS.Android,
                    TargetBrowser = TargetBrowser.Chrome,
                    TargetDeviceName = _config.AndroidDeviceName,
                    TargetDeviceOrientation = TargetDeviceOrientation.PORTRAIT
                };

                _browser = new UserBrowser()
                    .SetBaseUrl(_config.Url)
                    .SetTargetDevice(TargetDevice.Tablet)
                    .SetTargetBrowser(TargetBrowser.Chrome)
                    .SetDriver(new DriverSetup(new SauceLabsSettingsConfig(), driverOptions, new SauceLabsOptions()));

                RunTest();
            }

            [Test]
            public void IOS_Tablet_Sauce_Labs_Test()
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
            public void Android_Tablet_Sauce_Labs_Test()
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
}
