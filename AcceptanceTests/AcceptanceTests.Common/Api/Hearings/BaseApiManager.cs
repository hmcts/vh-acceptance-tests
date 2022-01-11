using AcceptanceTests.Common.Api.Clients;
using RestSharp;

using System;

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
            var response = client.Execute(request);
            NUnit.Framework.TestContext.WriteLine($"BaseApiManager Request: {request.Body} ApiUrl {ApiUrl} Token {Token} response {response.Content}");
            return response;
        }
    }
}
