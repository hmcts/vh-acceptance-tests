using System;
using AcceptanceTests.Model.Context;
using Coypu;

namespace AcceptanceTests.Tests.Steps
{
    public class StepsBase
    {
        protected readonly AppContextManager _appContextManager;
        protected ITestContext _testContext;
        protected readonly BrowserSession _driver;

        public StepsBase(AppContextManager appContextManager, ITestContext testContext, BrowserSession driver)
        {
            _appContextManager = appContextManager;
            _testContext = testContext;
            _driver = driver;
        }
    }
}
