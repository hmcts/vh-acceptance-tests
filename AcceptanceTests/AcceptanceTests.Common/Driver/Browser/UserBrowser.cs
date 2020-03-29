using System;
using System.Linq;
using System.Threading;
using AcceptanceTests.Common.Configuration.Users;
using AcceptanceTests.Common.Driver.Helpers;
using AcceptanceTests.Common.Driver.Support;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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
        public TargetBrowser _targetBrowser;

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

        public UserBrowser SetTargetBrowser(TargetBrowser targetBrowser)
        {
            _targetBrowser = targetBrowser;
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

        public void StopEdgeChromiumServer()
        {
            _driver.StopLocalEdgeChromiumService();
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

        public string SwitchTab(string titleOrUrl, int timeout = 10)
        {
            for (var i = 0; i < timeout; i++)
            {
                foreach (var window in Driver.WrappedDriver.WindowHandles)
                {
                    var tab = Driver.SwitchTo().Window(window);
                    if (tab.Title.Trim().ToLower().Contains(titleOrUrl.ToLower()) || tab.Url.Trim().ToLower().Contains(titleOrUrl.ToLower()))
                    {
                        return window;
                    }
                }

                Thread.Sleep(TimeSpan.FromSeconds(1));
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

        public void Refresh()
        {
            Driver.Navigate().Refresh();
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

        public void Click(By element, int timeout = 20)
        {
            Driver.WaitUntilElementClickable(element, timeout);
            BrowserClick(element);
        }

        private void BrowserClick(By element)
        {
            if (_targetBrowser == TargetBrowser.Firefox)
            {
                Driver.WaitUntilVisible(element).Click();
            }
            else
            {
                Driver.ExecuteScript("arguments[0].click();", Driver.FindElement(element));
            }
        }

        public void ClickLink(By element, int timeout = 20)
        {
            Driver.WaitUntilVisible(element, timeout);
            Driver.ExecuteScript("arguments[0].click();", Driver.FindElement(element));
        }

        public void ClickRadioButton(By element, int timeout = 20)
        {
            Driver.WaitUntilElementExists(element, timeout);
            Driver.ExecuteScript("arguments[0].click();", Driver.FindElement(element));
        }

        public void ClickCheckbox(By element, int timeout = 20)
        {
            Driver.WaitUntilElementExists(element, timeout);
            Driver.ExecuteScript("arguments[0].click();", Driver.FindElement(element));
        }

        public void Clear(By element)
        {
            Driver.WaitUntilVisible(element);
            Driver.ExecuteScript("arguments[0].value = '';", Driver.FindElement(element));
        }

        public void NavigateBack()
        {
            Driver.Navigate().Back();
        }

        public void WaitForPageToLoad(int timeout = 20)
        {
            new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout)).Until(
                d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public void BrowserTearDown()
        {
            Driver?.Quit();
            Driver?.Dispose();
        }
    }
}
