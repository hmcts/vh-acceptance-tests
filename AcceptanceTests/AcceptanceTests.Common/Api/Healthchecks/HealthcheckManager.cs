using System.Net;
using AcceptanceTests.Common.Api.Clients;
using AcceptanceTests.Common.Api.Requests;
using AcceptanceTests.Common.Api.Uris;
using FluentAssertions;

namespace AcceptanceTests.Common.Api.Healthchecks
{
    public static class HealthcheckManager
    {
        public static void CheckHealthOfBookingsApi(string apiUrl, string bearerToken, WebProxy webProxy = null)
        {
            var endpoint = BookingsApiUriFactory.BookingsApiHealthCheckEndpoints.HealthCheck;
            Send(endpoint, "Bookings Api", apiUrl, bearerToken, webProxy);
        }

        public static void CheckHealthOfUserApi(string apiUrl, string bearerToken, WebProxy webProxy = null)
        {
            var endpoint = UserApiUriFactory.HealthCheckEndpoints.CheckServiceHealth;
            Send(endpoint, "User Api", apiUrl, bearerToken, webProxy);
        }

        public static void CheckHealthOfVideoApi(string apiUrl, string bearerToken, WebProxy webProxy = null)
        {
            var endpoint = VideoApiUriFactory.VideoApiHealthCheckEndpoints.CheckServiceHealth;
            Send(endpoint, "Video Api", apiUrl, bearerToken, webProxy);
        }

        public static void Send(string endpoint, string apiName, string apiUrl, string bearerToken, WebProxy webProxy = null)
        {
            var request = RequestBuilder.Get(endpoint);
            var client = ApiClient.SetClient(apiUrl, bearerToken, webProxy);
            var response = RequestExecutor.SendToApi(request, client);
            response.StatusCode.Should().Be(HttpStatusCode.OK, $"the {apiName} is available, but healthcheck failed with '{response.StatusCode}' and error message '{response.ErrorMessage}'");
        }
    }
}
