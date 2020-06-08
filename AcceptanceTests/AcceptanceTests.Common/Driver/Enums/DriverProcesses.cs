using System.Collections.Generic;

namespace AcceptanceTests.Common.Driver.Enums
{
    internal static class DriverProcesses
    {
        internal static readonly List<string> ProcessNames = new List<string>
        {
            "chromedriver",
            "edgedriver",
            "firefoxdriver",
            "gecko",
            "IEDriverServer",
            "microsoftwebdriver",
        };
    }
}
