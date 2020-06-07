using System;
using System.Collections.Generic;
using AcceptanceTests.Common.Driver.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace AcceptanceTests.Common.Driver.Drivers
{
    public abstract class Drivers
    {
        internal string AndroidAppiumVersion { get; set; }
        internal string AndroidPlatformVersion { get; set; }
        internal string BrowserVersion { get; set; }
        internal string BuildPath { get; set; }
        internal string DeviceName { get; set; }
        internal string IOSAppiumVersion { get; set; }
        internal string IOSPlatformVersion { get; set; }
        internal TimeSpan LocalTimeout { get; set; }
        internal bool LoggingEnabled { get; set; }
        internal string MacPlatform { get; set; }
        internal TargetDeviceOrientation Orientation { get; set; }
        internal Proxy Proxy { get; set; }
        internal const string ProxyByPassList = "proxy-bypass-list=<-loopback>";
        internal bool HeadlessMode { get; set; }
        internal Dictionary<string, object> SauceOptions { get; set; }
        internal Uri Uri { get; set; }

        public abstract RemoteWebDriver InitialiseForSauceLabs();
        public abstract IWebDriver InitialiseForLocal();
    }
}
