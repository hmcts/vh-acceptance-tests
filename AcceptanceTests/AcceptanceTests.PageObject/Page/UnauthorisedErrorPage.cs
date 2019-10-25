using AcceptanceTests.PageObject.Components;
using Coypu;

namespace AcceptanceTests.PageObject.Page
{
    public class UnauthorisedErrorPage : Page
    {
        private static string UnauthorisedErrorTextLocator => "//div[@class='govuk-grid-column-full']";
        public ContactUsComponent _contactUs;

        public UnauthorisedErrorPage(BrowserSession driver) : base(driver)
        {
            HeadingText = "You are not authorised to use this service";
            Path = "/unauthorised";
            _contactUs = new ContactUsComponent(driver);
        }

        public string UnauthorisedText() => WrappedDriver.FindXPath(UnauthorisedErrorTextLocator).Text.Trim();
    }
}
