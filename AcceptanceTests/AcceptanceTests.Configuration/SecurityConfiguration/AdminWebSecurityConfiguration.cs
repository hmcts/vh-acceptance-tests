namespace AcceptanceTests.Configuration.SecurityConfiguration
{
    public class AdminWebSecurityConfiguration : SecurityConfigurationBase
    {
        public string RedirectUri { get; set; }
        public string PostLogoutRedirectUri { get; set; }
    }
}
