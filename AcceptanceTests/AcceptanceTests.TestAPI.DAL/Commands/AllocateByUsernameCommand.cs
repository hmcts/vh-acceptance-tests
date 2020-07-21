using System;
using System.Threading.Tasks;
using AcceptanceTests.TestAPI.DAL.Commands.Core;
using AcceptanceTests.TestAPI.DAL.Exceptions;
using AcceptanceTests.TestAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace AcceptanceTests.TestAPI.DAL.Commands
{
    public class AllocateByUsernameCommand : ICommand
    {
        public string Username { get; set; }
        public int ExtendedExpiryInMinutes { get; set; }

        public AllocateByUsernameCommand(string username, int extendedExpiryInMinutes = 10)
        {
            Username = username;
            ExtendedExpiryInMinutes = extendedExpiryInMinutes;
        }
    }

    public class AllocateByUsernameCommandHandler : ICommandHandler<AllocateByUsernameCommand>
    {
        private readonly TestApiDbContext _context;

        public AllocateByUsernameCommandHandler(TestApiDbContext context)
        {
            _context = context;
        }

        public async Task Handle(AllocateByUsernameCommand command)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(x => x.Username.ToLower() == command.Username.ToLower());

            if (user == null)
            {
                throw new UserNotFoundException(command.Username);
            }

            var allocationExists = await _context.Allocations.AnyAsync(x => x.User.Id == user.Id);

            if (!allocationExists)
            {
                var newAllocation = new Allocation(user);
                await _context.Allocations.AddAsync(newAllocation);
                await _context.SaveChangesAsync();
            }

            var allocation = await _context.Allocations.SingleOrDefaultAsync(x => x.User.Id == user.Id);

            if (allocation.IsAllocated())
            {
                throw new UserUnavailableException(command.Username);
            }

            allocation.Allocate(command.ExtendedExpiryInMinutes);
            await _context.SaveChangesAsync();
        }
    }
}
