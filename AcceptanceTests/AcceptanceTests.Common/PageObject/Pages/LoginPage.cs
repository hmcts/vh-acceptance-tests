using OpenQA.Selenium;

namespace AcceptanceTests.Common.PageObject.Pages
{
    public static class LoginPage
    {
        public static By UsernameTextfield = By.Id("i0116");
        public static By PasswordField = By.Id("i0118");
        public static By LoginHeader = By.Id("loginHeader");
        public static By Next = By.Id("idSIButton9");
        public static By SignIn = By.Id("idSIButton9");
        public static string SignInTitle = "Sign in to your account";
        public static By ReSignInButton(string username) => By.XPath($"//div[contains(text(), '{username}')]");
        public static By CurrentPassword = By.Id("currentPassword");
        public static By NewPassword = By.Id("newPassword");
        public static By ConfirmNewPassword = By.Id("confirmNewPassword");
        public static By SignInButtonAfterPasswordChange = By.Id("idSIButton9");
    }
}
