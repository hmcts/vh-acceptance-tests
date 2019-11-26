using System.Collections.Generic;
using OpenQA.Selenium;

namespace AcceptanceTests.PageObject.Components.DropdownLists
{
    public interface IDropdownList : IComponent
    {
        string Name { get; }
        void SelectOption(string option);
        void SelectFirst();
        IEnumerable<IWebElement> GetList();
    }
}
