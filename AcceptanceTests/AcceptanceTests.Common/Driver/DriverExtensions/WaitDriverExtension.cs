using System;
using System.Collections.Generic;
using System.Linq;
using Coypu;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AcceptanceTests.Common.Driver.DriverExtensions
{
    public class WaitDriverExtension
    {
        private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(15);
        private static readonly Options DefaultOptions = new Options { Timeout = DefaultTimeout, WaitBeforeClick = DefaultTimeout };

        public static IEnumerable<ElementScope> WaitForElementsPresentByCss(BrowserSession driver, string cssLocator)
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

        public static IEnumerable<SnapshotElementScope> WaitForElementsPresentByXPath(BrowserSession driver, string xPathLocator)
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

        public static IWebElement WaitUntilElementVisible(BrowserSession driver, By elementLocator, int timeout = 30)
        {
            try
            {
                var wait = new WebDriverWait((IWebDriver)driver.Native, TimeSpan.FromSeconds(timeout));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(elementLocator));
            }
            catch (NoSuchElementException ex)
            {
                throw new NoSuchElementException($"Element with locator: '{elementLocator}' was not found in current context page.", ex);
            }
        }

        public static IList<IWebElement> WaitUntilElementsVisible(BrowserSession driver, By elementLocator, int timeout = 30)
        {
            try
            {
                var wait = new WebDriverWait((IWebDriver)driver.Native, TimeSpan.FromSeconds(timeout));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(elementLocator));
            }
            catch (NoSuchElementException ex)
            {
                throw new NoSuchElementException($"Elements with locator: '{elementLocator}' was not found in current context page.", ex);
            }
        }

        public static IWebElement WaitUntilElementClickable(BrowserSession driver, By elementLocator, int timeout = 30)
        {
            try
            {
                var wait = new WebDriverWait((IWebDriver)driver.Native, TimeSpan.FromSeconds(timeout));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(elementLocator));
            }
            catch (NoSuchElementException ex)
            {
                throw new NoSuchElementException($"Element with locator: '{elementLocator}' was not found in current context page.", ex);
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
