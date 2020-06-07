using System.Configuration;
using AcceptanceTests.Common.Driver.Configuration;
using AcceptanceTests.Common.Driver.Enums;

namespace AcceptanceTests.Common.Driver.Browser
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

            return browserVersion;
        }
    }
}
