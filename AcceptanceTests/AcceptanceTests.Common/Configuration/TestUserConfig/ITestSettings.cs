namespace AcceptanceTests.Common.Configuration.TestUserConfig
{
    public interface ITestSettings
    {
        string TestClientId { get; set; }
        string TestClientSecret { get; set; }
        string TestUserPassword { get; set; }
        string TestUsernameStem { get; set; }
    }
}
