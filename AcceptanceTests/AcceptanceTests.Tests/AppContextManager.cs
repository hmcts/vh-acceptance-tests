using System;
using System.Reflection;
using AcceptanceTests.Configuration;
using AcceptanceTests.Driver.Settings;
using AcceptanceTests.Driver.Support;
using AcceptanceTests.Model;
using AcceptanceTests.Model.Context;
using Microsoft.Extensions.Configuration;

namespace AcceptanceTests.Tests
{
    public class AppContextManager
    {
        //private static BookingsApiClientHelper _bookingsApiHelper;
        //private static DataSetUpValidation _dataSetUpValidation;
        public IConfigurationRoot ConfigRoot { get; private set; }

        public ITestContext SetUpTestContext(SutSupport currentApp)
        {
            var appSecret = SutSettings.GetTargetAppSecret(currentApp);
            ConfigRoot = ConfigurationManager.BuildDefaultConfigRoot(currentApp.ToString(), appSecret);
            //var restClient = new RestClient(vhServiceConfig.BookingsApiUrl);
            //_bookingsApiHelper.CreateClient(restClient, token);
            ITestContext testContext = (TestContextBase)ConfigurationManager.ParseConfigurationIntoTestContext(ConfigRoot).Result;
            Console.WriteLine(MethodBase.GetCurrentMethod().Name, "Setting TestContext.BaseUrl to: " + testContext.BaseUrl);
            testContext.CurrentApp = currentApp.ToString();
            Console.WriteLine(MethodBase.GetCurrentMethod().Name, "Setting TestContext.CurrentApp to: " + testContext.CurrentApp);
            return testContext;
        }

        public ITestContext SwitchTargetAppContext(string targetApp, ITestContext currentTestContext)
        {
            ITestContext testContext;
            var currentUserContext = currentTestContext;
            //currentUserContext.CurrentUser = new TestUser();
            //currentUserContext.CurrentUser.Username = currentTestContext.UserContext.CurrentUser.Username;
            //currentUserContext.TestUserSecrets.TestUserPassword = currentTestContext.UserContext.TestUserSecrets.TestUserPassword;

            SutSupport parsedTargetApp = GetTargetApp(targetApp);
            

            if (!parsedTargetApp.Equals(EnumParser.ParseText<SutSupport>(currentTestContext.CurrentApp)))
            {

                testContext = SetUpTestContext(parsedTargetApp);
                testContext.UserContext.CurrentUser = currentUserContext.UserContext.CurrentUser;
                testContext.UserContext.TestUserSecrets = currentTestContext.UserContext.TestUserSecrets;
                //testContext.UserContext.CurrentUser.Username = currentUserContext.CurrentUser.Username;
                //testContext.UserContext.TestUserSecrets.TestUserPassword = currentUserContext.TestUserSecrets.TestUserPassword;
            }  
            else
                testContext = currentTestContext;

            return testContext;
        }

        public SutSupport GetTargetApp(string targetApp)
        {
            SutSupport parsedTargetApp = SutSupport.AdminWebsite;

            if (!string.IsNullOrEmpty(targetApp))
                parsedTargetApp = EnumParser.ParseText<SutSupport>(targetApp);

            return parsedTargetApp;
        }
    }
}
