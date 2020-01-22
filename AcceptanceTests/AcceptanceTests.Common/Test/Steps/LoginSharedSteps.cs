using AcceptanceTests.Common.Driver.Helpers;
using AcceptanceTests.Common.PageObject.Pages;
using FluentAssertions;
using Protractor;

namespace AcceptanceTests.Common.Test.Steps
{
    public sealed class LoginSharedSteps
    {
        private readonly NgWebDriver _driver;
        private readonly string _username;
        private readonly string _password;

        public LoginSharedSteps(NgWebDriver driver, string username, string password)
        {
            _driver = driver;
            _username = username;
            _password = password;
        }
        public void ProgressToNextPage()
        {
            EnterUsername(_username);
            ClickNextButton();
            EnterPassword(_password);
            ClickSignInButton();
        }

        public void ReSignBackIn()
        {
            _driver.WaitUntilElementClickable(LoginPage.ReSignInButton(_username)).Click();
            EnterPassword(_password);
            ClickSignInButton();
        }

        private void EnterUsername(string username)
        {
            _driver.WaitUntilVisible(LoginPage.UsernameTextfield).Clear();
            _driver.WaitUntilVisible(LoginPage.UsernameTextfield).SendKeys(username);
        }

        private void ClickNextButton() => _driver.WaitUntilVisible(LoginPage.Next).Click();

        private void EnterPassword(string password)
        {
            var maskedPassword = new string('*', (password ?? string.Empty).Length);
            NUnit.Framework.TestContext.WriteLine($"Using password {maskedPassword}");
            _driver.WaitUntilVisible(LoginPage.PasswordField).Clear();
            _driver.WaitUntilVisible(LoginPage.PasswordField).SendKeys(password);
        }

        private void ClickSignInButton() => _driver.WaitUntilVisible(LoginPage.SignIn).Click();

        public void WhenTheUserAttemptsToLogout()
        {
            _driver.WaitUntilElementClickable(CommonPages.SignOutLink).Click();
            _driver.WaitUntilVisible(CommonPages.SignOutMessage).Displayed.Should().BeTrue();
            _driver.WaitUntilElementClickable(CommonPages.SignInLink).Click();
        }
        public void ThenTheSignOutLinkIsDisplayed()
        {
            _driver.WaitUntilVisible(CommonPages.SignOutLink).Displayed.Should().BeTrue();
        }
    }
}
