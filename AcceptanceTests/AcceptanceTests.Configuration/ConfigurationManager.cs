using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using AcceptanceTests.Configuration.SecuritySettings;
using AcceptanceTests.Configuration.Settings;
using AcceptanceTests.Model.User;
using AcceptanceTests.Model.Context;

namespace AcceptanceTests.Configuration
{
    public class ConfigurationManager
    {
        public static IConfigurationRoot BuildDefaultConfigRoot(string userSecrets)
        {
            var configRootBuilder = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json")
             .AddJsonFile("useraccounts.json")
             .AddEnvironmentVariables()
             .AddUserSecrets(userSecrets);

            return configRootBuilder.Build();
        }

        public async static Task<string> GetBearerToken(IConfigurationRoot configRoot)
        {
            var azureAdConfig = Options.Create(configRoot.GetSection("AzureAd").Get<SecuritySettingsBase>()).Value;
            var vhServiceConfig = Options.Create(configRoot.GetSection("VhServices").Get<ServiceSettingsBase>()).Value;
            var authContext = new AuthenticationContext(azureAdConfig.Authority);
            var credential = new ClientCredential(azureAdConfig.ClientId, azureAdConfig.ClientSecret);
            var token = await authContext.AcquireTokenAsync(vhServiceConfig.BookingsApiResourceId, credential);

            return token.AccessToken;
        }

        public async static Task<TestContextBase> ParseConfigurationIntoTestContext(IConfigurationRoot configRoot, TestContextBase testContext)
        {
            var vhServiceConfig = Options.Create(configRoot.GetSection("VhServices").Get<ServiceSettingsBase>()).Value;
            var userAccountConfig = Options.Create(configRoot.GetSection("TestUserSecrets").Get<UserSecrets>()).Value;
            testContext.BookingsApiBearerToken = await GetBearerToken(configRoot);
            testContext.BaseUrl = vhServiceConfig.BookingsApiUrl;
            testContext.TestUserSecrets = userAccountConfig;
            testContext.BaseUrl = configRoot.GetSection("WebsiteUrl").Value;
            testContext.VideoAppUrl = configRoot.GetSection("VideoAppUrl").Value;

            return testContext;
        }

        public static UserSecrets GetTestUserSecrets(IConfigurationRoot configRoot)
        {
            return Options.Create(configRoot.GetSection("TestUserSecrets").Get<UserSecrets>()).Value;
        }
    }
}
