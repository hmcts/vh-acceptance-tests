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
            var endpoint = new VideoWebUriFactory().CallbackEndpoints.Event;
            var request = new RequestBuilder().Post(endpoint, eventRequest);
            var client = new ApiClient(_videoWebUrl, _callBackToken).GetClient();
            return new RequestExecutor(request).SendToApi(client);
        }
    }
}
