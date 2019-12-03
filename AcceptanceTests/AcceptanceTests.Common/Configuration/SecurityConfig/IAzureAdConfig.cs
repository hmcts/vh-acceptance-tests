namespace AcceptanceTests.Common.Configuration.SecurityConfig
{
    public interface IAzureAdConfig
    {
        string ClientId { get; set; }
        string ClientSecret { get; set; }
        string Authority { get; set; }
        string TenantId { get; set; }
    }
}
