namespace AcceptanceTests.Configuration.Settings
{
    public interface IServiceSettings
    {
        string BookingsApiUrl { get; set; }
        string BookingsApiResourceId { get; set; }
        string UserApiUrl { get; set; }
        string UserApiResourceId { get; set; }
    }
}