using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Protractor;

namespace AcceptanceTests.Common.Driver.Helpers
{
    public static class DriverExtension
    {
        public static NgWebElement FindElement(this ISearchContext context, By by, int timeout = 20, bool displayed = false)
        {
            var wait = new DefaultWait<ISearchContext>(context)
            { Timeout = TimeSpan.FromSeconds(timeout) };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            return wait.Until(ctx => {
                var elem = ctx.FindElement(by);
                if (displayed && !elem.Displayed)
                    return null;

                return ((NgWebElement)(elem));
            });
        }
        public static ReadOnlyCollection<IWebElement> FindElements(this IWebDriver driver, By elementLocator, int timeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(elementLocator));
            }
            catch (NoSuchElementException ex)
            {
                throw new NoSuchElementException($"Element with locator: '{elementLocator}' was not found in current context page.", ex);
            }
        }
        public static IWebElement WaitUntilElementClickable(this IWebDriver driver, By elementLocator, int timeout = 20)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(elementLocator));
            }
            catch (NoSuchElementException ex)
            {
                throw new NoSuchElementException($"Element with locator: '{elementLocator}' was not found in current context page.", ex);
            }
        }

        public static IWebElement WaitUntilElementClickable(this IWebDriver driver, IWebElement element, int timeout = 20)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
            }
            catch (NoSuchElementException ex)
            {
                throw new NoSuchElementException($"Element with tag name: '{element.TagName}' was not found in current context page.", ex);
            }
        }


        public static void WaitUntilTextPresent(this IWebDriver driver, By elementLocator, string text, int timeout = 20)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementValue(elementLocator, text));
            }
            catch (NoSuchElementException ex)
            {
                throw new NoSuchElementException($"Element with locator: '{elementLocator}' was not found in current context page with text {text}.", ex);
            }
        }

        public static IWebElement WaitUntilElementExists(this IWebDriver driver, By elementLocator, int timeout = 20)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(elementLocator));
            }
            catch (NoSuchElementException ex)
            {
                throw new NoSuchElementException($"Element with locator: '{elementLocator}' was not found in current context page.", ex);
            }
        }

        public static bool WaitUntilElementNotVisible(this IWebDriver driver, By elementLocator, int timeout = 20)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(elementLocator));
            }
            catch (NoSuchElementException ex)
            {
                throw new NoSuchElementException($"Element with locator: '{elementLocator}' was not found in current context page.", ex);
            }
        }
        public static IWebElement WaitUntilVisible(this IWebDriver driver, By elementLocator, int timeout = 30)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(elementLocator));
            }
            catch (NoSuchElementException ex)
            {
                throw new NoSuchElementException($"Element with locator: '{elementLocator}' was not found in current context page.", ex);
            }
        }

        public static IList<IWebElement> WaitUntilElementsVisible(this IWebDriver driver, By elementLocator, int timeout = 30)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(elementLocator));
            }
            catch (NoSuchElementException ex)
            {
                throw new NoSuchElementException($"Element with locator: '{elementLocator}' was not found in current context page.", ex);
            }
        }

        public static void ClickAndWaitForPageToLoad(this IWebDriver driver, By elementLocator, int timeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                var element = driver.FindElement(elementLocator);
                element.Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(element));
            }
            catch (NoSuchElementException ex)
            {
                throw new NoSuchElementException($"Element with locator: '{elementLocator}' was not found in current context page.", ex);
            }
        }

        public static void WaitForListToBePopulated(this IWebDriver driver, By elementLocator, int timeout = 30)
        {
            try
            {
                for (var i = 0; i < timeout; i++)
                {
                    var count = new SelectElement(driver.WaitUntilElementExists(elementLocator)).Options.Count;
                    if (count > 0)
                    {
                        break;
                    }

                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
            }
            catch (NoSuchElementException ex)
            {
                throw new NoSuchElementException($"Dropdown list with locator: '{elementLocator}' was not populated.", ex);
            }
        }

        public static void WaitForPageToLoad(this IWebDriver driver, int timeout = 30)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.TagName("router-outlet")));
            }
            catch (Exception e)
            {
                var url = driver.Url;
                throw new TimeoutException($"Timed out waiting for page to load, the expected <router-outlet> element did not appear on current url '{url}'", e);
            }
        }
    }
}
