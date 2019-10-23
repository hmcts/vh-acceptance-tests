using System;
using Coypu.Drivers;
using Coypu.Drivers.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace AcceptanceTests.Driver
{
    public class SauceLabsDriver : SeleniumWebDriver
    {
        public SauceLabsDriver(Browser browser, ICapabilities capabilities, string host)
            : base(CustomWebDriver(capabilities, host), browser)
        {
        }

        private static RemoteWebDriver CustomWebDriver(ICapabilities capabilities, string host)
        {
            var commandTimeout = TimeSpan.FromMinutes(1.5);
            var remoteAppHost = new Uri(host);
            return new RemoteWebDriver(remoteAppHost, capabilities, commandTimeout);
        }
    }
}
