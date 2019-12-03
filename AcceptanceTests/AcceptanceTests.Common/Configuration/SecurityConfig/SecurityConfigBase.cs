namespace AcceptanceTests.Common.Configuration.SecurityConfig
{
    public class SecurityConfigBase : ISecurityConfig
    {
        public string Authority { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string TenantId { get; set; }
    }
}
