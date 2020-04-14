﻿using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace AcceptanceTests.Common.Driver.Strategies.Desktop.Windows
{
    internal class FirefoxWindowsDriverStrategy : Drivers
    {
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
            var options = new FirefoxOptions { PlatformName = "Windows 10", BrowserVersion = "latest", AcceptInsecureCertificates = true };
            SauceOptions.Add("extendedDebugging", true);
            options.SetPreference("media.navigator.streams.fake", true);
            options.SetPreference("media.navigator.permission.disabled", true);
            options.SetPreference("devtools.chrome.enabled", true);
            options.SetPreference("devtools.debugger.prompt - connection", false);
            options.SetPreference("devtools.debugger.remote - enabled", true);
            options.AddArgument("start-debugger-server: 9222");
            options.AddAdditionalCapability("sauce:options", SauceOptions, true);
            return new RemoteWebDriver(Uri, options);
        }

        public override IWebDriver InitialiseForLocal()
        {
            var geckoService = FirefoxDriverService.CreateDefaultService(BuildPath);
            geckoService.Host = "::1";
            var options = new FirefoxOptions(){ AcceptInsecureCertificates = true };

            if (LoggingEnabled)
                options.SetLoggingPreference(LogType.Browser, LogLevel.Info);

            options.SetPreference("media.navigator.streams.fake", true);
            options.SetPreference("media.navigator.permission.disabled", true);
            return new FirefoxDriver(geckoService, options, LocalTimeout);
        }
    }
}
