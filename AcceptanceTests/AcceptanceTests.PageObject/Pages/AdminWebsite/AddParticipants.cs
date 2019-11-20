using System;
using AcceptanceTests.PageObject.Components;
using Coypu;

namespace AcceptanceTests.PageObject.Pages.AdminWebsite
{
    public class AddParticipants : Page
    {
        public AddressForm _addressForm; 

        public AddParticipants(BrowserSession driver, string uri) : base(driver)
        {
            Path = uri;
            _addressForm = new AddressForm(driver);
        }
    }
}
