using AcceptanceTests.Common.Driver.Browser;
using AcceptanceTests.Common.Driver.Helpers;
using AcceptanceTests.Common.PageObject.Pages;
using FluentAssertions;

namespace AcceptanceTests.Common.Test.Steps
{
    public sealed class LoginSharedSteps
    {
        private readonly UserBrowser _browser;
        private readonly string _username;
        private readonly string _password;

        public LoginSharedSteps(UserBrowser browser, string username, string password)
        {
            _browser = browser;
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
            _browser.Click(LoginPage.ReSignInButton(_username));
            EnterPassword(_password);
            ClickSignInButton();
        }

        private void EnterUsername(string username)
        {
            _browser.Driver.WaitUntilVisible(LoginPage.UsernameTextfield).Clear();
            _browser.Driver.WaitUntilVisible(LoginPage.UsernameTextfield).SendKeys(username);
        }

        private void ClickNextButton() => _browser.Click(LoginPage.Next);

        private void EnterPassword(string password)
        {
            _browser.Driver.WaitUntilVisible(LoginPage.PasswordField).Clear();
            _browser.Driver.WaitUntilVisible(LoginPage.PasswordField).SendKeys(password);
        }

        private void ClickSignInButton() => _browser.Click(LoginPage.SignIn);

        public void WhenTheUserAttemptsToLogout()
        {
            _browser.Click(CommonPages.SignOutLink);
            _browser.Driver.WaitUntilVisible(CommonPages.SignOutMessage).Displayed.Should().BeTrue();
            _browser.Click(CommonPages.SignInLink);
        }
        public void ThenTheSignOutLinkIsDisplayed()
        {
            _browser.Driver.WaitUntilVisible(CommonPages.SignOutLink).Displayed.Should().BeTrue();
        }
    }
}
