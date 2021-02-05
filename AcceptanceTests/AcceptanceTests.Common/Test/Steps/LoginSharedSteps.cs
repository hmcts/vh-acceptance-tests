using AcceptanceTests.Common.Driver.Drivers;
using AcceptanceTests.Common.Driver.Enums;
using AcceptanceTests.Common.Driver.Helpers;
using AcceptanceTests.Common.PageObject.Pages;
using FluentAssertions;
using System;
using System.Threading;

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
            var timeOut = _browser.TargetDevice == TargetDevice.Tablet ? 50 : 30;
            EnterUsername(_username, timeOut);
            ClickNextButton();
            if(_browser.TargetDevice == TargetDevice.Tablet) Thread.Sleep(TimeSpan.FromSeconds(3));
            EnterPassword(_password, timeOut);
            ClickSignInButton();
        }

        public void ReSignBackIn()
        {
            var timeOut = _browser.TargetDevice == TargetDevice.Tablet ? 60 : 30;

            _browser.Click(LoginPage.ReSignInButton(_username));
            EnterPassword(_password, timeOut);
            ClickSignInButton();
        }

        private void EnterUsername(string username, int timeOut)
        {
            _browser.Driver.WaitUntilVisible(LoginPage.LoginHeader, timeOut).Click();
            _browser.Driver.WaitUntilVisible(LoginPage.UsernameTextfield, timeOut).Clear();
            _browser.Driver.WaitUntilVisible(LoginPage.UsernameTextfield, timeOut).SendKeys(username);
        }

        private void ClickNextButton() => _browser.Click(LoginPage.Next);

        private void EnterPassword(string password, int timeOut)
        {
            _browser.Driver.WaitUntilVisible(LoginPage.PasswordField, timeOut).Clear();
            _browser.Driver.WaitUntilVisible(LoginPage.PasswordField, timeOut).SendKeys(password);
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

        public void ChangeThePassword(string oldPassword, string newPassword)
        {
            _browser.Driver.WaitUntilVisible(LoginPage.CurrentPassword).Clear();
            _browser.Driver.WaitUntilVisible(LoginPage.CurrentPassword).SendKeys(oldPassword);
            _browser.Driver.WaitUntilVisible(LoginPage.NewPassword).Clear();
            _browser.Driver.WaitUntilVisible(LoginPage.NewPassword).SendKeys(newPassword);
            _browser.Driver.WaitUntilVisible(LoginPage.ConfirmNewPassword).Clear();
            _browser.Driver.WaitUntilVisible(LoginPage.ConfirmNewPassword).SendKeys(newPassword); 
            _browser.Click(LoginPage.SignInButtonAfterPasswordChange);
        }
    }
}
