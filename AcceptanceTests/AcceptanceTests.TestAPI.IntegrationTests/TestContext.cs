using System.Net.Http;
using AcceptanceTests.TestAPI.DAL;
using AcceptanceTests.TestAPI.IntegrationTests.Configuration;
using AcceptanceTests.TestAPI.IntegrationTests.Data;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;

namespace AcceptanceTests.TestAPI.IntegrationTests
{
    public class TestContext
    {
        public Config Config { get; set; }
        public DbContextOptions<TestApiDbContext> DbContextOptions { get; set; }
        public HttpContent HttpContent { get; set; }
        public HttpMethod HttpMethod { get; set; }
        public HttpResponseMessage Response { get; set; }
        public TestServer Server { get; set; }
        public TestData Test { get; set; }
        public TestDataManager TestDataManager { get; set; }
        public Tokens Tokens { get; set; }
        public string Uri { get; set; }
    }
}
