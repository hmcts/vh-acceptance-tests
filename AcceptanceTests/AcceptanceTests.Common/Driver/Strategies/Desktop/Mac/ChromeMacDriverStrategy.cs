using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace AcceptanceTests.Common.Driver.Strategies.Desktop.Mac
{
    internal class ChromeMacDriverStrategy : Drivers
    {
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
            var options = new ChromeOptions
            {
                BrowserVersion = "latest",
                PlatformName = MacPlatform,
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
            var browserOptions = new ChromeOptions();
            browserOptions.AddArgument("ignore-certificate-errors");
            if (BlockedCamAndMic) return new ChromeDriver(BuildPath, browserOptions, LocalTimeout);
            browserOptions.AddArgument("use-fake-ui-for-media-stream");
            browserOptions.AddArgument("use-fake-device-for-media-stream");
            if (UseVideoFiles)
                browserOptions.AddArgument($"use-file-for-fake-video-capture={BuildPath}/Videos/{Filename}");
            return new ChromeDriver(BuildPath, browserOptions, LocalTimeout);
        }
    }
}
