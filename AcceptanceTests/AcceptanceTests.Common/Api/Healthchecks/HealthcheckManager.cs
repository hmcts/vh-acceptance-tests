using System.Net;
using AdminWebsite.Common.Api.Clients;
using AdminWebsite.Common.Api.Requests;
using AdminWebsite.Common.Api.Uris;
using FluentAssertions;

namespace AdminWebsite.Common.Api.Healthchecks
{
    public class HealthcheckManager
    {
        private readonly string _apiUrl;
        private readonly string _bearerToken;
        public HealthcheckManager(string apiUrl, string bearerToken)
        {
            _apiUrl = apiUrl;
            _bearerToken = bearerToken;
        }

        public void CheckHealthOfBookingsApi()
        {
            var endpoint = new BookingsApiUriFactory().HealthCheckEndpoints.HealthCheck;
            Send(endpoint, "Bookings Api");
        }

        public void CheckHealthOfUserApi()
        {
            var endpoint = new UserApiUriFactory().HealthCheckEndpoints.CheckServiceHealth();
            Send(endpoint, "User Api");
        }

        public void CheckHealthOfVideoApi()
        {
            var endpoint = new VideoApiUriFactory().HealthCheckEndpoints.CheckServiceHealth();
            Send(endpoint, "Video Api");
        }

        public void Send(string endpoint, string apiName)
        {
            var request = new RequestBuilder().Get(endpoint);
            var client = new ApiClient(_apiUrl, _bearerToken).GetClient();
            var response = new RequestExecutor(request).SendToApi(client);
            response.StatusCode.Should().Be(HttpStatusCode.OK, $"the {apiName} is available");
        }
    }
}
