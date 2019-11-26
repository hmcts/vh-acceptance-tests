using AcceptanceTests.Model.FormData;
using OpenQA.Selenium;
using Coypu;
using AcceptanceTests.Driver.DriverExtensions;

namespace AcceptanceTests.PageObject.Components.Forms
{
    public class AddressForm : Component, IFormComponent
    {
        //Clears the input values and then enter details into fields
        public void HouseNumber(string houseNumber) => InputDriverExtension.ClearValuesAndEnterText(WrappedDriver, By.Id("houseNumber"), houseNumber);
        public void Street(string street) => InputDriverExtension.ClearValuesAndEnterText(WrappedDriver, By.Id("street"), street);
        public void City(string city) => InputDriverExtension.ClearValuesAndEnterText(WrappedDriver, By.Id("city"), city);
        public void County(string county) => InputDriverExtension.ClearValuesAndEnterText(WrappedDriver, By.Id("county"), county);

        public AddressForm(BrowserSession driver) : base(driver)
        {
        }

        public void FillFormDetails(object formDataObject)
        {
            var address = (Address)formDataObject;
            HouseNumber(address.HouseNumber);
            Street(address.Street);
            City(address.City);
            County(address.County);
        }
    }
}
