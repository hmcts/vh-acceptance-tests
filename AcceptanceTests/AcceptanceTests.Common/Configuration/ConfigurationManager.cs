using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace AcceptanceTests.Common.Configuration
{
    public static class ConfigurationManager
    {
        public static IConfigurationRoot BuildConfig(string userSecretsId)
        {
            var configRootBuilder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json")
                .AddJsonFile($"appsettings.Development.json", optional: true)
                .AddJsonFile($"appsettings.Production.json", optional: true)
                .AddUserSecrets(userSecretsId);
            return configRootBuilder.Build();
        }
        
        public static IConfigurationRoot BuildConfig(string userSecretsId, string testSecretsId)
        {
            var testConfigBuilder = new ConfigurationBuilder()
                .AddUserSecrets(testSecretsId)
                .Build();

            return new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json")
                .AddUserSecrets(userSecretsId)
                .AddConfiguration(testConfigBuilder)
                .Build();
        }

        public static void VerifyConfigValuesSet(object o)
        {
            foreach (var pi in o.GetType().GetProperties())
            {
                if (pi.PropertyType != typeof(string)) continue;
                var value = (string)pi.GetValue(o);
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidDataContractException($"Expected property {pi} is empty or has not been set");
                }
            }
        }

        public static async Task<string> GetBearerToken(IAzureAdConfig azureAdConfig, string resourceId)
        {
            var authContext = new AuthenticationContext(azureAdConfig.Authority);
            var credential = new ClientCredential(azureAdConfig.ClientId, azureAdConfig.ClientSecret);
            var token = await authContext.AcquireTokenAsync(resourceId, credential);
            return token.AccessToken;
        }
    }
}
