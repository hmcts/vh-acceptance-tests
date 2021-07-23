using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace AcceptanceTests.Common.Driver.Drivers.Desktop.Mac
{
    internal class ChromeMacDriverStrategy : Drivers
    {
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
            var options = new ChromeOptions
            {
                BrowserVersion = BrowserVersion,
                PlatformName = MacPlatform,
                UseSpecCompliantProtocol = true,
                AcceptInsecureCertificates = true
            };

            if (LoggingEnabled)
                SauceOptions.Add("extendedDebugging", true);

            options.UseSpecCompliantProtocol = true;
            options.AddArgument("use-fake-ui-for-media-stream");
            options.AddArgument("use-fake-device-for-media-stream");
            options.AddAdditionalCapability("sauce:options", SauceOptions, true);

            return new RemoteWebDriver(Uri, options.ToCapabilities());
        }

        public override IWebDriver InitialiseForLocal()
        {
            var browserOptions = new ChromeOptions();
            browserOptions.AddArgument("no-sandbox");
            browserOptions.AddArgument("ignore-certificate-errors");

            if (Proxy?.HttpProxy != null)
            {
                browserOptions.Proxy = Proxy;
                browserOptions.AddArgument(ProxyByPassList);
            }

            browserOptions.AddArgument("use-fake-ui-for-media-stream");
            browserOptions.AddArgument("use-fake-device-for-media-stream");
            return new ChromeDriver(BuildPath, browserOptions, LocalDesktopTimeout);
        }
    }
}
