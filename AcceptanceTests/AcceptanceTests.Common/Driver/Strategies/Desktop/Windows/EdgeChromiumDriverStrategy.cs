using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;

namespace AcceptanceTests.Common.Driver.Strategies.Desktop.Windows
{
    internal class EdgeChromiumDriverStrategy : Drivers
    {
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
            var options = new EdgeOptions()
            {
                BrowserVersion = "latest",
                PlatformName = "Windows 10"
            };

            var capabilities = new Dictionary<string, object>
            {
                ["args"] = new List<string> { "use-fake-ui-for-media-stream", "use-fake-device-for-media-stream" }
            };

            capabilities.Add("ms:edgeOptions", options.ToCapabilities());

            return new RemoteWebDriver(Uri, new DesiredCapabilities(capabilities), SauceLabsTimeout);
        }

        public override IWebDriver InitialiseForLocal()
        {
            var capabilities = new Dictionary<string, object>
            {
                ["args"] = new List<string> { "use-fake-ui-for-media-stream", "use-fake-device-for-media-stream" }
            };

            capabilities.Add("ms:edgeOptions", new Dictionary<string, object>()
            {
                {"binary", @"C:\Program Files (x86)\Microsoft\Edge Dev\Application\msedge.exe"}
            });

            return new RemoteWebDriver(Uri, new DesiredCapabilities(capabilities), LocalTimeout);
        }
    }
}
