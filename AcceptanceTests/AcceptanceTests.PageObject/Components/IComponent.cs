using System;
using Coypu;

namespace AcceptanceTests.PageObject.Components
{
    public interface IComponent
    {
        string ComponentLocator { get; }
        BrowserSession WrappedDriver { get; }
    }
}
