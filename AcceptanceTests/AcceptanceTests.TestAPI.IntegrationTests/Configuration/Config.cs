using AcceptanceTests.TestAPI.Common.Configuration;

namespace AcceptanceTests.TestAPI.IntegrationTests.Configuration
{
    public class Config
    {
        public AzureAdConfiguration AzureAdConfiguration { get; set; }
        public DbConfig DbConnection { get; set; }
        public string UsernameStem { get; set; }
        public ServicesConfiguration VhServices { get; set; }
    }
}
