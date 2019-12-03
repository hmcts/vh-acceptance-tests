namespace AcceptanceTests.Common.Configuration.TestConfiguration
{
    public interface IServiceConfig
    {
        string BookingsApiUrl { get; set; }
        string BookingsApiResourceId { get; set; }
        string UserApiUrl { get; set; }
        string UserApiResourceId { get; set; }
    }
}
