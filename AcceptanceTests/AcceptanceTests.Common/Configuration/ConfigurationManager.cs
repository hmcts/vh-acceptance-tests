using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace AcceptanceTests.Common.Configuration
{
    public class ConfigurationManager
    {
        private readonly string _userSecretsId;
        public ConfigurationManager(string userSecretsId)
        {
            _userSecretsId = userSecretsId;
        }

        public IConfigurationRoot BuildConfig(string environment = "")
        {
            var configRootBuilder = new ConfigurationBuilder()
                .AddUserSecrets(_userSecretsId)
                .AddJsonFile($"appsettings{environment}.json")
                .AddJsonFile("useraccounts.json");
            return configRootBuilder.Build();
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
