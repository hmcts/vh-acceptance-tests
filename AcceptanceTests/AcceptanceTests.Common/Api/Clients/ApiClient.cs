using RestSharp;

namespace AcceptanceTests.Common.Api.Clients
{
    public static class ApiClient
    { 
        public static RestClient SetClient(string apiUrl, string bearerToken)
        {
            var client = new RestClient(apiUrl);
            client.AddDefaultHeader("Accept", "application/json");
            client.AddDefaultHeader("Authorization", $"Bearer {bearerToken}");
            return client;
        }
    }
}