using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace AcceptanceTests.Common.Driver.Strategies.Desktop.Windows
{
    internal class ChromeWindowsDriverStrategy : Drivers
    {
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
            var options = new ChromeOptions
            {
                BrowserVersion = "latest",
                PlatformName = "Windows 10",
                UseSpecCompliantProtocol = true,
                AcceptInsecureCertificates = true
            };

            if (LoggingEnabled)
                SauceOptions.Add("extendedDebugging", true);

            options.AddArgument("use-fake-ui-for-media-stream");
            options.AddArgument("use-fake-device-for-media-stream");
            options.AddAdditionalCapability("sauce:options", SauceOptions, true);

            return new RemoteWebDriver(Uri, options.ToCapabilities(), SauceLabsTimeout);
        }

        public override IWebDriver InitialiseForLocal()
        {
            var options = new ChromeOptions();
            options.AddArgument("ignore-certificate-errors");
            options.AddArgument("use-fake-ui-for-media-stream");
            options.AddArgument("use-fake-device-for-media-stream");

            if (LoggingEnabled)
                options.SetLoggingPreference(LogType.Browser, LogLevel.Info);

            if (UseVideoFiles)
                options.AddArgument($"use-file-for-fake-video-capture={BuildPath}/Videos/{Filename}");
            return new ChromeDriver(BuildPath, options, LocalTimeout);
        }
    }
}
