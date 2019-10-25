using System;
using System.Collections.Generic;
using System.Linq;
using AcceptanceTests.Driver.Drivers;
using Coypu;

namespace AcceptanceTests.PageObject.Page
{
    public class DashboardPage : Page
    {
        private const string PanelTitleLocator = "h1.vhpanel-title";

        public DashboardPage(BrowserSession driver) : base(driver)
        {
            Path = "dashboard";
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
            var elements = DriverExtension.WaitForElementPresentByCss(WrappedDriver, PanelTitleLocator);
            var count = elements.Count();
            return elements;
        }
    }
}
