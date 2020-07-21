using System;
using System.Threading.Tasks;
using AcceptanceTests.TestAPI.DAL.Commands.Core;
using AcceptanceTests.TestAPI.DAL.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AcceptanceTests.TestAPI.DAL.Commands
{
    public class UnallocateByUserIdCommand : ICommand
    {
        public Guid UserId { get; set; }

        public UnallocateByUserIdCommand(Guid userId)
        {
            UserId = userId;
        }
    }

    public class UnallocateByUserCommandHandler : ICommandHandler<UnallocateByUserIdCommand>
    {
        private readonly TestApiDbContext _context;

        public UnallocateByUserCommandHandler(TestApiDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UnallocateByUserIdCommand command)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == command.UserId);

            if (user == null)
            {
                throw new UserNotFoundException(command.UserId);
            }

            var allocation = await _context.Allocations
                .SingleOrDefaultAsync(x => x.User.Id == user.Id);

            if (allocation == null)
            {
                throw new UserAllocationNotFoundException(command.UserId);
            }

            allocation.Unallocate();
            await _context.SaveChangesAsync();
        }
    }
}
