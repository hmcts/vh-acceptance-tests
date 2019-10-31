using System;
using AcceptanceTests.Model.Context;
using Coypu;
using TechTalk.SpecFlow;

namespace AcceptanceTests.Tests.Steps
{
    [Binding]
    public class QuestionnaireSteps : StepsBase
    {
        public QuestionnaireSteps(AppContextManager appContextManager, ITestContext testContext, BrowserSession driver)
            : base(appContextManager, testContext, driver)
        {
        }

        [Given(@"I don't have any pending questionnaires to complete")]
        public void GivenIDontHaveAnyPendingQuestionnairesToComplete()
        {
            //TODO: Call bookings API to ensure the current user doesn't have any pending questionnaires to complete
            ScenarioContext.Current.Pending();
        }

        [Given(@"I answered '(.*)' to '(.*)' question")]
        public void GivenIAnsweredToQuestion(string answer, string questionTitle)
        {
            //TODO: Call bookings API to ensure the current user doesn't have any pending questionnaires to complete and have answered No to the appropriate question
            ScenarioContext.Current.Pending();
        }
    }
}
