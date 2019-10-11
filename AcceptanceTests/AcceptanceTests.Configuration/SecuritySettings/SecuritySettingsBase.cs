namespace AcceptanceTests.Configuration.SecuritySettings
{
    public class SecuritySettingsBase : ISecuritySettings
    {
        public string Authority { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string TenantId { get; set; }
    }
}
