using System.Collections.Generic;
using AcceptanceTests.Common.Driver.Enums;
using AcceptanceTests.Common.Driver.Support;

namespace AcceptanceTests.Common.Driver.Drivers
{
    internal interface IDriversBasedOnOS
    {
        Dictionary<TargetBrowser, Drivers> GetDrivers();
    }
}
