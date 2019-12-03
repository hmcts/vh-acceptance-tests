using Coypu;

namespace AcceptanceTests.Common.PageObject.Pages.Common
{
    public interface IPage
    {
        string HeadingText { get; }
        string Uri { get; }
        BrowserSession WrappedDriver { get; }

        void Visit();
        bool IsPageLoaded();
        void WaitForPageToLoad();
    }
}