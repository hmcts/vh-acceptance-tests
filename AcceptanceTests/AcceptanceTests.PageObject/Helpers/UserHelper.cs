using AcceptanceTests.Model.Context;
using AcceptanceTests.Model.User;

namespace AcceptanceTests.PageObject.Helpers
{
    public class UserHelper
    {
        public UserHelper()
        {
        }

        public static TestUser SetCurrentUser(ITestContext testContext, string role)
        {
            var currentUser = testContext.UserContext.GetFirstOrDefaultUserByRole(role);
            currentUser.Username = testContext.UserContext.GetUsername(currentUser);
            return currentUser;
        }
    }
}
