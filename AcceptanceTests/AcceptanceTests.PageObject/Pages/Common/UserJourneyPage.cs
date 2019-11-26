using System.Collections.Generic;
using AcceptanceTests.Driver.DriverExtensions;
using AcceptanceTests.PageObject.Components.Forms;
using Coypu;
using OpenQA.Selenium;

namespace AcceptanceTests.PageObject.Pages.Common
{
    public class UserJourneyPage : Page
    {
        protected List<string> breadcrumbs = new List<string> { "Hearing details", "Hearing schedule", "Assign judge",
                                                        "Add participants", "Other information", "Summary"};
        protected List<IFormComponent> _pageFormList;
        
        public UserJourneyPage(BrowserSession driver, string uri, string headingText) : base(driver, uri)
        {
            HeadingText = headingText;
        }

        public UserJourneyPage(BrowserSession driver, string uri) : base(driver, uri)
        {
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
            ButtonDriverExtension.ClickElement(WrappedDriver, By.Id("nextButton"));
        }

        public virtual void Cancel()
        {
            IsPageLoaded();
            ButtonDriverExtension.ClickElement(WrappedDriver, By.Id("cancelButton"));
        }
    }
}
