using AcceptanceTests.Model.Forms;
using AcceptanceTests.Driver.Drivers;
using OpenQA.Selenium;
using Coypu;

namespace AcceptanceTests.PageObject.Components
{
    public class AddressForm : Component
    {
        //Gets the input fields only
        public void HouseNumber() => By.Id("houseNumber");
        public void Street() => By.Id("street");
        public void City() => By.Id("city");
        public void County() => By.Id("county");

        //Clears the input values then enter details into fields
        //sometimes the ClearFieldInputValues() doesn't work, so using ClearFieldInputValuesKeyboard() instead
        public void HouseNumber(string houseNumber) => DriverExtension.ClearFieldInputValuesKeyboard(WrappedDriver, By.Id("houseNumber"), houseNumber);
        public void Street(string street) => DriverExtension.ClearFieldInputValuesKeyboard(WrappedDriver, By.Id("street"), street);
        public void City(string city) => DriverExtension.ClearFieldInputValuesKeyboard(WrappedDriver, By.Id("city"), city);
        public void County(string county) => DriverExtension.ClearFieldInputValuesKeyboard(WrappedDriver, By.Id("county"), county);

        protected AddressForm(BrowserSession driver) : base(driver)
        {
        }

        public void AddAddress(Address address)
        {
            HouseNumber(address.HouseNumber);
            HouseNumber(address.HouseNumber);
            Street(address.Street);
            City(address.City);
            County(address.County);
        }
    }
}
