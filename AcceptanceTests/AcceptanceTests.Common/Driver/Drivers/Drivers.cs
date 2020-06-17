using System;
using System.Collections.Generic;
using AcceptanceTests.Common.Driver.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace AcceptanceTests.Common.Driver.Drivers
{
    public abstract class Drivers
    {
        internal string AppiumVersion { get; set; }
        internal string BrowserVersion { get; set; }
        internal string BuildPath { get; set; }
        internal string DeviceName { get; set; }
        internal bool HeadlessMode { get; set; }
        internal TimeSpan LocalAppiumTimeout { get; set; }
        internal TimeSpan LocalDesktopTimeout { get; set; }
        internal bool LoggingEnabled { get; set; }
        internal string MacPlatform { get; set; }
        internal TargetDeviceOrientation Orientation { get; set; }
        internal string PlatformVersion { get; set; }
        internal Proxy Proxy { get; set; }
        internal const string ProxyByPassList = "proxy-bypass-list=<-loopback>";
        internal bool ResetDeviceBetweenTests { get; set; }
        internal bool RealDevice { get; set; }
        internal string RealDeviceApiKey { get; set; }
        internal string RealDeviceServerUrl { get; set; }
        internal Dictionary<string, object> SauceOptions { get; set; }
        internal Uri Uri { get; set; }
        internal string UUID { get; set; }
        public abstract RemoteWebDriver InitialiseForSauceLabs();
        public abstract IWebDriver InitialiseForLocal();
    }
}
