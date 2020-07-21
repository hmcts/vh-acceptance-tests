using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcceptanceTests.TestAPI.Common.Builders;
using AcceptanceTests.TestAPI.DAL.Commands.Core;
using AcceptanceTests.TestAPI.DAL.Queries;
using AcceptanceTests.TestAPI.DAL.Queries.Core;
using AcceptanceTests.TestAPI.Domain;
using AcceptanceTests.TestAPI.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace AcceptanceTests.TestAPI.DAL.Commands
{
    public interface IAllocationService
    {
        /// <summary>
        /// Allocate user service. This will re-use existing allocations entries before attempting to
        /// create a new one.
        /// </summary>
        /// <param name="userType">Type of user to allocate (e.g. Judge)</param>
        /// <param name="application">Application to assign to (e.g. VideoWeb)</param>
        /// <returns>An allocated user</returns>
        Task<User> AllocateToService(UserType userType, Application application);
    }

    public class AllocationService : IAllocationService
    {
        private readonly IQueryHandler _queryHandler;
        private readonly ICommandHandler _commandHandler;
        private readonly ILogger<AllocationService> _logger;

        public AllocationService(ICommandHandler commandHandler, IQueryHandler queryHandler, ILogger<AllocationService> logger)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
            _logger = logger;
        }

        public async Task<User> AllocateToService(UserType userType, Application application)
        {
            var users = await GetAllUsersByUserTypeAndApplication(userType, application);
            _logger.LogDebug($"Found {users.Count} users of type '{userType}' and application '{application}'");

            await CreateAllocationsForUsersIfRequired(users);

            if (users.Count > 0)
            {
                _logger.LogDebug($"All {users.Count} users now have allocations");
            }

            var user = await GetUnallocatedUser(users);

            if (user == null)
            {
                _logger.LogDebug($"All {users.Count} users were already allocated");

                var userId = await CreateNewUser(userType, application, IterateUserNumber(users));
                _logger.LogDebug($"A new user with Id {userId} has been created");

                await CreateNewAllocation(userId);
                _logger.LogDebug($"The new user with Id {userId} has a new allocation");

                user = await GetUserById(userId);
            }

            await AllocateUser(user.Id);
            _logger.LogDebug($"User with username '{user.Username}' has been allocated");

            return user;
        }

        private async Task<List<User>> GetAllUsersByUserTypeAndApplication(UserType userType, Application application)
        {
            var getAllUsersByUserTypeQuery = new GetAllUsersByUserTypeQuery(userType, application);
            return await _queryHandler.Handle<GetAllUsersByUserTypeQuery, List<User>>(getAllUsersByUserTypeQuery);
        }

        private async Task CreateAllocationsForUsersIfRequired(IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                var allocation = await GetAllocationByUserId(user.Id);

                if (allocation != null) continue;
                await CreateNewAllocation(user.Id);
                _logger.LogDebug($"The user with Id {user.Id} has a new allocation");
            }
        }

        private async Task<Allocation> GetAllocationByUserId(Guid userId)
        {
            var getAllocationByUserIdQuery = new GetAllocationByUserIdQuery(userId);
            return await _queryHandler.Handle<GetAllocationByUserIdQuery, Allocation>(getAllocationByUserIdQuery);
        }

        private async Task CreateNewAllocation(Guid userId)
        {
            var createNewAllocationCommand = new CreateNewAllocationCommand(userId); 
            await _commandHandler.Handle(createNewAllocationCommand);
        }

        private async Task<User> GetUnallocatedUser(IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                var allocation = await GetAllocationByUserId(user.Id);

                if (allocation.ExpiresAt == null)
                {
                    return user;
                }

                if (!allocation.Allocated || allocation.ExpiresAt < DateTime.UtcNow)
                {
                    return user;
                }
            }

            return null;
        }

        private static int IterateUserNumber(IEnumerable<User> users)
        {
            return users.Select(user => user.Number).ToList().Max() + 1;
        }

        private async Task<User> GetUserById(Guid userId)
        {
            var getUserByIdQuery = new GetUserByIdQuery(userId);
            return await _queryHandler.Handle<GetUserByIdQuery, User>(getUserByIdQuery);
        }

        private async Task<Guid> CreateNewUser(UserType userType, Application application, int newNumber)
        {
            const string emailStem = "EMAIL_STEM_TO_REPLACE";

            var request = new UserRequestBuilder(emailStem, newNumber)
                .WithUserType(userType)
                .ForApplication(application)
                .Build();

            var createNewUserCommand = new CreateNewUserCommand
            (
                request.Username, request.ContactEmail, request.FirstName, request.LastName,
                request.DisplayName, request.Number, request.UserType, request.Application
            );

            await _commandHandler.Handle(createNewUserCommand);

            return createNewUserCommand.NewUserId;
        }

        private async Task AllocateUser(Guid userId)
        {
            var allocateCommand = new AllocateByUserIdCommand(userId);
            await _commandHandler.Handle(allocateCommand);
        }
    }
}
