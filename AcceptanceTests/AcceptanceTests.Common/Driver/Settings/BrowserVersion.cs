using System.Configuration;
using AcceptanceTests.Common.Driver.Enums;
using Castle.Core.Internal;

namespace AcceptanceTests.Common.Driver.Settings
{
    public static class BrowserVersion
    {
        private const string DefaultEdgeVersion = "18.17763";

        public static string GetBrowserVersion(string browserVersion, DriverOptions options)
        {
            if (options.TargetBrowser == TargetBrowser.Edge)
            {
                return DefaultEdgeVersion;
            }

            if (options.TargetOS == TargetOS.MacOs &&
                options.TargetBrowser == TargetBrowser.Safari &&
                browserVersion.ToLower().Equals("beta"))
            {
                throw new SettingsPropertyWrongTypeException("'Beta' version is not available on Sauce Labs for Safari");
            }

            if (browserVersion.IsNullOrEmpty())
            {
                return "latest";
            }

            return browserVersion;
        }
    }
}
