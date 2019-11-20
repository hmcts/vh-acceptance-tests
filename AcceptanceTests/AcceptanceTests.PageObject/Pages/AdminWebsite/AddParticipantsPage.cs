using AcceptanceTests.Model.Forms;
using AcceptanceTests.PageObject.Components;
using AcceptanceTests.PageObject.Pages.Common;
using Coypu;

namespace AcceptanceTests.PageObject.Pages.AdminWebsite
{
    public class AddParticipantsPage : Page
    {
        public AddressForm _addressForm; 

        public AddParticipantsPage(BrowserSession driver, string uri) : base(driver, uri)
        {
        }

        public void FillInAddress(Address address)
        {
            _addressForm = new AddressForm(WrappedDriver);
            _addressForm.AssertComponentLoaded(this);
            _addressForm.AddAddress(address);
        }
    }
}
