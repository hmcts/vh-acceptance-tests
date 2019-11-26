using System;
using System.Collections.Generic;
using System.Linq;
using Coypu;
using FluentAssertions;
using OpenQA.Selenium;

namespace AcceptanceTests.Driver.DriverExtensions
{
    public class DropdownDriverExtension
    {
        public static IEnumerable<IWebElement> GetListOfElements(string dropdownName, BrowserSession driver, By elements, int expectedCount)
        {
            IEnumerable<IWebElement> elementList = null;
            try
            {
                elementList = WaitDriverExtension.WaitUntilElementsVisible(driver, elements);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                elementList.Count().Should().BeGreaterOrEqualTo(expectedCount, $"{dropdownName} list items it " +
                                                                                        $"not equal or greater than {expectedCount}");
            }
            return elementList;
        }

        public static void SelectOption(string dropdownName, BrowserSession driver, By locator, string option)
        {
            var elementList = GetListOfElements(dropdownName, driver, locator, 1);

            foreach (var element in elementList)
            {
                if (option != element.Text.Trim()) continue;
                ButtonDriverExtension.ClickElement(driver, locator);
                break;
            }
        }

        public static string SelectFirstOption(string dropdownName, BrowserSession driver, By locator)
        {
            var elementList = GetListOfElements(dropdownName, driver, locator, 1);
            try
            {
                elementList.First().Click();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Couldn't select first element from {dropdownName} dropdown list.");
                throw ex;
            }
            return elementList.First().Text.Trim();
        }

        public static string SelectLastItem(string dropdownName, BrowserSession driver, By locator)
        {
            var elementList = GetListOfElements(dropdownName, driver, locator, 1);
            try
            {
                elementList.Last().Click();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Couldn't select last element from {dropdownName} dropdown list.");
                throw ex;
            }
            return elementList.Last().Text.Trim();
        }
    }
}
