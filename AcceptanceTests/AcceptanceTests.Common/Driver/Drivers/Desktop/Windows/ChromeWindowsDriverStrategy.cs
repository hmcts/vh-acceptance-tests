﻿using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace AcceptanceTests.Common.Driver.Drivers.Desktop.Windows
{
    internal class ChromeWindowsDriverStrategy : Drivers
    {
        public override RemoteWebDriver InitialiseForSauceLabs()
        {
            var options = new ChromeOptions
            {
                BrowserVersion = BrowserVersion,
                PlatformName = "Windows 10",
                UseSpecCompliantProtocol = true,
                AcceptInsecureCertificates = true
            };

            if (LoggingEnabled)
                SauceOptions.Add("extendedDebugging", true);

            options.AddArgument("use-fake-ui-for-media-stream");
            options.AddArgument("use-fake-device-for-media-stream");
            options.AddAdditionalCapability("sauce:options", SauceOptions, true);
            NUnit.Framework.TestContext.WriteLine($"does it fail in ChromeWindowsDriverStrategy.InitialiseForSauceLabs and url = {Uri}");
            //return new RemoteWebDriver(new Uri(Uri.AbsolutePath), options.ToCapabilities());
            /* return Uri != null
                ? new RemoteWebDriver(new Uri(Uri.AbsolutePath), options.ToCapabilities())
                : new RemoteWebDriver(Uri, options.ToCapabilities());
            */
            if (Uri != null)
            {
                NUnit.Framework.TestContext.WriteLine($"uri is not null = {Uri.AbsolutePath}");
                return new RemoteWebDriver(new Uri(Uri.AbsolutePath), options.ToCapabilities());
             }
            else {
                NUnit.Framework.TestContext.WriteLine($"uri is null = {Uri}");
                return new RemoteWebDriver(Uri, options.ToCapabilities());
            }
        }

        public override IWebDriver InitialiseForLocal()
        {
            var options = new ChromeOptions();
            options.AddArgument("ignore-certificate-errors");
            options.AddArgument("use-fake-ui-for-media-stream");
            options.AddArgument("use-fake-device-for-media-stream");

            if(HeadlessMode)
                options.AddArgument("--headless");

            if (Proxy?.HttpProxy != null)
            {
                options.Proxy = Proxy;
                options.AddArgument(ProxyByPassList);
            }

            if (LoggingEnabled)
                options.SetLoggingPreference(LogType.Browser, LogLevel.Info);

            return new ChromeDriver(BuildPath, options, LocalDesktopTimeout);
        }
    }
}
