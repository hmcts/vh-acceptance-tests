using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AcceptanceTests.TestAPI.Common.Builders;
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
    [Route("allocations")]
    [ApiController]
    public class AllocationController : ControllerBase
    {
        private readonly IQueryHandler _queryHandler;
        private readonly ICommandHandler _commandHandler;
        private readonly ILogger<AllocationController> _logger;

        public AllocationController(ICommandHandler commandHandler, IQueryHandler queryHandler, ILogger<AllocationController> logger)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
            _logger = logger;
        }

        /// <summary>
        /// Create new allocation for new unallocated user by User Id
        /// </summary>
        /// <param name="userId">User Id of the new user</param>
        /// <returns>Details of the created allocation</returns>
        [HttpPost]
        [ProducesResponseType(typeof(UserAllocationResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateNewAllocationByIdAsync(Guid userId)
        {
            _logger.LogDebug("CreateNewAllocation");

            var allocationId = await CreateAllocationAsync(userId);
            _logger.LogDebug("New Allocation Created");

            var getAllocationByIdQuery = new GetAllocationByIdQuery(allocationId);
            var queriedAllocation = await _queryHandler.Handle<GetAllocationByIdQuery, Allocation>(getAllocationByIdQuery);

            var response = AllocationToDetailsResponseMapper.MapToResponse(queriedAllocation);

            _logger.LogInformation($"Created allocation for {response.Username} with id {response.Id}");

            return CreatedAtAction(nameof(CreateNewAllocationByIdAsync), new { allocationId = response.Id }, response);
        }

        /// <summary>
        /// Allocate a user in an environment
        /// </summary>
        /// <param name="request">Specifics of the allocate</param>
        /// <returns>Details of the allocated user</returns>
        [HttpPut]
        [ProducesResponseType(typeof(AllocationDetailsResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AllocateUserAsync(AllocateUserRequest request)
        {
            _logger.LogDebug($"AllocateUserAsync");

            // Get a list of all existing Judges in Video Web
            var getAllUsersByUserTypeQuery = new GetAllUsersByUserTypeQuery(request.UserType, request.Application);
            var users = await _queryHandler.Handle<GetAllUsersByUserTypeQuery, List<User>>(getAllUsersByUserTypeQuery);

            // Keep a count of the user with the highest number 
            var highestNumber = 0;

            // Does the list have at least 1 Judge?
            if (users.Count > 0)
            {
                highestNumber = 1;
                // Yes : Continue
                // 1 by 1, do they have an allocation already in this env?
                foreach (var user in users)
                {
                    var getAllocationByUsernameQuery = new GetAllocationByUsernameQuery(user.Username);
                    var allocation = await _queryHandler.Handle<GetAllocationByUsernameQuery, Allocation>(getAllocationByUsernameQuery);
                    if (allocation == null)
                    {
                        // Create new allocation 
                        //allocation = new Allocation
                    }

                    if (!allocation.IsAllocated())
                    {
                        allocation.Allocate(10);
                        var allocationResponse = AllocationToDetailsResponseMapper.MapToResponse(allocation);

                        _logger.LogDebug($"User '{user.DisplayName}' allocated");

                        return Ok(allocationResponse);
                    }

                    if (user.Number > highestNumber)
                    {
                        highestNumber = user.Number;
                    }
                }
            }
            
            // Create new User
            var newUserRequest = new UserRequestBuilder("EMAILSTEMGOESHERE", highestNumber + 1)
                .WithUserType(request.UserType)
                .ForApplication(request.Application)
                .Build();

            var userId = await CreateUserAsync(newUserRequest);
            _logger.LogDebug($"New user '{newUserRequest.DisplayName}' created");

            // Create new allocation 
            var allocationId = await CreateAllocationAsync(userId);
            _logger.LogDebug($"New allocation for '{newUserRequest.DisplayName}' created");

            // Allocate the new user 
            allocationId = await AllocateAsync(allocationId);
            _logger.LogDebug($"'{newUserRequest.DisplayName}' allocated with id {allocationId}");

            var getAllocationByIdQuery = new GetAllocationByIdQuery(allocationId);
            var queriedAllocation = await _queryHandler.Handle<GetAllocationByIdQuery, Allocation>(getAllocationByIdQuery);

            if (queriedAllocation == null)
            {
                _logger.LogWarning($"Unable to find allocation with id {allocationId}");

                return NotFound();
            }

            var response = AllocationToDetailsResponseMapper.MapToResponse(queriedAllocation);

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

        private async Task<Guid> CreateAllocationAsync(Guid userId)
        {
            var existingAllocation = await _queryHandler.Handle<GetAllocationByUserIdQuery, Allocation>(
                new GetAllocationByUserIdQuery(userId));

            if (existingAllocation != null) return existingAllocation.Id;

            var createNewAllocationCommand = new CreateNewAllocationCommand(userId);

            await _commandHandler.Handle(createNewAllocationCommand);

            return createNewAllocationCommand.NewAllocationId;
        }

        private async Task<Guid> AllocateAsync(Guid allocationId)
        {
            var allocateCommand = new AllocateByIdCommand(allocationId);

            await _commandHandler.Handle(allocateCommand);

            return allocateCommand.AllocationId;
        }

        private bool IsAllocated(Allocation allocation)
        {
            if (allocation.ExpiresAt == null)
            {
                return false;
            }

            return allocation.Allocated && DateTime.UtcNow < allocation.ExpiresAt;
        }
    }
}
