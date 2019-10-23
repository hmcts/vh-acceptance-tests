using Coypu;

namespace AcceptanceTests.PageObject.Page
{
    public interface IPage
    {
        string HeadingText { get; }
        string Path { get; }
        BrowserSession WrappedDriver { get; }

        void Visit();
        bool IsPageLoaded();
    }
}