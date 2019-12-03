﻿using System.Collections.Generic;
using AcceptanceTests.Common.Model.FormData;
using AcceptanceTests.Common.PageObject.Pages.Common;
using AcceptanceTests.Driver.DriverExtensions;
using Coypu;
using OpenQA.Selenium;

namespace AcceptanceTests.Common.PageObject.Components.DropdownLists
{
    public class DropdownList : Component, IDropdownList
    {
        public string Name { get; private set; }
        public By ByLocator { get; private set; }
        public void SelectOption(string option) => ListDriverExtension.SelectOption(Name, WrappedDriver, ByLocator, option);
        public string SelectFirst() => ListDriverExtension.SelectFirstOption(Name, WrappedDriver, ByLocator);
        public string SelectLast() => ListDriverExtension.SelectLastOption(Name, WrappedDriver, ByLocator);
        public IEnumerable<IWebElement> GetList() => ListDriverExtension.GetListOfElements(Name, WrappedDriver, ByLocator, 0);

        public DropdownList(BrowserSession driver, string name, string locator) : base(driver)
        {
            Name = name;
            ComponentLocator = CommonLocator.DropDownList(locator);
            ByLocator = By.XPath(CommonLocator.DropDownList(locator));
        }

        public void FillFormDetails(IFormData formDataObject)
        {
            var option = (DropdownListFormData)formDataObject;
            if ((option == null) || (option != null && string.IsNullOrEmpty(option.SelectedOption)))
            {
                SelectFirst();
            }
            else
            {
                SelectOption(option.SelectedOption);
            }
        }
    }
}