using OpenQA.Selenium;

namespace AcceptanceTests.Common.PageObject.Pages
{
    public static class LogoutSuccessPage
    {
        //public static By SignBackInLink => By.XPath($"//a[contains(text(),'login']");
        public static By SignBackInLink => By.PartialLinkText("here");
    }
}
