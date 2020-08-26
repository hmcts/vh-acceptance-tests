using System;
using System.Collections.Generic;
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
            var endpoint = VideoApiUriFactory.ConferenceEndpoints.BookNewConference;
            var request = RequestBuilder.Post(endpoint, conferenceRequest);
            var client = ApiClient.CreateClient(_videoApiUrl, _videoApiBearerToken);
            return RequestExecutor.SendToApi(request, client);
        }

        public IRestResponse GetConferencesForTodayIndividual(string username)
        {
            var endpoint = VideoApiUriFactory.ConferenceEndpoints.GetTodaysConferencesForIndividual;
            var parameters = new Dictionary<string, string> { { "username", username } };
            var request = RequestBuilder.Get(endpoint, parameters);
            var client = ApiClient.CreateClient(_videoApiUrl, _videoApiBearerToken);
            return RequestExecutor.SendToApi(request, client);
        }

        public IRestResponse GetAudioRecordingLink(Guid hearingId)
        {
            var endpoint = VideoApiUriFactory.AudioRecordingEndpoints.GetAudioRecordingLink(hearingId);
            var request = RequestBuilder.Get(endpoint);
            var client = ApiClient.CreateClient(_videoApiUrl, _videoApiBearerToken);
            return RequestExecutor.SendToApi(request, client);
        }

        public bool PollForConferenceExists(Guid hearingId, int timeout = 60)
        {
            return PollForConferenceResponse(hearingId, timeout).StatusCode == HttpStatusCode.OK;
        }

        public bool PollForConferenceDeleted(Guid hearingId, int timeout = 60)
        {
            var endpoint = VideoApiUriFactory.ConferenceEndpoints.GetConferenceByHearingRefId(hearingId);
            return new Polling().WithEndpoint(endpoint).Url(_videoApiUrl).Token(_videoApiBearerToken)
                .UntilStatusIs(HttpStatusCode.NotFound).Poll(timeout).StatusCode == HttpStatusCode.NotFound;
        }

        public IRestResponse PollForConferenceResponse(Guid hearingId, int timeout = 60)
        {
            var endpoint = VideoApiUriFactory.ConferenceEndpoints.GetConferenceByHearingRefId(hearingId);
            return new Polling().WithEndpoint(endpoint).Url(_videoApiUrl).Token(_videoApiBearerToken)
                .UntilStatusIs(HttpStatusCode.OK).Poll(timeout);
        }

        public IRestResponse GetSelfTestScore(Guid conferenceId, Guid participantId)
        {
            var endpoint = VideoApiUriFactory.VideoApiParticipantsEndpoints.GetSelfTestScore(conferenceId, participantId);
            var request = RequestBuilder.Get(endpoint);
            var client = ApiClient.CreateClient(_videoApiUrl, _videoApiBearerToken);
            return RequestExecutor.SendToApi(request, client);
        }



        public IRestResponse SendEvent(object eventRequest)
        {
            var endpoint = VideoApiUriFactory.VideoEventsEndpoints.Event;
            var request = RequestBuilder.Post(endpoint, eventRequest);
            var client = ApiClient.CreateClient(_videoApiUrl, _videoApiBearerToken);
            return RequestExecutor.SendToApi(request, client);
        }
    }
}
