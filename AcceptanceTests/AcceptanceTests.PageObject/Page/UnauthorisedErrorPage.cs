using Coypu;

namespace AcceptanceTests.PageObject.Page
{
    public class UnauthorisedErrorPage : Page
    {
        private static string UnauthorisedErrorTextLocator => "//div[@class='govuk-grid-column-full']";
        private static string ContactUsForHelpLocator => "#citizen-contact-details";

        public UnauthorisedErrorPage(BrowserSession driver) : base(driver)
        {
            HeadingText = "You are not authorised to use this service";
            Path = "/unauthorised";
        }

        public string UnauthorisedText() => WrappedDriver.FindXPath(UnauthorisedErrorTextLocator).Text.Trim();
        public string ContactUsForHelpText() => WrappedDriver.FindCss("#citizen-contact-details").Text.Trim();
    }
}
