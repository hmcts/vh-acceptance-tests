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
            NUnit.Framework.TestContext.WriteLine($"API Request: {request.} ApiUrl {ApiUrl} Token {Token}");
            var client = ApiClient.CreateClient(ApiUrl, Token);
            return client.Execute(request);
        }
    }
}
