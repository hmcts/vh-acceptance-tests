using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace AcceptanceTests.Common.Driver.Strategies.Desktop.Windows
{
    internal class ChromeDriverStrategy : Drivers
    {
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
            var browserOptions = new ChromeOptions
            {
                BrowserVersion = "latest",
                PlatformName = "Windows 10",
                UseSpecCompliantProtocol = true,
                AcceptInsecureCertificates = true
            };

            if (!BlockedCamAndMic)
            {
                browserOptions.AddArgument("use-fake-ui-for-media-stream");
                browserOptions.AddArgument("use-fake-device-for-media-stream");
            }
            browserOptions.AddAdditionalCapability("sauce:options", SauceOptions, true);

            return new RemoteWebDriver(Uri, browserOptions.ToCapabilities(), SauceLabsTimeout);
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
