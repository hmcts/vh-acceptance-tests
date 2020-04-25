namespace AcceptanceTests.Common.Driver.Support
{
    public class DriverOptions
    {
        public string BrowserVersion { get; set; }
        public BrowserVersions BrowserVersions { get; set; }
        public bool EnableLogging { get; set; }
        public int LocalCommandTimeoutInSeconds { get; set; } = 20;
        public string MacPlatformVersion { get; set; } = "macOS 10.15";
        public string MacScreenResolution = "2360x1770";
        public string SeleniumVersion = "3.141.59";
        public int SauceLabsCommandTimeoutInSeconds { get; set; } = 60 * 3;
        public int SauceLabsIdleTimeoutInSeconds { get; set; } = 60 * 7;
        public TargetBrowser TargetBrowser { get; set; }
        public TargetDevice TargetDevice { get; set; }
        public int SauceLabsMaxDurationInSeconds { get; set; } = 60 * 10;

        public string Timezone = "London";
        public string WindowsScreenResolution = "2560x1600";
    }
}
