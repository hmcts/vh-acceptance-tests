using System;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Service.Options;

namespace AcceptanceTests.Common.Driver.Drivers.Services
{
    public class AppiumService : IDriverService
    {
        private static AppiumLocalService _appiumLocalService;

        Uri IDriverService.Start()
        {
            var args = new OptionCollector().AddArguments(GeneralOptionList.PreLaunch());
            _appiumLocalService = new AppiumServiceBuilder().WithArguments(args).UsingAnyFreePort().Build();
            _appiumLocalService.Start();
            Assert.IsTrue(_appiumLocalService.IsRunning);
            return _appiumLocalService.ServiceUrl;
        }

        void IDriverService.Stop()
        {
            _appiumLocalService?.Dispose();
        }
    }
}
