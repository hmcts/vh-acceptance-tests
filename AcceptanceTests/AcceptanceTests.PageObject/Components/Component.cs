using System;
using AcceptanceTests.Driver.DriverExtensions;
using AcceptanceTests.Driver.Drivers;
using AcceptanceTests.PageObject.Pages.Common;
using Coypu;

namespace AcceptanceTests.PageObject.Components
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
