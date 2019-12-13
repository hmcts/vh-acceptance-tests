namespace AcceptanceTests.Common.Configuration
{
    public interface ITestSettingsConfig
    {
        string TestUsernameStem { get; set; }
        string TestUserPassword { get; set; }
    }
}
