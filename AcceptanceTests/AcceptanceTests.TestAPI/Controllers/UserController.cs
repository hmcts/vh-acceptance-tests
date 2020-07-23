using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AcceptanceTests.TestAPI.Contract.Requests;
using AcceptanceTests.TestAPI.Contract.Responses;
using AcceptanceTests.TestAPI.DAL.Commands;
using AcceptanceTests.TestAPI.DAL.Commands.Core;
using AcceptanceTests.TestAPI.DAL.Exceptions;
using AcceptanceTests.TestAPI.DAL.Helpers;
using AcceptanceTests.TestAPI.DAL.Queries;
using AcceptanceTests.TestAPI.DAL.Queries.Core;
using AcceptanceTests.TestAPI.Domain;
using AcceptanceTests.TestAPI.Domain.Enums;
using AcceptanceTests.TestAPI.Mappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AcceptanceTests.TestAPI.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IQueryHandler _queryHandler;
        private readonly ICommandHandler _commandHandler;
        private readonly ILogger<UserController> _logger;

        public UserController(ICommandHandler commandHandler, IQueryHandler queryHandler, ILogger<UserController> logger)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
            _logger = logger;
        }

        /// <summary>
        /// Get the details of a user
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>Full details of a user</returns>
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(UserDetailsResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUserDetailsByIdAsync(Guid userId)
        {
            _logger.LogDebug($"GetUserDetailsByIdAsync {userId}");

            var getUserByIdQuery = new GetUserByIdQuery(userId);
            var queriedUser = await _queryHandler.Handle<GetUserByIdQuery, User>(getUserByIdQuery);

            if (queriedUser == null)
            {
                _logger.LogWarning($"Unable to find user with id {userId}");

                return NotFound();
            }

            var response = UserToDetailsResponseMapper.MapToResponse(queriedUser);
            return Ok(response);
        }

        /// <summary>
        /// Get all users details by user type and application
        /// </summary>
        /// <param name="userType">Type of user (e.g Judge)</param>
        /// <param name="application">Application (e.g. VideoWeb)</param>
        /// <returns>List of all users details for a specified application and user type</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<UserDetailsResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllUsersByUserTypeAndApplicationAsync(UserType userType, Application application)
        {
            _logger.LogDebug($"GetAllUsersByUserTypeAndApplicationAsync {userType} {application}");

            var getAllUsersByUserTypeQuery = new GetAllUsersByUserTypeQuery(userType, application);
            var users = await _queryHandler.Handle<GetAllUsersByUserTypeQuery, List<User>>(getAllUsersByUserTypeQuery);
            _logger.LogDebug($"{users.Count} user(s) retrieved");

            if (users.Count.Equals(0))
            {
                return NotFound();
            }

            var usersResponse = users.Select(UserToDetailsResponseMapper.MapToResponse).ToList();

            return Ok(usersResponse);
        }

        /// <summary>
        /// Get iterated user number
        /// </summary>
        /// <param name="userType">Type of user (e.g Judge)</param>
        /// <param name="application">Application (e.g. VideoWeb)</param>
        /// <returns>The highest available user number</returns>
        [HttpGet("iterate")]
        [ProducesResponseType(typeof(IteratedUserNumberResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetHighestUserNumberByUserTypeAsync(UserType userType, Application application)
        {
            _logger.LogDebug($"GetHighestUserNumberByUserTypeAsync {userType} {application}");

            var getHighestNumberQuery = new GetNextUserNumberByUserTypeQuery(userType, application);
            var number = await _queryHandler.Handle<GetNextUserNumberByUserTypeQuery, Integer>(getHighestNumberQuery);
            _logger.LogDebug($"Highest user number plus 1 will be {number}");

            var response = NumberToResponseMapper.MapToResponse(number);

            return Ok(response);
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="request">Details of the new user</param>
        /// <returns>Details of the created user</returns>
        [HttpPost]
        [ProducesResponseType(typeof(UserDetailsResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateNewUserAsync(CreateUserRequest request)
        {
            _logger.LogDebug("CreateNewUser");

            var userId = await CreateUserAsync(request);
            _logger.LogDebug("New User Created");

            var getUserByIdQuery = new GetUserByIdQuery(userId);
            var queriedUser = await _queryHandler.Handle<GetUserByIdQuery, User>(getUserByIdQuery);

            var response = UserToDetailsResponseMapper.MapToResponse(queriedUser);

            _logger.LogInformation($"Created user {response.Username} with id {response.Id}");

            return CreatedAtAction(nameof(GetUserDetailsByIdAsync), new { userId = response.Id }, response);
        }

        /// <summary>
        /// Delete user by user id
        /// </summary>
        /// <param name="userId">User Id of the user</param>
        /// <returns>Delete a user</returns>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteUserByUserIdAsync(Guid userId)
        {
            _logger.LogDebug($"DeleteUserByUserIdAsync {userId}");

            var deleteUserCommand = new DeleteUserByUserIdCommand(userId);

            await _commandHandler.Handle(deleteUserCommand);

            _logger.LogInformation($"Successfully deleted user with id {userId}");

            return NoContent();
        }

        private async Task<Guid> CreateUserAsync(CreateUserRequest request)
        {
            var existingUser = await _queryHandler.Handle<GetUserByUsernameQuery, User>(
                new GetUserByUsernameQuery(request.Username, request.Application));

            if (existingUser != null)
            {
                throw new UserAlreadyExistsException(existingUser.Username, existingUser.Application);
            }

            var createNewUserCommand = new CreateNewUserCommand
            (
                request.Username, request.ContactEmail, request.FirstName, request.LastName, 
                request.DisplayName, request.Number, request.UserType, request.Application
            );

            await _commandHandler.Handle(createNewUserCommand);

            return createNewUserCommand.NewUserId;
        }
    }
}
