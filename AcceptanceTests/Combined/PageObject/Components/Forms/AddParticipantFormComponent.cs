using AcceptanceTests.Common.Model.FormData;
using AcceptanceTests.Common.PageObject.Components.DropdownLists;
using AcceptanceTests.Driver.DriverExtensions;
using Coypu;
using OpenQA.Selenium;

namespace AcceptanceTests.Common.PageObject.Components.Forms
{
    public class AddParticipantFormComponent : Component, IFormComponent
    {
        public DropdownList Party => new DropdownList(WrappedDriver, "Party", "party");
        public DropdownList Role => new DropdownList(WrappedDriver, "Role", "role");
        public void Email(string participantEmail) => InputDriverExtension.EnterValues(WrappedDriver, By.Id("participantEmail"), participantEmail);
        public DropdownList Title => new DropdownList(WrappedDriver, "Title", "title");
        public void FirstName(string firstName) => InputDriverExtension.EnterValues(WrappedDriver, By.Id("firstName"), firstName);
        public void LastName(string lastName) => InputDriverExtension.EnterValues(WrappedDriver, By.Id("lastName"), lastName);
        public void Organisation(string orgName) => InputDriverExtension.EnterValues(WrappedDriver, By.CssSelector("[id$=companyName]"), orgName);

        public AddressFormComponent AddressForm { get; }

        public AddParticipantFormComponent(BrowserSession driver) : base(driver)
        {
            AddressForm = new AddressFormComponent(driver);
        }

        public void FillFormDetails(IFormData formDataObject)
        {
            var addParticipantData = (ParticipantDatails)formDataObject;

            if (addParticipantData == null)
            {
                addParticipantData =(ParticipantDatails)new ParticipantDatails().GenerateFake();
            }

            Party.FillFormDetails(addParticipantData.Party);
            Role.FillFormDetails(addParticipantData.Role);
            Email(addParticipantData.Email);
            Title.FillFormDetails(addParticipantData.Title);
            FirstName(addParticipantData.FirstName);
            LastName(addParticipantData.LastName);
            Organisation(addParticipantData.Organisation);
            AddressForm.FillFormDetails(addParticipantData.Address);
        }
    }  
}
