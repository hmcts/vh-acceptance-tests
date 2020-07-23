using System.Net.Http;
using System.Threading.Tasks;
using AcceptanceTests.Common.Api.Helpers;
using AcceptanceTests.TestAPI.Contract.Responses;
using AcceptanceTests.TestAPI.IntegrationTests.Configuration;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace AcceptanceTests.TestAPI.IntegrationTests.Steps
{
    [Binding]
    public sealed class HealthCheckSteps : BaseSteps
    {
        private readonly TestContext _context;

        public HealthCheckSteps(TestContext context)
        {
            _context = context;
        }

        [Given(@"I have a get health request")]
        public void GivenIMakeACallToTheHealthCheckEndpoint()
        {
            _context.Uri = ApiUriFactory.HealthCheckEndpoints.CheckServiceHealth;
            _context.HttpMethod = HttpMethod.Get;
        }

        [Then(@"the application version should be retrieved")]
        public async Task ThenTheApplicationVersionShouldBeRetrieved()
        {
            var json = await _context.Response.Content.ReadAsStringAsync();
            var response = RequestHelper.DeserialiseSnakeCaseJsonToResponse<HealthCheckResponse>(json);
            response.Version.Version.Should().NotBeNull();
            response.ErrorMessage.Should().BeNullOrWhiteSpace();
            response.Successful.Should().BeTrue();
        }
    }
}
