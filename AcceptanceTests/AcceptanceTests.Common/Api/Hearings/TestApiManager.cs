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

        public bool PollForSelfTestScoreExists(Guid conferenceId, Guid participantId, int timeout = 30)
        {
            return PollForSelfTestScoreResponse(conferenceId, participantId, timeout).StatusCode == HttpStatusCode.OK;
        }

        public IRestResponse PollForSelfTestScoreResponse(Guid conferenceId, Guid participantId, int timeout = 30)
        {
            var endpoint = TestApiUriFactory.ConferenceEndpoints.GetSelfTestScore(conferenceId, participantId);
            return new Polling().WithEndpoint(endpoint).Url(ApiUrl).Token(Token).UntilStatusIs(HttpStatusCode.OK).Poll(timeout);
        }

        public IRestResponse RemoveParticipantFromConference(Guid conferenceId, Guid participantId)
        {
            var endpoint = TestApiUriFactory.ConferenceEndpoints.DeleteParticipant(conferenceId, participantId);
            var request = RequestBuilder.Delete(endpoint);
            return SendToApi(request);
        }

        public IRestResponse GetTasks(Guid conferenceId)
        {
            var endpoint = TestApiUriFactory.ConferenceEndpoints.GetTasksByConferenceId(conferenceId);
            var request = RequestBuilder.Get(endpoint);
            return SendToApi(request);
        }

        public IRestResponse DeleteConference(Guid hearingRefId, Guid conferenceId)
        {
            var endpoint = TestApiUriFactory.ConferenceEndpoints.DeleteConference(hearingRefId, conferenceId);
            var request = RequestBuilder.Delete(endpoint);
            return SendToApi(request);
        }
    }
}
