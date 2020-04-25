using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace AcceptanceTests.Common.Driver.Strategies.Desktop.Windows
{
    internal class EdgeChromiumWindowsDriverStrategy : Drivers
    {
        [System.Obsolete]
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
            var capabilities = new DesiredCapabilities(new Dictionary<string, object>()
            {
                { "browserName", "MicrosoftEdge" },
                { "platformName", "Windows 10" },
                { "browserVersion", BrowserVersions.EdgeChromium },
                { "ms:inPrivate", true },
                { "ms:edgeOptions", new Dictionary<string, object>() {
                    { "args", new List<string> { "use-fake-ui-for-media-stream", "use-fake-device-for-media-stream", "log-level=1" } }}},
                { "sauce:options", SauceOptions}
            });
            return new RemoteWebDriver(Uri, capabilities);
        }

        [System.Obsolete]
        public override IWebDriver InitialiseForLocal()
        {
            var argsList = new List<string> { "use-fake-ui-for-media-stream", "use-fake-device-for-media-stream", "log-level=1" };

            if (Proxy?.HttpProxy != null)
            {
                argsList.Add($"proxy-server={Proxy.HttpProxy}");
                argsList.Add(ProxyByPassList);
            }

            var capabilities = new DesiredCapabilities(new Dictionary<string, object>()
            {
                { "ms:edgeOptions", new Dictionary<string, object>() {
                    {  "binary", @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe" },
                    {  "args", argsList }
                }}
            });

            return new RemoteWebDriver(Uri, capabilities, LocalTimeout);
        }
    }
}
