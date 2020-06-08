using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AcceptanceTests.Common.Driver.Browser;
using AcceptanceTests.Common.Driver.Enums;
using AcceptanceTests.Common.Driver.Exceptions;
using AcceptanceTests.Common.Driver.Helpers;
using Castle.Core.Internal;
using OpenQA.Selenium;

namespace AcceptanceTests.Common.Driver.Drivers
{
    public static class DriverManager
    {
        public static TargetBrowser GetTargetBrowser(string browser)
        {
            return Enum.TryParse(browser, true, out TargetBrowser targetBrowser) ? targetBrowser : TargetBrowser.Chrome;
        }

        public static TargetDevice GetTargetDevice(string device)
        {
            return Enum.TryParse(device, true, out TargetDevice targetDevice) ? targetDevice : TargetDevice.Desktop;
        }

        public static TargetOS GetTargetOS(string os)
        {
            if (os.IsNullOrEmpty())
            {
                return DetectOS.GetCurrentOS();
            }

            try
            {
                return Enum.Parse<TargetOS>(os, true);
            }
            catch (Exception e)
            {
                throw new OperatingSystemNotFoundException(e.Message);
            }
        }

        public static void KillAnyLocalDriverProcesses()
        {
            var processes = Process.GetProcesses();
            foreach (var process in processes)
            {
                try
                {
                    var shouldKill = DriverProcesses.ProcessNames.Any(processName => process.ProcessName.ToLower().Contains(processName));
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
