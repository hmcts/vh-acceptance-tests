using System;
using System.Collections.Generic;
using System.Linq;
using AcceptanceTests.Driver.DriverExtensions;
using AcceptanceTests.PageObject.Pages.Common;
using Coypu;

namespace AcceptanceTests.PageObject.Pages.AdminWebsite
{
    public class DashboardPage : UserJourneyPage
    {
        private const string PanelTitleLocator = "h1.vhpanel-title";
        private static string BookHearingPanelButton => "//*[@id='vhpanel-green']/h1";
        private static string QuestionnaireResultPanelButton => "//*[@id='vhpanel-blue']/h1";

        public DashboardPage(BrowserSession driver, string uri, string headingText) : base(driver, uri, headingText)
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
            var elements = WaitDriverExtension.WaitForElementPresentByCss(WrappedDriver, PanelTitleLocator);
            
            return elements;
        }

        public void BookHearing() => ButtonDriverExtension.ClickElement(WrappedDriver, BookHearingPanelButton);
        public void QuestionnaireResult() => ButtonDriverExtension.ClickElement(WrappedDriver, QuestionnaireResultPanelButton);
    }
}
