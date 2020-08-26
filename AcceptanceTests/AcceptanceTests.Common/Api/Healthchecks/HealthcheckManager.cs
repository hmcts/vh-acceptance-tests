using System.Net;
using AcceptanceTests.Common.Api.Clients;
using AcceptanceTests.Common.Api.Requests;
using AcceptanceTests.Common.Api.Uris;
using FluentAssertions;

namespace AcceptanceTests.Common.Api.Healthchecks
{
    public static class HealthcheckManager
    {
        public static void CheckHealthOfTestApi(string apiUrl, string bearerToken, WebProxy webProxy = null)
        {
            var endpoint = BookingsApiUriFactory.BookingsApiHealthCheckEndpoints.HealthCheck;
            Send(endpoint, "Bookings Api", apiUrl, bearerToken, webProxy);
        }

        public static void Send(string endpoint, string apiName, string apiUrl, string bearerToken, WebProxy webProxy = null)
        {
            var request = RequestBuilder.Get(endpoint);
            var client = ApiClient.CreateClient(apiUrl, bearerToken, webProxy);
            var response = RequestExecutor.SendToApi(request, client);
            response.StatusCode.Should().Be(HttpStatusCode.OK, $"the {apiName} is available, but healthcheck failed with '{response.StatusCode}' and error message '{response.ErrorMessage}'");
        }
    }
}
