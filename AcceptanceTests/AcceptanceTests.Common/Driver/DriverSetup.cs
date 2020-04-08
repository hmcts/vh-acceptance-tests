using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AcceptanceTests.Common.Configuration;
using AcceptanceTests.Common.Driver.Strategies;
using AcceptanceTests.Common.Driver.Strategies.Desktop.Mac;
using AcceptanceTests.Common.Driver.Strategies.Desktop.Windows;
using AcceptanceTests.Common.Driver.Strategies.Mobile.Android;
using AcceptanceTests.Common.Driver.Strategies.Mobile.iOS;
using AcceptanceTests.Common.Driver.Strategies.Tablet.Android;
using AcceptanceTests.Common.Driver.Strategies.Tablet.iOS;
using AcceptanceTests.Common.Driver.Support;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using TechTalk.SpecFlow;
using DriverOptions = AcceptanceTests.Common.Driver.Support.DriverOptions;

namespace AcceptanceTests.Common.Driver
{
    public class DriverSetup
    {
        private const string WindowsScreenResolution = "2560x1600";
        private const string MacScreenResolution = "2360x1770";
        private const int SauceLabsIdleTimeoutInSeconds = 60 * 30;
        private const int SauceLabsCommandTimeoutInSeconds = 60 * 3;
        private const int LocalCommandTimeoutInSeconds = 20;
        private const string SauceLabSeleniumVersion = "3.141.59";
        private const string SauceLabsMacPlatformVersion = "macOS 10.15";
        private const string Timezone = "London";
        private readonly SauceLabsSettingsConfig _sauceLabsSettings;
        private readonly ScenarioInfo _scenario;
        private static TargetBrowser _targetBrowser;
        private static TargetDevice _targetDevice;
        private static EdgeDriverService _edgeService;

        public DriverSetup(SauceLabsSettingsConfig sauceLabsSettings, ScenarioInfo scenario, DriverOptions driverOptions)
        {
            _sauceLabsSettings = sauceLabsSettings;
            _scenario = scenario;
            _targetBrowser = driverOptions.TargetBrowser;
            _targetDevice = driverOptions.TargetDevice;
        }

        public IWebDriver GetDriver(string filename)
        {
            return _sauceLabsSettings.RunningOnSauceLabs() ? InitialiseSauceLabsDriver(_scenario) : InitialiseLocalDriver(filename, _scenario);
        }

        private IWebDriver InitialiseSauceLabsDriver(ScenarioInfo scenario)
        {
            var releaseDefinitionName = Environment.GetEnvironmentVariable("Release_DefinitionName");
            var releaseName = Environment.GetEnvironmentVariable("RELEASE_RELEASENAME");
            var shortReleaseDefinitionName = releaseDefinitionName?.Replace("vh-", "");
            var attemptNumber = GetAttemptNumber();
            var sauceOptions = new Dictionary<string, object>
            {
                {"username", _sauceLabsSettings.Username},
                {"accessKey", _sauceLabsSettings.AccessKey},
                {"name", _scenario.Title},
                {"build", $"{shortReleaseDefinitionName} {releaseName} {_targetDevice} {_targetBrowser} {attemptNumber}"},
                {"idleTimeout", SauceLabsIdleTimeoutInSeconds},
                {"seleniumVersion", SauceLabSeleniumVersion},
                {"timeZone", Timezone }
            };

            AddScreenResolutionForDesktop(sauceOptions);

            var drivers = GetDrivers();
            drivers[_targetBrowser].BlockedCamAndMic = scenario.Tags.Contains("Blocked");
            drivers[_targetBrowser].LoggingEnabled = scenario.Tags.Contains("LoggingEnabled");
            drivers[_targetBrowser].IdleTimeout = TimeSpan.FromSeconds(SauceLabsIdleTimeoutInSeconds);
            drivers[_targetBrowser].MacPlatform = SauceLabsMacPlatformVersion;
            drivers[_targetBrowser].SauceLabsTimeout = TimeSpan.FromSeconds(SauceLabsCommandTimeoutInSeconds);
            drivers[_targetBrowser].SauceOptions = sauceOptions;
            drivers[_targetBrowser].Uri = new Uri(_sauceLabsSettings.RemoteServerUrl);
            return drivers[_targetBrowser].InitialiseForSauceLabs();
        }

