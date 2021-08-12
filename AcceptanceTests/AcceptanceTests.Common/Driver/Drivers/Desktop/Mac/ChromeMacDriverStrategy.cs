using System;
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
            NUnit.Framework.TestContext.WriteLine($"does it fail in ChromeWindowsDriverStrategy.InitialiseForSauceLabs and url = {Uri} for Mac");

            if (Uri != null && Uri.AbsoluteUri != null)
            {
                NUnit.Framework.TestContext.WriteLine($"uri for Mac is not null = {Uri.AbsolutePath}");
                return new RemoteWebDriver(new Uri(Uri.AbsolutePath), options.ToCapabilities(), TimeSpan.FromSeconds(30));
            }
            else
            {
                NUnit.Framework.TestContext.WriteLine($"uri for Mac is null = {Uri}");
                return new RemoteWebDriver(Uri, options.ToCapabilities(), TimeSpan.FromSeconds(30));
            }
        }

        public override IWebDriver InitialiseForLocal()
        {
            var browserOptions = new ChromeOptions();
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
