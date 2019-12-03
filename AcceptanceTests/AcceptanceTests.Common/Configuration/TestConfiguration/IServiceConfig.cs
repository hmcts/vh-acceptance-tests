namespace AcceptanceTests.Common.Configuration.TestConfiguration
{
    public interface IServiceConfig
    {
         string AdminWebUrl { get; set; }
         string AdminWebResourceId { get; set; }
         string BookingsApiUrl { get; set; }
         string BookingsApiResourceId { get; set; }
         string KinlySelfTestScoreEndpointUrl { get; set; }
         string PexipSelfTestNodeUri { get; set; }
         string ServiceWebUrl { get; set; }
         string ServiceWebResourceId { get; set; }
         string VideoApiUrl { get; set; }
         string VideoApiResourceId { get; set; }
         string VideoWebUrl { get; set; }
         string VideoWebResourceId { get; set; }
         string UserApiUrl { get; set; }
         string UserApiResourceId { get; set; }
    }
}
