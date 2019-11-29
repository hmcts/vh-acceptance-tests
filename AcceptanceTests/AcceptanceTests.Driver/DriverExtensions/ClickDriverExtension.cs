﻿using Coypu;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace AcceptanceTests.Driver.DriverExtensions
{
    public class ClickDriverExtension
    {
        public static void ClickElement(BrowserSession driver, By elementLocator)
        {
            var element = WaitDriverExtension.WaitUntilElementClickable(driver, elementLocator);
            ClickElement(driver, element);
        }

        public static void CheckCheckboxElement(BrowserSession driver, By elementLocator)
        {
            var ngDriver = NativeDriverExtension.GetDriver(driver);
            var element = ngDriver.FindElement(elementLocator);

            if (!element.Selected)
            {
                ClickElement(driver, element);
            }
        }

        public static void UnCheckCheckboxElement(BrowserSession driver, By elementLocator)
        {
            var ngDriver = NativeDriverExtension.GetDriver(driver);
            var element = ngDriver.FindElement(elementLocator);

            if (element.Selected)
            {
                ClickElement(driver, element);
            }
        }

        private static void ClickElement(BrowserSession driver, IWebElement element)
        {
            var action = new Actions(NativeDriverExtension.GetDriver(driver));
            action.MoveToElement(element).Perform();
            element.Click();
        }
    }
}
