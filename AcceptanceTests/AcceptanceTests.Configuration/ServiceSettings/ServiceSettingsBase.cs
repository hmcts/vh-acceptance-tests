namespace AcceptanceTests.Configuration.Settings
{
    public class ServiceSettingsBase : IServiceSettings
    {
        public string BookingsApiUrl { get; set; }
        public string BookingsApiResourceId { get; set; }
        public string UserApiUrl { get; set; }
        public string UserApiResourceId { get; set; }
    }
}
