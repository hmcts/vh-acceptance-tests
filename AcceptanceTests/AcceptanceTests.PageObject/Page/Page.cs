using System;
using Coypu;

namespace AcceptanceTests.PageObject.Page
{
    public class Page : IPage
    {
        
        public string HeadingText { get; protected set; }
        public string Path { get; protected set; }
        public BrowserSession WrappedDriver { get; protected set; }

        protected Page(BrowserSession driver)
        {
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
            AssertCurrentPageLocation(Path);
            AssertCurrentPageHeading(HeadingText);
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
    } 
}
