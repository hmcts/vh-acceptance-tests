using AcceptanceTests.Driver.DriverExtensions;
using AcceptanceTests.Model.FormData;
using AcceptanceTests.PageObject.Components.DropdownLists;
using Coypu;
using OpenQA.Selenium;

namespace AcceptanceTests.PageObject.Components
{
    public class HearingDetailsForm : Component, IFormComponent
    {
        public void CaseNumber(string value) => InputDriverExtension.ClearTextAndEnterText(WrappedDriver, By.Id("caseNumber"), value);
        public void CaseName(string value) => InputDriverExtension.ClearTextAndEnterText(WrappedDriver, By.Id("caseName"), value);
        public DropdownList HearingTypesDropdownList => new DropdownList(WrappedDriver, "Hearing Types", "hearingType");
        public void CheckQuestionnaireNotRequired() => ButtonDriverExtension.ClickElement(WrappedDriver, By.Id("questionnaireNotRequired"));

        //public DropdownList CaseTypesDropdownList => new DropdownList(WrappedDriver, "Case Types", "caseTypes");

        internal HearingDetailsForm(BrowserSession driver) : base(driver)
        {
        }

        public void FillFormDetails(object formDataObject)
        {
            var hearingDetails = (HearingDetails)formDataObject;

            if (hearingDetails == null)
            {
                hearingDetails = new HearingDetails().GenerateFakeHearing();
            }

            CaseNumber(hearingDetails.CaseNumber);
            CaseName(hearingDetails.CaseName);
            HearingTypesDropdownList.SelectFirst();
            CheckQuestionnaireNotRequired();
        }
    }
}
