namespace AcceptanceTests.Configuration.SecuritySettings
{
    public class ServiceWebSecuritySettings : SecuritySettingsBase
    {
        public string GraphApiBaseUri { get; set; }
        public string TemporaryPassword { get; set; }
        public string VhServiceResourceId { get; set; }
    }
}
