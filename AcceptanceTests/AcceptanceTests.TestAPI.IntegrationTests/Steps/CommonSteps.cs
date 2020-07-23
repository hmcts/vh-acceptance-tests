using System;
using System.Net;
using System.Threading.Tasks;
using AcceptanceTests.TestAPI.Domain.Enums;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace AcceptanceTests.TestAPI.IntegrationTests.Steps
{
    [Binding]
    public class CommonSteps : BaseSteps
    {
        private readonly TestContext _context;

        public CommonSteps(TestContext c)
        {
            _context = c;
        }

        [Given(@"I have a user")]
        public async Task GivenIHaveAUser()
        {
            _context.Test.User = await _context.TestDataManager.SeedUser();
        }

        [Given(@"I have a user with user type (.*)")]
        [Given(@"I have another user with user type (.*)")]
        public async Task GivenIHaveAUserWithUserTypeJudge(UserType userType)
        {
            _context.Test.Users.Add(await _context.TestDataManager.SeedUser(userType));
        }

        [When(@"I send the request to the endpoint")]
        [When(@"I send the same request twice")]
        public async Task WhenISendTheRequestToTheEndpoint()
        {
            _context.Response = _context.HttpMethod.Method switch
            {
                "GET" => await SendGetRequestAsync(_context),
                "POST" => await SendPostRequestAsync(_context),
                "PATCH" => await SendPatchRequestAsync(_context),
                "PUT" => await SendPutRequestAsync(_context),
                "DELETE" => await SendDeleteRequestAsync(_context),
                _ => throw new ArgumentOutOfRangeException(_context.HttpMethod.ToString(),
                    _context.HttpMethod.ToString(), null)
            };
        }

        [Then(@"the response should have the status (.*) and success status (.*)")]
        public void ThenTheResponseShouldHaveStatus(HttpStatusCode statusCode, bool isSuccess)
        {
            _context.Response.StatusCode.Should().Be(statusCode);
            _context.Response.IsSuccessStatusCode.Should().Be(isSuccess);
            NUnit.Framework.TestContext.WriteLine($"Status Code: {_context.Response.StatusCode}");
        }
    }
}
