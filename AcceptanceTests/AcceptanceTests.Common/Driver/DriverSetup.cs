using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AcceptanceTests.Common.Configuration;
using AcceptanceTests.Common.Driver.Strategies;
using AcceptanceTests.Common.Driver.Support;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace AcceptanceTests.Common.Driver
{
    public class DriverSetup
    {
        private readonly SauceLabsSettingsConfig _sauceLabsSettings;
        private readonly ScenarioInfo _scenario;
        private static TargetBrowser _targetBrowser;
        private const string WindowsScreenResolution = "2560x1600";
        private const string MacScreenResolution = "2360x1770";
        private const int SauceLabsIdleTimeoutInSeconds = 60 * 30;
        private const int SauceLabsCommandTimeoutInSeconds = 60 * 3;
        private const int LocalCommandTimeoutInSeconds = 20;
        private const string SauceLabSeleniumVersion = "3.141.59";
        private const string SauceLabsMacPlatformVersion = "macOS 10.14";

        public DriverSetup(SauceLabsSettingsConfig sauceLabsSettings, ScenarioInfo scenario, TargetBrowser targetBrowser)
        {
            _sauceLabsSettings = sauceLabsSettings;
            _scenario = scenario;
            _targetBrowser = targetBrowser;
        }

        public IWebDriver GetDriver(string filename)
        {
            return _sauceLabsSettings.RunningOnSauceLabs() ? InitialiseSauceLabsDriver() : InitialiseLocalDriver(filename, _scenario);
        }

        private IWebDriver InitialiseSauceLabsDriver()
        {
            var buildName = Environment.GetEnvironmentVariable("Build_DefinitionName");
            var releaseName = Environment.GetEnvironmentVariable("RELEASE_RELEASENAME");
            var shortBuildName = buildName?.Replace("hmcts.vh-", "");
            var sauceOptions = new Dictionary<string, object>
            {
                {"username", _sauceLabsSettings.Username},
                {"accessKey", _sauceLabsSettings.AccessKey},
                {"name", _scenario.Title},
                {"build", $"{shortBuildName} {releaseName} {_targetBrowser}"},
                {"idleTimeout", SauceLabsIdleTimeoutInSeconds},
                {"seleniumVersion", SauceLabSeleniumVersion},
                {
                    "screenResolution", _targetBrowser == TargetBrowser.Safari
                        ? MacScreenResolution
                        : WindowsScreenResolution
                }
            };

            var drivers = GetDrivers();
            drivers[_targetBrowser].MacPlatform = SauceLabsMacPlatformVersion;
            drivers[_targetBrowser].SauceOptions = sauceOptions;
            drivers[_targetBrowser].IdleTimeout = TimeSpan.FromSeconds(SauceLabsIdleTimeoutInSeconds);
            drivers[_targetBrowser].SauceLabsTimeout = TimeSpan.FromSeconds(SauceLabsCommandTimeoutInSeconds);
            drivers[_targetBrowser].Uri = new Uri(_sauceLabsSettings.RemoteServerUrl);
            return drivers[_targetBrowser].InitialiseForSauceLabs();
        }

        private static IWebDriver InitialiseLocalDriver(string filename, ScenarioInfo scenario)
        {
            var drivers = GetDrivers();
            drivers[_targetBrowser].SauceLabsTimeout = TimeSpan.FromSeconds(SauceLabsCommandTimeoutInSeconds);
            drivers[_targetBrowser].BuildPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            drivers[_targetBrowser].Filename = filename;
            drivers[_targetBrowser].UseVideoFiles = scenario.Tags.Contains("Video");
            drivers[_targetBrowser].LocalTimeout = TimeSpan.FromSeconds(LocalCommandTimeoutInSeconds);
            return drivers[_targetBrowser].InitialiseForLocal();
        }

        private static Dictionary<TargetBrowser, Drivers> GetDrivers()
        {
            var drivers = new Dictionary<TargetBrowser, Drivers>
            {
                {TargetBrowser.Chrome, new ChromeDriverStrategy()},
                {TargetBrowser.Firefox, new FirefoxDriverStrategy()},
                {TargetBrowser.Edge, new EdgeDriverStrategy()},
                {TargetBrowser.Ie11, new InternetExplorerDriverStrategy()},
                {TargetBrowser.Safari, new SafariDriverStrategy()},
                {TargetBrowser.ChromeMac, new ChromeMacDriverStrategy()},
                {TargetBrowser.FirefoxMac, new FirefoxMacDriverStrategy()}
            };
            return drivers;
        }
    }
}
