using System.Collections.Generic;
using AcceptanceTests.Common.Driver.Drivers.Desktop.Mac;
using AcceptanceTests.Common.Driver.Drivers.Desktop.Windows;
using AcceptanceTests.Common.Driver.Enums;
using AcceptanceTests.Common.Driver.Settings;

namespace AcceptanceTests.Common.Driver.Drivers.Desktop
{
    internal class DesktopDrivers : IDrivers
    {
        public Dictionary<TargetBrowser, Drivers> GetDrivers(DriverOptions driverOptions)
        {
            var drivers = new Dictionary<TargetOS, IDriversBasedOnOS>()
            {
                {TargetOS.Windows, new WindowsDrivers()},
                {TargetOS.MacOs, new MacDrivers()}
            };
            return drivers[driverOptions.TargetOS].GetDrivers();
        }
    }

    internal class WindowsDrivers : IDriversBasedOnOS
    {
        public Dictionary<TargetBrowser, Drivers> GetDrivers()
        {
            var drivers = new Dictionary<TargetBrowser, Drivers>
            {
                {TargetBrowser.Chrome, new ChromeWindowsDriverStrategy()},
                {TargetBrowser.Edge, new EdgeDriverStrategy()},
                {TargetBrowser.EdgeChromium, new EdgeChromiumWindowsDriverStrategy()},
                {TargetBrowser.Firefox, new FirefoxWindowsDriverStrategy()},
                {TargetBrowser.Ie11, new InternetExplorerDriverStrategy()}
            };
            return drivers;
        }
    }

    internal class MacDrivers : IDriversBasedOnOS
    {
        public Dictionary<TargetBrowser, Drivers> GetDrivers()
        {
            var drivers = new Dictionary<TargetBrowser, Drivers>
            {
                {TargetBrowser.Chrome, new ChromeMacDriverStrategy()},
                {TargetBrowser.Firefox, new FirefoxMacDriverStrategy()},
                {TargetBrowser.Safari, new SafariDriverStrategy()},
                {TargetBrowser.EdgeChromium, new EdgeMacDriverStrategy()}
            };
            return drivers;
        }
    }
}
