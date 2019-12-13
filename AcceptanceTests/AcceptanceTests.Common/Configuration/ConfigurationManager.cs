using System.Linq;
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

        public IConfigurationRoot BuildConfig()
        {
            var configRootBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("useraccounts.json")
                .AddUserSecrets(_userSecretsId);
            return configRootBuilder.Build();
        }

        public static bool VerifyConfigValuesSet(object o)
        {
            return !o.GetType().GetProperties()
                .Where(pi => pi.PropertyType == typeof(string))
                .Select(pi => (string)pi.GetValue(o))
                .Any(string.IsNullOrEmpty);
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
