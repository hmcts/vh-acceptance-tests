using System.Collections.Generic;
using AcceptanceTests.Common.Model.User;

namespace AcceptanceTests.Common.Configuration.TestUserConfig
{
    public class TestUserBase : ITestUser
    {
        public string TestClientId { get; set; }
        public string TestClientSecret { get; set; }
        public string TestUserPassword { get; set; }
        public string TestUsernameStem { get; set; }
        public List<UserAccountBase> UserAccounts { get; set; }
    }
}
