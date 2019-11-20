using Coypu;

namespace AcceptanceTests.PageObject.Pages.Common
{
    public interface IPage
    {
        string HeadingText { get; }
        string Path { get; }
        BrowserSession WrappedDriver { get; }

        void Visit();
        bool IsPageLoaded();
        void WaitForPageToLoad();
    }
}