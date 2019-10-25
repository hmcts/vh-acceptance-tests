using System;
using System.Collections.Generic;
using Coypu;

namespace AcceptanceTests.Driver.Drivers
{
    public class DriverExtension
    {
        private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(15);
        private static readonly Options DefaultOptions = new Options { Timeout = DefaultTimeout, WaitBeforeClick = DefaultTimeout };

        public static IEnumerable<ElementScope> WaitForElementPresentByCss(BrowserSession driver, string cssLocator)
        {
            try
            {
                var result = new Func<IEnumerable<ElementScope>>(() => driver.FindAllCss(cssLocator, null, DefaultOptions))();

                return result;
            }
            catch (Exception)
            {
                throw new MissingHtmlException($"Css selected Element {cssLocator} was not found on page after waiting {DefaultTimeout} seconds.");
            }
        }

        public static IEnumerable<SnapshotElementScope> WaitForElementPresentByXPath(BrowserSession driver, string xPathLocator)
        {
            try
            {
                var result = new Func<IEnumerable<SnapshotElementScope>>(() => driver.FindAllXPath(xPathLocator, null, DefaultOptions))();

                return result;
            }
            catch (Exception)
            {
                throw new MissingHtmlException($"XPath selected Element {xPathLocator} was not found on page after waiting {DefaultTimeout} seconds.");
            }
        }
    }
}
