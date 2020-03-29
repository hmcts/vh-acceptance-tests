using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;

namespace AcceptanceTests.Common.Driver.Strategies.Desktop.Windows
{
    internal class EdgeChromiumDriverStrategy : Drivers
    {
        [System.Obsolete]
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
            var options = new EdgeOptions()
            {
                PlatformName = "Windows 10",
                BrowserVersion = "latest"
            };

            var capabilities = new DesiredCapabilities(new Dictionary<string, object>()
            {
                { "ms:inPrivate", true },
                { "ms:edgeOptions", new Dictionary<string, object>() {
                    {  "binary", @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe" },
                    {  "args", new List<string> { "use-fake-ui-for-media-stream", "use-fake-device-for-media-stream" } }
                }},
                { "sauce:options", SauceOptions}
            });
            capabilities.SetCapability("ms:edgeOptions", options);

            return new RemoteWebDriver(Uri, capabilities, SauceLabsTimeout);
        }

        [System.Obsolete]
        public override IWebDriver InitialiseForLocal()
        {
            var capabilities = new DesiredCapabilities(new Dictionary<string, object>()
            {
                { "ms:edgeOptions", new Dictionary<string, object>() {
                    {  "binary", @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe" },
                    {  "args", new List<string> { "use-fake-ui-for-media-stream", "use-fake-device-for-media-stream" } }
                }}
            });

            return new RemoteWebDriver(Uri, capabilities, LocalTimeout);
        }
    }
}
