using System;
using System.Data;
using System.Net;
using System.Threading;
using AcceptanceTests.Common.Api.Clients;
using AcceptanceTests.Common.Api.Requests;
using RestSharp;

namespace AcceptanceTests.Common.Api.Helpers
{
    public class Polling
    {
        private string _endpoint;
        private string _url;
        private string _token;
        private HttpStatusCode _expected;
        private const int DEFAULT_TIMEOUT = 30;

        public Polling WithEndpoint(string endpoint)
        {
            _endpoint = endpoint;
            return this;
        }

        public Polling Url(string url)
        {
            _url = url;
            return this;
        }

        public Polling Token(string token)
        {
            _token = token;
            return this;
        }

        public Polling UntilStatusIs(HttpStatusCode expected)
        {
            _expected = expected;
            return this;
        }

        private IRestResponse SendTheRequest()
        {
            var request = RequestBuilder.Get(_endpoint);
            var client = ApiClient.CreateClient(_url, _token);
            return RequestExecutor.SendToApi(request, client);
        }
        public IRestResponse Poll()//int timeout = 60)
        {
            for (var i = 0; i < DEFAULT_TIMEOUT; i++)
            {
                var response = SendTheRequest();
                if (response.StatusCode == _expected)
                {
                    return response;
                }
                //Thread.Sleep(TimeSpan.FromSeconds(1));    /* unable to see why waiting for 1 second on each iteration is required or significant */
            }

            throw new DataException($"Expected status {_expected} not reached after {DEFAULT_TIMEOUT} seconds.");
        }
    }
}
