using AcceptanceTests.Common.Api.Clients;
using AcceptanceTests.Common.Api.Requests;
using AcceptanceTests.Common.Api.Uris;
using RestSharp;

namespace AcceptanceTests.Common.Api.Hearings
{
    public class VideoWebApiManager
    {
        private readonly string _videoWebUrl;
        private readonly string _callBackToken;

        public VideoWebApiManager(string videoWebUrl, string callBackToken)
        {
            _videoWebUrl = videoWebUrl;
            _callBackToken = callBackToken;
        }

        public IRestResponse SendCallBackEvent(object eventRequest)
        {
            var endpoint = VideoWebUriFactory.VideoWebCallbackEndpoints.Event;
            var request = RequestBuilder.Post(endpoint, eventRequest);
            var client = ApiClient.CreateClient(_videoWebUrl, _callBackToken);
            NUnit.Framework.TestContext.WriteLine($"SendCallBackEvent: eventrequest {eventRequest} endpoint {endpoint} videowebURL {_videoWebUrl} callback token {_callBackToken}");
            return RequestExecutor.SendToApi(request, client);
        }
    }
}
