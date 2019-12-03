namespace AcceptanceTests.Common.Configuration.TestConfiguration
{
    public class ServiceConfigBase : IServiceConfig
    {
        public string BookingsApiUrl { get; set; }
        public string BookingsApiResourceId { get; set; }
        public string UserApiUrl { get; set; }
        public string UserApiResourceId { get; set; }
    }
}
