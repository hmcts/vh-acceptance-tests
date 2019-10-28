using System;
using System.IO;
using System.Reflection;
using AcceptanceTests.Driver.Capabilities;
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
        public static DriverOptions GetDesktopDriverOptions(BrowserSupport targetBrowser, string scenarioTitle, bool blockCameraAndMic)
        {
            DriverOptions driverOptions = DesktopDriverCapabilities.GetDriverCapabilities(targetBrowser, blockCameraAndMic);
            //driverOptions = DesktopDriverCapabilities.GetDesktopDriverAdditionalOptions(driverOptions, scenarioTitle);

            return driverOptions;
        }

        public static DesiredCapabilities GetDesktopDriverCapabilities(BrowserSupport targetBrowser, string scenarioTitle, bool blockCameraAndMic)
        {
            var options = GetDesktopDriverOptions(targetBrowser, scenarioTitle, blockCameraAndMic);
            var desiredCapabilities = DesktopDriverCapabilities.GetDesktopDriverAdditionalOptions(options, scenarioTitle).ToCapabilities() as DesiredCapabilities;
            return desiredCapabilities;
        }

        public static IWebDriver InitDesktopBrowser(BrowserSupport targetBrowser, DriverOptions options)
        {
            IWebDriver driver = null;
            switch (targetBrowser)
            {
                case BrowserSupport.Chrome:
                    driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), (ChromeOptions)options);
                    break;
                case BrowserSupport.Safari:
                    driver = new SafariDriver((SafariOptions)options);
                    break;
                case BrowserSupport.Edge:
                    driver = new EdgeDriver((EdgeOptions)options);
                    break;
                case BrowserSupport.Firefox:
                    driver = new FirefoxDriver((FirefoxOptions)options);
                    //driver = new FirefoxDriver();
                    break;
            }
            return driver;
        }
    }
}
