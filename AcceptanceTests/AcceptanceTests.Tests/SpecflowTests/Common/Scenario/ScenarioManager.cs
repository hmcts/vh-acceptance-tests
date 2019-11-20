using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace AcceptanceTests.SpecflowTests.Common.Scenario
{
    public class ScenarioManager
    {
        public static string GetScenarioTitle(ScenarioContext scenarioContext)
        {
            var title = "Acceptance Test Framework Integration Test : Tests";
            if (scenarioContext != null)
                title = scenarioContext.ScenarioInfo.Title;

            return title;
        }

        public static bool HasTag(string tagName, ScenarioContext scenarioContext)
        {
            bool hasTag = false;

            if (scenarioContext != null)
                hasTag = scenarioContext.ScenarioInfo.Tags.Any(tag => tag.Equals(tagName, StringComparison.CurrentCultureIgnoreCase));

            return hasTag;
        }
    }
}
