using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace AcceptanceTests.Common.Driver.Strategies.Desktop.Windows
{
    internal class EdgeChromiumDriverStrategy : Drivers
    {
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
            var capabilities = new DesiredCapabilities(new Dictionary<string, object>()
            {
                { "ms:edgeOptions", new Dictionary<string, object>() {
                    {  "binary", @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe" },
                    {  "args", new List<string> { "use-fake-ui-for-media-stream", "use-fake-device-for-media-stream" } }
                }}
            });

            return new RemoteWebDriver(Uri, capabilities, SauceLabsTimeout);
        }

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
