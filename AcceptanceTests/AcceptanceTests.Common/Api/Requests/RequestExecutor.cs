using RestSharp;

namespace AcceptanceTests.Common.Api.Requests
{
    public static class RequestExecutor
    {
        public static IRestResponse SendToApi(IRestRequest request, RestClient apiClient)
        {
            return apiClient.Execute(request);
        }
    }
}
