using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AcceptanceTests.Common.Api.Helpers;
using AcceptanceTests.TestAPI.Contract.Requests;
using AcceptanceTests.TestAPI.Contract.Responses;
using AcceptanceTests.TestAPI.Domain.Enums;
using AcceptanceTests.TestAPI.IntegrationTests.Configuration;
using AcceptanceTests.TestAPI.IntegrationTests.Helpers;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace AcceptanceTests.TestAPI.IntegrationTests.Steps
{
    [Binding]
    public class AllocationSteps : BaseSteps
    {
        private readonly TestContext _context;
        private readonly CommonSteps _commonSteps;

        public AllocationSteps(TestContext context, CommonSteps commonSteps)
        {
            _context = context;
            _commonSteps = commonSteps;
        }

        [Given(@"I have a Allocate user by user type (.*) and application request")]
        public void GivenIHaveAAllocateUserByUserTypeAndApplicationRequest(UserType userType)
        {
            _context.Uri = ApiUriFactory.AllocationEndpoints.AllocateByUserTypeAndApplication(userType, Application.TestApi);
            _context.HttpMethod = HttpMethod.Put;
        }

        [Given(@"I have a get allocation by user id request with a valid user id")]
        public void GivenIHaveAGetAllocationByUserIdRequestWithAValidUserId()
        {
            _context.Uri = ApiUriFactory.AllocationEndpoints.GetAllocationByUserId(_context.Test.User.Id);
            _context.HttpMethod = HttpMethod.Get;
        }

        [Given(@"I have a get allocation by user id request with a nonexistent user id")]
        public void GivenIHaveAGetAllocationByUserIdRequestWithANonexistentUserId()
        {
            _context.Uri = ApiUriFactory.AllocationEndpoints.GetAllocationByUserId(Guid.NewGuid());
            _context.HttpMethod = HttpMethod.Get;
        }

        [Given(@"I have a valid create allocation request for a valid user")]
        public void GivenIHaveAValidCreateAllocationRequestForAValidUser()
        {
            _context.Uri = ApiUriFactory.AllocationEndpoints.CreateAllocation(_context.Test.User.Id);
            _context.HttpMethod = HttpMethod.Post;
        }

        [Given(@"I have a valid create allocation request for a nonexistent user")]
        public void GivenIHaveAValidCreateAllocationRequestForANonexistentUser()
        {
            _context.Uri = ApiUriFactory.AllocationEndpoints.CreateAllocation(Guid.NewGuid());
            _context.HttpMethod = HttpMethod.Post;
        }

        [Given(@"I have a delete allocation request with a valid user id")]
        public void GivenIHaveADeleteAllocationRequestWithAValidUserId()
        {
            _context.Uri = ApiUriFactory.AllocationEndpoints.DeleteAllocation(_context.Test.User.Id);
            _context.HttpMethod = HttpMethod.Delete;
        }

        [Given(@"I have a delete allocation request with a nonexistent user id")]
        public void GivenIHaveADeleteAllocationRequestWithANonexistentUserId()
        {
            _context.Uri = ApiUriFactory.AllocationEndpoints.DeleteAllocation(Guid.NewGuid());
            _context.HttpMethod = HttpMethod.Delete;
        }

        [Given(@"I have an allocate by user id request for a valid user")]
        public void GivenIHaveAnAllocateByUserIdRequestForAValidUser()
        {
            _context.Uri = ApiUriFactory.AllocationEndpoints.AllocateByUserId(_context.Test.User.Id);
            _context.HttpMethod = HttpMethod.Put;
        }

        [Given(@"I have an allocate by user id request for a nonexistent user")]
        public void GivenIHaveAnAllocateByUserIdRequestForANonexistentUser()
        {
            _context.Uri = ApiUriFactory.AllocationEndpoints.AllocateByUserId(Guid.NewGuid());
            _context.HttpMethod = HttpMethod.Put;
        }

        [Given(@"I have a valid unallocate users by username request")]
        public void GivenIHaveAValidUnallocateUsersByUsernameRequest()
        {
            var usernames = _context.Test.Users.Select(user => user.Username).ToList();

            var request = new UnallocateUsersRequest()
            {
                Usernames = usernames
            };

            _context.Uri = ApiUriFactory.AllocationEndpoints.UnallocateUsers;
            _context.HttpMethod = HttpMethod.Put;

            var jsonBody = RequestHelper.SerialiseRequestToSnakeCaseJson(request);
            _context.HttpContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");
        }

        [Given(@"I have a valid unallocate users by username request for a nonexistent user")]
        public void GivenIHaveAValidUnallocateUsersByUsernameRequestForANonexistentUser()
        {
            var usernames = new List<string>(){"MadeUpUsername@email.com"};

            var request = new UnallocateUsersRequest()
            {
                Usernames = usernames
            };

            _context.Uri = ApiUriFactory.AllocationEndpoints.UnallocateUsers;
            _context.HttpMethod = HttpMethod.Put;

            var jsonBody = RequestHelper.SerialiseRequestToSnakeCaseJson(request);
            _context.HttpContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");
        }

        [Then(@"the user should be allocated")]
        public async Task ThenTheUserShouldBeAllocated()
        {
            GivenIHaveAGetAllocationByUserIdRequestWithAValidUserId();
            await _commonSteps.WhenISendTheRequestToTheEndpoint();
            _commonSteps.ThenTheResponseShouldHaveStatus(HttpStatusCode.OK, true);
            var response = await Response.GetResponses<AllocationDetailsResponse>(_context.Response.Content);
            response.Should().NotBeNull();
            response.Allocated.Should().BeTrue();
            response.ExpiresAt.Should().BeAfter(DateTime.UtcNow.AddMinutes(9));
            response.ExpiresAt.Should().BeBefore(DateTime.UtcNow.AddMinutes(10));
        }

        [Then(@"the response contains the allocation details")]
        public async Task ThenTheResponseContainsTheNewAllocationDetails()
        {
            var response = await Response.GetResponses<AllocationDetailsResponse>(_context.Response.Content);
            response.Should().NotBeNull();
            response.Id.Should().NotBeEmpty();
            response.Allocated.Should().BeFalse();
            response.ExpiresAt.Should().BeNull();
            response.UserId.Should().Be(_context.Test.User.Id);
            response.Username.Should().Contain(_context.Test.User.Username);
        }

        [Then(@"the response contains the allocation details for the (.*)")]
        public async Task ThenTheResponseContainsTheNewAllocationDetailsForTheUserType(UserType userType)
        {
            var response = await Response.GetResponses<AllocationDetailsResponse>(_context.Response.Content);
            response.Should().NotBeNull();
            response.Id.Should().NotBeEmpty();
            response.Allocated.Should().BeFalse();
            response.ExpiresAt.Should().BeNull();
            response.UserId.Should().NotBeEmpty();
            response.Username.Should().Contain(userType.ToString());
        }

        [Then(@"a list of user allocation details should be retrieved for the (.*) users")]
        public async Task ThenAListOfUserAllocationDetailsShouldBeRetrieved(string allocatedText)
        {
            var allocated = allocatedText.ToLower().Equals("allocated");

            var response = await Response.GetResponses<List<AllocationDetailsResponse>>(_context.Response.Content);
            response.Count.Should().BeGreaterThan(0);

            foreach (var allocationDetails in response)
            {
                _context.Test.Allocations.Any(x => x.Id == allocationDetails.Id).Should().BeTrue();
                _context.Test.Users.Any(x => x.Id == allocationDetails.UserId).Should().BeTrue();
                _context.Test.Users.Any(x => x.Username == allocationDetails.Username).Should().BeTrue();
                
                allocationDetails.Allocated.Should().Be(allocated);

                if (allocated)
                {
                    allocationDetails.ExpiresAt.Should().NotBeNull();
                }
                else
                {
                    allocationDetails.ExpiresAt.Should().BeNull();
                }
            }
        }
    }
}
