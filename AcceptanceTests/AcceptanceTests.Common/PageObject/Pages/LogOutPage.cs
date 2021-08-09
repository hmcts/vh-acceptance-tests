using OpenQA.Selenium;

namespace AcceptanceTests.Common.PageObject.Pages
{
    public static class LogOutPage
    {
        //public static By LogoutLink(string username) => By.XPath($"//*[contains(text(), '{username}')]");
        public static By LogoutLink(string username) => By.XPath($"//div[@class='table' and .//*[contains(text(), '{username}')]]");
    }
}
