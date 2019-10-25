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
                var options = GetDefaultOptions();
                var until = new Func<IEnumerable<ElementScope>>(() =>
               driver.FindAllCss(cssLocator, null, options))();
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
                var options = GetDefaultOptions();
                var until = new Func<IEnumerable<ElementScope>>(() =>
               driver.FindAllXPath(xPathLocator, null, options))();
            }
            catch (Exception)
            {
                throw new MissingHtmlException($"Element {xPathLocator} was not found on page after waiting for {DefaultTimeout.Seconds} seconds.");
            }

            Console.WriteLine($"Element {xPathLocator} successfully found on page.");
        }

        public static Options GetDefaultOptions()
        {
            var options = new Options
            {
                WaitBeforeClick = DefaultTimeout,
                Timeout = DefaultTimeout
            };

            return options;
        }
    }
}
