using RestSharp;

namespace AcceptanceTests.Common.Api.Requests
{
    public class RequestExecutor
    {
        private readonly IRestRequest _request;

        public RequestExecutor(IRestRequest request)
        {
            _request = request;
        }

        public IRestResponse SendToApi(RestClient apiClient)
        {
            return apiClient.Execute(_request);
        }
    }
}
