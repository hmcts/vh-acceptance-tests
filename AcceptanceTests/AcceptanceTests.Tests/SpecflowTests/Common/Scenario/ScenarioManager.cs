using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace AcceptanceTests.SpecflowTests.Common.Scenario
{
    public class ScenarioManager
    {
        public static string GetScenarioTitle(ScenarioInfo scenarioInfo)
        {
            var title = "Acceptance Test Framework Integration Test : Tests";
            if (scenarioInfo != null)
                title = scenarioInfo.Title;

            return title;
        }

        public static bool HasTag(string tagName, ScenarioInfo scenarioInfo)
        {
            bool hasTag = false;

            if (scenarioInfo != null)
                hasTag = scenarioInfo.Tags.Any(tag => tag.Equals(tagName, StringComparison.CurrentCultureIgnoreCase));

            return hasTag;
        }
    }
}
