using System.Collections.Generic;
using AcceptanceTests.Common.Driver.Enums;

namespace AcceptanceTests.Common.Driver.Drivers
{
    internal interface IDriversBasedOnOS
    {
        Dictionary<TargetBrowser, Drivers> GetDrivers();
    }
}
