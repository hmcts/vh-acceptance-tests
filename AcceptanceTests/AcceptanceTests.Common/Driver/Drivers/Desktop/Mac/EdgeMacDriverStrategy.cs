using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;

namespace AcceptanceTests.Common.Driver.Drivers.Desktop.Mac
{
    internal class EdgeMacDriverStrategy : Drivers
    {
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
            var options = new EdgeOptions();
            options.AddAdditionalOption("browserName", "MicrosoftEdge");
            options.AddAdditionalOption("platformName", MacPlatform);
            options.AddAdditionalOption("browserVersion", BrowserVersion);
            options.AddAdditionalOption("ms:inPrivate", true);
            options.AddAdditionalOption("ms:edgeOptions", new Dictionary<string, object>() {
                { "args", new List<string> { "use-fake-ui-for-media-stream", "use-fake-device-for-media-stream", "log-level=1" } }});
            options.AddAdditionalOption("sauce:options", SauceOptions);
            return new RemoteWebDriver(Uri, options);
        }

        public override IWebDriver InitialiseForLocal()
        {
            var argsList = new List<string> { "use-fake-ui-for-media-stream", "use-fake-device-for-media-stream", "log-level=1" };

            if (Proxy?.HttpProxy != null)
            {
                argsList.Add($"proxy-server={Proxy.HttpProxy}");
                argsList.Add(ProxyByPassList);
            }
            var options = new EdgeOptions();
            options.AddAdditionalOption("ms:edgeOptions", new Dictionary<string, object>()
            {
                { "binary", @$"{BuildPath}/msedge.exe" },
                { "args", argsList }
            });
            
            return new RemoteWebDriver(Uri, options);
        }
    }
}
