using AdminWebsite.Common.PageObject.Helpers;
using OpenQA.Selenium;

namespace AdminWebsite.Common.PageObject.Pages
{
    public class CommonPages
    {
        public By SignOutLink => By.PartialLinkText("Sign out");
        public By SignOutMessage => CommonLocators.ElementContainingText("Signed out successfully");
        public By SignInLink => By.PartialLinkText("here");
        public By QuoteYourCaseNumberText => CommonLocators.ElementContainingText("Call us on");
        public By ContactUsLink => CommonLocators.ElementContainingText("Contact us for help");
        public By ContactUsEmail => CommonLocators.ElementContainingText("Send us a message");
        public By BetaBanner => CommonLocators.ElementContainingText("beta");
        public By ContactUsPhone(string phone) => CommonLocators.ElementContainingText(phone);
    }
}
