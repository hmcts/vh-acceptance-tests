﻿using AcceptanceTests.Model.FormData;
using AcceptanceTests.PageObject.Components.DropdownLists;
using Coypu;
using OpenQA.Selenium;

namespace AcceptanceTests.PageObject.Components.Forms
{
    public class AssignJudgeFormComponent : Component, IFormComponent
    {
        public DropdownList CourtroomAccount => new DropdownList(WrappedDriver, "Courtroom Account", "judgeName");
        public By JudgeName => By.Id("judgeDisplayName");
        public string ChangeSelectedClerk() => CourtroomAccount.SelectLast();

        public AssignJudgeFormComponent(BrowserSession driver) : base(driver)
        {
        }

        public void FillFormDetails(IFormData formData)
        {
            var assignJudgeData = (DropdownListFormData)formData;
            CourtroomAccount.FillFormDetails(assignJudgeData);
        }
    }
}
