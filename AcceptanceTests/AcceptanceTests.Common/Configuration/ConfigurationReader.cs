using System.Collections.Generic;
using AcceptanceTests.Common.Configuration.AzureConfig;
using AcceptanceTests.Common.Configuration.TestConfiguration;
using AcceptanceTests.Common.Configuration.TestUserConfig;
using AcceptanceTests.Common.Model.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AcceptanceTests.Common.Configuration
{
    public class ConfigurationReader
    {
        public static string GetTargetApp(IConfigurationRoot configRoot)
        {
            return configRoot.GetSection("TargetApp").Value;
        }

        public static string GetTargetBrowser(IConfigurationRoot configRoot)
        {
            return configRoot.GetSection("TargetBrowser").Value;
        }

        public static IAzureAdConfig GetAzureAdConfig(IConfigurationRoot configRoot)
        {
            return Options.Create(configRoot.GetSection("AzureAd").Get<AzureAdConfigBase>()).Value;
        }

        public static IServiceConfig GetVhServiceConfig(IConfigurationRoot configRoot)
        {
            return Options.Create(configRoot.GetSection("VhServices").Get<ServiceConfigBase>()).Value;
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

        public static List<UserAccountBase> GetTestUsers(IConfigurationRoot configRoot)
        {
            var userList = configRoot.GetSection("UserAccounts").Get<List<UserAccountBase>>();
            return Options.Create(userList).Value;
        }
    }
}
