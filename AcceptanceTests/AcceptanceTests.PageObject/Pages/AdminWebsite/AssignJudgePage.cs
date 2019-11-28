using System.Collections.Generic;
using AcceptanceTests.PageObject.Components.DropdownLists;
using AcceptanceTests.PageObject.Components.Forms;
using AcceptanceTests.PageObject.Pages.Common;
using Coypu;

namespace AcceptanceTests.PageObject.Pages.AdminWebsite
{
    public class AssignJudgePage : UserJourneyPage
    {
        IFormComponent _assignJudgeForm;

        public AssignJudgePage(BrowserSession driver, string path) : base(driver, path)
        {
            HeadingText = "Assign a courtroom account";
            _assignJudgeForm = new AssignJudgeFormComponent(driver);
            _pageFormList = new List<IFormComponent>
            {
                _assignJudgeForm
            };
        }
    }
}
