using System.Configuration;
using AcceptanceTests.Common.Driver.Enums;
using Castle.Core.Internal;
using FluentAssertions;

namespace AcceptanceTests.Common.Driver.Settings
{
    public static class BrowserVersion
    {
        private const string DefaultEdgeVersion = "18.17763";

        public static string SetBrowserVersion(DriverOptions options)
        {
            if (options.TargetBrowser == TargetBrowser.Edge)
            {
                return DefaultEdgeVersion;
            }
            
            if (options.TargetBrowserVersion.IsNullOrEmpty())
            {
                return "latest";
            }

            if (options.TargetOS == TargetOS.MacOs &&
                options.TargetBrowser == TargetBrowser.Safari &&
                options.TargetBrowserVersion.ToLower().Equals("beta"))
            {
                throw new SettingsPropertyWrongTypeException("'Beta' version is not available on Sauce Labs for Safari");
            }

            options.TargetBrowserVersion.ToLower().Should().ContainAny(".", "latest", "beta", "dev");

            return options.TargetBrowserVersion.ToLower();
        }
    }
}
