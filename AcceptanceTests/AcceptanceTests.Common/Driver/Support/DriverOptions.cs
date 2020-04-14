using System;

namespace AcceptanceTests.Common.Driver.Support
{
    public class DriverOptions
    {
        public bool EnableLogging { get; set; }
        public TargetBrowser TargetBrowser { get; set; }
        public TargetDevice TargetDevice { get; set; }
    }
}
