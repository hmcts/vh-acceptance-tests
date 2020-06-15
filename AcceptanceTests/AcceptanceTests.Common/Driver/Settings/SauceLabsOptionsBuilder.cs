using System;
using System.Collections.Generic;
using System.Globalization;
using AcceptanceTests.Common.Configuration;
using AcceptanceTests.Common.Driver.Enums;
using RestSharp.Extensions;

namespace AcceptanceTests.Common.Driver.Settings
{
    public class SauceLabsOptionsBuilder
    {
        private readonly DriverOptions _driverOptions;
        private readonly SauceLabsOptions _sauceLabsOptions;
        private readonly SauceLabsSettingsConfig _sauceLabsSettings;

        public SauceLabsOptionsBuilder(DriverOptions driverOptions, SauceLabsOptions sauceLabsOptions, SauceLabsSettingsConfig sauceLabsSettings)
        {
            _driverOptions = driverOptions;
            _sauceLabsOptions = sauceLabsOptions;
            _sauceLabsSettings = sauceLabsSettings;
        }

        public Dictionary<string, object> Build()
        {
            var releaseDefinitionName = GetReleaseDefinition();
            var releaseName = Environment.GetEnvironmentVariable("RELEASE_RELEASENAME");
            var attemptNumber = GetAttemptNumber();
            var build = $"{releaseDefinitionName} {releaseName} {_driverOptions.TargetDevice} {_driverOptions.TargetBrowser} {_sauceLabsOptions.BrowserVersion} {attemptNumber}";
            var sauceOptions = new Dictionary<string, object>
            {
                {"username", _sauceLabsSettings.Username},
                {"accessKey", _sauceLabsSettings.AccessKey},
                {"name", _sauceLabsOptions.Name},
                {"build", build},
                {"commandTimeout", _sauceLabsOptions.CommandTimeoutInSeconds},
                {"idleTimeout", _sauceLabsOptions.IdleTimeoutInSeconds},
                {"maxDuration", _sauceLabsOptions.MaxDurationInSeconds},
                {"seleniumVersion", _sauceLabsOptions.SeleniumVersion},
                {"timeZone", _sauceLabsOptions.Timezone }
            };

            AddScreenResolutionForDesktop(sauceOptions, _sauceLabsOptions);
            _sauceLabsOptions.BrowserVersion = BrowserVersion.GetBrowserVersion(_sauceLabsOptions.BrowserVersion, _driverOptions);
            return sauceOptions;
        }

        private static string GetReleaseDefinition()
        {
            var definition = Environment.GetEnvironmentVariable("Release_DefinitionName")?.Replace("vh-", "") ?? $"{DateTime.Today:dd.MM.yyyy}";
            return definition.ToCamelCase(new CultureInfo("en-GB", false));
        }

        private void AddScreenResolutionForDesktop(IDictionary<string, object> sauceOptions, SauceLabsOptions sauceLabsOptions)
        {
            if (_driverOptions.TargetDevice != TargetDevice.Desktop) return;
            var resolution = _driverOptions.TargetOS == TargetOS.MacOs
                ? sauceLabsOptions.MacScreenResolution
                : sauceLabsOptions.WindowsScreenResolution;
            sauceOptions.Add("screenResolution", resolution);
        }

        private static string GetAttemptNumber()
        {
            var attemptNumber = Environment.GetEnvironmentVariable("Release_AttemptNumber");
            if (string.IsNullOrWhiteSpace(attemptNumber)) return string.Empty;
            return Convert.ToInt32(attemptNumber) > 1 ? attemptNumber : string.Empty;
        }
    }
}
