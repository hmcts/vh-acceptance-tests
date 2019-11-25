using AcceptanceTests.Model.FormData;
using AcceptanceTests.PageObject.Components;
using AcceptanceTests.PageObject.Pages.Common;
using Coypu;

namespace AcceptanceTests.PageObject.Pages.AdminWebsite
{
    public class AddParticipantsPage : UserJourneyPage
    {
        public AddressForm _addressForm; 

        public AddParticipantsPage(BrowserSession driver, string uri) : base(driver, uri)
        {
            HeadingText = "Add Participants";
        }

        public void FillInAddress(Address address)
        {
            _addressForm = new AddressForm(WrappedDriver);
            _addressForm.AssertComponentLoaded(this);
            _addressForm.FillFormDetails(address);
        }
    }
}
