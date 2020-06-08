using System.Collections.Generic;
using AcceptanceTests.Common.Driver.Enums;

namespace AcceptanceTests.Common.Driver.Drivers
{
    internal interface IDrivers
    {
        Dictionary<TargetBrowser, Drivers> GetDrivers(DriverOptions driverOptions);
    }
}
