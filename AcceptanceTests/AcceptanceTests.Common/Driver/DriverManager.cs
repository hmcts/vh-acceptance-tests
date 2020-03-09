using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AcceptanceTests.Common.Driver.Browser;
using AcceptanceTests.Common.Driver.SauceLabs;
using AcceptanceTests.Common.Driver.Support;
using OpenQA.Selenium;

namespace AcceptanceTests.Common.Driver
{
    public static class DriverManager
    {
        private static readonly List<string> ProcessesToCheck = new List<string>
        {
            "chromedriver",
            "edgedriver",
            "firefoxdriver",
            "gecko",
            "IEDriverServer",
            "microsoftwebdriver",
        };

        public static TargetBrowser GetTargetBrowser(string browser)
        {
            return Enum.TryParse(browser, true, out TargetBrowser targetBrowser) ? targetBrowser : TargetBrowser.Chrome;
        }

        public static TargetDevice GetTargetDevice(string device)
        {
            return Enum.TryParse(device, true, out TargetDevice targetDevice) ? targetDevice : TargetDevice.Desktop;
        }

        public static void KillAnyLocalDriverProcesses()
        {
            var processes = Process.GetProcesses();
            foreach (var process in processes)
            {
                try
                {
                    var shouldKill = ProcessesToCheck.Any(processName => process.ProcessName.ToLower().Contains(processName));
                    if (shouldKill)
                        process.Kill();
                }
                catch (Exception e)
                {
                    NUnit.Framework.TestContext.WriteLine(e.Message);
                }
            }
        }

        public static void LogTestResult(bool runningOnSauceLabs, IWebDriver driver, bool passed)
        {
            if (!runningOnSauceLabs) return;
            SauceLabsResult.LogPassed(passed, driver);
        }

        public static void TearDownBrowsers(Dictionary<string, UserBrowser> browsers)
        {
            foreach (var browser in browsers.Values)
            {
                browser.BrowserTearDown();
            }
        }
    }
}
