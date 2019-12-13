using OpenQA.Selenium;

namespace AcceptanceTests.Common.PageObject.Pages
{
    public class LoginPage
    {
        public By UsernameTextfield = By.CssSelector("#i0116");
        public By PasswordField = By.XPath("//input[contains(@data-bind,'password')]");
        public By Next = By.XPath("//input[contains(@data-bind,'Next') and (@value='Next')]");
        public By SignIn = By.XPath("//input[contains(@data-bind,'SignIn') and (@value='Sign in')]");
        public string SignInTitle = "Sign in to your account";
    }
}
