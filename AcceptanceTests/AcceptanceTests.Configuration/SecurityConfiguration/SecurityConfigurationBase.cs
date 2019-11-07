namespace AcceptanceTests.Configuration.SecurityConfiguration
{
    public class SecurityConfigurationBase : ISecurityConfiguration
    {
        public string Authority { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string TenantId { get; set; }
    }
}
