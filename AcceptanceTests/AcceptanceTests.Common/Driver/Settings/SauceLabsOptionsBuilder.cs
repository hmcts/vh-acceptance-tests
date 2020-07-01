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
            var attemptNumber = GetAttemptNumber();
            _sauceLabsOptions.BrowserVersion = BrowserVersion.GetBrowserVersion(_sauceLabsOptions.BrowserVersion, _driverOptions);
            var build = $"{GetBuildDefinition()}{GetGitVersionNumber()}     [ {_driverOptions.TargetDevice} | {_driverOptions.TargetOS} | {_driverOptions.TargetBrowser} | {_sauceLabsOptions.BrowserVersion.ToPascalCase(new CultureInfo("en-GB", false))} ] {attemptNumber}";
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
            return sauceOptions;
        }

        private static string GetBuildDefinition()
        {
            var definition = Environment.GetEnvironmentVariable("BUILD_DEFINITIONNAME")?
                                 .ToLower()
                                 .Replace("hmcts.vh-", "")
                                 .Replace("-", " ")
                                 .Replace("cd", "")
                                 .Replace("webnightly", " Web Nightly")
                                 .Replace("web", " Web")
                             ?? $"{DateTime.Today:dd.MM.yyyy}";
            return new CultureInfo("en-GB", false).TextInfo.ToTitleCase(definition);
        }

        private static string GetGitVersionNumber()
        {
            var gitVersionNumber = Environment.GetEnvironmentVariable("GITVERSION_FULLSEMVER");
            return !string.IsNullOrEmpty(gitVersionNumber) ? $" | {gitVersionNumber}" : string.Empty;
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
            var attemptNumber = Environment.GetEnvironmentVariable("Build_AttemptNumber");
            if (string.IsNullOrWhiteSpace(attemptNumber)) return string.Empty;
            return Convert.ToInt32(attemptNumber) > 1 ? $" : Attempt {attemptNumber}" : string.Empty;
        }
    }
}
