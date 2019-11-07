using System;
using Coypu.Drivers;
using Coypu.Drivers.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace AcceptanceTests.Driver
{
    public class SauceLabsDriver : SeleniumWebDriver
    {

        public SauceLabsDriver(Browser browser, DriverOptions options, string host)
            : base(CustomWebDriver(options, host), browser)
        {
        }

        private static RemoteWebDriver CustomWebDriver(DriverOptions options, string host)
        {
            var remoteAppHost = new Uri(host);

            var remoteDriver = new RemoteWebDriver(remoteAppHost, options);
            return remoteDriver;
        }
#pragma warning disable 618
        public SauceLabsDriver(Browser browser, DesiredCapabilities capabilities, string host, TimeSpan commandTimeout)
            : base(CustomWebDriver(capabilities, host, commandTimeout), browser)
        {
        }

        private static RemoteWebDriver CustomWebDriver(DesiredCapabilities capabilities, string host, TimeSpan commandTimeout)
        {
            var remoteAppHost = new Uri(host);

            var remoteDriver = new RemoteWebDriver(remoteAppHost, capabilities, commandTimeout);
            return remoteDriver;
        }
#pragma warning restore 618
    }
}
