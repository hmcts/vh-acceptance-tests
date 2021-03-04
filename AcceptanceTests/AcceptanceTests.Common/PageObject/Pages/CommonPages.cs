using AcceptanceTests.Common.PageObject.Helpers;
using OpenQA.Selenium;

namespace AcceptanceTests.Common.PageObject.Pages
{
    public static class CommonPages
    {
        public static readonly By SignOutLink = By.PartialLinkText("Sign out");
        public static readonly By SignOutMessage = CommonLocators.ElementContainingText("Signed out successfully");
        public static readonly By SignInLink = By.PartialLinkText("here");
        public static readonly By QuoteYourCaseNumberText = CommonLocators.ElementContainingText("Call us on");
        public static readonly By ContactUsLink = CommonLocators.ElementContainingText("Contact us for help");
        public static readonly By ContactUsEmailText = CommonLocators.ElementContainingText("Send us a message");
        public static readonly By BetaBanner = By.XPath("//*[contains(@class,'govuk-phase-banner') and contains(text(),'beta')]");
        public static By ContactUsEmail(string email) => CommonLocators.ElementContainingText(email);
        public static By ContactUsPhone(string phone) => CommonLocators.ElementContainingText(phone);
    }
}
