using System;
using System.Collections.Generic;
using System.Linq;
using Coypu;
using FluentAssertions;
using OpenQA.Selenium;

namespace AcceptanceTests.Common.Driver.DriverExtensions
{
    public class ListDriverExtension
    {
        public static IEnumerable<IWebElement> GetListOfElements(string label, BrowserSession driver, By elements, int expectedCount)
        {
            IEnumerable<IWebElement> elementList = null;
            try
            {
                elementList = WaitDriverExtension.WaitUntilElementsVisible(driver, elements);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                elementList.Count().Should().BeGreaterOrEqualTo(expectedCount, $"{label} list items it " +
                                                                                        $"not equal or greater than {expectedCount}");
            }
            return elementList;
        }

        public static void SelectOption(string label, BrowserSession driver, By locator, string option)
        {
            var elementList = GetListOfElements(label, driver, locator, 1);

            foreach (var element in elementList)
            {
                if (option != element.Text.Trim()) continue;
                ClickDriverExtension.ClickElement(driver, locator);
                break;
            }
        }

        public static string SelectFirstOption(string label, BrowserSession driver, By locator)
        {
            var elementList = GetListOfElements(label, driver, locator, 1);
            try
            {
                elementList.First().Click();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Couldn't select first element from {label} dropdown list.");
                throw ex;
            }
            return elementList.First().Text.Trim();
        }

        public static string SelectLastOption(string label, BrowserSession driver, By locator)
        {
            var elementList = GetListOfElements(label, driver, locator, 1);
            try
            {
                elementList.Last().Click();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Couldn't select last element from {label} dropdown list.");
                throw ex;
            }
            return elementList.Last().Text.Trim();
        }
    }
}
