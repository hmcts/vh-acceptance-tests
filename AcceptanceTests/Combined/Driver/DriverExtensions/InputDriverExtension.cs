using System;
using Coypu;
using OpenQA.Selenium;

namespace AcceptanceTests.Common.Driver.DriverExtensions
{
    public class InputDriverExtension
    {
        public static void EnterValues(BrowserSession driver, By elementLocator, string value) => WaitDriverExtension.WaitUntilElementVisible(driver, elementLocator).SendKeys(value);
        public static void EnterValues(IWebElement webElement, string value) => webElement.SendKeys(value);

        public static void ClearValuesAndEnterText(BrowserSession driver, By element, string value)
        {
            var webElement = ClearValues(driver, element);
            EnterValues(webElement, value);
        }

        public static IWebElement ClearValues(BrowserSession driver, By element)
        {
            var webElement = WaitDriverExtension.WaitUntilElementVisible(driver, element);
            ClearWebElementValues(webElement);
            return webElement;
        }

        public static IWebElement ClearWebElementValues(IWebElement webElement)
        {
            var text = webElement.Text;
            var textAttribute = webElement.GetAttribute("value");
            if (!string.IsNullOrEmpty(text))
            {
                webElement.Clear();
            }
            else if (string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(textAttribute))
            {
                ClearValuesUsingKeyboard(webElement, textAttribute);
            }
            else
            {
                Console.WriteLine();
            }

            return webElement;
        }

        public static void ClearValuesUsingKeyboard(IWebElement webElement, string value)
        {
            var elementName = webElement.GetAttribute("name");
            Console.WriteLine($"Current element {elementName} text is {value}");

            while (!string.IsNullOrEmpty(value))
            {
                try
                {
                    webElement.SendKeys(Keys.Backspace);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An exception occurred while sending Keys.Backspace to element {elementName}");
                    Console.WriteLine(ex.Message);
                }

                value = webElement.GetAttribute("value");
            }

            webElement.SendKeys(value);
        }
    }
}
