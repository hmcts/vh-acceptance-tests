using System.Linq;
using AcceptanceTests.Common.Driver;
using AcceptanceTests.Common.Driver.Drivers;
using AcceptanceTests.Common.Driver.Helpers;
using AcceptanceTests.Common.PageObject.Helpers;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Protractor;
using TechTalk.SpecFlow;

namespace AcceptanceTests.Common.Test.Steps
{
    [Binding]
    public class CommonSharedSteps
    {
        [When(@"the user clicks the (.*) button")]
        public void WhenTheUserClicksTheButton(UserBrowser browser, string label)
        {
            browser.Driver.WaitUntilVisible(CommonLocators.ButtonWithInnerText(label)).Displayed.Should().BeTrue();
            browser.Click(CommonLocators.ButtonWithInnerText(label));
        }

        [When(@"the user selects the (.*) radiobutton")]
        public void WhenTheUserSelectsTheRadiobutton(UserBrowser browser, string label)
        {
            browser.ClickRadioButton(CommonLocators.RadioButtonWithLabel(label));
            browser.Driver.WaitUntilElementExists(CommonLocators.RadioButtonWithLabel(label)).Selected.Should().BeTrue();
        }

        [When(@"the user clicks the (.*) link")]
        public void WhenTheUserClicksTheChangeCameraOrMicrophoneLink(UserBrowser browser, string linkText)
        {
            browser.Driver.WaitUntilVisible(CommonLocators.LinkWithText(linkText)).Displayed.Should().BeTrue();
            browser.ClickLink(CommonLocators.LinkWithText(linkText));
        }

        [When(@"the user selects the (.*) option from the dropdown")]
        public void WhenTheUserSelectsTheOptionFromTheDropdown(NgWebDriver driver, By element, string partialText)
        {
            const int TIMEOUT = 2;
            driver.WaitForListToBePopulated(element/*, TIMEOUT*/);
            var hearingTypeOptions = new SelectElement(driver.WaitUntilElementExists(element));
            var found = false;
            if (hearingTypeOptions.Options.Any(option => option.Text.ToLower().Contains(partialText.ToLower())))
            {
                found = true;
                hearingTypeOptions.SelectByText(partialText);
            }

            var options = "";

            if (hearingTypeOptions.Options.Count < 10)
            {
                options = string.Join(",", hearingTypeOptions.Options.Select(i => i.Text).ToArray());
            }

            found.Should().BeTrue($"Option '{partialText}' found in the list of options {options}");
            driver.WaitUntilElementExists(element, TIMEOUT).SendKeys(Keys.Tab);
        }
    }
}
