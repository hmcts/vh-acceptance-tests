﻿using AcceptanceTests.Driver.Capabilities;
using AcceptanceTests.Driver.Support;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;

namespace AcceptanceTests.Driver
{
    public class DesktopDriver
    { 
        public static DriverOptions GetDesktopDriverOptions(BrowserSupport targetBrowser, bool blockCameraAndMic)
        {
            DriverOptions driverOptions = DesktopDriverCapabilities.GetDriverCapabilities(targetBrowser, blockCameraAndMic);
            driverOptions.AcceptInsecureCertificates = true;
            driverOptions.UnhandledPromptBehavior = UnhandledPromptBehavior.Accept;
            
            return driverOptions;
        }

        public static DesiredCapabilities GetDesktopDriverCapabilities(BrowserSupport targetBrowser, string scenarioTitle, bool blockCameraAndMic)
        {
            var options = GetDesktopDriverOptions(targetBrowser, blockCameraAndMic);
            return DesktopDriverCapabilities.GetDesktopDriverCapabilities(options, scenarioTitle) as DesiredCapabilities;
        }

        public static IWebDriver InitDesktopLocalBrowser(BrowserSupport targetBrowser, DriverOptions options)
        {
            IWebDriver driver = null;
            switch (targetBrowser)
            {
                case BrowserSupport.Chrome:
                    driver = new ChromeDriver((ChromeOptions)options);
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
