using System.Collections.Generic;
using System.Linq;
using Coypu;
using FluentAssertions;

namespace AcceptanceTests.Driver.DriverExtensions
{
    public class DropdownDriverExtension
    {
        public static IEnumerable<SnapshotElementScope> GetListOfElements(string downpdownName, BrowserSession driver,
                                                                        string xpath, int expectedCount)
        {
            var elementList = WaitDriverExtension.WaitForElementsPresentByXPath(driver, xpath);
            driver.RetryUntilTimeout(() => elementList.Count().Should().BeGreaterOrEqualTo(expectedCount, $"{downpdownName} list items it " +
                                                                                        $"not equal or greater than {expectedCount}" ));
            return elementList;
        }

        public static void SelectOption(string dropdownName, BrowserSession driver, string xpath, string option)
        {
            var elementList = GetListOfElements(dropdownName, driver, xpath, 1);

            foreach (var element in elementList)
            {
                if (option != element.Text.Trim()) continue;
                ButtonDriverExtension.ClickElement(driver, xpath);
                break;
            }
        }

        public static string SelectFirstOption(string dropdownName, BrowserSession driver, string xpath)
        {
            var elementList = GetListOfElements(dropdownName, driver, xpath, 1);
            driver.Select(elementList.First().Text);
            return elementList.Last().Text.Trim();
        }

        public static string SelectLastItem(string dropdownName, BrowserSession driver, string xpath)
        {
            var elementList = GetListOfElements(dropdownName, driver, xpath, 1);
            driver.Select(elementList.Last().Text);
            return elementList.Last().Text.Trim();
        }
    }
}
