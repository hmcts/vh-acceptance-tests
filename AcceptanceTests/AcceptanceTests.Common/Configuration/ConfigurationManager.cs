using System;
using System.Threading.Tasks;
using AcceptanceTests.Common.Configuration.SecurityConfig;
using AcceptanceTests.Common.Configuration.TestConfiguration;
using AcceptanceTests.Common.Model.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace AcceptanceTests.Common.Configuration
{
    public class ConfigurationManager
    {
        public static string GetTargetAppFromAppSettingsConfiguration()
        {
            var configRoot = new ConfigurationBuilder()
             .AddJsonFile($"appsettings.json");
            var targetApp = ConfigurationReader.GetTargetApp(configRoot.Build());
            return targetApp;
        }

        public static IConfigurationRoot BuildDefaultConfigRoot(string rootFolder, string targetApp, string userSecrets)
        {
            var path = $"{rootFolder}/{targetApp}/Resources/{targetApp.ToLower()}";

            Console.WriteLine($"Loading configuration from path {path}");
            var configRootBuilder = new ConfigurationBuilder()
             .AddJsonFile($"appsettings.json")
             .AddJsonFile($"{path}.appsettings.json")
             .AddJsonFile($"{path}.useraccounts.json")
             .AddEnvironmentVariables()
             .AddUserSecrets(userSecrets);

            return configRootBuilder.Build();
        }

        public static async Task<string> GetBearerToken(IAzureAdConfig azureAdConfig, IServiceConfig vhServiceConfig)
        {
            var authContext = new AuthenticationContext(azureAdConfig.Authority);
            var credential = new ClientCredential(azureAdConfig.ClientId, azureAdConfig.ClientSecret);
            var token = await authContext.AcquireTokenAsync(vhServiceConfig.BookingsApiResourceId, credential);

            return token.AccessToken;
        }

        public static async Task<ITestContext> ParseConfigurationIntoTestContext(IConfigurationRoot configRoot)
        {
            var azureAdConfig = ConfigurationReader.GetAzureAdConfig(configRoot);
            var vhServiceConfig = ConfigurationReader.GetVhServiceConfig(configRoot);

            var testContext = new TestContextBase
            {
                TargetBrowser = ConfigurationReader.GetTargetBrowser(configRoot),
                BookingsApiBearerToken = await GetBearerToken(azureAdConfig, vhServiceConfig),
                BookingsApiBaseUrl = vhServiceConfig.BookingsApiUrl,
                BaseUrl = ConfigurationReader.GetWebsiteUrl(configRoot),
                VideoAppUrl = ConfigurationReader.GetVideoAppUrl(configRoot),
                UserContext = new UserContext
                {
                    TestUserSecrets = ConfigurationReader.GetTestUserSecrets(configRoot),
                    TestUsers = ConfigurationReader.GetTestUsers(configRoot)
                }
            };


            return testContext;
        }
    }
}
