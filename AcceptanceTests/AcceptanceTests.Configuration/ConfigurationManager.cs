using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using AcceptanceTests.Configuration.SecurityConfiguration;
using AcceptanceTests.Configuration.ServiceConfiguration;
using AcceptanceTests.Model.Context;

namespace AcceptanceTests.Configuration
{
    public class ConfigurationManager
    {
        public static IConfigurationRoot BuildDefaultConfigRoot(string targetApp, string userSecrets)
        {
            var configRootBuilder = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json")
             .AddJsonFile($"{targetApp.ToLower()}.useraccounts.json")
             .AddEnvironmentVariables()
             .AddUserSecrets(userSecrets);

            return configRootBuilder.Build();
        }

        public async static Task<string> GetBearerToken(ISecurityConfiguration azureAdConfig, IServiceConfiguration vhServiceConfig)
        {
            var authContext = new AuthenticationContext(azureAdConfig.Authority);
            var credential = new ClientCredential(azureAdConfig.ClientId, azureAdConfig.ClientSecret);
            var token = await authContext.AcquireTokenAsync(vhServiceConfig.BookingsApiResourceId, credential);

            return token.AccessToken;
        }

        public async static Task<ITestContext> ParseConfigurationIntoTestContext(IConfigurationRoot configRoot)
        {
            var azureAdConfig = ConfigurationReader.GetAzureAdConfig(configRoot);
            var vhServiceConfig = ConfigurationReader.GetVhServiceConfig(configRoot);

            var testContext = new TestContextBase();
            testContext.BookingsApiBearerToken = await GetBearerToken(azureAdConfig, vhServiceConfig);
            testContext.BookingsApiBaseUrl = vhServiceConfig.BookingsApiUrl;
            testContext.BaseUrl = ConfigurationReader.GetWebsiteUrl(configRoot);
            testContext.VideoAppUrl = ConfigurationReader.GetVideoAppUrl(configRoot);

            testContext.UserContext = new UserContext();
            testContext.UserContext.TestUserSecrets = ConfigurationReader.GetTestUserSecrets(configRoot);
            testContext.UserContext.TestUsers = ConfigurationReader.GetTestUsers(configRoot);

            return testContext;
        }
    }
}
