namespace AdminWebsite.Common.Configuration
{
    public class SauceLabsSettingsConfig
    {
        public string Username { get; set; }
        public string AccessKey { get; set; }
        public string MobileAccessKey { get; set; }
        public string RemoteServerUrl { get; set; }
        public string RemoteMobileServerUrl { get; set; }

        public bool RunningOnSauceLabs()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(AccessKey);
        }
        public void SetRemoteServerUrlForDesktop(string serverUrl)
        {
            RemoteServerUrl = $"http://{Username}:{AccessKey}{serverUrl}";
        }

        public void SetRemoteServerUrlForMobile(string serverUrl)
        {
            RemoteServerUrl = $"http://{Username}:{MobileAccessKey}{serverUrl}";
        }
    }
}
