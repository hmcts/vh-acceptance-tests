using System.Collections.Generic;
using AcceptanceTests.Common.Model.User;

namespace AcceptanceTests.Common.Configuration.TestUserConfig
{
    public interface ITestUser
    {
        string TestClientId { get; set; }
        string TestClientSecret { get; set; }
        string TestUserPassword { get; set; }
        string TestUsernameStem { get; set; }
        List<UserAccountBase> UserAccounts { get; set; }
    }
}
