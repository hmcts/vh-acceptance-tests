using System.Collections.Generic;
using AcceptanceTests.Model.FormData;
using AcceptanceTests.PageObject.Components.Forms;
using AcceptanceTests.PageObject.Pages.Common;
using Coypu;

namespace AcceptanceTests.PageObject.Pages.AdminWebsite
{
    public class AddParticipantsPage : UserJourneyPage
    {
        public AddParticipantFormComponent _addParticipantForm;
        public AddressFormComponent _addressForm; 

        public AddParticipantsPage(BrowserSession driver, string uri) : base(driver, uri)
        {
            HeadingText = "Add Participants";
            _addParticipantForm = new AddParticipantFormComponent(driver);
            _addressForm = new AddressFormComponent(driver);
            _pageFormList = new List<IFormComponent>
            {
                _addParticipantForm,
                _addressForm
            };
        }

        //public void FillInAddress(Address address)
        //{
        //    _addressForm = new AddressFormComponent(WrappedDriver);
        //    _addressForm.AssertComponentLoaded(this);
        //    _addressForm.FillFormDetails(address);
        //}
    }
}
