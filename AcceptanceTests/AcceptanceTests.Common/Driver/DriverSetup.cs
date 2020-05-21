using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using AcceptanceTests.Common.AudioRecordings;
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
using DriverOptions = AcceptanceTests.Common.Driver.Support.DriverOptions;

namespace AcceptanceTests.Common.Driver
{
    public class DriverSetup
    {
        private readonly SauceLabsSettingsConfig _sauceLabsSettings;
        private static Proxy _proxy;
        private static DriverOptions _driverOptions;
        private static SauceLabsOptions _sauceLabsOptions;
        private static TargetBrowser _targetBrowser;
        private static TargetDevice _targetDevice;
        private static EdgeDriverService _edgeService;

        public DriverSetup(SauceLabsSettingsConfig sauceLabsSettings, DriverOptions driverOptions, SauceLabsOptions sauceLabsOptions, Proxy proxy = null)
        {
            _sauceLabsSettings = sauceLabsSettings;
            _driverOptions = driverOptions; 
            _targetBrowser = driverOptions.TargetBrowser;
            _targetDevice = driverOptions.TargetDevice;
            _sauceLabsOptions = sauceLabsOptions;
            _proxy = proxy;
        }

        public IWebDriver GetDriver()
        {
            return _sauceLabsSettings.RunningOnSauceLabs() ? InitialiseSauceLabsDriver() : InitialiseLocalDriver();
        }

        private IWebDriver InitialiseSauceLabsDriver()
        {
            var releaseDefinitionName = GetReleaseDefinition();
            var releaseName = Environment.GetEnvironmentVariable("RELEASE_RELEASENAME");
            var attemptNumber = GetAttemptNumber();
            var build = $"{releaseDefinitionName} {releaseName} {_targetDevice} {_targetBrowser} {_sauceLabsOptions.BrowserVersion} {attemptNumber}";
            var sauceOptions = new Dictionary<string, object>
            {
                {"username", _sauceLabsSettings.Username},
                {"accessKey", _sauceLabsSettings.AccessKey},
                {"name", _sauceLabsOptions.Title},
                {"build", build},
                {"commandTimeout", _sauceLabsOptions.CommandTimeoutInSeconds},
                {"idleTimeout", _sauceLabsOptions.IdleTimeoutInSeconds},
                {"maxDuration", _sauceLabsOptions.MaxDurationInSeconds},
                {"seleniumVersion", _sauceLabsOptions.SeleniumVersion},
                {"timeZone", _sauceLabsOptions.Timezone }
            };

            AddScreenResolutionForDesktop(sauceOptions, _sauceLabsOptions);
            SetBrowserVersion(_targetBrowser, _sauceLabsOptions.BrowserVersion);

            var drivers = GetDrivers();
            drivers[_targetBrowser].BrowserVersions = _sauceLabsOptions.BrowserVersions;
            drivers[_targetBrowser].LoggingEnabled = _sauceLabsOptions.EnableLogging;
            drivers[_targetBrowser].MacPlatform = _sauceLabsOptions.MacPlatformVersion;
            drivers[_targetBrowser].SauceOptions = sauceOptions;
            drivers[_targetBrowser].Uri = new Uri(_sauceLabsSettings.RemoteServerUrl);
            return drivers[_targetBrowser].InitialiseForSauceLabs();
        }

        private static string GetReleaseDefinition()
        {
            var definition = Environment.GetEnvironmentVariable("Release_DefinitionName")?.Replace("vh-", "") ?? $"{DateTime.Today:dd.MM.yyyy}";
            return definition.ToCamelCase(new CultureInfo("en-GB", false));
        }

        private static void AddScreenResolutionForDesktop(IDictionary<string, object> sauceOptions, SauceLabsOptions sauceLabsOptions)
        {
            if (_targetDevice != TargetDevice.Desktop) return;
            var resolution = _targetBrowser == TargetBrowser.Safari
                ? sauceLabsOptions.MacScreenResolution
                : sauceLabsOptions.WindowsScreenResolution;
            sauceOptions.Add("screenResolution", resolution);
        }

        private static void SetBrowserVersion(TargetBrowser targetBrowser, string browserVersion)
        {
            _sauceLabsOptions.BrowserVersions = new BrowserVersions();
            _ = (targetBrowser switch
            {
                TargetBrowser.Firefox => _sauceLabsOptions.BrowserVersions.Firefox = browserVersion,
                TargetBrowser.Chrome => _sauceLabsOptions.BrowserVersions.Chrome = browserVersion,
                TargetBrowser.EdgeChromium => _sauceLabsOptions.BrowserVersions.EdgeChromium = browserVersion,
                TargetBrowser.MacChrome => _sauceLabsOptions.BrowserVersions.ChromeMac = browserVersion,
                TargetBrowser.MacFirefox => _sauceLabsOptions.BrowserVersions.FirefoxMac = browserVersion,
                TargetBrowser.Safari => _sauceLabsOptions.BrowserVersions.Safari = browserVersion,
                TargetBrowser.Ie11 => _sauceLabsOptions.BrowserVersions.InternetExplorer = browserVersion,
                TargetBrowser.Edge => _sauceLabsOptions.BrowserVersions.Edge = browserVersion,
                TargetBrowser.AndroidMobileChrome => _sauceLabsOptions.BrowserVersions.AndroidMobileChrome,
                TargetBrowser.iOSMobileChrome => _sauceLabsOptions.BrowserVersions.iOSMobileChrome,
                TargetBrowser.iOSMobileSafari => _sauceLabsOptions.BrowserVersions.iOSMobileSafari,
                TargetBrowser.AndroidTabletChrome => _sauceLabsOptions.BrowserVersions.AndroidTabletChrome,
                TargetBrowser.iOSTabletChrome => _sauceLabsOptions.BrowserVersions.iOSTabletChrome,
                TargetBrowser.iOSTabletSafari => _sauceLabsOptions.BrowserVersions.iOSTabletSafari,
                TargetBrowser.Samsung => _sauceLabsOptions.BrowserVersions.Samsung,
                TargetBrowser.Default => _sauceLabsOptions.BrowserVersions.Chrome,
                _ => _sauceLabsOptions.BrowserVersions.Chrome
            });
        }

        private static string GetAttemptNumber()
        {
            var attemptNumber = Environment.GetEnvironmentVariable("Release_AttemptNumber");
            if (string.IsNullOrWhiteSpace(attemptNumber)) return string.Empty;
            return Convert.ToInt32(attemptNumber) > 1 ? attemptNumber : string.Empty;
        }

        private static IWebDriver InitialiseLocalDriver()
        {
            var drivers = GetDrivers();
            drivers[_targetBrowser].LoggingEnabled = false;
            drivers[_targetBrowser].BuildPath = FileManager.GetAssemblyDirectory();
            drivers[_targetBrowser].LocalTimeout = TimeSpan.FromSeconds(_driverOptions.LocalCommandTimeoutInSeconds);
            drivers[_targetBrowser].Proxy = _proxy; 
            drivers[_targetBrowser].HeadlessMode = _driverOptions.HeadlessMode;

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
