using System;
using System.IO;
using System.Reflection;
using AcceptanceTests.Configuration;
using AcceptanceTests.Driver.Settings;
using AcceptanceTests.Driver.Support;
using AcceptanceTests.Model;
using AcceptanceTests.Model.Context;
using Microsoft.Extensions.Configuration;

namespace AcceptanceTests.SpecflowTests.Common
{
    public class AppContextManager
    {
        private const string TestBuildName = "Acceptance Tests.Tests";
        public IConfigurationRoot ConfigRoot { get; private set; }

        public ITestContext SetUpTestContext(string injectedApp = null)
        {
            var targetApp = GetTargetApp(injectedApp);  
            
            var appSecret = SutSettings.GetTargetAppSecret(targetApp);
            var path =$"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/SpecflowTests/{targetApp}/resources";
            ConfigRoot = ConfigurationManager.BuildDefaultConfigRoot(path, targetApp.ToString(), appSecret);

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
                testContext = SetUpTestContext(targetApp);
                testContext.UserContext.CurrentUser = currentUserContext.UserContext.CurrentUser;
                testContext.UserContext.TestUserSecrets = currentTestContext.UserContext.TestUserSecrets;
            }  
            else
                testContext = currentTestContext;

            return testContext;
        }

        private SutSupport GetTargetApp(string injectedApp)
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
