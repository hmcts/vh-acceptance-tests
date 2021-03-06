﻿using OpenQA.Selenium;

namespace AcceptanceTests.Common.PageObject.Helpers
{
    public static class CommonLocators
    {
        public static By ElementContainingText(string text) => By.XPath($"//*[contains(text(), \"{text}\")]");
        public static By ButtonWithInnerText(string innerText) => By.XPath($"//button[contains(text(),'{innerText}')]");
        public static By ErrorMessage = By.XPath("//div[@class='govuk-error-message']");
        public static By CheckboxWithLabel(string label) => By.XPath($"//label[contains(text(),'{label}')]/../input[contains(@class,'govuk-checkboxes__input')]");
        public static By ImageWithAltTags(string alt) => By.XPath($"//img[contains(@alt,'{alt}')]");
        public static By RadioButtonWithLabel(string label) => By.XPath($"//div[@class='govuk-radios__item']//label[contains(text(),'{label}')]/../input");
        public static By WarningMessageAfterRadioButton(string label) => By.XPath($"//label[contains(text(),'{label}')]/p[@class='govuk-details__text']");
        public static By TextfieldWithName(string name) => By.XPath($"//input[@name='{name}']");
        public static By TableCellContainingText(string text) => By.XPath($"//table//td/p[contains(text(),'{text}')]");
        public static By IframeButtonWithTooltip(string tooltip) => By.XPath($"//a[@data-tooltip='{tooltip}']");
        public static By AlertCellText(string text) => By.XPath($"//div[@id='tasks-list']//p[contains(text(),'{text}')]");
        public static By LinkWithText(string linkText) => By.PartialLinkText(linkText);
    }
}
