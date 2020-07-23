using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AcceptanceTests.Common.Api.Helpers;
using AcceptanceTests.TestAPI.Common.Builders;
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
    public class UserSteps : BaseSteps
    {
        private readonly TestContext _context;
        private readonly CommonSteps _commonSteps;
        private CreateUserRequest _createUserRequest;

        public UserSteps(TestContext context, CommonSteps commonSteps)
        {
            _context = context;
            _commonSteps = commonSteps;
        }

        [Given(@"I have a valid get user details by id request")]
        public void GivenIHaveAValidGetUserDetailsByIdRequest()
        {
            _context.Uri = ApiUriFactory.UserEndpoints.GetUserById(_context.Test.User.Id);
            _context.HttpMethod = HttpMethod.Get;
        }

        [Given(@"I have a get user details by id request with a nonexistent user id")]
        public void GivenIHaveAGetUserDetailsByIdRequestWithANonexistentUserId()
        {
            _context.Uri = ApiUriFactory.UserEndpoints.GetUserById(Guid.NewGuid());
            _context.HttpMethod = HttpMethod.Get;
        }

        [Given(@"I have a valid create user request for a (.*)")]
        public async Task GivenIHaveAValidCreateUserRequest(UserType userType)
        {
            const Application application = Application.TestApi;
            var number = await _context.TestDataManager.IterateUserNumber(userType, application);
            
            _createUserRequest = new UserBuilder(_context.Config.UsernameStem, number)
                .WithUserType(userType)
                .ForApplication(application)
                .BuildRequest();

            _context.Uri = ApiUriFactory.UserEndpoints.CreateUser;
            _context.HttpMethod = HttpMethod.Post;

            var jsonBody = RequestHelper.SerialiseRequestToSnakeCaseJson(_createUserRequest);
            
            _context.HttpContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");
        }

        [Given(@"I have a valid get all users by user type (.*) and application request")]
        public void GivenIHaveAValidGetAllUsersByUserTypeAndApplicationRequest(UserType userType)
        {
            _context.Uri = ApiUriFactory.UserEndpoints.GetAllUsersByUserTypeAndApplication(userType, Application.TestApi);
            _context.HttpMethod = HttpMethod.Get;
        }

        [Given(@"I have a valid get user number iterated request")]
        public void GivenIHaveAValidGetUserNumberIteratedRequest()
        {
            _context.Uri = ApiUriFactory.UserEndpoints.GetIteratedUserNumber(UserType.None, Application.None);
            _context.HttpMethod = HttpMethod.Get;
        }

        [Given(@"I have a valid delete user by user id request")]
        public void GivenIHaveAValidDeleteUserByUserIdRequest()
        {
            _context.Uri = ApiUriFactory.UserEndpoints.DeleteUser(_context.Test.User.Id);
            _context.HttpMethod = HttpMethod.Delete;
        }

        [Given(@"I have a valid delete user by user id request for a nonexistent user")]
        public void GivenIHaveAValidDeleteUserByUserIdRequestForNonexistentUser()
        {
            _context.Uri = ApiUriFactory.UserEndpoints.DeleteUser(Guid.NewGuid());
            _context.HttpMethod = HttpMethod.Delete;
        }

        [When(@"I send the create user request twice")]
        public async Task WhenISendTheCreateUserRequestTwice()
        {
            await SendCreateRequestTwice();
            await GivenIHaveAValidCreateUserRequest(UserType.Judge);
            await SendCreateRequestTwice();
        }

        private async Task SendCreateRequestTwice()
        {
            await _commonSteps.WhenISendTheRequestToTheEndpoint();
            _context.Response.StatusCode.Should().Be(HttpStatusCode.Created);
            var response = await Response.GetResponses<UserDetailsResponse>(_context.Response.Content);
            _context.Test.UserResponses.Add(response);
        }

        [Then(@"the user numbers should be incremented")]
        public void ThenTheNumbersShouldBeIncremented()
        {
            var firstUserNumber = _context.Test.UserResponses.First().Number;
            var secondUserNumber = _context.Test.UserResponses.Last().Number;
            firstUserNumber.Should().Be(secondUserNumber - 1);
        }


        [Then(@"the response contains the new user details")]
        public async Task ThenTheResponseContainsTheNewUserDetails()
        {
            var response = await Response.GetResponses<UserDetailsResponse>(_context.Response.Content);
            response.Should().NotBeNull();
            response.Application.Should().Be(_createUserRequest.Application);
            response.ContactEmail.Should().Be(_createUserRequest.ContactEmail);
            response.CreatedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(10));
            response.DisplayName.Should().Be(_createUserRequest.DisplayName);
            response.FirstName.Should().Be(_createUserRequest.FirstName);
            response.Id.Should().NotBeEmpty();
            response.LastName.Should().Be(_createUserRequest.LastName);
            response.Number.Should().Be(_createUserRequest.Number);
            response.UserType.Should().Be(_createUserRequest.UserType);
            response.Username.Should().Be(_createUserRequest.Username);
        }

        [Then(@"the user details should be retrieved")]
        public async Task ThenTheUserDetailsShouldBeRetrieved()
        {
            var response = await Response.GetResponses<UserDetailsResponse>(_context.Response.Content);
            response.Should().NotBeNull();
            response.Application.Should().Be(_context.Test.User.Application);
            response.ContactEmail.Should().Be(_context.Test.User.ContactEmail);
            response.CreatedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(10));
            response.DisplayName.Should().Be(_context.Test.User.DisplayName);
            response.FirstName.Should().Be(_context.Test.User.FirstName);
            response.Id.Should().NotBeEmpty();
            response.LastName.Should().Be(_context.Test.User.LastName);
            response.Number.Should().Be(_context.Test.User.Number);
            response.UserType.Should().Be(_context.Test.User.UserType);
            response.Username.Should().Be(_context.Test.User.Username);
        }

        [Then(@"a list of user details for the given user type and application should be retrieved")]
        public async Task ThenAListOfUserDetailsShouldBeRetrieved()
        {
            var users = await Response.GetResponses<List<UserDetailsResponse>>(_context.Response.Content);
            users.Count.Should().BeGreaterOrEqualTo(2);
            users.All(x => x.UserType == UserType.Judge && x.Application == Application.TestApi).Should().BeTrue();
            users.Any(x => string.Equals(x.Username, _context.Test.Users.First().Username, StringComparison.CurrentCultureIgnoreCase)).Should().BeTrue();
            users.Any(x => string.Equals(x.Username, _context.Test.Users[1].Username, StringComparison.CurrentCultureIgnoreCase)).Should().BeTrue();
            users.Any(x => string.Equals(x.Username, _context.Test.Users.Last().Username, StringComparison.CurrentCultureIgnoreCase)).Should().BeFalse();
        }

        [Then(@"the iterated number should be retrieved")]
        public async Task ThenTheIteratedNumberShouldBeRetrieved()
        {
            var response = await Response.GetResponses<IteratedUserNumberResponse>(_context.Response.Content);
            response.Should().NotBeNull();
            response.Number.Should().BeGreaterThan(0);
        }
    }
}
