namespace AcceptanceTests.Configuration.SecurityConfiguration
{
    public class ServiceWebSecurityConfiguration : SecurityConfigurationBase
    {
        public string GraphApiBaseUri { get; set; }
        public string TemporaryPassword { get; set; }
        public string VhServiceResourceId { get; set; }
    }
}
