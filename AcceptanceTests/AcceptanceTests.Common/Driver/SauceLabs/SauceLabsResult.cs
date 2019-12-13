using System;
using OpenQA.Selenium;

namespace AdminWebsite.Common.Driver.SauceLabs
{
    public static class SauceLabsResult
    {
        public static void LogPassed(bool passed, IWebDriver driver)
        {

            try
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
            }
            catch (Exception e)
            {
                NUnit.Framework.TestContext.WriteLine($"<{e.GetType().Name}> Failed to report test status to SauceLabs: {e.Message}");
            }
        }
    }
}
