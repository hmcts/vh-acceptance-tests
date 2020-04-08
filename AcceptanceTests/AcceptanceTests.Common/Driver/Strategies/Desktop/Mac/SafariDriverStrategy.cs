using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;

namespace AcceptanceTests.Common.Driver.Strategies.Desktop.Mac
{
    internal class SafariDriverStrategy : Drivers
    {
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
            var options = new SafariOptions(){
                PlatformName = MacPlatform,
                BrowserVersion = "13.0"
            };

            if (LoggingEnabled)
                options.SetLoggingPreference(LogType.Browser, LogLevel.Info);

            options.AddAdditionalCapability("sauce:options", SauceOptions);
            return new RemoteWebDriver(Uri, options);
        }

        public override IWebDriver InitialiseForLocal()
        {
            var options = new SafariOptions()
            {
                PlatformName = MacPlatform,
                BrowserVersion = "13.0.5",
                UnhandledPromptBehavior = UnhandledPromptBehavior.Accept
            };

            if (LoggingEnabled)
                options.SetLoggingPreference(LogType.Browser, LogLevel.Info);

            return new SafariDriver(BuildPath, options, LocalTimeout);
        }
    }
}
