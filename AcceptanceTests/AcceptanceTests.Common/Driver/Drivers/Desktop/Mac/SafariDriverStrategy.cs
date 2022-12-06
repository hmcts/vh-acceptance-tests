using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;

namespace AcceptanceTests.Common.Driver.Drivers.Desktop.Mac
{
    internal class SafariDriverStrategy : Drivers
    {
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
            var options = new SafariOptions(){
                PlatformName = MacPlatform,
                BrowserVersion = BrowserVersion
            };

            if (LoggingEnabled)
                SauceOptions.Add("extendedDebugging", true);

            options.AddAdditionalOption("sauce:options", SauceOptions);
            return new RemoteWebDriver(Uri, options);
        }

        public override IWebDriver InitialiseForLocal()
        {
            var options = new SafariOptions()
            {
                PlatformName = MacPlatform,
                BrowserVersion = "14.0.2",
                UnhandledPromptBehavior = UnhandledPromptBehavior.Accept
            };
            
            if (LoggingEnabled)
                options.SetLoggingPreference(LogType.Browser, LogLevel.Info);

            const string driverDirectoryForMacOs = "/usr/bin/";
            return new SafariDriver(driverDirectoryForMacOs, options, LocalDesktopTimeout);
        }
    }
}
