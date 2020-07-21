using System;
using System.Net;
using System.Threading.Tasks;
using AcceptanceTests.TestAPI.Contract.Requests;
using AcceptanceTests.TestAPI.Contract.Responses;
using AcceptanceTests.TestAPI.DAL.Commands;
using AcceptanceTests.TestAPI.DAL.Commands.Core;
using AcceptanceTests.TestAPI.DAL.Queries;
using AcceptanceTests.TestAPI.DAL.Queries.Core;
using AcceptanceTests.TestAPI.Domain;
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
        /// Create new user
        /// </summary>
        /// <param name="request">Details of the new user</param>
        /// <returns>Details of the created user</returns>
        [HttpPost]
        [ProducesResponseType(typeof(UserAllocationResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateNewUserAsync(CreateNewUserRequest request)
        {
            _logger.LogDebug("CreateNewUser");

            var userId = await CreateUserAsync(request);
            _logger.LogDebug("New User Created");

            var getUserByIdQuery = new GetUserByIdQuery(userId);
            var queriedUser = await _queryHandler.Handle<GetUserByIdQuery, User>(getUserByIdQuery);

            var response = UserToDetailsResponseMapper.MapUserToResponse(queriedUser);

            _logger.LogInformation($"Created user {response.Username} with id {response.Id}");

            return CreatedAtAction(nameof(GetUserDetailsByIdAsync), new { userId = response.Id }, response);
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

            var response = UserToDetailsResponseMapper.MapUserToResponse(queriedUser);
            return Ok(response);
        }


        private async Task<Guid> CreateUserAsync(CreateNewUserRequest request)
        {
            var existingUser = await _queryHandler.Handle<GetUserByUsernameQuery, User>(
                new GetUserByUsernameQuery(request.Username));

            if (existingUser != null) return existingUser.Id;

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
