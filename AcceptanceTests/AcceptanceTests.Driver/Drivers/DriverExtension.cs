using System;
using System.Collections.Generic;
using Coypu;

namespace AcceptanceTests.Driver.Drivers
{
    public class DriverExtension
    {
        private static TimeSpan DefaultTimeout = TimeSpan.FromSeconds(15);

        public static void WaitForElementPresentByCss(BrowserSession driver, string cssLocator)
        {
            try
            {
                var until = new Func<IEnumerable<ElementScope>>(() =>
               driver.FindAllCss(cssLocator, null, new Options { Timeout = DefaultTimeout }))();
            }
            catch (Exception)
            {
                throw new MissingHtmlException($"Element {cssLocator} was not found on page after waiting for {DefaultTimeout} seconds.");
            }
        }

        public static void WaitForElementPresentByXPath(BrowserSession driver, string xPathLocator)
        {
            try
            {
                var until = new Func<IEnumerable<ElementScope>>(() =>
               driver.FindAllCss(xPathLocator, null, new Options { Timeout = DefaultTimeout }))();
            }
            catch (Exception)
            {
                throw new MissingHtmlException($"Element {xPathLocator} was not found on page after waiting for {DefaultTimeout.Seconds} seconds.");
            }
        }
    }
}
