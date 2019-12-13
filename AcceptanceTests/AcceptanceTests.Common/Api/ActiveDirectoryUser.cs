using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Polly;

namespace AdminWebsite.Common.Api
{
    public static class ActiveDirectoryUser
    {
        private const string ApiBaseUrl = "https://graph.microsoft.com/v1.0/";

        public static async Task DeleteTheUserFromAdAsync(string tenantId, string user, string token)
        {
            var url = $@"{ApiBaseUrl}{tenantId}/users/{user}";
            await SendGraphApiRequest(HttpMethod.Delete, url, token);
            Console.WriteLine($"Deleted user: {user}");
        }

        private static async Task<string> SendGraphApiRequest(HttpMethod method, string url, string token)
        {
            var policy = Policy.HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.NotFound)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (msg, time) => { Console.WriteLine($"Received {msg.Result.StatusCode} for {method} {url}"); });

            var result = await policy.ExecuteAsync(async () =>
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var httpRequestMessage = new HttpRequestMessage(method, url);
                return await client.SendAsync(httpRequestMessage);
            });

            var response = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to execute {method} on {url}, got response {result.StatusCode}: {response}");
            }

            return response;
        }
    }
}
