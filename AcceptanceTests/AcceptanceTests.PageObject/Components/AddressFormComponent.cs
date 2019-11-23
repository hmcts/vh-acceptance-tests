using AcceptanceTests.Model.Forms;
using AcceptanceTests.Driver.Drivers;
using OpenQA.Selenium;
using Coypu;
using AcceptanceTests.Driver.DriverExtensions;

namespace AcceptanceTests.PageObject.Components
{
    public class AddressForm : Component, IFormComponent
    {
        //Clears the input values then enter details into fields
        //sometimes the ClearFieldInputValues() doesn't work, so using ClearFieldInputValuesKeyboard() instead
        public void HouseNumber(string houseNumber) => InputDriverExtension.ClearFieldInputValuesKeyboard(WrappedDriver, By.Id("houseNumber"), houseNumber);
        public void Street(string street) => InputDriverExtension.ClearFieldInputValuesKeyboard(WrappedDriver, By.Id("street"), street);
        public void City(string city) => InputDriverExtension.ClearFieldInputValuesKeyboard(WrappedDriver, By.Id("city"), city);
        public void County(string county) => InputDriverExtension.ClearFieldInputValuesKeyboard(WrappedDriver, By.Id("county"), county);

        public AddressForm(BrowserSession driver) : base(driver)
        {
        }

        public void FillFormDetails(object formDataObject)
        {
            var address = (Address)formDataObject;
            HouseNumber(address.HouseNumber);
            HouseNumber(address.HouseNumber);
            Street(address.Street);
            City(address.City);
            County(address.County);
        }
    }
}
