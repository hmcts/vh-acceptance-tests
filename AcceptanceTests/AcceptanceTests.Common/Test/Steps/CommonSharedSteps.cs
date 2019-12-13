using System.Linq;
using AdminWebsite.Common.Driver.Helpers;
using AdminWebsite.Common.PageObject.Helpers;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Protractor;
using TechTalk.SpecFlow;

namespace AdminWebsite.Common.Test.Steps
{
    [Binding]
    public class CommonSharedSteps
    {
        [When(@"the user clicks the (.*) button")]
        public void WhenTheUserClicksTheButton(NgWebDriver driver, string label)
        {
            driver.WaitUntilVisible(CommonLocators.ButtonWithInnerText(label))
                .Displayed.Should().BeTrue();

            driver.WaitUntilVisible(CommonLocators.ButtonWithInnerText(label)).Click();
        }

        [When(@"the user selects the (.*) radiobutton")]
        public void WhenTheUserSelectsTheRadiobutton(NgWebDriver driver, string label)
        {
            driver.WaitUntilElementExists(CommonLocators.RadioButtonWithLabel(label)).Click();

            driver.WaitUntilElementExists(CommonLocators.RadioButtonWithLabel(label)).Selected
                .Should().BeTrue();
        }

        [When(@"the user clicks the (.*) link")]
        public void WhenTheUserClicksTheChangeCameraOrMicrophoneLink(NgWebDriver driver, string linkText)
        {
            driver.WaitUntilVisible(CommonLocators.LinkWithText(linkText)).Displayed
                .Should().BeTrue();

            driver.WaitUntilVisible(CommonLocators.LinkWithText(linkText)).Click();
        }

        [When(@"the user selects the (.*) option from the dropdown")]
        public void WhenTheUserSelectsTheOptionFromTheDropdown(NgWebDriver driver, By element, string partialText)
        {
            driver.WaitForListToBePopulated(element);
            var hearingTypeOptions = new SelectElement(driver.WaitUntilElementExists(element));
            var found = false;
            if (hearingTypeOptions.Options.Any(option => option.Text.ToLower().Contains(partialText.ToLower())))
            {
                found = true;
                hearingTypeOptions.SelectByText(partialText);
            }
            var options = string.Join(",", hearingTypeOptions.Options.Select(i => i.Text).ToArray());
            found.Should().BeTrue($"Option '{partialText}' found in the list of options {options}");
            driver.WaitUntilElementExists(element).SendKeys(Keys.Tab);
        }
    }
}
