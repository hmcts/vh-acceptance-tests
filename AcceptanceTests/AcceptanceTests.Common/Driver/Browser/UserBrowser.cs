using System;
using System.Linq;
using AcceptanceTests.Common.Configuration.Users;
using AcceptanceTests.Common.Driver.Helpers;
using FluentAssertions;
using OpenQA.Selenium;
using Polly;
using Protractor;

namespace AcceptanceTests.Common.Driver.Browser
{
    public class UserBrowser
    {
        private const int BrowserRetries = 2;
        private string _videoFileName;
        private string _baseUrl;
        public NgWebDriver Driver { get; set; }
        private DriverSetup _driver;
        public string LastWindowName { get; set; }

        public UserBrowser(UserAccount user)
        {
            SetFileName(user);
        }

        public UserBrowser SetBaseUrl(string baseUrl)
        {
            _baseUrl = baseUrl;
            return this;
        }

        public UserBrowser SetDriver(DriverSetup driver)
        {
            _driver = driver;
            return this;
        }

        private void SetFileName(UserAccount user)
        {
            _videoFileName = user.Role.Equals("Judge") || user.Role.Equals("Clerk") || user.Role.Equals("Video Hearings Officer")
                ? "clerk.y4m"
                : $"{user.Lastname.ToLower()}.y4m";
        }

        public void LaunchBrowser()
        {
            var driver = _driver.GetDriver(_videoFileName);
            Driver = new NgWebDriver(driver);
            TryMaximizeBrowser();
            Driver.IgnoreSynchronization = true;
        }

        private void TryMaximizeBrowser()
        {
            try
            {
                Driver.Manage().Window.Maximize();
            }
            catch (NotImplementedException e)
            {
                NUnit.Framework.TestContext.WriteLine("Skipping maximize, not supported on current platform: " + e.Message);
            }
        }

        public void NavigateToPage(string url = "")
        {
            if (string.IsNullOrEmpty(_baseUrl))
            {
                throw new InvalidOperationException("BaseUrl has not been set");
            }

            NUnit.Framework.TestContext.WriteLine($"Navigating to {_baseUrl}{url}");
            Driver.WrappedDriver.Navigate().GoToUrl($"{_baseUrl}{url}");
        }

        public void Retry(Action action, int times = BrowserRetries)
        {
            Policy
                .Handle<Exception>()
                .WaitAndRetry(times, retryAttempt => TimeSpan.FromSeconds(Math.Pow(5, retryAttempt)))
                .Execute(action);
        }

        public string SwitchTab(string titleOrUrl)
        {
            foreach (var window in Driver.WrappedDriver.WindowHandles)
            {
                var tab = Driver.SwitchTo().Window(window);
                if (tab.Title.Trim().ToLower().Contains(titleOrUrl.ToLower()) || tab.Url.Trim().ToLower().Contains(titleOrUrl.ToLower()))
                {
                    return window;
                }
            }
            throw new ArgumentException($"No windows with title or Url '{titleOrUrl}' were found.");
        }

        public void PageUrl(string page, bool notOnThePage = false)
        {
            if (notOnThePage)
            {
                Driver.Url.Trim().ToLower().Should().NotContain(page.ToLower());
            }
            else
            {
                Retry(() => Driver.Url.Trim().ToLower().Should().Contain(page.ToLower()), BrowserRetries);
            }
        }
        public void CloseTab()
        {
            Driver.Close();
            Driver.SwitchTo().Window(Driver.WrappedDriver.WindowHandles.First());
        }

        public void ScrollTo(By element)
        {
            Driver.WaitUntilVisible(element);
            Driver.ExecuteScript("arguments[0].scrollIntoView(true);", Driver.FindElement(element));
        }

        public void NavigateBack()
        {
            Driver.Navigate().Back();
        }

        public void BrowserTearDown()
        {
            Driver?.Quit();
            Driver?.Dispose();
        }
    }
}
