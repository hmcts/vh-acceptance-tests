﻿using System;
using System.Collections.Generic;
using AcceptanceTests.Common.AudioRecordings;
using AcceptanceTests.Common.Configuration;
using AcceptanceTests.Common.Driver.Drivers.Desktop;
using AcceptanceTests.Common.Driver.Drivers.Mobile;
using AcceptanceTests.Common.Driver.Drivers.Services;
using AcceptanceTests.Common.Driver.Drivers.Tablet;
using AcceptanceTests.Common.Driver.Enums;
using AcceptanceTests.Common.Driver.Settings;
using OpenQA.Selenium;
using DriverOptions = AcceptanceTests.Common.Driver.Settings.DriverOptions;

namespace AcceptanceTests.Common.Driver.Drivers
{
    public class DriverSetup
    {
        private readonly SauceLabsSettingsConfig _sauceLabsSettings;
        private static Proxy _proxy;
        private static DriverOptions _driverOptions;
        private static SauceLabsOptions _sauceLabsOptions;
        private IDriverService _driverService;

        public DriverSetup(SauceLabsSettingsConfig sauceLabsSettings, DriverOptions driverOptions, SauceLabsOptions sauceLabsOptions, Proxy proxy = null)
        {
            _sauceLabsSettings = sauceLabsSettings;
            _driverOptions = driverOptions;
            _sauceLabsOptions = sauceLabsOptions;
            _proxy = proxy;
        }

        public IWebDriver GetDriver()
        {
            return _sauceLabsSettings.RunningOnSauceLabs() ? InitialiseSauceLabsDriver() : InitialiseLocalDriver();
        }

        private static void SetDefaultSettings(bool runningOnSauceLabs)
        {
            if (!DefaultDevices.IsMobileOrTablet(_driverOptions.TargetDevice)) return;
            _sauceLabsOptions.AppiumVersion = DefaultDevices.GetAppiumVersion(_driverOptions.TargetDevice, _driverOptions.TargetOS, runningOnSauceLabs);
            _driverOptions.PlatformVersion = DefaultDevices.GetPlatformVersion(_driverOptions.TargetDevice, _driverOptions.TargetOS, runningOnSauceLabs);
            
            if(DefaultDevices.DeviceNameHasNotBeenSet(_driverOptions.TargetDeviceName)) 
                _driverOptions.TargetDeviceName = DefaultDevices.GetTargetDeviceName(_driverOptions.TargetDevice, _driverOptions.TargetOS, runningOnSauceLabs);
        }

        private IWebDriver InitialiseSauceLabsDriver()
        {
            SetDefaultSettings(true);
            var sauceOptions = new SauceLabsOptionsBuilder(_driverOptions, _sauceLabsOptions, _sauceLabsSettings).Build();
            DriverSetupValidator.ValidateDriver(_driverOptions);
            DriverSetupValidator.ValidateSauceLabs(_driverOptions.TargetDevice, _sauceLabsOptions);

            var drivers = GetDrivers();
            drivers[_driverOptions.TargetBrowser].AppiumVersion = _sauceLabsOptions.AppiumVersion;
            drivers[_driverOptions.TargetBrowser].BrowserVersion = _sauceLabsOptions.BrowserVersion;
            drivers[_driverOptions.TargetBrowser].DeviceName = _driverOptions.TargetDeviceName;
            drivers[_driverOptions.TargetBrowser].LoggingEnabled = _sauceLabsOptions.EnableLogging;
            drivers[_driverOptions.TargetBrowser].MacPlatform = _sauceLabsOptions.MacPlatformVersion;
            drivers[_driverOptions.TargetBrowser].Orientation = _driverOptions.TargetDeviceOrientation;
            drivers[_driverOptions.TargetBrowser].PlatformVersion = _driverOptions.PlatformVersion;
            drivers[_driverOptions.TargetBrowser].SauceOptions = sauceOptions;
            drivers[_driverOptions.TargetBrowser].Uri = new Uri(_sauceLabsSettings.RemoteServerUrl);
            return drivers[_driverOptions.TargetBrowser].InitialiseForSauceLabs();
        }

        private IWebDriver InitialiseLocalDriver()
        {
            SetDefaultSettings(false);
            DriverSetupValidator.ValidateDriver(_driverOptions);
            var drivers = GetDrivers();
            drivers[_driverOptions.TargetBrowser].BuildPath = FileManager.GetAssemblyDirectory();
            drivers[_driverOptions.TargetBrowser].DeviceName = _driverOptions.TargetDeviceName;
            drivers[_driverOptions.TargetBrowser].HeadlessMode = _driverOptions.HeadlessMode;
            drivers[_driverOptions.TargetBrowser].LocalTimeout = TimeSpan.FromSeconds(_driverOptions.LocalCommandTimeoutInSeconds);
            drivers[_driverOptions.TargetBrowser].LoggingEnabled = false;
            drivers[_driverOptions.TargetBrowser].Proxy = _proxy;
            StartLocalServices(drivers);
            return drivers[_driverOptions.TargetBrowser].InitialiseForLocal();
        }

        private static Dictionary<TargetBrowser, Drivers> GetDrivers()
        {
            return _driverOptions.TargetDevice switch
            {
                TargetDevice.Desktop => new DesktopDrivers().GetDrivers(_driverOptions),
                TargetDevice.Tablet => new TabletDrivers().GetDrivers(_driverOptions),
                TargetDevice.Mobile => new MobileDrivers().GetDrivers(_driverOptions),
                _ => new TabletDrivers().GetDrivers(_driverOptions)
            };
        }

        private void StartLocalServices(IReadOnlyDictionary<TargetBrowser, Drivers> drivers)
        {
            if (_driverOptions.TargetBrowser == TargetBrowser.EdgeChromium)
            {
                _driverService = new EdgeChromiumService();
                drivers[_driverOptions.TargetBrowser].Uri = _driverService.Start();
            }

            if (_driverOptions.TargetDevice == TargetDevice.Tablet || _driverOptions.TargetDevice == TargetDevice.Mobile)
            {
                _driverService = new AppiumService();
                drivers[_driverOptions.TargetBrowser].Uri = _driverService.Start();
            }
        }

        public void StopServices()
        {
            _driverService?.Stop();
        }
    }
}
