using BoDi;
using NUnit.Framework;

namespace AcceptanceTests.Tests.IntegrationTests
{
    public class HookTestsBase
    {
        protected AppContextManager _appContextManager;
        protected IObjectContainer _objectContainer;

        public void SetUp()
        {
            _appContextManager = new AppContextManager();
            _objectContainer = new ObjectContainer();
        }
    }
}
