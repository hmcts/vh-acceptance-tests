using System;
using System.Data;
using System.Net;
using System.Threading;
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

        public IRestResponse AllocateUser(object requestBody)
        {
            var endpoint = TestApiUriFactory.AllocationEndpoints.AllocateSingleUser;
            var request = RequestBuilder.Patch(endpoint, requestBody);
            return SendToApi(request);
        }

        public IRestResponse AllocateUsers(object requestBody)
        {
            var endpoint = TestApiUriFactory.AllocationEndpoints.AllocateUsers;
            var request = RequestBuilder.Patch(endpoint, requestBody);
            return SendToApi(request);
        }

        public IRestResponse UnallocateUsers(object requestBody)
        {
            var endpoint = TestApiUriFactory.AllocationEndpoints.UnallocateUsers;
            var request = RequestBuilder.Patch(endpoint, requestBody);
            return SendToApi(request);
        }

        public IRestResponse CreateHearing(object hearingRequest)
        {
            var endpoint = TestApiUriFactory.HearingEndpoints.CreateHearing;
            var request = RequestBuilder.Post(endpoint, hearingRequest);
            return SendToApi(request);
        }

        public IRestResponse ConfirmHearingToCreateConference(Guid hearingId, object updateRequest)
        {
            var endpoint = TestApiUriFactory.HearingEndpoints.ConfirmHearing(hearingId);
            var request = RequestBuilder.Patch(endpoint, updateRequest);
            return SendToApi(request);
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

        public IRestResponse GetConferencesForTodayVho()
        {
            var endpoint = TestApiUriFactory.ConferenceEndpoints.GetConferencesForVho;
            var request = RequestBuilder.Get(endpoint);
            return SendToApi(request);
        }

        public IRestResponse GetConferencesForTodayJudge(string username)
        {
            var endpoint = TestApiUriFactory.ConferenceEndpoints.GetConferencesForJudge(username);
            var request = RequestBuilder.Get(endpoint);
            return SendToApi(request);
        }

        public IRestResponse DeleteHearing(Guid hearingId)
        {
            var endpoint = TestApiUriFactory.HearingEndpoints.DeleteHearing(hearingId);
            var request = RequestBuilder.Delete(endpoint);
            return SendToApi(request);
        }

        public IRestResponse GetHearingsByUsername(string username)
        {
            var endpoint = TestApiUriFactory.HearingEndpoints.GetHearingsByUsername(username);
            var request = RequestBuilder.Get(endpoint);
            return SendToApi(request);
        }

        public IRestResponse PollForHearingByUsername(string username, string caseName, int timeout = 60)
        {
            for (var i = 0; i < timeout; i++)
            {
                var rawResponse = GetHearingsByUsername(username);
                if (!rawResponse.IsSuccessful) continue;
                if (rawResponse.Content.ToLower().Contains(caseName.ToLower()))
                {
                    return rawResponse;
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            throw new DataException($"Hearing with case name '{caseName}' not found after {timeout} seconds.");
        }

        public bool PollForParticipantNameUpdated(string username, string updatedDisplayName, int timeout = 60)
        {
            for (var i = 0; i < timeout; i++)
            {
                var rawResponse = GetHearingsByUsername(username);
                if (!rawResponse.IsSuccessful) continue;
                if (rawResponse.Content.ToLower().Contains(updatedDisplayName.ToLower()))
                {
                    return true;
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

            return false;
        }

        public IRestResponse GetHearing(Guid hearingId)
        {
            var endpoint = TestApiUriFactory.HearingEndpoints.GetHearing(hearingId);
            var request = RequestBuilder.Get(endpoint);
            return SendToApi(request);
        }

        public IRestResponse GetConferenceByConferenceId(Guid conferenceId)
        {
            var endpoint = TestApiUriFactory.ConferenceEndpoints.GetConferenceById(conferenceId);
            var request = RequestBuilder.Get(endpoint);
            return SendToApi(request);
        }

        public IRestResponse GetConferenceByHearingId(Guid hearingRefId)
        {
            var endpoint = TestApiUriFactory.ConferenceEndpoints.GetConferenceByHearingRefId(hearingRefId);
            var request = RequestBuilder.Get(endpoint);
            return SendToApi(request);
        }

        public IRestResponse SendEvent(object eventRequest)
        {
            var endpoint = TestApiUriFactory.ConferenceEndpoints.CreateVideoEvent;
            var request = RequestBuilder.Post(endpoint, eventRequest);
            return SendToApi(request);
        }

        public IRestResponse GetAudioRecordingLink(Guid hearingId)
        {
            var endpoint = TestApiUriFactory.ConferenceEndpoints.GetAudioRecordingLinkByHearingId(hearingId);
            var request = RequestBuilder.Get(endpoint);
            return SendToApi(request);
        }
    }
}
