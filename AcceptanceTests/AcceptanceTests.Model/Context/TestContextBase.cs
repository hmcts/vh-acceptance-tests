namespace AcceptanceTests.Model.Context
{
    public class TestContextBase : ITestContext
    {
        public string CurrentApp { get; set; }
        public UserContext UserContext { get; set; }
        public string BaseUrl { get; set; }
        public string VideoAppUrl { get; set; }
        public string BookingsApiBearerToken { get; set; }
        public string BookingsApiBaseUrl { get; set; }
    }
}
