namespace AcceptanceTests.Common.Configuration
{
    public class SauceLabsSettingsConfig
    {
        public string Username { get; set; }
        public string AccessKey { get; set; }
        public string RealDeviceApiKey { get; set; }
        //public string RemoteRealDeviceServerUrl { get; set; } = "https://eu1.appium.testobject.com/wd/hub";
        public string RemoteRealDeviceServerUrl { get; set; } = "https://ondemand.eu-central-1.saucelabs.com/wd/hub";
        public string RemoteServerUrl { get; set; }

        public bool RunningOnSauceLabs()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(AccessKey);
        }
        public void SetRemoteServerUrlForDesktop(string serverUrl)
        {
            RemoteServerUrl = $"http://{Username}:{AccessKey}{serverUrl}";
        }
    }
}
