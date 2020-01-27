using System;
using System.Net;
using AcceptanceTests.Common.Api.Clients;
using AcceptanceTests.Common.Api.Helpers;
using AcceptanceTests.Common.Api.Requests;
using AcceptanceTests.Common.Api.Uris;
using RestSharp;

namespace AcceptanceTests.Common.Api.Hearings
{
    public class VideoApiManager
    {
        private readonly string _videoApiUrl;
        private readonly string _videoApiBearerToken;

        public VideoApiManager(string videoApiUrl, string videoApiBearerToken)
        {
            _videoApiUrl = videoApiUrl;
            _videoApiBearerToken = videoApiBearerToken;
        }

        public IRestResponse CreateConference(object conferenceRequest)
        {
            var endpoint = new VideoApiUriFactory().ConferenceEndpoints.BookNewConference;
            var request = new RequestBuilder().Post(endpoint, conferenceRequest);
            var client = new ApiClient(_videoApiUrl, _videoApiBearerToken).GetClient();
            var response = new RequestExecutor(request).SendToApi(client);
            return response;
        }

        public IRestResponse GetConferenceByHearingId(Guid hearingId)
        {
            var endpoint = new VideoApiUriFactory().ConferenceEndpoints.GetConferenceByHearingRefId(hearingId);
            var request = new RequestBuilder().Get(endpoint);
            var client = new ApiClient(_videoApiUrl, _videoApiBearerToken).GetClient();
            return new RequestExecutor(request).SendToApi(client);
        }

        public bool PollForConferenceExists(Guid hearingId, int timeout = 60)
        {
            return PollForConferenceResponse(hearingId, timeout).StatusCode == HttpStatusCode.OK;
        }

        public bool PollForConferenceDeleted(Guid hearingId, int timeout = 60)
        {
            var endpoint = new VideoApiUriFactory().ConferenceEndpoints.GetConferenceByHearingRefId(hearingId);
            return new Polling().WithEndpoint(endpoint).Url(_videoApiUrl).Token(_videoApiBearerToken)
                .UntilStatusIs(HttpStatusCode.NotFound).Poll(timeout).StatusCode == HttpStatusCode.NotFound;
        }

        public IRestResponse PollForConferenceResponse(Guid hearingId, int timeout = 60)
        {
            var endpoint = new VideoApiUriFactory().ConferenceEndpoints.GetConferenceByHearingRefId(hearingId);
            return new Polling().WithEndpoint(endpoint).Url(_videoApiUrl).Token(_videoApiBearerToken)
                .UntilStatusIs(HttpStatusCode.OK).Poll(timeout);
        }

        public IRestResponse GetSelfTestScore(Guid conferenceId, Guid participantId)
        {
            var endpoint = new VideoApiUriFactory().ParticipantsEndpoints.GetSelfTestScore(conferenceId, participantId);
            var request = new RequestBuilder().Get(endpoint);
            var client = new ApiClient(_videoApiUrl, _videoApiBearerToken).GetClient();
            return new RequestExecutor(request).SendToApi(client);
        }

        public bool PollForSelfTestScoreExists(Guid conferenceId, Guid participantId, int timeout = 30)
        {
            return PollForSelfTestScoreResponse(conferenceId, participantId, timeout).StatusCode == HttpStatusCode.OK;
        }

        public IRestResponse PollForSelfTestScoreResponse(Guid conferenceId, Guid participantId, int timeout = 30)
        {
            var endpoint = new VideoApiUriFactory().ParticipantsEndpoints.GetSelfTestScore(conferenceId, participantId);
            return new Polling().WithEndpoint(endpoint).Url(_videoApiUrl).Token(_videoApiBearerToken)
                .UntilStatusIs(HttpStatusCode.OK).Poll(timeout);
        }

        public IRestResponse RemoveParticipantFromConference(Guid conferenceId, Guid participantId)
        {
            var endpoint = new VideoApiUriFactory().ParticipantsEndpoints.RemoveParticipantFromConference(conferenceId, participantId);
            var request = new RequestBuilder().Delete(endpoint);
            var client = new ApiClient(_videoApiUrl, _videoApiBearerToken).GetClient();
            return new RequestExecutor(request).SendToApi(client);
        }
    }
}
