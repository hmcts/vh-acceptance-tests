namespace AcceptanceTests.Configuration.ServiceConfiguration
{
    public interface IServiceConfiguration
    {
        string BookingsApiUrl { get; set; }
        string BookingsApiResourceId { get; set; }
        string UserApiUrl { get; set; }
        string UserApiResourceId { get; set; }
    }
}