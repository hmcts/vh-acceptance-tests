namespace AcceptanceTests.Common.Driver.Settings
{
    public class SauceLabsOptions
    {
        public string AppiumVersion { get; set; } = "1.17.1";
        public int CommandTimeoutInSeconds { get; set; } = 60 * 3;
        public bool EnableLogging { get; set; } = true;
        public int IdleTimeoutInSeconds { get; set; } = 60 * 7;
        public string MacPlatformVersion { get; set; } = "macOS 10.15";
        public string MacScreenResolution = "2360x1770";
        public int MaxDurationInSeconds { get; set; } = 60 * 10;
        public string SeleniumVersion = "3.141.59";
        public string Timezone = "London";
        public string Name { get; set; }
        public string WindowsScreenResolution = "2560x1600";
    }
}
