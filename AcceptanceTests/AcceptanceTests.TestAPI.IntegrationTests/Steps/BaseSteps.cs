using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;

namespace AcceptanceTests.TestAPI.IntegrationTests.Steps
{
    public abstract class BaseSteps
    {
        private HttpClient CreateClient(TestServer server, string token)
        {
            var client = server.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            return client;
        }

        protected async Task<HttpResponseMessage> SendGetRequestAsync(TestContext context)
        {
            using var client = CreateClient(context.Server, context.Tokens.TestApiBearerToken);
            return await client.GetAsync(context.Uri);
        }

        protected async Task<HttpResponseMessage> SendPatchRequestAsync(TestContext context)
        {
            using var client = CreateClient(context.Server, context.Tokens.TestApiBearerToken);
            return await client.PatchAsync(context.Uri, context.HttpContent);
        }

        protected async Task<HttpResponseMessage> SendPostRequestAsync(TestContext context)
        {
            using var client = CreateClient(context.Server, context.Tokens.TestApiBearerToken);
            return await client.PostAsync(context.Uri, context.HttpContent);
        }

        protected async Task<HttpResponseMessage> SendPutRequestAsync(TestContext context)
        {
            using var client = CreateClient(context.Server, context.Tokens.TestApiBearerToken);
            return await client.PutAsync(context.Uri, context.HttpContent);
        }

        protected async Task<HttpResponseMessage> SendDeleteRequestAsync(TestContext context)
        {
            using var client = CreateClient(context.Server, context.Tokens.TestApiBearerToken);
            return await client.DeleteAsync(context.Uri);
        }
    }
}
