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
        private const int DEFAULT_TIMEOUT = 30;

        public TestApiManager(string apiUrl, string token)
        {
            ApiUrl = apiUrl;
            Token = token;
        }

        public IRestResponse HealthCheck()
        {
            var endpoint = TestApiUriFactory.HealthCheckEndpoints.CheckServiceHealth;
            NUnit.Framework.TestContext.WriteLine($"Healthcheck link: {endpoint}");
            var request = RequestBuilder.Get(endpoint);
            return SendToApi(request);
        }

        public IRestResponse AllocateUser(object requestBody)
        {
            var endpoint = TestApiUriFactory.AllocationEndpoints.AllocateSingleUser;
            var request = RequestBuilder.Patch(endpoint, requestBody);
            NUnit.Framework.TestContext.WriteLine($"AllocateUser link: {endpoint} Request body {requestBody}");
            return SendToApi(request);
        }

        public IRestResponse AllocateUsers(object requestBody)
        {
            var endpoint = TestApiUriFactory.AllocationEndpoints.AllocateUsers;
            var request = RequestBuilder.Patch(endpoint, requestBody);
            NUnit.Framework.TestContext.WriteLine($"AllocateUsers (multiple) link: {endpoint} Request body {requestBody}");
            return SendToApi(request);
        }

        public IRestResponse UnallocateUsers(object requestBody)
        {
            var endpoint = TestApiUriFactory.AllocationEndpoints.UnallocateUsers;
            var request = RequestBuilder.Patch(endpoint, requestBody);
            NUnit.Framework.TestContext.WriteLine($"UnallocateUsers (multiple) link: {endpoint} Request body {requestBody}");
            return SendToApi(request);
        }

        public IRestResponse CreateHearing(object hearingRequest)
        {
            var endpoint = TestApiUriFactory.HearingEndpoints.CreateHearing;
            var request = RequestBuilder.Post(endpoint, hearingRequest);
            NUnit.Framework.TestContext.WriteLine($"CreateHearing link: {endpoint} Request body {request}");
            return SendToApi(request);
        }

        public IRestResponse ConfirmHearingToCreateConference(Guid hearingId, object updateRequest)
        {
            var endpoint = TestApiUriFactory.HearingEndpoints.ConfirmHearing(hearingId);
            var request = RequestBuilder.Patch(endpoint, updateRequest);
            NUnit.Framework.TestContext.WriteLine($"ConfirmHearingToCreateConferencelink: {endpoint} Request body {request} Hearing ID {hearingId} request {updateRequest.ToString()}");
            return SendToApi(request);
        }

        public bool PollForSelfTestScoreExists(Guid conferenceId, Guid participantId, int timeout = DEFAULT_TIMEOUT)
        {
            NUnit.Framework.TestContext.WriteLine($"PollForSelfTestScoreExistslink: Conference ID {conferenceId} participant id {participantId}");
            return PollForSelfTestScoreResponse(conferenceId, participantId, timeout).StatusCode == HttpStatusCode.OK;
        }

        public IRestResponse PollForSelfTestScoreResponse(Guid conferenceId, Guid participantId, int timeout = DEFAULT_TIMEOUT)
        {
            var endpoint = TestApiUriFactory.ConferenceEndpoints.GetSelfTestScore(conferenceId, participantId);
            NUnit.Framework.TestContext.WriteLine($"PollForSelfTestScoreResponse link: Conference ID {conferenceId} participant id {participantId}");
            return new Polling().WithEndpoint(endpoint).Url(ApiUrl).Token(Token).UntilStatusIs(HttpStatusCode.OK).Poll(timeout);
        }

        public IRestResponse RemoveParticipantFromConference(Guid conferenceId, Guid participantId)
        {
            var endpoint = TestApiUriFactory.ConferenceEndpoints.DeleteParticipant(conferenceId, participantId);
            var request = RequestBuilder.Delete(endpoint);
            NUnit.Framework.TestContext.WriteLine($"RemoveParticipantFromConference link: {endpoint} Request body {request} Conferenceid {conferenceId} participant id {participantId}");
            return SendToApi(request);
        }

        public IRestResponse GetTasks(Guid conferenceId)
        {
            var endpoint = TestApiUriFactory.ConferenceEndpoints.GetTasksByConferenceId(conferenceId);
            var request = RequestBuilder.Get(endpoint);
            NUnit.Framework.TestContext.WriteLine($"GetTasks link: {endpoint} Request body {request} conferenceid {conferenceId}");
            return SendToApi(request);
        }

        public IRestResponse DeleteConference(Guid hearingRefId, Guid conferenceId)
        {
            var endpoint = TestApiUriFactory.ConferenceEndpoints.DeleteConference(hearingRefId, conferenceId);
            var request = RequestBuilder.Delete(endpoint);
            NUnit.Framework.TestContext.WriteLine($"DeleteConference link: {endpoint} Request body {request} conferenceid {conferenceId}");
            return SendToApi(request);
        }

        public IRestResponse GetConferencesForTodayVho()
        {
            var endpoint = TestApiUriFactory.ConferenceEndpoints.GetConferencesForVho;
            var request = RequestBuilder.Get(endpoint);
            NUnit.Framework.TestContext.WriteLine($"GetConferencesForTodayVho link: {endpoint} Request body {request}");
            return SendToApi(request);
        }

        public IRestResponse GetConferencesForTodayJudge(string username)
        {
            var endpoint = TestApiUriFactory.ConferenceEndpoints.GetConferencesForJudge(username);
            var request = RequestBuilder.Get(endpoint);
            NUnit.Framework.TestContext.WriteLine($"GetConferencesForTodayJudge link: {endpoint} Request body {request} User {username}");
            return SendToApi(request);
        }

        public IRestResponse DeleteHearing(Guid hearingId)
        {
            var endpoint = TestApiUriFactory.HearingEndpoints.DeleteHearing(hearingId);
            var request = RequestBuilder.Delete(endpoint);
            NUnit.Framework.TestContext.WriteLine($"DeleteHearing link: {endpoint} Request body {request} hearing id {hearingId}");
            return SendToApi(request);
        }

        public IRestResponse GetHearingsByUsername(string username)
        {
            var endpoint = TestApiUriFactory.HearingEndpoints.GetHearingsByUsername(username);
            var request = RequestBuilder.Get(endpoint);
            NUnit.Framework.TestContext.WriteLine($"GetHearingsByUsername link: {endpoint} Request body {request} Username {username}");
            return SendToApi(request);
        }

        public IRestResponse PollForHearingByUsername(string username, string caseName, int timeout = DEFAULT_TIMEOUT)
        {
            for (var i = 0; i < timeout; i++)
            {
                var rawResponse = GetHearingsByUsername(username);
                if (!rawResponse.IsSuccessful) continue;
                if (rawResponse.Content.ToLower().Contains(caseName.ToLower()))
                {
                    NUnit.Framework.TestContext.WriteLine($"PollForHearingByUsername link: user {username} case name  {caseName} content {rawResponse.Content}");
                    return rawResponse;
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            throw new DataException($"Hearing with case name '{caseName}' not found after {timeout} seconds.");
        }

        public bool PollForParticipantNameUpdated(string username, string updatedDisplayName, int timeout = DEFAULT_TIMEOUT)
        {
            for (var i = 0; i < timeout; i++)
            {
                var rawResponse = GetHearingsByUsername(username);
                if (!rawResponse.IsSuccessful) continue;
                if (rawResponse.Content.ToLower().Contains(updatedDisplayName.ToLower()))
                {
                    NUnit.Framework.TestContext.WriteLine($"PollForParticipantNameUpdatedlink: user name {username} updated display name  {updatedDisplayName} content {rawResponse.Content}");
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
            NUnit.Framework.TestContext.WriteLine($"GetHearing link: {endpoint} Request body {request} Hearing id {hearingId}");
            return SendToApi(request);
        }

        public IRestResponse GetConferenceByConferenceId(Guid conferenceId)
        {
            var endpoint = TestApiUriFactory.ConferenceEndpoints.GetConferenceById(conferenceId);
            var request = RequestBuilder.Get(endpoint);
            NUnit.Framework.TestContext.WriteLine($"GetConferenceByConferenceId link: {endpoint} Request body {request} conference id {conferenceId}");
            return SendToApi(request);
        }

        public IRestResponse GetConferenceByHearingId(Guid hearingRefId)
        {
            var endpoint = TestApiUriFactory.ConferenceEndpoints.GetConferenceByHearingRefId(hearingRefId);
            var request = RequestBuilder.Get(endpoint);
            NUnit.Framework.TestContext.WriteLine($"GetConferenceByHearingId link: {endpoint} Request body {request} hearing id {hearingRefId}");
            return SendToApi(request);
        }

        public IRestResponse SendEvent(object eventRequest)
        {
            var endpoint = TestApiUriFactory.ConferenceEndpoints.CreateVideoEvent;
            var request = RequestBuilder.Post(endpoint, eventRequest);
            NUnit.Framework.TestContext.WriteLine($"SendEvent {eventRequest}: {endpoint} Request body {request}");
            return SendToApi(request);
        }

        public IRestResponse GetAudioRecordingLink(Guid hearingId)
        {
            var endpoint = TestApiUriFactory.ConferenceEndpoints.GetAudioRecordingLinkByHearingId(hearingId);
            var request = RequestBuilder.Get(endpoint);
            NUnit.Framework.TestContext.WriteLine($"GetAudioRecordingLink link: {endpoint} Request body {request} Hearing id {hearingId}");
            return SendToApi(request);
        }

        public IRestResponse SetSuitabilityAnswers(Guid hearingId, Guid participantId, object suitabilityRequest)
        {
            var endpoint = TestApiUriFactory.HearingEndpoints.UpdateSuitabilityAnswers(hearingId, participantId);
            var request = RequestBuilder.Put(endpoint, suitabilityRequest);
            NUnit.Framework.TestContext.WriteLine($"SetSuitabilityAnswers link: {endpoint} Request body {request} participant {participantId} hearing id {hearingId} suitability Request {suitabilityRequest.ToString()}");
            return SendToApi(request);
        }

        public IRestResponse GetSuitabilityAnswers(string username)
        {
            var endpoint = TestApiUriFactory.HearingEndpoints.GetSuitabilityAnswers(username);
            var request = RequestBuilder.Get(endpoint);
            NUnit.Framework.TestContext.WriteLine($"GetSuitabilityAnswers link: {endpoint} Request body {request} user name {username}");
            return SendToApi(request);
        }

        public IRestResponse GetUserByUsername(string username)
        {
            var endpoint = TestApiUriFactory.UserEndpoints.GetUserByUsername(username);
            var request = RequestBuilder.Get(endpoint);
            NUnit.Framework.TestContext.WriteLine($"GetUserByUsername link: {endpoint} Request body {request} username {username}");
            return SendToApi(request);
        }

        public IRestResponse GetUserByUserPrincipalName(string username)
        {
            var endpoint = TestApiUriFactory.UserEndpoints.GetUserByUserPrincipalName(username);
            var request = RequestBuilder.Get(endpoint);
            NUnit.Framework.TestContext.WriteLine($"GetUserByUserPrincipalName link: {endpoint} Request body {request}  user name {username}");
            return SendToApi(request);
        }

        public IRestResponse GetUserExistsInAD(string username)
        {
            var endpoint = TestApiUriFactory.UserEndpoints.GetUserExistsInAd(username);
            var request = RequestBuilder.Get(endpoint);
            NUnit.Framework.TestContext.WriteLine($"GetUserExistsInAD link: {endpoint} Request body {request}  user name {username}");
            return SendToApi(request);
        }

        public IRestResponse PollForParticipantExistsInAD(string username, int timeout = DEFAULT_TIMEOUT)
        {
            var endpoint = TestApiUriFactory.UserEndpoints.GetUserExistsInAd(username);
            var request = RequestBuilder.Get(endpoint);
            const int PAUSE = 3;

            for (var i = 0; i < timeout / PAUSE; i++)
            {
                var rawResponse = SendToApi(request);
                if (rawResponse.IsSuccessful)
                {
                    NUnit.Framework.TestContext.WriteLine($"PollForParticipantExistsInAD link: {endpoint} Request body {request}  user name {username} content {rawResponse.Content}");
                    return rawResponse;
                }
                Thread.Sleep(TimeSpan.FromSeconds(PAUSE));
            }

            throw new TimeoutException($"Failed to find user with username '{username}' in AAD after {timeout} seconds");
        }

        public IRestResponse DeleteUserFromAD(string contactEmail)
        {
            var endpoint = TestApiUriFactory.UserEndpoints.DeleteUserInAd(contactEmail);
            var request = RequestBuilder.Delete(endpoint);
            NUnit.Framework.TestContext.WriteLine($"DeleteUserFromAD link: {endpoint} Request body {request} contact email {contactEmail}");
            return SendToApi(request);
        }

        public bool PollForConferenceExists(Guid hearingId, int timeout = 60)
        {
            NUnit.Framework.TestContext.WriteLine($"PollForConferenceExists link: hearing id {hearingId}");
            return PollForConferenceResponse(hearingId, timeout).StatusCode == HttpStatusCode.OK;
        }

        public IRestResponse PollForConferenceResponse(Guid hearingId, int timeout = 60)
        {
            var endpoint = VideoApiUriFactory.ConferenceEndpoints.GetConferenceByHearingRefId(hearingId);
            NUnit.Framework.TestContext.WriteLine($"PollForConferenceResponse link: {endpoint} hearing id {hearingId}");
            return new Polling().WithEndpoint(endpoint).Url(ApiUrl).Token(Token)
                .UntilStatusIs(HttpStatusCode.OK).Poll(timeout);
        }

        public bool PollForConferenceDeleted(Guid hearingId, int timeout = 60)
        {
            var endpoint = VideoApiUriFactory.ConferenceEndpoints.GetConferenceByHearingRefId(hearingId);
            NUnit.Framework.TestContext.WriteLine($"PollForConferenceDeleted link: {endpoint} hearing id {hearingId}");
            return new Polling().WithEndpoint(endpoint).Url(ApiUrl).Token(Token)
                .UntilStatusIs(HttpStatusCode.NotFound).Poll(timeout).StatusCode == HttpStatusCode.NotFound;
        }

        public IRestResponse GetPersonByUsername(string username)
        {
            var endpoint = TestApiUriFactory.UserEndpoints.GetPersonByUsername(username);
            var request = RequestBuilder.Get(endpoint);
            NUnit.Framework.TestContext.WriteLine($"GetPersonByUsername link: {endpoint} Request body {request} user name {username}");
            return SendToApi(request);
        }

        public IRestResponse RefreshJudgesCache()
        {
            var endpoint = TestApiUriFactory.UserEndpoints.RefreshJudgesCache();
            var request = RequestBuilder.Get(endpoint);
            NUnit.Framework.TestContext.WriteLine($"RefreshJudgesCache link: {endpoint} Request body {request}");
            return SendToApi(request);
        }

        public IRestResponse ResetUserPassword(object passwordRequest)
        {
            var endpoint = TestApiUriFactory.UserEndpoints.ResetUserPassword();
            var request = RequestBuilder.Patch(endpoint, passwordRequest);
            NUnit.Framework.TestContext.WriteLine($"ResetUserPassword link: {endpoint} Request body {request} password request {passwordRequest}");
            return SendToApi(request);
        }

        public IRestResponse DeleteTestData(object deleteRequest)
        {
            var endpoint = TestApiUriFactory.UtilityEndpoints.DeleteHearings;
            var request = RequestBuilder.Post(endpoint, deleteRequest);
            NUnit.Framework.TestContext.WriteLine($"DeleteTestData link: {endpoint} Request body {request} delete request {deleteRequest.ToString()}");
            return SendToApi(request);
        }
    }
}
