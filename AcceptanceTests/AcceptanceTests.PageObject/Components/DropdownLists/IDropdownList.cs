using System.Collections.Generic;
using Coypu;

namespace AcceptanceTests.PageObject.Components.DropdownLists
{
    public interface IDropdownList : IComponent
    {
        string Name { get; }
        void SelectOption(string option);
        void SelectFirst();
        IEnumerable<SnapshotElementScope> GetList();
    }
}
