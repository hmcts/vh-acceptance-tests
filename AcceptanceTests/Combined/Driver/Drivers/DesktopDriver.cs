﻿using AcceptanceTests.Common.Driver.Capabilities;
using AcceptanceTests.Common.Driver.Settings;
using AcceptanceTests.Common.Driver.Support;
using AcceptanceTests.Common.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using TechTalk.SpecFlow;

namespace AcceptanceTests.Common.Driver.Drivers
{
    public class DesktopDriver
    { 
        public static DriverOptions GetDesktopLocalDriverOptions(BrowserSupport targetBrowser, bool blockCameraAndMic)
        {
            DriverOptions driverOptions = DesktopDriverOptions.GetLocalDriverOptions(targetBrowser, blockCameraAndMic);

            return driverOptions;
        }

        public static DriverOptions GetDesktopRemoteDriverOptions(BrowserSettings browserSettings, ScenarioInfo scenarioInfo, string buildName, bool blockCameraAndMic)
        {
            var driverOptions = DesktopDriverOptions.GetRemoteDesktopOptions(browserSettings, scenarioInfo, buildName, blockCameraAndMic);
            return driverOptions;
        }
#pragma warning disable 618
        public static DesiredCapabilities GetDesktopDriverCapabilities(BrowserSettings browserSettings, ScenarioInfo scenarioInfo, string buildName, bool blockCameraAndMic)
        {
            var targetBrowser = EnumParser.ParseText<BrowserSupport>(browserSettings.BrowserName);
            var desiredCapabilities = DesktopDriverCapabilities.GetRemoteDesktopCapabilities(targetBrowser, browserSettings, scenarioInfo, buildName, blockCameraAndMic);
            return desiredCapabilities;
        }
#pragma warning restore 618

        public static IWebDriver InitDesktopLocalBrowser(BrowserSupport targetBrowser, DriverOptions options)
        {
            IWebDriver driver = null;
            switch (targetBrowser)
            {
                case BrowserSupport.Chrome:
                    driver = new ChromeDriver("./", (ChromeOptions) options);
                    break;
                case BrowserSupport.Safari:
                    driver = new SafariDriver((SafariOptions)options);
                    break;
                case BrowserSupport.Edge:
                    driver = new EdgeDriver((EdgeOptions)options);
                    break;
                case BrowserSupport.Firefox:
                    driver = new FirefoxDriver((FirefoxOptions)options);
                    break;
            }
            return driver;
        }
    }
}