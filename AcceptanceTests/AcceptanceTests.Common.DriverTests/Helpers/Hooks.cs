using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AcceptanceTests.DriverTests.Helpers
{
    public static class Hooks
    {
        internal static TestConfiguration GetUserSecrets()
        {
            var configRootBuilder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json")
                .AddUserSecrets("7c8eafa9-e05a-410b-aec2-ce368a920a7f");

            var configBuilder = configRootBuilder.Build();
            var config = Options.Create(configBuilder.GetSection("TestsConfiguration").Get<TestConfiguration>()).Value;
            config.SauceLabsAccessKey.Should().NotBeNullOrEmpty();
            config.SauceLabsRealDeviceApiKey.Should().NotBeNullOrEmpty();
            config.SauceLabsUsername.Should().NotBeNullOrEmpty();
            config.RemoteServer = $"http://{config.SauceLabsUsername}:{config.SauceLabsAccessKey}{config.ServerUrl}";
            return config;
        }
    }
}
