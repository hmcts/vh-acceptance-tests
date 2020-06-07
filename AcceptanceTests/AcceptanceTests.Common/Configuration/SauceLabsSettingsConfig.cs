namespace AcceptanceTests.Common.Configuration
{
    public class SauceLabsSettingsConfig
    {
        public string Username { get; set; }
        public string AccessKey { get; set; }
        public string MobileAccessKey { get; set; }
        public string RemoteServerUrl { get; set; }
        public string RemoteRealDeviceServerUrl { get; set; }

        public bool RunningOnSauceLabs()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(AccessKey);
        }
        public void SetRemoteServerUrlForDesktop(string serverUrl)
        {
            RemoteServerUrl = $"http://{Username}:{AccessKey}{serverUrl}";
        }

        public void SetRemoteServerUrlForRealDevices(string serverUrl)
        {
            RemoteServerUrl = $"http://{Username}:{MobileAccessKey}{serverUrl}";
        }
    }
}
