using System;
using AcceptanceTests.Common.Driver.DriverExtensions;
using AcceptanceTests.Common.PageObject.Pages.Common;
using Coypu;

namespace AcceptanceTests.Common.PageObject.Components
{
    public class Component: IComponent
    {
        public string ComponentLocator { get; protected set; }
        public BrowserSession WrappedDriver { get; protected set; }

        protected Component(BrowserSession driver)
        {
            WrappedDriver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        public void AssertComponentLoaded(IPage page)
        {
            try
            {
                WaitDriverExtension.WaitForElementsPresentByXPath(WrappedDriver, ComponentLocator);
            }
            catch (Exception)
            {
                throw new MissingHtmlException($"Component {ComponentLocator} was not found on page {page.Uri}.");
            }
            
        }
    }
}
