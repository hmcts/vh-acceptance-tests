using System;
using System.Net;
using System.Threading;
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
            var endpoint = new VideoApiUriFactory().ConferenceEndpoints.GetConferenceByHearingRefId(hearingId);
            return new Polling().WithEndpoint(endpoint).Url(_videoApiUrl).Token(_videoApiBearerToken)
                .UntilStatusIs(HttpStatusCode.OK).PollForExists(timeout);
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
            var endpoint = new VideoApiUriFactory().ParticipantsEndpoints.GetSelfTestScore(conferenceId, participantId);
            return new Polling().WithEndpoint(endpoint).Url(_videoApiUrl).Token(_videoApiBearerToken)
                .UntilStatusIs(HttpStatusCode.OK).PollForExists(timeout);
        }
    }
}
