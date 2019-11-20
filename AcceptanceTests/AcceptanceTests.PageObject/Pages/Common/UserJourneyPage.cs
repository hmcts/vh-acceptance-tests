using System.Collections.Generic;
using AcceptanceTests.Driver.Drivers;
using AcceptanceTests.PageObject.Components;
using Coypu;
using OpenQA.Selenium;

namespace AcceptanceTests.PageObject.Pages.Common
{
    public class UserJourneyPage : Page, IForm
    {
        protected List<Page> _pages = new List<Page>();

        public UserJourneyPage(BrowserSession driver) : base(driver)
        {
        }

        public List<Page> Pages { get => _pages; }

        public virtual void Continue()
        {
            IsPageLoaded();
            DriverExtension.WaitUntilElementVisible(WrappedDriver, By.Id("next")).Click();
        }

        public virtual void FillDetails()
        {
            throw new System.NotImplementedException("Please override and implement this method in the subclass");
        }
    }
}
