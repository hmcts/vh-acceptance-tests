using AcceptanceTests.Common.Driver.Enums;
using AcceptanceTests.Common.Driver.Support;

namespace AcceptanceTests.Common.Driver.Configuration
{
    public class DriverOptions
    {
        public TargetDeviceOrientation TargetDeviceOrientation { get; set; }
        public bool HeadlessMode { get; set; } = false;
        public int LocalCommandTimeoutInSeconds { get; set; } = 20;
        public TargetBrowser TargetBrowser { get; set; }
        public string TargetBrowserVersion { get; set; }
        public TargetDevice TargetDevice { get; set; }
        public string TargetDeviceName { get; set; }
        public TargetOS TargetOS { get; set; }
    }
}
