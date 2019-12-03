using AcceptanceTests.Common.Driver.DriverExtensions;
using Coypu;

namespace AcceptanceTests.Common.PageObject.Components
{
    public class ContactUsComponent : Component
    {
        public ContactUsComponent(BrowserSession driver) : base(driver)
        {
            ComponentLocator = "#citizen-contact-details";
        }

        public string ContactUsForHelpText()
        {
            WaitDriverExtension.WaitForElementsPresentByCss(WrappedDriver, ComponentLocator);
            var text = WrappedDriver.FindCss(ComponentLocator).Text.Trim();
            return text;
        }
    }
}
