﻿using AcceptanceTests.PageObject.Pages.Common;
using Coypu;

namespace AcceptanceTests.PageObject.Pages.AdminWebsite
{
    public class AssignJudgePage : UserJourneyPage
    {
        public AssignJudgePage(BrowserSession driver, string path) : base(driver, path)
        {
            HeadingText = "Assign Judge";
        }
    }
}
