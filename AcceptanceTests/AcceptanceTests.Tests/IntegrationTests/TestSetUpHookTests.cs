using System;
using AcceptanceTests.Driver.Support;
using AcceptanceTests.Model;
using AcceptanceTests.Model.Context;
using AcceptanceTests.Tests.Hooks;
using FluentAssertions;
using NUnit.Framework;

namespace AcceptanceTests.Tests.IntegrationTests
{
    public class TestSetUpHookTests : HookTestsBase
    {
        [SetUp]
        public void TestSetUp()
        {
            SetUp();
        }

        [Test]
        public void OneTimeTestSetUpTargetAppTest()
        {
            TestSetUpHook hook = new TestSetUpHook(_objectContainer, _appContextManager);
            hook.OneTimeSetup();
            var targetApp = NUnitParamReader.GetTargetApp();
            Console.WriteLine($"Expected target app is: {targetApp}");
            var testContext = _objectContainer.Resolve<ITestContext>();
            EnumParser.ParseText<SutSupport>(testContext.CurrentApp).Should().Be(targetApp);
        }
    }
}
