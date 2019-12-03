﻿using AcceptanceTests.Common.Configuration.TestUserConfig;
using AcceptanceTests.Common.Model.Context;
using AcceptanceTests.Common.Model.User;

namespace AcceptanceTests.Common.PageObject.Helpers
{
    public class UserHelper
    {
        public UserHelper()
        {
        }

        public static UserAccountBase SetCurrentUser(ITestContext testContext, string role)
        {
            var currentUser = testContext.UserContext.GetFirstOrDefaultUserByRole(role);
            currentUser.Username = testContext.UserContext.GetUsername(currentUser);
            return currentUser;
        }
    }
}