using System;
using System.Linq;
using System.Threading;
using AcceptanceTests.Common.Driver.Enums;
using AcceptanceTests.Common.Driver.Helpers;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Polly;
using Protractor;

namespace AcceptanceTests.Common.Driver.Drivers
{
    public class UserBrowser
    {
        // 4 retries ^2 will execute after 2 seconds, then 4, 8 then finally 16 (30 seconds total)
        private const int ActionRetries = 4;
        private const int BrowserRetries = 4;
        private string _baseUrl;
        public NgWebDriver AngularDriver { get; set; }
        private DriverSetup DriverServices { get; set; }
        private IWebDriver WebDriver;
        public string LastWindowName { get; set; }
        public TargetBrowser _targetBrowser;
        private TargetDevice _targetDevice;

        public UserBrowser SetBaseUrl(string baseUrl)
        {
            _baseUrl = baseUrl;
            return this;
        }

        public UserBrowser SetDriver(DriverSetup driver)
        {
            DriverServices = driver;
            WebDriver = driver.GetDriver();
            return this;
        }

        public UserBrowser SetTargetDevice(TargetDevice targetDevice)
        {
            _targetDevice = targetDevice;
            return this;
        }

        public UserBrowser SetTargetBrowser(TargetBrowser targetBrowser)
        {
            _targetBrowser = targetBrowser;
            return this;
        }

        public void LaunchBrowser()
        {
            AngularDriver = new NgWebDriver(WebDriver) {IgnoreSynchronization = true};

            if (_targetDevice == TargetDevice.Desktop)
            {
                TryMaximizeBrowser();
            }
        }

        private void TryMaximizeBrowser()
        {
            try
            {
                WebDriver.Manage().Window.Maximize();
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

            WebDriver.Navigate().GoToUrl($"{_baseUrl}{url}");
        }

        public void Retry(Action action, int times = ActionRetries)
        {
            Policy
                .Handle<Exception>()
                .WaitAndRetry(times, retryAttempt => 
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (exception, timeSpan, context) => { NUnit.Framework.TestContext.WriteLine($"Encountered error '{exception.Message}' after {timeSpan.Seconds} seconds. Retrying..."); })
                .Execute(action);
        }

        public string SwitchTab(string titleOrUrl, int timeout = 10)
        {
            for (var i = 0; i < timeout; i++)
            {
                foreach (var window in AngularDriver.WrappedDriver.WindowHandles)
                {
                    var tab = AngularDriver.SwitchTo().Window(window);
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
                AngularDriver.Url.Trim().ToLower().Should().NotContain(page.ToLower());
            }
            else
            {
                Retry(() => AngularDriver.Url.Trim().ToLower().Should().Contain(page.ToLower()), BrowserRetries);
            }
        }

        public void Refresh()
        {
            AngularDriver.Navigate().Refresh();
        }

        public void CloseTab()
        {
            AngularDriver.Close();
            AngularDriver.SwitchTo().Window(AngularDriver.WrappedDriver.WindowHandles.First());
        }

        public void ScrollTo(By element)
        {
            AngularDriver.ExecuteScript("arguments[0].scrollIntoView(true);", AngularDriver.FindElement(element));
        }

        public void ScrollToTheTopOfThePage()
        {
            ScrollTo(By.TagName("header"));
        }

        public void Click(By element, int timeout = 20)
        {
            if (_targetDevice != TargetDevice.Tablet)
            {
                AngularDriver.WaitUntilElementClickable(element, timeout);
            }
            BrowserClick(element);
        }

        private void BrowserClick(By element)
        {
            if (_targetBrowser == TargetBrowser.Firefox ||
                _targetDevice == TargetDevice.Tablet)
            {
                AngularDriver.WaitUntilVisible(element).Click();
            }
            else
            {
                AngularDriver.ExecuteScript("arguments[0].click();", AngularDriver.FindElement(element));
            }
        }

        public bool IsDisplayed(By element)
        {
            try
            {
                return AngularDriver.FindElement(element).Displayed;
            }
            catch
            {
                return false;
            }
        }

        public void ClickLink(By element, int timeout = 20)
        {
            AngularDriver.WaitUntilVisible(element, timeout);
            AngularDriver.ExecuteScript("arguments[0].click();", AngularDriver.FindElement(element));
        }

        public void ClickRadioButton(By element, int timeout = 20)
        {
            AngularDriver.WaitUntilElementExists(element, timeout);
            AngularDriver.ExecuteScript("arguments[0].click();", AngularDriver.FindElement(element));
        }

        public void ClickCheckbox(By element, int timeout = 20)
        {
            AngularDriver.WaitUntilElementExists(element, timeout);
            AngularDriver.ExecuteScript("arguments[0].click();", AngularDriver.FindElement(element));
        }

        public void Clear(By element)
        {
            AngularDriver.WaitUntilVisible(element);
            AngularDriver.ExecuteScript("arguments[0].value = '';", AngularDriver.FindElement(element));
            AngularDriver.WaitUntilTextEmpty(element);
        }

        public void NavigateBack()
        {
            AngularDriver.Navigate().Back();
        }

        public void WaitForPageToLoad(int timeout = 20)
        {
            new WebDriverWait(AngularDriver, TimeSpan.FromSeconds(timeout)).Until(
                d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public void BrowserTearDown()
        {
            AngularDriver?.Quit();
            AngularDriver?.Dispose();
            DriverServices?.StopServices();
        }
    }
}
