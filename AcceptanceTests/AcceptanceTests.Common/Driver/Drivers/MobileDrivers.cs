using System.Collections.Generic;
using AcceptanceTests.Common.Driver.Drivers.Mobile.Android;
using AcceptanceTests.Common.Driver.Drivers.Mobile.iOS;
using AcceptanceTests.Common.Driver.Enums;

namespace AcceptanceTests.Common.Driver.Drivers
{
    internal class MobileDrivers : IDrivers
    {
        public Dictionary<TargetBrowser, Drivers> GetDrivers(DriverOptions driverOptions)
        {
            var drivers = new Dictionary<TargetOS, IDriversBasedOnOS>()
            {
                {TargetOS.Android, new AndroidMobileDrivers()},
                {TargetOS.iOS, new IOSMobileDrivers()}
            };
            return drivers[driverOptions.TargetOS].GetDrivers();
        }
    }

    internal class AndroidMobileDrivers : IDriversBasedOnOS
    {
        public Dictionary<TargetBrowser, Drivers> GetDrivers()
        {
            var drivers = new Dictionary<TargetBrowser, Drivers>
            {
                {TargetBrowser.Chrome, new ChromeAndroidMobileDriverStrategy()}
            };
            return drivers;
        }
    }

    internal class IOSMobileDrivers : IDriversBasedOnOS
    {
        public Dictionary<TargetBrowser, Drivers> GetDrivers()
        {
            var drivers = new Dictionary<TargetBrowser, Drivers>
            {
                {TargetBrowser.Safari, new SafariiOSMobileDriverStrategy()}
            };
            return drivers;
        }
    }
}
