using System;
using System.Collections.Generic;
using System.Diagnostics;
using AdminWebsite.Common.Driver.Browser;
using AdminWebsite.Common.Driver.SauceLabs;
using AdminWebsite.Common.Driver.Support;
using OpenQA.Selenium;

namespace AdminWebsite.Common.Driver
{
    public class DriverManager
    {
        private bool _runningOnSauceLabs;
        public DriverManager RunningOnSauceLabs(bool runningOnSauceLabs)
        {
            _runningOnSauceLabs = runningOnSauceLabs;
            return this;
        }

        public TargetBrowser GetTargetBrowser()
        {
            return Enum.TryParse(NUnit.Framework.TestContext.Parameters["TargetBrowser"], true, out TargetBrowser targetTargetBrowser) ? targetTargetBrowser : TargetBrowser.Chrome;
        }

        public TargetDevice GetTargetDevice()
        {
            return Enum.TryParse(NUnit.Framework.TestContext.Parameters["TargetDevice"], true, out TargetDevice targetTargetDevice) ? targetTargetDevice : TargetDevice.Desktop;
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
