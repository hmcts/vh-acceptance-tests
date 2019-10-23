using Coypu;

namespace AcceptanceTests.PageObject.Page
{
    public class UnauthorisedErrorPage : Page
    {
        private static string UnauthorisedErrorText => "//*[@class='govuk-heading-xl']";

        public UnauthorisedErrorPage(BrowserSession driver) : base(driver)
        {
            HeadingText = "You are not authorised to use this service";
            Path = "/unauthorised";
        }

        public string UnauthorisedText() => WrappedDriver.FindXPath(UnauthorisedErrorText).Text.Trim();
    }
}
