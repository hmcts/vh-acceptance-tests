using AcceptanceTests.Common.Driver;
using AcceptanceTests.Common.Driver.Drivers;
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
            _browser.AngularDriver.WaitUntilVisible(LoginPage.LoginHeader).Click();
            _browser.AngularDriver.WaitUntilVisible(LoginPage.UsernameTextfield).Clear();
            _browser.AngularDriver.WaitUntilVisible(LoginPage.UsernameTextfield).SendKeys(username);
        }

        private void ClickNextButton() => _browser.Click(LoginPage.Next);

        private void EnterPassword(string password)
        {
            _browser.AngularDriver.WaitUntilVisible(LoginPage.PasswordField).Clear();
            _browser.AngularDriver.WaitUntilVisible(LoginPage.PasswordField).SendKeys(password);
        }

        private void ClickSignInButton() => _browser.Click(LoginPage.SignIn);

        public void WhenTheUserAttemptsToLogout()
        {
            _browser.Click(CommonPages.SignOutLink);
            _browser.AngularDriver.WaitUntilVisible(CommonPages.SignOutMessage).Displayed.Should().BeTrue();
            _browser.Click(CommonPages.SignInLink);
        }
        public void ThenTheSignOutLinkIsDisplayed()
        {
            _browser.AngularDriver.WaitUntilVisible(CommonPages.SignOutLink).Displayed.Should().BeTrue();
        }

        public void ChangeThePassword(string oldPassword, string newPassword)
        {
            _browser.AngularDriver.WaitUntilVisible(LoginPage.CurrentPassword).Clear();
            _browser.AngularDriver.WaitUntilVisible(LoginPage.CurrentPassword).SendKeys(oldPassword);
            _browser.AngularDriver.WaitUntilVisible(LoginPage.NewPassword).Clear();
            _browser.AngularDriver.WaitUntilVisible(LoginPage.NewPassword).SendKeys(newPassword);
            _browser.AngularDriver.WaitUntilVisible(LoginPage.ConfirmNewPassword).Clear();
            _browser.AngularDriver.WaitUntilVisible(LoginPage.ConfirmNewPassword).SendKeys(newPassword); 
            _browser.Click(LoginPage.SignInButtonAfterPasswordChange);
        }
    }
}
