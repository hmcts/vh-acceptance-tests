using System;
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
            var request = new RequestBuilder().Get(_endpoint);
            var client = new ApiClient(_url, _token).GetClient();
            return new RequestExecutor(request).SendToApi(client);
        }
        public IRestResponse Poll(int timeout = 30)
        {
            for (var i = 0; i < timeout; i++)
            {
                var response = SendTheRequest();
                if (response.StatusCode == _expected)
                {
                    return response;
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

            return null;
        }

        public bool PollForExists(int timeout = 30)
        {
            for (var i = 0; i < timeout; i++)
            {
                var response = SendTheRequest();
                if (response.StatusCode == _expected)
                {
                    return true;
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

            return false;
        }
    }
}
