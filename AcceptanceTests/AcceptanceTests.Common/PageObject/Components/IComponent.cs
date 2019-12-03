using Coypu;

namespace AcceptanceTests.Common.PageObject.Components
{
    public interface IComponent
    {
        string ComponentLocator { get; }
        BrowserSession WrappedDriver { get; }
    }
}
