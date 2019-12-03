namespace AcceptanceTests.Common.Configuration.TestUserConfig
{
    public class UserAccountBase : IUserAccount
    {
        public string Key { get; set; }
        public string Role { get; set; }
        public string AlternativeEmail { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string DisplayName { get; set; }
        public string Username { get; set; }
    }
}
