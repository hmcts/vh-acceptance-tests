using System.Collections.Generic;
using AcceptanceTests.Common.Model.FormData;
using AcceptanceTests.Common.PageObject.Components.Forms;
using AcceptanceTests.Driver.DriverExtensions;
using Coypu;
using OpenQA.Selenium;

namespace AcceptanceTests.Common.PageObject.Pages.Common
{
    public class UserJourneyPage : Page
    {
        protected List<IFormComponent> _pageFormList;
        
        public UserJourneyPage(BrowserSession driver, string uri, string headingText) : base(driver, uri)
        {
            HeadingText = headingText;
        }

        public UserJourneyPage(BrowserSession driver, string uri) : base(driver, uri)
        {
        }

        public virtual void FillDetails(IFormData formData)
        {
            foreach (var form in _pageFormList)
            {
                form.FillFormDetails(formData);
            }
        }

        public virtual void Continue()
        {
            IsPageLoaded();
            ClickDriverExtension.ClickElement(WrappedDriver, By.Id("nextButton"));
        }

        public virtual void Cancel()
        {
            IsPageLoaded();
            ClickDriverExtension.ClickElement(WrappedDriver, By.Id("cancelButton"));
        }
    }
}
