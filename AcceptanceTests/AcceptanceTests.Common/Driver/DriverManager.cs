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
    public class DriverManager
    {
        private bool _runningOnSauceLabs;
        private static readonly List<string> ProcessesToCheck = new List<string>
        {
            "chrome",
            "edge",
            "firefox",
            "gecko",
            "IEDriverServer",
            "microsoftwebdriver",
        };

        public DriverManager RunningOnSauceLabs(bool runningOnSauceLabs)
        {
            _runningOnSauceLabs = runningOnSauceLabs;
            return this;
        }

        public TargetBrowser GetTargetBrowser(string browser)
        {
            return Enum.TryParse(browser, true, out TargetBrowser targetBrowser) ? targetBrowser : TargetBrowser.Chrome;
        }

        public TargetDevice GetTargetDevice(string device)
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

        public void LogTestResult(IWebDriver driver, bool passed)
        {
            if (!_runningOnSauceLabs) return;
            SauceLabsResult.LogPassed(passed, driver);
        }

        public void TearDownBrowsers(Dictionary<string, UserBrowser> browsers)
        {
            foreach (var browser in browsers.Values)
            {
                browser.BrowserTearDown();
            }
        }
    }
}
