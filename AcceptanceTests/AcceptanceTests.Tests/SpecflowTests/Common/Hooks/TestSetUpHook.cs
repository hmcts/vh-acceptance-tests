using TechTalk.SpecFlow;
using BoDi;
using System.Reflection;
using System.IO;
using AcceptanceTests.Model.Context;

namespace AcceptanceTests.Tests.SpecflowTests.Common.Hooks
{
    public class TestSetUpHook
    {
        private readonly IObjectContainer _objectContainer;
        private readonly AppContextManager _appContextManager;

        public TestSetUpHook(IObjectContainer objectContainer, AppContextManager appContextManager)
        {
            _objectContainer = objectContainer;
            _appContextManager = appContextManager;
        }

        public void OneTimeSetup()
        {
            var path = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/SpecflowTests/";
            OneTimeSetup(path);
        } 

        public ITestContext OneTimeSetup(string path)
        {
            var testContext = _appContextManager.SetUpTestContext(path);
            _objectContainer.RegisterInstanceAs(testContext);

            return testContext;
        }
    }
}
