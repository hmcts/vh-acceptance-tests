using System;
using System.Linq;
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
            _context.Test.User = await _context.TestDataManager.SeedUser(userType);
            _context.Test.Users.Add(_context.Test.User);
        }

        [Given(@"I have a user with an allocation")]
        public async Task GivenIHaveAUserWithAnAllocation()
        {
            await GivenIHaveAUser();
            _context.Test.Allocation = await _context.TestDataManager.SeedAllocation(_context.Test.User.Id);
        }

        [Given(@"I have a user with an allocation who is allocated")]
        [Given(@"I have another user with an allocation who is allocated")]
        public async Task GivenIHaveAUserWithAnAllocationWhoIsAllocated()
        {
            await GivenIHaveAUserWithAnAllocation();
            _context.Test.Users.Add(_context.Test.User);
            _context.Test.Allocations.Add(await _context.TestDataManager.AllocateUser(_context.Test.Users.Last().Id));
        }

        [Given(@"I have a user with an allocation who is unallocated")]
        [Given(@"I have another user with an allocation who is unallocated")]
        public async Task GivenIHaveAUserWithAnAllocationWhoIsUnallocated()
        {
            await GivenIHaveAUserWithAnAllocation();
            _context.Test.Users.Add(_context.Test.User);
            _context.Test.Allocations.Add(await _context.TestDataManager.UnallocateUser(_context.Test.Users.Last().Id));
        }

        [Given(@"I have a (.*) user with an allocation who is allocated")]
        public async Task GivenIHaveAUserOfTypeWithAnAllocationWhoIsAllocated(UserType userType)
        {
            await GivenIHaveAUserWithUserTypeJudge(userType);
            _context.Test.Users.Add(_context.Test.User);
            await _context.TestDataManager.SeedAllocation(_context.Test.User.Id);
            _context.Test.Allocations.Add(_context.Test.Allocation);
            await _context.TestDataManager.AllocateUser(_context.Test.Users.Last().Id);
        }

        [Given(@"I have a (.*) user with an allocation who is unallocated")]
        public async Task GivenIHaveAUserOfTypeWithAnAllocationWhoIsUnallocated(UserType userType)
        {
            await GivenIHaveAUserWithUserTypeJudge(userType);
            _context.Test.Users.Add(_context.Test.User);
            await _context.TestDataManager.SeedAllocation(_context.Test.User.Id);
            _context.Test.Allocations.Add(_context.Test.Allocation);
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
