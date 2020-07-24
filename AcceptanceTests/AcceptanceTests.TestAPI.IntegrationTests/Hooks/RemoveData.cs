using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AcceptanceTests.TestAPI.IntegrationTests.Hooks
{
    [Binding]
    public static class RemoveData
    {
        [AfterScenario(Order = (int)HooksSequence.RemoveDataCreatedDuringTest)]
        public static async Task RemoveDataCreatedDuringTest(TestContext context)
        {
            //await context.TestDataManager.DeleteUsers();
        }

        [AfterScenario(Order = (int)HooksSequence.RemoveServer)]
        public static void RemoveServer(TestContext context)
        {
            context.Server.Dispose();
        }
    }
}
