using Coypu;
using OpenQA.Selenium;

namespace AcceptanceTests.PageObject.Pages
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