        private static void AddScreenResolutionForDesktop(IDictionary<string, object> sauceOptions)
        {
            if (_targetDevice != TargetDevice.Desktop) return;
            var resolution = _targetBrowser == TargetBrowser.Safari
                ? MacScreenResolution
                : WindowsScreenResolution;
            sauceOptions.Add("screenResolution", resolution);
        }

        private static string GetAttemptNumber()
        {
            var attemptNumber = Environment.GetEnvironmentVariable("Release_AttemptNumber");
            if (string.IsNullOrWhiteSpace(attemptNumber)) return string.Empty;
            return Convert.ToInt32(attemptNumber) > 1 ? attemptNumber : string.Empty;
        }

        private static IWebDriver InitialiseLocalDriver(string filename, ScenarioInfo scenario)
        {
            var drivers = GetDrivers();
            drivers[_targetBrowser].BlockedCamAndMic = scenario.Tags.Contains("Blocked");
            drivers[_targetBrowser].LoggingEnabled = scenario.Tags.Contains("LoggingEnabled");
            drivers[_targetBrowser].BuildPath = _targetBrowser == TargetBrowser.Safari ? "/usr/bin/" : Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            drivers[_targetBrowser].Filename = filename;
            drivers[_targetBrowser].LocalTimeout = TimeSpan.FromSeconds(LocalCommandTimeoutInSeconds);
            drivers[_targetBrowser].SauceLabsTimeout = TimeSpan.FromSeconds(SauceLabsCommandTimeoutInSeconds);
            drivers[_targetBrowser].UseVideoFiles = scenario.Tags.Contains("Video");

            if (_targetBrowser == TargetBrowser.EdgeChromium)
            {
                StartLocalEdgeChromiumService();
                drivers[_targetBrowser].Uri = _edgeService.ServiceUrl;
            }

            return drivers[_targetBrowser].InitialiseForLocal();
        }

        private static Dictionary<TargetBrowser, Drivers> GetDrivers()
        {
            return _targetDevice switch
            {
                TargetDevice.Desktop => GetDesktopDrivers(),
                TargetDevice.Tablet => GetTabletDrivers(),
                TargetDevice.Mobile => GetMobileDrivers(),
                _ => GetDesktopDrivers()
            };
        }

        private static Dictionary<TargetBrowser, Drivers> GetDesktopDrivers()
        {
            var drivers = new Dictionary<TargetBrowser, Drivers>
            {
                {TargetBrowser.Chrome, new ChromeWindowsDriverStrategy()},
                {TargetBrowser.Edge, new EdgeDriverStrategy()},
                {TargetBrowser.EdgeChromium, new EdgeChromiumWindowsDriverStrategy()},
                {TargetBrowser.Firefox, new FirefoxWindowsDriverStrategy()},
                {TargetBrowser.Ie11, new InternetExplorerDriverStrategy()},
                {TargetBrowser.MacChrome, new ChromeMacDriverStrategy()},
                {TargetBrowser.MacFirefox, new FirefoxMacDriverStrategy()},
                {TargetBrowser.Safari, new SafariDriverStrategy()}
            };
            return drivers;
        }

        private static Dictionary<TargetBrowser, Drivers> GetTabletDrivers()
        {
            var drivers = new Dictionary<TargetBrowser, Drivers>
            {
                {TargetBrowser.AndroidTabletChrome, new ChromeAndroidTabletDriverStrategy()},
                {TargetBrowser.iOSTabletChrome, new ChromeiOSTabletDriverStrategy()},
                {TargetBrowser.iOSTabletSafari, new SafariTabletDriverStrategy()}
            };
            return drivers;
        }

        private static Dictionary<TargetBrowser, Drivers> GetMobileDrivers()
        {
            var drivers = new Dictionary<TargetBrowser, Drivers>
            {
                {TargetBrowser.AndroidMobileChrome, new ChromeAndroidMobileDriverStrategy()},
                {TargetBrowser.iOSMobileChrome, new ChromeiOSMobileDriverStrategy()},
                {TargetBrowser.iOSMobileSafari, new SafariiOSMobileDriverStrategy()}
            };
            return drivers;
        }

        public static void StartLocalEdgeChromiumService()
        {
            _edgeService = EdgeDriverService.CreateDefaultService(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"msedgedriver.exe");
            _edgeService.UseVerboseLogging = true;
            _edgeService.UseSpecCompliantProtocol = true;
            _edgeService.Start();
        }

        public void StopLocalEdgeChromiumService()
        {
            _edgeService?.Dispose();
        }
    }
}
