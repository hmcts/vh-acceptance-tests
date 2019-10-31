using System;
using System.Collections.Generic;
using System.Linq;
using Coypu;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

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
                var result = new Func<IEnumerable<ElementScope>>(() => driver.FindAllCss(cssLocator, x => x.Any(), DefaultOptions))();
                Console.WriteLine($"Element {cssLocator} successfully found on page.");
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
                var result = new Func<IEnumerable<SnapshotElementScope>>(() => driver.FindAllXPath(xPathLocator, x => x.Any(), DefaultOptions))();
                Console.WriteLine($"Element {xPathLocator} successfully found on page.");
                return result;
            }
            catch (Exception)
            {
                throw new MissingHtmlException($"XPath selected Element {xPathLocator} was not found on page after waiting {DefaultTimeout} seconds.");
            }
        }

        public static void WaitForPageToLoad(BrowserSession driver, string url)
        {
            try
            {
                var wait = new WebDriverWait((IWebDriver)driver.Native, DefaultTimeout);
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlContains(url)); 
            }
            catch (Exception)
            {
                throw new MissingHtmlException($"Page {url} was not found after waiting {DefaultTimeout} seconds.");
            }
        }
    }
}
