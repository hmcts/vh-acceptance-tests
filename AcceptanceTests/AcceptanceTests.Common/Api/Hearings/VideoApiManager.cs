using System;
using System.Net;
using System.Threading;
using AcceptanceTests.Common.Api.Clients;
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

        public bool PollForConference(Guid? hearingId, int timeout = 60)
        {
            if (hearingId == null)
            {
                throw new DataMisalignedException("Hearing Id cannot be null");
            }
            for (var i = 0; i < timeout; i++)
            {
                var conference = GetConferenceByHearingId((Guid)hearingId);
                if (conference.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

            return false;
        }

        public IRestResponse GetConferenceByHearingId(Guid hearingId)
        {
            var endpoint = new VideoApiUriFactory().ConferenceEndpoints.GetConferenceByHearingRefId(hearingId);
            var request = new RequestBuilder().Get(endpoint);
            var client = new ApiClient(_videoApiUrl, _videoApiBearerToken).GetClient();
            return new RequestExecutor(request).SendToApi(client);
        }

        public IRestResponse GetSelfTestScore(Guid conferenceId, Guid participantId)
        {
            var endpoint = new VideoApiUriFactory().ParticipantsEndpoints.GetSelfTestScore(conferenceId, participantId);
            var request = new RequestBuilder().Get(endpoint);
            var client = new ApiClient(_videoApiUrl, _videoApiBearerToken).GetClient();
            return new RequestExecutor(request).SendToApi(client);
        }
    }
}
