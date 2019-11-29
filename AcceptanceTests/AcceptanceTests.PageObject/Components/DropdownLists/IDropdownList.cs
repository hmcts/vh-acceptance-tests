using System.Collections.Generic;
using AcceptanceTests.PageObject.Components.Forms;
using OpenQA.Selenium;

namespace AcceptanceTests.PageObject.Components.DropdownLists
{
    public interface IDropdownList : IFormComponent
    {
        string Name { get; }
        void SelectOption(string option);
        string SelectFirst();
        string SelectLast();
        IEnumerable<IWebElement> GetList();
    }
}
