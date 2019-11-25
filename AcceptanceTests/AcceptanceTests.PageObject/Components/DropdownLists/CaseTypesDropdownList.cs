using System.Collections.Generic;
using AcceptanceTests.Driver.DriverExtensions;
using AcceptanceTests.PageObject.Pages.Common;
using Coypu;

namespace AcceptanceTests.PageObject.Components.DropdownLists
{
    public class DropdownList : Component, IDropdownList
    {
        public string Name { get; private set; }
        public string Locator { get; private set; }
        public void SelectOption(string option) => DropdownDriverExtension.SelectOption(Name, WrappedDriver, Locator, option);
        public void SelectFirst() => DropdownDriverExtension.SelectFirstOption(Name, WrappedDriver, Locator);
        public IEnumerable<SnapshotElementScope> GetList() => DropdownDriverExtension.GetListOfElements(Name, WrappedDriver, Locator, 0);

        public DropdownList(BrowserSession driver, string name, string locator) : base(driver)
        {
            Name = name;
            Locator = CommonLocator.DropDownList(locator);
        }
    }
}
