namespace AcceptanceTests.Configuration.TestConfiguration
{
    public interface IServiceConfiguration
    {
        string BookingsApiUrl { get; set; }
        string BookingsApiResourceId { get; set; }
        string UserApiUrl { get; set; }
        string UserApiResourceId { get; set; }
    }
}