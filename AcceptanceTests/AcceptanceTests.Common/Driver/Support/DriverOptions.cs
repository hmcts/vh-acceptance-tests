namespace AcceptanceTests.Common.Driver.Support
{
    public class DriverOptions
    {
        public BrowserVersions BrowserVersions { get; set; }
        public int LocalCommandTimeoutInSeconds { get; set; } = 20;
        public bool EnableLogging { get; set; }
        public string MacScreenResolution = "2360x1770";
        public string SeleniumVersion = "3.141.59";
        public string MacPlatformVersion { get; set; } = "macOS 10.15";
        public int SauceLabsCommandTimeoutInSeconds { get; set; } = 60 * 5;
        public int SauceLabsIdleTimeoutInSeconds { get; set; } = 60 * 30;
        public string Timezone = "London";
        public TargetBrowser TargetBrowser { get; set; }
        public TargetDevice TargetDevice { get; set; }
        public string WindowsScreenResolution = "2560x1600";
    }
}
