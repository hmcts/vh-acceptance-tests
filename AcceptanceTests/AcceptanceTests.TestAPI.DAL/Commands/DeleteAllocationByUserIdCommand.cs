using System;
using System.Threading.Tasks;
using AcceptanceTests.TestAPI.DAL.Commands.Core;
using AcceptanceTests.TestAPI.DAL.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AcceptanceTests.TestAPI.DAL.Commands
{
    public class DeleteAllocationByUserIdCommand : ICommand
    {
        public Guid UserId { get; }

        public DeleteAllocationByUserIdCommand(Guid userId)
        {
            UserId = userId;
        }
    }

    public class DeleteAllocationByUserIdCommandHandler : ICommandHandler<DeleteAllocationByUserIdCommand>
    {
        private readonly TestApiDbContext _context;

        public DeleteAllocationByUserIdCommandHandler(TestApiDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteAllocationByUserIdCommand command)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == command.UserId);

            if (user == null)
            {
                throw new UserNotFoundException(command.UserId);
            }

            var allocation = await _context.Allocations.SingleOrDefaultAsync(x => x.UserId == command.UserId);

            if (allocation == null)
            {
                throw new UserAllocationNotFoundException(command.UserId);
            }

            _context.Remove(allocation);

            await _context.SaveChangesAsync();
        }
    }
}
