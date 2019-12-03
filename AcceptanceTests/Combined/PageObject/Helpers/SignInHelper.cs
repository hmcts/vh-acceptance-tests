using AcceptanceTests.Common.Model.Context;
using AcceptanceTests.Common.PageObject.Pages.Common;
using Coypu;

namespace AcceptanceTests.Common.PageObject.Helpers
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
