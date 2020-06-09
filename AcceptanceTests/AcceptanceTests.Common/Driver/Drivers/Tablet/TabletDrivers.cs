using System.Collections.Generic;
using AcceptanceTests.Common.Driver.Drivers.Tablet.Android;
using AcceptanceTests.Common.Driver.Drivers.Tablet.iOS;
using AcceptanceTests.Common.Driver.Enums;
using AcceptanceTests.Common.Driver.Settings;

namespace AcceptanceTests.Common.Driver.Drivers.Tablet
{
    internal class TabletDrivers : IDrivers
    {
        public Dictionary<TargetBrowser, Drivers> GetDrivers(DriverOptions driverOptions)
        {
            var drivers = new Dictionary<TargetOS, IDriversBasedOnOS>()
            {
                {TargetOS.Android, new AndroidTabletDrivers()},
                {TargetOS.iOS, new IOSTabletDrivers()}
            };
            return drivers[driverOptions.TargetOS].GetDrivers();
        }
    }

    internal class AndroidTabletDrivers : IDriversBasedOnOS
    {
        public Dictionary<TargetBrowser, Drivers> GetDrivers()
        {
            var drivers = new Dictionary<TargetBrowser, Drivers>
            {
                {TargetBrowser.Chrome, new ChromeAndroidTabletDriverStrategy()}
            };
            return drivers;
        }
    }

    internal class IOSTabletDrivers : IDriversBasedOnOS
    {
        public Dictionary<TargetBrowser, Drivers> GetDrivers()
        {
            var drivers = new Dictionary<TargetBrowser, Drivers>
            {
                {TargetBrowser.Safari, new SafariTabletDriverStrategy()}
            };
            return drivers;
        }
    }
}
