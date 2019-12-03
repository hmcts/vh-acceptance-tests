namespace AcceptanceTests.Common.Configuration.TestUserConfig
{
    public class TestSettingsBase : ITestSettings
    {
        public string TestClientId { get; set; }
        public string TestClientSecret { get; set; }
        public string TestUserPassword { get; set; }
        public string TestUsernameStem { get; set; }
    }
}
