using System;
using System.Threading.Tasks;
using AcceptanceTests.TestAPI.DAL.Commands.Core;
using AcceptanceTests.TestAPI.DAL.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AcceptanceTests.TestAPI.DAL.Commands
{
    public class CreateNewAllocationCommand : ICommand
    {
        public Guid UserId { get; set; }
        public Guid NewAllocationId { get; set; }

        public CreateNewAllocationCommand(Guid userId)
        {
            UserId = userId;
        }
    }

    public class CreateNewAllocationCommandHandler : ICommandHandler<CreateNewAllocationCommand>
    {
        private readonly TestApiDbContext _context;

        public CreateNewAllocationCommandHandler(TestApiDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateNewAllocationCommand command)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(x => x.Id == command.UserId);

            if (user == null)
            {
                throw new UserNotFoundException(command.UserId);
            }

            var allocation = new Domain.Allocation(user);
            await _context.Allocations.AddAsync(allocation);
            await _context.SaveChangesAsync();
            command.NewAllocationId = allocation.Id;
        }
    }
}
