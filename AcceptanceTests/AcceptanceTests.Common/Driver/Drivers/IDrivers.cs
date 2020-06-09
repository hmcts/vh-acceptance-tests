using System.Collections.Generic;
using AcceptanceTests.Common.Driver.Enums;
using AcceptanceTests.Common.Driver.Settings;

namespace AcceptanceTests.Common.Driver.Drivers
{
    internal interface IDrivers
    {
        Dictionary<TargetBrowser, Drivers> GetDrivers(DriverOptions driverOptions);
    }
}
