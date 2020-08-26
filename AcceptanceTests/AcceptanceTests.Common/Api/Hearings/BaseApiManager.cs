using AcceptanceTests.Common.Api.Clients;
using RestSharp;

namespace AcceptanceTests.Common.Api.Hearings
{
    public class BaseApiManager
    {
        protected string ApiUrl;
        protected string Token;

        public BaseApiManager(string apiUrl, string token)
        {
            ApiUrl = apiUrl;
            Token = token;
        }

        protected BaseApiManager()
        {
        }

        public IRestResponse SendToApi(IRestRequest request)
        {
            var client = ApiClient.CreateClient(ApiUrl, Token);
            return client.Execute(request);
        }
    }
}
