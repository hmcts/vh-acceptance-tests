using RestSharp;
using System.Net;

namespace AcceptanceTests.Common.Api.Clients
{
    public static class ApiClient
    { 
        public static RestClient CreateClient(string apiUrl, string bearerToken, WebProxy webProxy = null)
        {
            var client = new RestClient(apiUrl) { Proxy = webProxy };
            client.AddDefaultHeader("Accept", "application/json");
            client.AddDefaultHeader("Authorization", $"Bearer {bearerToken}");
            NUnit.Framework.TestContext.WriteLine($"API Request: {client.BaseUrl} API URL: {apiUrl}");
            return client;
        }
    }
}