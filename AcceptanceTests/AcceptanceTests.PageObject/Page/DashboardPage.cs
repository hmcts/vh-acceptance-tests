using System;
using System.Collections.Generic;
using System.Linq;
using Coypu;

namespace AcceptanceTests.PageObject.Page
{
    public class DashboardPage : Page
    {
        private static string PanelTitleLocator = "//*[@class='vhpanel-title']";

        public DashboardPage(BrowserSession driver) : base(driver)
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

        public IEnumerable<SnapshotElementScope> GetDashboardPanelElements()
        {
            return WrappedDriver.FindAllXPath(PanelTitleLocator);
        }
    }
}
