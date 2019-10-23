using System.Collections.Generic;
using AcceptanceTests.Configuration.SecurityConfiguration;
using AcceptanceTests.Configuration.ServiceConfiguration;
using AcceptanceTests.Model.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AcceptanceTests.Configuration
{
    public class ConfigurationReader
    {
        public static ISecurityConfiguration GetAzureAdConfig(IConfigurationRoot configRoot)
        {
            return Options.Create(configRoot.GetSection("AzureAd").Get<SecurityConfigurationBase>()).Value;
        }

        public static IServiceConfiguration GetVhServiceConfig(IConfigurationRoot configRoot)
        {
            return Options.Create(configRoot.GetSection("VhServices").Get<ServiceConfigurationBase>()).Value;
        }

        public static string GetWebsiteUrl(IConfigurationRoot configRoot)
        {
            return configRoot.GetSection("WebsiteUrl").Value;
        }

        public static string GetVideoAppUrl(IConfigurationRoot configRoot)
        {
            return configRoot.GetSection("VideoAppUrl").Value;
        }

        public static UserSecrets GetTestUserSecrets(IConfigurationRoot configRoot)
        {
            return Options.Create(configRoot.GetSection("TestUserSecrets").Get<UserSecrets>()).Value;
        }

        public static List<TestUser> GetTestUsers(IConfigurationRoot configRoot)
        {
            var userList = configRoot.GetSection("UserAccounts").Get<List<TestUser>>();
            return Options.Create(userList).Value;
        }
    }
}
