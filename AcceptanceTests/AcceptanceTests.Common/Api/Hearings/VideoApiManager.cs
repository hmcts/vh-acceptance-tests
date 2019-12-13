using System;
using System.Net;
using System.Threading;
using AdminWebsite.Common.Api.Clients;
using AdminWebsite.Common.Api.Requests;
using AdminWebsite.Common.Api.Uris;
using RestSharp;

namespace AdminWebsite.Common.Api.Hearings
{
    public class VideoApiManager
    {
        private const int _timeout = 60;
        private readonly string _videoApiUrl;
        private readonly string _videoApiBearerToken;

        public VideoApiManager(string videoApiUrl, string videoApiBearerToken)
        {
            _videoApiUrl = videoApiUrl;
            _videoApiBearerToken = videoApiBearerToken;
        }

        public bool PollForConference(Guid? hearingId)
        {
            if (hearingId == null)
            {
                throw new DataMisalignedException("Hearing Id cannot be null");
            }
            for (var i = 0; i < _timeout; i++)
            {
                var conference = GetConferenceByHearingId((Guid)hearingId);
                if (conference.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

            throw new DataMisalignedException("Conference was not created.");
        }

        public IRestResponse GetConferenceByHearingId(Guid hearingId)
        {
            var endpoint = new VideoApiUriFactory().ConferenceEndpoints.GetConferenceByHearingRefId(hearingId);
            var request = new RequestBuilder().Get(endpoint);
            var client = new ApiClient(_videoApiUrl, _videoApiBearerToken).GetClient();
            return new RequestExecutor(request).SendToApi(client);
        }
    }
}
