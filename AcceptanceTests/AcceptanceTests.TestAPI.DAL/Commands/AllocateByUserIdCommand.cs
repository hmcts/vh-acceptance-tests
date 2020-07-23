using System;
using System.Threading.Tasks;
using AcceptanceTests.TestAPI.DAL.Commands.Core;
using AcceptanceTests.TestAPI.DAL.Exceptions;
using AcceptanceTests.TestAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace AcceptanceTests.TestAPI.DAL.Commands
{
    public class AllocateByUserIdCommand : ICommand
    {
        public Guid UserId { get; set; }
        public int ExtendedExpiryInMinutes { get; set; }
        public User User { get; set; }

        public AllocateByUserIdCommand(Guid userId, int extendedExpiryInMinutes = 10)
        {
            UserId = userId;
            ExtendedExpiryInMinutes = extendedExpiryInMinutes;
        }
    }

    public class AllocateCommandHandler : ICommandHandler<AllocateByUserIdCommand>
    {
        private readonly TestApiDbContext _context;

        public AllocateCommandHandler(TestApiDbContext context)
        {
            _context = context;
        }

        public async Task Handle(AllocateByUserIdCommand command)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == command.UserId);

            if (user == null)
            {
                throw new UserNotFoundException(command.UserId);
            }

            var allocationExists = await _context.Allocations
                .AnyAsync(x => x.User.Id == user.Id);

            if (!allocationExists)
            {
                var newAllocation = new Allocation(user);
                await _context.Allocations.AddAsync(newAllocation);
                await _context.SaveChangesAsync();
            }

            var allocation = await _context.Allocations.SingleOrDefaultAsync(x => x.User.Id == user.Id);

            if (allocation.IsAllocated())
            {
                throw new UserUnavailableException();
            }

            allocation.Allocate(command.ExtendedExpiryInMinutes);
            await _context.SaveChangesAsync();
            command.User = user;
        }
    }
}
