using System;
using System.Threading.Tasks;
using AcceptanceTests.TestAPI.DAL.Commands.Core;
using AcceptanceTests.TestAPI.DAL.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AcceptanceTests.TestAPI.DAL.Commands
{
    public class AllocateByIdCommand : ICommand
    {
        public Guid AllocationId { get; set; }
        public int ExtendedExpiryInMinutes { get; set; }

        public AllocateByIdCommand(Guid allocationId, int extendedExpiryInMinutes = 10)
        {
            AllocationId = allocationId;
            ExtendedExpiryInMinutes = extendedExpiryInMinutes;
        }
    }

    public class AllocateByIdCommandHandler : ICommandHandler<AllocateByIdCommand>
    {
        private readonly TestApiDbContext _context;

        public AllocateByIdCommandHandler(TestApiDbContext context)
        {
            _context = context;
        }

        public async Task Handle(AllocateByIdCommand command)
        {
            var allocation = await _context.Allocations.SingleOrDefaultAsync(x => x.Id == command.AllocationId);

            if (allocation.IsAllocated())
            {
                throw new UserUnavailableException(command.AllocationId);
            }

            allocation.Allocate(command.ExtendedExpiryInMinutes);
            await _context.SaveChangesAsync();
        }
    }
}
