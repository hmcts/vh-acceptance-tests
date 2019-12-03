namespace AcceptanceTests.Common.Configuration.TestConfiguration
{
    public class ServiceConfigBase : IServiceConfig
    {
        public string AdminWebUrl { get; set; }
        public string AdminWebResourceId { get; set; }
        public string BookingsApiUrl { get; set; }
        public string BookingsApiResourceId { get; set; }
        public string KinlySelfTestScoreEndpointUrl { get; set; }
        public string PexipSelfTestNodeUri { get; set; }
        public string ServiceWebUrl { get; set; }
        public string ServiceWebResourceId { get; set; }
        public string VideoApiUrl { get; set; }
        public string VideoApiResourceId { get; set; }
        public string VideoWebUrl { get; set; }
        public string VideoWebResourceId { get; set; }
        public string UserApiUrl { get; set; }
        public string UserApiResourceId { get; set; }
    }
}
