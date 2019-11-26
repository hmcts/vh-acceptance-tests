using System.Collections.Generic;
using AcceptanceTests.PageObject.Components.DropdownLists;
using AcceptanceTests.PageObject.Components.Forms;
using AcceptanceTests.PageObject.Pages.Common;
using Coypu;
using OpenQA.Selenium;

namespace AcceptanceTests.PageObject.Pages.AdminWebsite
{
    public class AssignJudgePage : UserJourneyPage
    {
        public DropdownList CourtroomAccount() => new DropdownList(WrappedDriver, "Courtroom Account", "judgeDisplayName");
        public By JudgeName => By.Id("judgeDisplayName");
        public string ChangeSelectedClerk() => CourtroomAccount().SelectLast();

        public AssignJudgePage(BrowserSession driver, string path) : base(driver, path)
        {
            HeadingText = "Assign a courtroom account";
            _pageFormList = new List<IFormComponent>
            {
                CourtroomAccount()
            };
        }
    }
}
