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
    }
}
