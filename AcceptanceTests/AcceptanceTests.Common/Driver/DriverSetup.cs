using System;
using System.Collections.Generic;
using System.Globalization;
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
using RestSharp.Extensions;
using TechTalk.SpecFlow;
using DriverOptions = AcceptanceTests.Common.Driver.Support.DriverOptions;

namespace AcceptanceTests.Common.Driver
{
    public class DriverSetup
    {
        private readonly SauceLabsSettingsConfig _sauceLabsSettings;
        private readonly ScenarioInfo _scenario;
        private readonly Proxy _proxy;
        private static DriverOptions _driverOptions;
        private static TargetBrowser _targetBrowser;
        private static TargetDevice _targetDevice;
        private static EdgeDriverService _edgeService;

        public DriverSetup(SauceLabsSettingsConfig sauceLabsSettings, ScenarioInfo scenario, DriverOptions driverOptions, Proxy proxy = null)
        {
            _sauceLabsSettings = sauceLabsSettings;
            _scenario = scenario;
            _driverOptions = driverOptions; 
            _targetBrowser = driverOptions.TargetBrowser;
            _targetDevice = driverOptions.TargetDevice;
            _proxy = proxy;
        }

        public IWebDriver GetDriver()
        {
            return _sauceLabsSettings.RunningOnSauceLabs() ? InitialiseSauceLabsDriver(_scenario) : InitialiseLocalDriver(_scenario, _proxy);
        }

        private IWebDriver InitialiseSauceLabsDriver(ScenarioInfo scenario)
        {
            var releaseDefinitionName = GetReleaseDefinition();
            var releaseName = Environment.GetEnvironmentVariable("RELEASE_RELEASENAME");
            var attemptNumber = GetAttemptNumber();
            var build = $"{releaseDefinitionName} {releaseName} {_targetDevice} {_targetBrowser} {_driverOptions.BrowserVersion} {attemptNumber}";
            var sauceOptions = new Dictionary<string, object>
            {
                {"username", _sauceLabsSettings.Username},
                {"accessKey", _sauceLabsSettings.AccessKey},
                {"name", _scenario.Title},
                {"build", build},
                {"idleTimeout", _driverOptions.SauceLabsIdleTimeoutInSeconds},
                {"seleniumVersion", _driverOptions.SeleniumVersion},
                {"timeZone", _driverOptions.Timezone }
            };

            AddScreenResolutionForDesktop(sauceOptions, _driverOptions);
            SetBrowserVersion(_targetBrowser, _driverOptions.BrowserVersion);

            var drivers = GetDrivers();
            drivers[_targetBrowser].BlockedCamAndMic = scenario.Tags.Contains("Blocked");
            drivers[_targetBrowser].BrowserVersions = _driverOptions.BrowserVersions;
            drivers[_targetBrowser].LoggingEnabled = _driverOptions.EnableLogging;
            drivers[_targetBrowser].IdleTimeout = TimeSpan.FromSeconds(_driverOptions.SauceLabsIdleTimeoutInSeconds);
            drivers[_targetBrowser].MacPlatform = _driverOptions.MacPlatformVersion;
            drivers[_targetBrowser].SauceLabsTimeout = TimeSpan.FromSeconds(_driverOptions.SauceLabsCommandTimeoutInSeconds);
            drivers[_targetBrowser].SauceOptions = sauceOptions;
            drivers[_targetBrowser].Uri = new Uri(_sauceLabsSettings.RemoteServerUrl);
            return drivers[_targetBrowser].InitialiseForSauceLabs();
        }

        private static string GetReleaseDefinition()
        {
            var definition = Environment.GetEnvironmentVariable("Release_DefinitionName")?.Replace("vh-", "") ?? $"{DateTime.Today:dd.MM.yy}";
            return definition.ToCamelCase(new CultureInfo("en-GB", false));
        }

        private static void AddScreenResolutionForDesktop(IDictionary<string, object> sauceOptions, DriverOptions driverOptions)
        {
            if (_targetDevice != TargetDevice.Desktop) return;
            var resolution = _targetBrowser == TargetBrowser.Safari
                ? driverOptions.MacScreenResolution
                : driverOptions.WindowsScreenResolution;
            sauceOptions.Add("screenResolution", resolution);
        }

        private static void SetBrowserVersion(TargetBrowser targetBrowser, string browserVersion)
        {
            _driverOptions.BrowserVersions = new BrowserVersions();
            _ = (targetBrowser switch
            {
                TargetBrowser.Firefox => _driverOptions.BrowserVersions.Firefox = browserVersion,
                TargetBrowser.Chrome => _driverOptions.BrowserVersions.Chrome = browserVersion,
                TargetBrowser.EdgeChromium => _driverOptions.BrowserVersions.EdgeChromium = browserVersion,
                TargetBrowser.MacChrome => _driverOptions.BrowserVersions.ChromeMac = browserVersion,
                TargetBrowser.MacFirefox => _driverOptions.BrowserVersions.FirefoxMac = browserVersion,
                TargetBrowser.Safari => _driverOptions.BrowserVersions.Safari,
                TargetBrowser.Ie11 => _driverOptions.BrowserVersions.InternetExplorer = browserVersion,
                TargetBrowser.Edge => _driverOptions.BrowserVersions.Edge = browserVersion,
                TargetBrowser.AndroidMobileChrome => _driverOptions.BrowserVersions.AndroidMobileChrome,
                TargetBrowser.iOSMobileChrome => _driverOptions.BrowserVersions.iOSMobileChrome,
                TargetBrowser.iOSMobileSafari => _driverOptions.BrowserVersions.iOSMobileSafari,
                TargetBrowser.AndroidTabletChrome => _driverOptions.BrowserVersions.AndroidTabletChrome,
                TargetBrowser.iOSTabletChrome => _driverOptions.BrowserVersions.iOSTabletChrome,
                TargetBrowser.iOSTabletSafari => _driverOptions.BrowserVersions.iOSTabletSafari,
                TargetBrowser.Samsung => _driverOptions.BrowserVersions.Samsung,
                TargetBrowser.Default => _driverOptions.BrowserVersions.Chrome,
                _ => _driverOptions.BrowserVersions.Chrome
            });
        }

        private static string GetAttemptNumber()
        {
            var attemptNumber = Environment.GetEnvironmentVariable("Release_AttemptNumber");
            if (string.IsNullOrWhiteSpace(attemptNumber)) return string.Empty;
            return Convert.ToInt32(attemptNumber) > 1 ? attemptNumber : string.Empty;
        }

        private static IWebDriver InitialiseLocalDriver(ScenarioInfo scenario, Proxy proxy)
        {
            var drivers = GetDrivers();
            drivers[_targetBrowser].BlockedCamAndMic = scenario.Tags.Contains("Blocked");
            drivers[_targetBrowser].LoggingEnabled = scenario.Tags.Contains("LoggingEnabled");
            drivers[_targetBrowser].BuildPath = _targetBrowser == TargetBrowser.Safari ? "/usr/bin/" : Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            drivers[_targetBrowser].LocalTimeout = TimeSpan.FromSeconds(_driverOptions.LocalCommandTimeoutInSeconds);
            drivers[_targetBrowser].SauceLabsTimeout = TimeSpan.FromSeconds(_driverOptions.SauceLabsCommandTimeoutInSeconds);
            drivers[_targetBrowser].UseVideoFiles = scenario.Tags.Contains("Video");
            drivers[_targetBrowser].Proxy = proxy;

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
