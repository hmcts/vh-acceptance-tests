namespace AcceptanceTests.Model.Context
{
    public interface ITestContext
    {
        string CurrentApp { get; set; }
        string TargetBrowser { get; }
        UserContext UserContext { get; set; }
        string BaseUrl { get; set; }
        string VideoAppUrl { get; set; }
        string BookingsApiBearerToken { get; set; }
        string BookingsApiBaseUrl { get; set; }
    }
}