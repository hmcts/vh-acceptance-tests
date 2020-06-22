using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace AcceptanceTests.Common.Driver.Drivers.Desktop.Windows
{
    internal class EdgeChromiumWindowsDriverStrategy : Drivers
    {
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
            #pragma warning disable 618
            var capabilities = new DesiredCapabilities(new Dictionary<string, object>()
            #pragma warning restore 618
            {
                { "browserName", "MicrosoftEdge" },
                { "platformName", "Windows 10" },
                { "browserVersion", BrowserVersion },
                { "ms:inPrivate", true },
                { "ms:edgeOptions", new Dictionary<string, object>() {
                    { "args", new List<string> { "use-fake-ui-for-media-stream", "use-fake-device-for-media-stream", "log-level=1" } }}},
                { "sauce:options", SauceOptions}
            });
            return new RemoteWebDriver(Uri, capabilities);
        }

        public override IWebDriver InitialiseForLocal()
        {
            var argsList = new List<string> { "use-fake-ui-for-media-stream", "use-fake-device-for-media-stream", "log-level=1" };

            if (Proxy?.HttpProxy != null)
            {
                argsList.Add($"proxy-server={Proxy.HttpProxy}");
                argsList.Add(ProxyByPassList);
            }

            #pragma warning disable 618
            var capabilities = new DesiredCapabilities(new Dictionary<string, object>()
            #pragma warning restore 618
            {
                { "ms:edgeOptions", new Dictionary<string, object>() {
                    {  "binary", @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe" },
                    {  "args", argsList }
                }}
            });

            return new RemoteWebDriver(Uri, capabilities, LocalDesktopTimeout);
        }
    }
}
