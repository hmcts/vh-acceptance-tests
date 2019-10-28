namespace AcceptanceTests.Configuration.ServiceConfiguration
{
    public class ServiceConfigurationBase : IServiceConfiguration
    {
        public string BookingsApiUrl { get; set; }
        public string BookingsApiResourceId { get; set; }
        public string UserApiUrl { get; set; }
        public string UserApiResourceId { get; set; }
    }
}
