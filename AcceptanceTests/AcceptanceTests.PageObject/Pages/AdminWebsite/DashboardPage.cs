using System;
using System.Collections.Generic;
using System.Linq;
using AcceptanceTests.Driver.DriverExtensions;
using AcceptanceTests.PageObject.Pages.Common;
using Coypu;
using OpenQA.Selenium;

namespace AcceptanceTests.PageObject.Pages.AdminWebsite
{
    public class DashboardPage : UserJourneyPage
    {
        private const string PanelTitleLocator = "h1.vhpanel-title";
        private static By BookHearingPanelButton => By.XPath("//*[@id='vhpanel-green']/h1");
        private static By QuestionnaireResultPanelButton => By.XPath("//*[@id='vhpanel-blue']/h1");

        public DashboardPage(BrowserSession driver, string uri) : base(driver, uri)
        {
        }

        public bool IsPanelElementsCountDisplayed(int expectedPanelCount)
        {
            var panelElements = GetDashboardPanelElements();
            return panelElements.Count() == expectedPanelCount;
        }

        public bool IsCorrectPanelTitleDisplayed(string expectedPanelTitle)
        {
            var panelElements = GetDashboardPanelElements();
            var panel = panelElements.FirstOrDefault(x => x.Text.Equals(expectedPanelTitle, StringComparison.InvariantCultureIgnoreCase));
            return panel != null;
        }

        public IEnumerable<ElementScope> GetDashboardPanelElements()
        {
            var elements = WaitDriverExtension.WaitForElementsPresentByCss(WrappedDriver, PanelTitleLocator);
            
            return elements;
        }

        public void BookHearing() => ClickDriverExtension.ClickElement(WrappedDriver, BookHearingPanelButton);
        public void QuestionnaireResult() => ClickDriverExtension.ClickElement(WrappedDriver, QuestionnaireResultPanelButton);
    }
}
