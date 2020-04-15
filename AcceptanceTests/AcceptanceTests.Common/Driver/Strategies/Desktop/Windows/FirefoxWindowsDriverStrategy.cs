using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace AcceptanceTests.Common.Driver.Strategies.Desktop.Windows
{
    internal class FirefoxWindowsDriverStrategy : Drivers
    {
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
            var options = new FirefoxOptions { PlatformName = "Windows 10", BrowserVersion = "latest", AcceptInsecureCertificates = true };
            
            if (LoggingEnabled)
                SauceOptions.Add("extendedDebugging", true);
            
            options.SetPreference("media.navigator.streams.fake", true);
            options.SetPreference("media.navigator.permission.disabled", true);
            options.AddAdditionalCapability("sauce:options", SauceOptions, true);
            return new RemoteWebDriver(Uri, options);
        }

        public override IWebDriver InitialiseForLocal()
        {
            var geckoService = FirefoxDriverService.CreateDefaultService(BuildPath);
            geckoService.Host = "::1";
            var options = new FirefoxOptions(){ AcceptInsecureCertificates = true };

            if (Proxy?.HttpProxy != null)
            {
                options.Proxy = Proxy;
                options.AddArgument("--proxy-bypass-list=<-loopback>");
            }

            if (LoggingEnabled)
                options.SetLoggingPreference(LogType.Browser, LogLevel.Info);

            options.SetPreference("media.navigator.streams.fake", true);
            options.SetPreference("media.navigator.permission.disabled", true);
            return new FirefoxDriver(geckoService, options, LocalTimeout);
        }
    }
}
