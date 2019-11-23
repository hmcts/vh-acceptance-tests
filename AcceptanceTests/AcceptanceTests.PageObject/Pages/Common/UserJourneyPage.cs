using System.Collections.Generic;
using AcceptanceTests.Driver.DriverExtensions;
using AcceptanceTests.PageObject.Components;
using Coypu;
using OpenQA.Selenium;

namespace AcceptanceTests.PageObject.Pages.Common
{
    public class UserJourneyPage : Page
    {
        List<IFormComponent> _pageFormList;
        
        public UserJourneyPage(BrowserSession driver, string uri, string headingText) : base(driver, uri)
        {
            HeadingText = headingText;
        }

        public virtual void FillDetails(object formData)
        {
            foreach (var form in _pageFormList)
            {
                form.FillFormDetails(formData);
            }
        }

        public virtual void Continue()
        {
            IsPageLoaded();
            WaitDriverExtension.WaitUntilElementVisible(WrappedDriver, By.Id("next")).Click();
        }
    }
}
