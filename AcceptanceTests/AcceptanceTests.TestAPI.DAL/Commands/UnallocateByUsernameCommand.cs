using System;
using System.Threading.Tasks;
using AcceptanceTests.TestAPI.DAL.Commands.Core;
using AcceptanceTests.TestAPI.DAL.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AcceptanceTests.TestAPI.DAL.Commands
{
    public class UnallocateByUsernameCommand : ICommand
    {
        public string Username { get; set; }
        public Guid AllocationId { get; set; }

        public UnallocateByUsernameCommand(string username)
        {
            Username = username;
        }
    }

    public class UnallocateUserCommandHandler : ICommandHandler<UnallocateByUsernameCommand>
    {
        private readonly TestApiDbContext _context;

        public UnallocateUserCommandHandler(TestApiDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UnallocateByUsernameCommand command)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username.ToLower() == command.Username.ToLower());

            if (user == null)
            {
                throw new UserNotFoundException(command.Username);
            }

            var allocation = await _context.Allocations.SingleOrDefaultAsync(x => x.User.Id == user.Id);

            if (allocation == null)
            {
                throw new UserAllocationNotFoundException(command.Username);
            }

            allocation.Unallocate();
            await _context.SaveChangesAsync();
            command.AllocationId = allocation.Id;
        }
    }
}
