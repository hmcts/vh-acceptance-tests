using System;
using System.Reflection;
using AcceptanceTests.Configuration;
using AcceptanceTests.Driver.Settings;
using AcceptanceTests.Driver.Support;
using AcceptanceTests.Model;
using AcceptanceTests.Model.Context;
using Microsoft.Extensions.Configuration;

namespace AcceptanceTests.Tests.SpecflowTests.Common
{
    public class AppContextManager
    {
        private string _path;
        private const string TestBuildName = "Acceptance Tests.Tests";
        public IConfigurationRoot ConfigRoot { get; private set; }

        public ITestContext SetUpTestContext(string path, string injectedApp = null)
        {
            _path = path;
            var targetApp = GetTargetApp(injectedApp);  
            
            var appSecret = SutSettings.GetTargetAppSecret(targetApp);
            ConfigRoot = ConfigurationManager.BuildDefaultConfigRoot(_path, targetApp.ToString(), appSecret);

            ITestContext testContext = (TestContextBase)ConfigurationManager.ParseConfigurationIntoTestContext(ConfigRoot).Result;
            Console.WriteLine(MethodBase.GetCurrentMethod().Name, "Setting TestContext.BaseUrl to: " + testContext.BaseUrl);

            testContext.CurrentApp = targetApp.ToString();
            Console.WriteLine(MethodBase.GetCurrentMethod().Name, "Setting TestContext.CurrentApp to: " + testContext.CurrentApp);

            return testContext;
        }

        public ITestContext SwitchTargetAppContext(string targetApp, ITestContext currentTestContext)
        {
            ITestContext testContext;
            var currentUserContext = currentTestContext;

            var parsedTargetApp = EnumParser.ParseText <SutSupport>(targetApp);
            var currentApp = EnumParser.ParseText<SutSupport>(currentTestContext.CurrentApp);

            if (!parsedTargetApp.Equals(currentApp))
            {
                testContext = SetUpTestContext(_path, targetApp);
                testContext.UserContext.CurrentUser = currentUserContext.UserContext.CurrentUser;
                testContext.UserContext.TestUserSecrets = currentTestContext.UserContext.TestUserSecrets;
            }  
            else
                testContext = currentTestContext;

            return testContext;
        }

        public SutSupport GetTargetApp(string injectedApp = null)
        {
            SutSupport targetApp;
            if (string.IsNullOrEmpty(injectedApp))
            {
                var configApp = ConfigurationManager.GetTargetAppFromAppSettingsConfiguration();
                targetApp = NUnitParamReader.GetTargetApp(configApp);
            }
            else
            {
                targetApp = EnumParser.ParseText<SutSupport>(injectedApp);
            }
            return targetApp;
        }

        public static string GetBuildName()
        {
            var buildName = $"{ Environment.GetEnvironmentVariable("Build_DefinitionName") } { Environment.GetEnvironmentVariable("RELEASE_RELEASENAME")}";
            if (string.IsNullOrEmpty(buildName.Trim()))
                buildName = TestBuildName;

            return buildName;
        }
    }
}
