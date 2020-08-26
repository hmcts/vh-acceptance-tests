using System;
using System.Net;
using AcceptanceTests.Common.Api.Helpers;
using AcceptanceTests.Common.Api.Requests;
using AcceptanceTests.Common.Api.Uris;
using RestSharp;

namespace AcceptanceTests.Common.Api.Hearings
{
    public class TestApiManager : BaseApiManager
    {
        public TestApiManager(string apiUrl, string token)
        {
            ApiUrl = apiUrl;
            Token = token;
        }

        public IRestResponse HealthCheck()
        {
            var endpoint = TestApiUriFactory.HealthCheckEndpoints.CheckServiceHealth;
            var request = RequestBuilder.Get(endpoint);
            return SendToApi(request);
        }

        public IRestResponse PollForSelfTestScoreResponse(Guid conferenceId, Guid participantId, int timeout = 30)
        {
            var endpoint = TestApiUriFactory.ConferenceEndpoints.GetSelfTestScore(conferenceId, participantId);
            return new Polling().WithEndpoint(endpoint).Url(ApiUrl).Token(Token).UntilStatusIs(HttpStatusCode.OK).Poll(timeout);
        }
    }
}
