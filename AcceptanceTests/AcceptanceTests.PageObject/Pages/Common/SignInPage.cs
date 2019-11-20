using System;
using Coypu;

namespace AcceptanceTests.PageObject.Pages.Common
{
    public class SignInPage : Page
    {
        private string _usernameTextfieldLocator= "i0116";
        private string _passwordFieldLocator = "//input[contains(@data-bind,'password')]";
        private string _nextButton = "//input[contains(@data-bind,'Next') and (@value='Next')]";
        private string _signInButton = "//input[contains(@data-bind,'SignIn') and (@value='Sign in')]";

        public SignInPage(BrowserSession driver): base(driver)
        {
            HeadingText = "Sign in to your account";
            Path = "login.microsoftonline.com";
        }

        public void SignInAs(string username, string password)
        {
            EnterUsername(username);
            NextButton();
            EnterPassword(password);
            SignInButton();
        }

        public void EnterUsername(string username)
        {
            Console.WriteLine($"Logging in as {username}");
            var element = WrappedDriver.FindId(_usernameTextfieldLocator);
            element.FillInWith(username);
        }

        public void EnterPassword(string password)
        {
            string maskedPassword = new string('*', (password ?? string.Empty).Length);
            Console.WriteLine($"Using password {maskedPassword}");
            var element = WrappedDriver.FindXPath(_passwordFieldLocator);
            element.FillInWith(password);
        }

        public void NextButton()
        {
            var element = WrappedDriver.FindXPath(_nextButton);
            element.Click();
        }

        public void SignInButton()
        {
            var element = WrappedDriver.FindXPath(_signInButton);
            element.Click();
        }
    }
}
