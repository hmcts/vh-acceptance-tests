using System.Collections.Generic;
using AcceptanceTests.Common.PageObject.Components.Forms;
using OpenQA.Selenium;

namespace AcceptanceTests.Common.PageObject.Components.DropdownLists
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
