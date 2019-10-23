namespace AcceptanceTests.Configuration.SecurityConfiguration
{
    public interface ISecurityConfiguration
    {
        string ClientId { get; set; }
        string ClientSecret { get; set; }
        string Authority { get; set; }
        string TenantId { get; set; }
    }
}