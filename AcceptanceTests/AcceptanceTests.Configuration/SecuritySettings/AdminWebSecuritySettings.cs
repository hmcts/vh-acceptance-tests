namespace AcceptanceTests.Configuration.SecuritySettings
{
    public class AdminWebSecuritySettings : SecuritySettingsBase
    {
        public string RedirectUri { get; set; }
        public string PostLogoutRedirectUri { get; set; }
    }
}
