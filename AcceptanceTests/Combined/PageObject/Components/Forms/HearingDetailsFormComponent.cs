using AcceptanceTests.Common.Driver.DriverExtensions;
using AcceptanceTests.Common.Model.FormData;
using AcceptanceTests.Common.PageObject.Components.DropdownLists;
using Coypu;
using OpenQA.Selenium;

namespace AcceptanceTests.Common.PageObject.Components.Forms
{
    public class HearingDetailsFormComponent : Component, IFormComponent
    {
        public void CaseNumber(string value) => InputDriverExtension.ClearValuesAndEnterText(WrappedDriver, By.Id("caseNumber"), value);
        public void CaseName(string value) => InputDriverExtension.ClearValuesAndEnterText(WrappedDriver, By.Id("caseName"), value);
        public DropdownList HearingTypesDropdownList => new DropdownList(WrappedDriver, "Hearing Types", "hearingType");
        public void CheckQuestionnaireNotRequired() => ClickDriverExtension.CheckCheckboxElement(WrappedDriver, By.Id("questionnaireNotRequired"));
        public void UnCheckQuestionnaireRequired() => ClickDriverExtension.UnCheckCheckboxElement(WrappedDriver, By.Id("questionnaireNotRequired"));

        public HearingDetailsFormComponent(BrowserSession driver) : base(driver)
        {
        }

        public void FillFormDetails(IFormData formDataObject)
        {
            var hearingDetailsFormData = (HearingDetails)formDataObject;

            if (hearingDetailsFormData == null)
            {
                hearingDetailsFormData = (HearingDetails)new HearingDetails().GenerateFake();
            }

            CaseNumber(hearingDetailsFormData.CaseNumber);
            CaseName(hearingDetailsFormData.CaseName);
            HearingTypesDropdownList.FillFormDetails(hearingDetailsFormData.HearingType);

            if (hearingDetailsFormData.DontSendQuestionnaire)
                CheckQuestionnaireNotRequired();
            else
                UnCheckQuestionnaireRequired();

        }
    }
}
