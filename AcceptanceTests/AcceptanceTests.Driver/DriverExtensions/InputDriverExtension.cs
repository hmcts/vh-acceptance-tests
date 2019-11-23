using System;
using Coypu;
using OpenQA.Selenium;

namespace AcceptanceTests.Driver.DriverExtensions
{
    public class InputDriverExtension
    {
        public static void InputValues(BrowserSession driver, By element, string value) => WaitDriverExtension.WaitUntilElementVisible(driver, element).SendKeys(value);

        public static void ClearFieldInputValues(BrowserSession driver, By element, string value)
        {
            var webElement = WaitDriverExtension.WaitUntilElementVisible(driver, element);
            webElement.Clear();
            webElement.SendKeys(value);
        }

        public static void ClearFieldInputValuesKeyboard(BrowserSession driver, By element, string value)
        {
            var webElement = WaitDriverExtension.WaitUntilElementVisible(driver, element);
            var text = webElement.GetAttribute("value");

            Console.WriteLine($"Current element {element} text is {text}");

            while (!string.IsNullOrEmpty(text))
            {
                try
                {
                    webElement.SendKeys(Keys.Backspace);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An exception occurred when sending Keys.Backspace to element {element}");
                    Console.WriteLine(ex.Message);
                }

                text = webElement.GetAttribute("value");
            }

            webElement.SendKeys(value);
        }
    }
}
