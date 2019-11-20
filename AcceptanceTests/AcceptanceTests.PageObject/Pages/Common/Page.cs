using System;
using Coypu;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AcceptanceTests.PageObject.Pages.Common
{
    public class Page : IPage
    {
        
        public string HeadingText { get; protected set; }
        public string Path { get; protected set; }
        public BrowserSession WrappedDriver { get; protected set; }

        public Page(BrowserSession driver)
        {
            WrappedDriver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        public Page(BrowserSession driver, string path)
        {
            Path = path;
            WrappedDriver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        public void Visit()
        {
            if (!IsPageLoaded())
            {
                WrappedDriver.Visit(Path);
                IsPageLoaded();
            }
            
        }

        public bool IsPageLoaded()
        {
            WaitForPageToLoad();
            AssertCurrentPageLocation(Path);
            return true;
        }

        protected void AssertCurrentPageLocation(string expectedLocation)
        {
            if (!WrappedDriver.Location.ToString().Contains(expectedLocation))
            {
                throw new ArgumentException($"'{expectedLocation}' page is not the current page. The actual page was '{WrappedDriver.Location}'");
            }
        }

        protected void AssertCurrentPageHeading(string pageHeading)
        {
            bool containsPageHeading = WrappedDriver.HasContent(pageHeading);
            var browserTitle = WrappedDriver.Title;
            if (!(containsPageHeading || browserTitle.Contains(pageHeading)))
            {
                throw new ArgumentException($"'{pageHeading}' page is not the current page. The actual page was '{WrappedDriver.Title}'");
            }      
        }

        public void WaitForPageToLoad()
        {
            try
            {
                var wait = new WebDriverWait((IWebDriver)WrappedDriver.Native, TimeSpan.FromSeconds(20));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlContains(Path));
            }
            catch (Exception)
            {
                throw new ArgumentException($"'{Path}' page is not the current page. The actual page was '{WrappedDriver.Location}'");
            }
        }
    } 
}
