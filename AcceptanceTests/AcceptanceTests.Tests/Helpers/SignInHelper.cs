using System;
using AcceptanceTests.Driver.Support;
using AcceptanceTests.Model.Context;
using AcceptanceTests.PageObject.Page;
using Coypu;

namespace AcceptanceTests.Tests.Helpers
{
    public class SignInHelper
    {
        public static void SignIn(BrowserSession driver, ITestContext testContext)
        {
            SignInPage page = new SignInPage(driver);
            page.Visit();
            page.SignInAs(testContext.UserContext.CurrentUser.Username,
                            testContext.UserContext.TestUserSecrets.TestUserPassword);
        }
    }
}
