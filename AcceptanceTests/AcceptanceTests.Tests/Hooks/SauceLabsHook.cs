using System;
using Coypu;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using AcceptanceTests.Driver.Settings;
using Microsoft.Extensions.Configuration;
using Options = Microsoft.Extensions.Options.Options;

namespace AcceptanceTests.Tests.Hooks
{
    [Binding]
    public sealed class SaucelabsHook
    {

        public static SauceLabsSettings GetSauceLabsSettings(IConfigurationRoot configRoot)
        {
            var saucelabsSettings = Options.Create(configRoot.GetSection("Saucelabs").Get<SauceLabsSettings>()).Value;
            return saucelabsSettings;
        }

        public static void LogPassed(bool passed, BrowserSession driver)
        {
            try
            {
                driver.ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
            }
            catch (Exception e)
            {
                Console.WriteLine($"<{e.GetType().Name}> Failed to report test status to saucelabs: {e.Message}");
            }
        }
    }
}