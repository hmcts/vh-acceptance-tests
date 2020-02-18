using System;
using System.Collections.Generic;
using System.Diagnostics;
using AcceptanceTests.Common.Driver.Browser;
using AcceptanceTests.Common.Driver.SauceLabs;
using AcceptanceTests.Common.Driver.Support;
using OpenQA.Selenium;

namespace AcceptanceTests.Common.Driver
{
    public class DriverManager
    {
        private bool _runningOnSauceLabs;
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

        public void KillAnyLocalDriverProcesses(TargetBrowser browser, bool runningWithSauceLabs)
        {
            if (runningWithSauceLabs) return;
            var driverProcesses = Process.GetProcessesByName(browser == TargetBrowser.Firefox ? "geckodriver" : "ChromeDriver");

            foreach (var driverProcess in driverProcesses)
            {
                try
                {
                    driverProcess.Kill();
                }
                catch (Exception ex)
                {
                    NUnit.Framework.TestContext.WriteLine(ex.Message);
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
