using System.Collections.Generic;
using System.Net.Http;
using AcceptanceTests.Common.Configuration;
using AcceptanceTests.TestAPI.Common.Configuration;
using AcceptanceTests.TestAPI.Common.Security;
using AcceptanceTests.TestAPI.Contract.Responses;
using AcceptanceTests.TestAPI.DAL;
using AcceptanceTests.TestAPI.Domain;
using AcceptanceTests.TestAPI.IntegrationTests.Configuration;
using AcceptanceTests.TestAPI.IntegrationTests.Data;
using FluentAssertions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TechTalk.SpecFlow;

namespace AcceptanceTests.TestAPI.IntegrationTests.Hooks
{
    [Binding]
    public class ConfigHooks
    {
        private readonly IConfigurationRoot _configRoot;

        public ConfigHooks(TestContext context)
        {
            _configRoot = ConfigurationManager.BuildConfig("04df59fe-66aa-4fb2-8ac5-b87656f7675a");
            context.Config = new Config();
            context.Tokens = new Tokens();
        }

        [BeforeScenario(Order = (int) HooksSequence.ConfigHooks)]
        public void RegisterSecrets(TestContext context)
        {
            var azureOptions = RegisterAzureSecrets(context);
            RegisterHearingServices(context);
            RegisterDatabaseSettings(context);
            RegisterUsernameStem(context);
            RegisterTestData(context);
            RegisterServer(context);
            RegisterApiSettings(context);
            GenerateBearerTokens(context, azureOptions);
        }

        private IOptions<AzureAdConfiguration> RegisterAzureSecrets(TestContext context)
        {
            var azureOptions = Options.Create(_configRoot.GetSection("AzureAd").Get<AzureAdConfiguration>());
            context.Config.AzureAdConfiguration = azureOptions.Value;
            ConfigurationManager.VerifyConfigValuesSet(context.Config.AzureAdConfiguration);
            return azureOptions;
        }

        private void RegisterHearingServices(TestContext context)
        {
            context.Config.VhServices = Options.Create(_configRoot.GetSection("Services").Get<ServicesConfiguration>()).Value;
        }

        private void RegisterDatabaseSettings(TestContext context)
        {
            context.Config.DbConnection = Options.Create(_configRoot.GetSection("ConnectionStrings").Get<DbConfig>()).Value;
            ConfigurationManager.VerifyConfigValuesSet(context.Config.DbConnection);
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<TestApiDbContext>();
            dbContextOptionsBuilder.EnableSensitiveDataLogging();
            dbContextOptionsBuilder.UseSqlServer(context.Config.DbConnection.TestApi);
            context.DbContextOptions = dbContextOptionsBuilder.Options;
            context.TestDataManager = new TestDataManager(context, context.Config.VhServices, context.DbContextOptions);
        }

        private void RegisterUsernameStem(TestContext context)
        {
            context.Config.UsernameStem = _configRoot.GetValue<string>("UsernameStem");
        }

        private static void RegisterTestData(TestContext context)
        {
            context.Test = new TestData
            {
                Allocations = new List<Allocation>(),
                Users = new List<User>(),
                UserResponses = new List<UserDetailsResponse>()
            };
        }

        private static void RegisterServer(TestContext context)
        {
            var webHostBuilder = WebHost.CreateDefaultBuilder()
                .UseKestrel(c => c.AddServerHeader = false)
                .UseEnvironment("Development")
                .UseStartup<Startup>();
            context.Server = new TestServer(webHostBuilder);
        }

        private static void RegisterApiSettings(TestContext context)
        {
            context.Response = new HttpResponseMessage();
        }

        private static void GenerateBearerTokens(TestContext context, IOptions<AzureAdConfiguration> azureOptions)
        {
            context.Tokens.TestApiBearerToken = new AzureTokenProvider(azureOptions).GetClientAccessToken(
                azureOptions.Value.ClientId, azureOptions.Value.ClientSecret,
                azureOptions.Value.ValidAudience);
            context.Tokens.TestApiBearerToken.Should().NotBeNullOrEmpty();
        }
    }
}
