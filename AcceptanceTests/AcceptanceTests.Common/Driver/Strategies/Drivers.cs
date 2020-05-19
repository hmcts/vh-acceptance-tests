using System;
using System.Collections.Generic;
using AcceptanceTests.Common.Driver.Support;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace AcceptanceTests.Common.Driver.Strategies
{
    public abstract class Drivers
    {
        public BrowserVersions BrowserVersions { get; set; }
        internal string BuildPath { get; set; }
        internal TimeSpan LocalTimeout { get; set; }
        internal bool LoggingEnabled { get; set; }
        internal string MacPlatform { get; set; }
        internal Proxy Proxy { get; set; }
        internal const string ProxyByPassList = "proxy-bypass-list=<-loopback>";
        internal bool HeadlessMode { get; set; }
        internal Dictionary<string, object> SauceOptions { get; set; }
        internal Uri Uri { get; set; }
        public abstract RemoteWebDriver InitialiseForSauceLabs();
        public abstract IWebDriver InitialiseForLocal();
    }
}
