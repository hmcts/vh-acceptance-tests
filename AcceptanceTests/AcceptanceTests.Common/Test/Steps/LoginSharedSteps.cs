using AcceptanceTests.Common.Driver.Helpers;
using AcceptanceTests.Common.PageObject.Pages;
using FluentAssertions;
using Protractor;

namespace AcceptanceTests.Common.Test.Steps
{
    public sealed class LoginSharedSteps
    {
        private readonly NgWebDriver _driver;
        private readonly LoginPage _loginPage;
        private readonly CommonPages _commonPages;
        private readonly string _username;
        private readonly string _password;

        public LoginSharedSteps(NgWebDriver driver, LoginPage loginPage, CommonPages commonPages, 
            string username, string password)
        {
            _driver = driver;
            _loginPage = loginPage;
            _commonPages = commonPages;
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

        private void EnterUsername(string username)
        {
            _driver.WaitUntilVisible(_loginPage.UsernameTextfield).Clear();
            _driver.WaitUntilVisible(_loginPage.UsernameTextfield).SendKeys(username);
        }

        private void ClickNextButton() => _driver.WaitUntilVisible(_loginPage.Next).Click();

        private void EnterPassword(string password)
        {
            var maskedPassword = new string('*', (password ?? string.Empty).Length);
            NUnit.Framework.TestContext.WriteLine($"Using password {maskedPassword}");
            _driver.WaitUntilVisible(_loginPage.PasswordField).Clear();
            _driver.WaitUntilVisible(_loginPage.PasswordField).SendKeys(password);
        }

        private void ClickSignInButton() => _driver.WaitUntilVisible(_loginPage.SignIn).Click();

        public void WhenTheUserAttemptsToLogout()
        {
            _driver.WaitUntilElementClickable(_commonPages.SignOutLink).Click();
            _driver.WaitUntilVisible(_commonPages.SignOutMessage).Displayed.Should().BeTrue();
            _driver.WaitUntilElementClickable(_commonPages.SignInLink).Click();
        }
        public void ThenTheSignOutLinkIsDisplayed()
        {
            _driver.WaitUntilVisible(_commonPages.SignOutLink).Displayed.Should().BeTrue();
        }
    }
}
