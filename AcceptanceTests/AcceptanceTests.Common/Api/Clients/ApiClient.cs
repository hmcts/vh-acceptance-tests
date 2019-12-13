
using RestSharp;

namespace AdminWebsite.Common.Api.Clients
{
    public class ApiClient
    {
        private readonly string _apiUrl;
        private readonly string _bearerToken;

        public ApiClient(string apiUrl, string bearerToken)
        {
            _apiUrl = apiUrl;
            _bearerToken = bearerToken;
        }

        public RestClient GetClient()
        {
            var client = new RestClient(_apiUrl);
            client.AddDefaultHeader("Accept", "application/json");
            client.AddDefaultHeader("Authorization", $"Bearer {_bearerToken}");
            return client;
        }
    }
}
